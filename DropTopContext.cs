using DropTop.Renderer;
using DropTop.Renderer.Elements;
using DropTop.Services;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DropTop
{
    internal class DropTopContext : ApplicationContext
    {
        private NotifyIcon trayIcon;
        private MainRenderer renderer;
        private SettingsForm settingsForm;

        public DropTopContext()
        {
            // Creating context menu and tray icon
            var strip = new ContextMenuStrip();
            strip.Items.Add(new ToolStripMenuItem("DropTop", null));
            strip.Items.Add(new ToolStripSeparator());
            strip.Items.Add(new ToolStripMenuItem("Drop from top...", null, DropFromTop));
            strip.Items.Add(new ToolStripMenuItem("Send test notification...", null, SendTestNotification));
            strip.Items.Add(new ToolStripMenuItem("Settings...", null, OpenSettings));
            strip.Items.Add(new ToolStripMenuItem("Exit", null, Exit));
            trayIcon = new NotifyIcon()
            {
                Icon = Resource.TrayIcon,
                ContextMenuStrip = strip,
                Visible = true,
            };

            // Create Renderer (for falling files)
            renderer = new MainRenderer();

            // Handle notification activations
            ToastNotificationManagerCompat.OnActivated += NotificationService.HandleToastNotificationManagerOnActivate;

            // Activate FileWatcherService
            FileWatcherService.Current.InitializeWatchers();
        }

        void Exit(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            trayIcon.Visible = false;

            Application.Exit();
        }

        void DropFromTop(object sender, EventArgs e)
        {
            this.renderer.AddDrop(new Drop("C:\\Users\\marcu\\Downloads\\Activity2.pdf"));
        }
        void SendTestNotification(object sender, EventArgs e)
            //=> NotificationService.CreateNotification("C:\\Windows\\regedit.exe");
            => NotificationService.CreateNotification("C:\\Users\\marcu\\Downloads\\Activity2.pdf");

        void OpenSettings(object sender, EventArgs e)
        {
            // Create settings form
            settingsForm = new SettingsForm();
            settingsForm.Show();
        }

    }
}
