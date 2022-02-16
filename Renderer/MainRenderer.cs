using Box2DX.Collision;
using Box2DX.Common;
using Box2DX.Dynamics;
using DropTop.Renderer.Elements;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Color = System.Drawing.Color;

namespace DropTop.Renderer
{
    public class MainRenderer
    {
        public OverlayForm OverlayForm { get; set; }
        public BufferedPanel Canvas { get; set; }
        public World World { get; set; }
        public List<Drop> Drops { get; set; }
        public MouseHook MouseHook { get; set; }
        public Point MousePosition { get; set; }
        public bool IsMouseDown { get; set; }
        public MainRenderer()
        {
            this.OverlayForm = new OverlayForm();

            this.Canvas = new BufferedPanel();
            this.Canvas.Dock = DockStyle.Fill;
            this.Canvas.BackColor = Color.Transparent;
            this.Canvas.BringToFront();
            this.Canvas.Paint += new PaintEventHandler(this.Render);

            this.OverlayForm.Controls.Add(this.Canvas);
            this.OverlayForm.Show();
            Application.Idle += new EventHandler(this.HandleApplicationIdle);
            Application.EnableVisualStyles();
            this.SetWindowPassthru(true);

            // Setup the world for Box2D

            Vec2 gravity = new Vec2(0, 100.0f);
            AABB worldAabb = new AABB();
            worldAabb.LowerBound.Set(-200.0f, -100.0f);
            worldAabb.UpperBound.Set(200.0f, 200.0f);
            this.World = new World(worldAabb, gravity, false);
            CreateGround(this.World, 400, 500);

            this.Drops = new List<Drop>();

            this.MouseHook = new MouseHook();
            this.MouseHook.SetHook();
            this.MouseHook.MouseMoveEvent += MouseHook_MouseMoveEvent;
            this.MouseHook.MouseDownEvent += MouseHook_MouseDownEvent;
            this.MouseHook.MouseUpEvent += MouseHook_MouseUpEvent;
            this.MouseHook.MouseClickEvent += MouseHook_MouseClickEvent;
            this.MousePosition = new Point();
            this.IsMouseDown = false;
            this.OverlayForm.FormClosed += (sender, e) => this.MouseHook.UnHook();
        }

        #region Win32Imports
        [DllImport("user32.dll")]
        private static extern int PeekMessage(
          out NativeMessage message,
          IntPtr window,
          uint filterMin,
          uint filterMax,
          uint remove);
        public struct NativeMessage
        {
            public IntPtr Handle;
            public uint Message;
            public IntPtr WParameter;
            public IntPtr LParameter;
            public uint Time;
            public Point Location;
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
        private IntPtr OriginalWindowStyle => (IntPtr) (long) GetWindowLong(this.OverlayForm.Handle, -20);
        private IntPtr PassthruWindowStyle => (IntPtr) (long) (uint) ((int) GetWindowLong(this.OverlayForm.Handle, -20) | 524288 | 32);
        private void SetWindowPassthru(bool passthrough)
        {
            if (passthrough)
                SetWindowLong(this.OverlayForm.Handle, -20, this.PassthruWindowStyle);
            else
                SetWindowLong(this.OverlayForm.Handle, -20, this.OriginalWindowStyle);
        }
        #endregion
        #region RenderLoop
        private void Render(object sender, PaintEventArgs e) => this.Tick(e.Graphics);
        private bool IsApplicationIdle() => PeekMessage(out NativeMessage _, IntPtr.Zero, 0U, 0U, 0U) == 0;
        private void HandleApplicationIdle(object sender, EventArgs e)
        {
            while (this.IsApplicationIdle())
            {
                //this.OverlayForm.TopMost = true;
                //this.Canvas.BringToFront();
                this.Canvas.Invalidate();
                Thread.Sleep(8);
            }
        }
        #endregion

        public void AddDrop(Drop d)
        {
            this.Drops.Add(d);
        }

        public void Tick(Graphics g)
        {
            this.Act();
            this.Draw(g);
        }

        public void Draw(Graphics g)
        {
            // Create font and brush for dt and mouse strings
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.White);

            g.DrawString(DateTime.Now.Ticks.ToString(), drawFont, drawBrush, new Point(500, 550));
            g.DrawString($"({this.MousePosition.X}, {this.MousePosition.Y}) {this.IsMouseDown}", drawFont, drawBrush, new Point(500, 500));

            // Draw each active drop
            Body body = this.World.GetBodyList();
            while(body.GetNext() != null)
            {

                if (body.GetUserData() != null)
                {
                    var dropId = (Guid)body.GetUserData();
                    var d = this.Drops.First(dr => dr.Id == dropId);

                    var pos = new Point((int)body.GetPosition().X, (int)body.GetPosition().Y);
                    var angle = body.GetAngle() * 180 / Settings.Pi;
                    d.Render(g, pos, angle);
                    g.DrawString($"({pos.X}, {pos.X})", drawFont, drawBrush, new Point(500, 400));

                }

                // And then we get the next body
                body = body.GetNext();
            }

        }
        public void Act()
        {
            // We have to update the Physics World
            // So we step through the process like this
            this.World.Step(1 / 60.0f, 8, 1);
        }

        // Lets create the ground
        public void CreateGround(World world, float x, float y)
        {
            // We need to define a body with the position
            BodyDef bodyDef = new BodyDef();

            // Box2D doesn't use pixel measurement
            // So we need to divide by 30
            // 1m = 30px
            bodyDef.Position.Set(x / 30.0f, y / 30.0f);

            // Create the physics body
            Body body = world.CreateBody(bodyDef);

            // Define a new shape def
            PolygonDef shapeDef = new PolygonDef();
            shapeDef.SetAsBox((800.0f / 2) / 30.0f, (16.0f / 2) / 30.0f);
            shapeDef.Density = 0.0f;

            // Finalize the shape and body
            body.CreateFixture(shapeDef);
            body.SetMassFromShapes();
        }

        public void CreateBox(Drop d, int posX, int posY)
        {
            // We need to define a body with the position
            BodyDef bodyDef = new BodyDef();

            // Box2D doesn't use pixel measurement
            // So we need to divide by 30
            // 1m = 30px
            bodyDef.Position.Set(posX / 30.0f, posY / 30.0f);

            // Create the physics body
            Body body = this.World.CreateBody(bodyDef);

            // Define a new shape def
            PolygonDef shapeDef = new PolygonDef();
            shapeDef.SetAsBox((32.0f / 2) / 30.0f, (32.0f / 2) / 30.0f);

            // Set the shape properties
            shapeDef.Density = 1.0f;
            shapeDef.Friction = 0.7f;

            // Finalize the shape and body
            body.CreateFixture(shapeDef);
            body.SetMassFromShapes();

            // Add ID of Drop so that it can be referenced
            body.SetUserData(d.Id);
        }
        private void MouseHook_MouseClickEvent(object sender, MouseEventArgs e)
        {
            Debug.WriteLine(JsonConvert.SerializeObject(e, Formatting.Indented));
            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks % 2 == 1)
                {
                    var d = new Drop("C:\\Users\\marcu\\Downloads\\Activity2.pdf");
                    this.AddDrop(d);
                    this.CreateBox(d, e.X, e.Y);
                    this.IsMouseDown = true;
                } else
                    this.IsMouseDown = false;

            }
        }

        private void MouseHook_MouseMoveEvent(object sender, MouseEventArgs e)
            => this.MousePosition = e.Location;
        
        private void MouseHook_MouseDownEvent(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                this.IsMouseDown = true;
        }
        private void MouseHook_MouseUpEvent(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                this.IsMouseDown = false;
        }
    }
}
