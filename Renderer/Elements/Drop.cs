using Box2DX.Common;
using DropTop.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropTop.Renderer.Elements
{
    public class Drop : IRenderable
    {
        public Guid Id { get; set; }
        public string FileName { get; private set; }
        public string Path { get; private set; }
        public Bitmap Image { get; private set; }

        //protected Point Position { get; set; }
        //protected Vec2 Velocity { get; set; }

        protected SolidBrush BoxBrush { get; set; }
        protected Pen BoxPen { get; set; }

        public Drop(string filePath)
        {
            this.Id = Guid.NewGuid();
            this.Path = filePath;
            this.FileName = Path.Split('\\').Last();
            this.Image = FileService.GetFileThumb(this.Path);

            //this.Position = new Point
            //{
            //    X = Random.Shared.Next(0, 500),
            //    Y = Random.Shared.Next(0, 500),
            //};
            //this.Velocity = new Vec2
            //{
            //    X = 0,
            //    Y = 0
            //};
            this.BoxBrush = new SolidBrush(Color.Black);
            this.BoxPen = new Pen(Color.Black, 3);
        }

        public void Render(Graphics g, Point pos, float angle)
        {
            g.DrawImage(this.Image, pos.X, pos.Y, 50, 50);
            g.DrawRectangle(this.BoxPen, new Rectangle(pos.X, pos.Y, 50, 50));
        }
    }
}
