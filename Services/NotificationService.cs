using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.Storage;

namespace DropTop.Services
{
    public static class NotificationService
    {
        public static void CreateNotification(string filePath)
        {
            // Split file path to get file name
            var pathParts = filePath.Split('\\');
            var fileName = pathParts[pathParts.Length - 1];

            // Extract file thumbnail from Windows shell
            //ShellFile shellFile = ShellFile.FromFilePath(filePath);
            //Bitmap shellThumb = shellFile.Thumbnail.ExtraLargeBitmap;

            // Save thumbnail in local storage 
            var thumbFileName = $"{Guid.NewGuid()}.png";
            var thumbsFolderPath = Path.Combine(Path.GetTempPath(), "DropTop", "thumbs");
            var thumbPath = Path.Combine(thumbsFolderPath, thumbFileName);
            try 
            {
                Directory.CreateDirectory(thumbsFolderPath); // ensure directory
                using (var shellThumb = FileService.GetFileThumb(filePath))
                using (var s = new MemoryStream())
                using (var fs = new FileStream(thumbPath, FileMode.Create, FileAccess.Write))
                {
                    shellThumb.Save(s, System.Drawing.Imaging.ImageFormat.Png);
                    s.Position = 0;
                    fs.Position = 0;
                    s.WriteTo(fs);
                }
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            

            // Build notification
            var toast = new ToastContentBuilder()
                .AddArgument("action", "open")
                .AddArgument("filePath", filePath)
                .AddText("New file dropped")
                .AddText(fileName)
                .AddAppLogoOverride(new Uri(thumbPath), ToastGenericAppLogoCrop.Circle);

            toast.AddButton(new ToastButton()
                .SetContent("Open")
                .AddArgument("action", "open")
                .SetBackgroundActivation());
            toast.AddButton(new ToastButton()
                .SetContent("View in Explorer")
                .AddArgument("action", "viewInExplorer"));

            toast.Show();
        }

        

        

        public static void HandleToastNotificationManagerOnActivate(ToastNotificationActivatedEventArgsCompat e)
        {
            // Obtain the arguments from the notification
            ToastArguments args = ToastArguments.Parse(e.Argument);
            string action = args["action"];
            string filePath = args["filePath"];

            switch (action)
            {
                case "open":
                    FileService.HandleFileOpen(filePath);
                    break;
                case "viewInExplorer":
                    FileService.HandleViewFileInExplorer(filePath);
                    break;
            }
        }
    }
}
