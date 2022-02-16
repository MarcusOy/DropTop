using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Foundation.Collections;
using Windows.Storage;

namespace DropTop.Services
{
    public static class FileService
    {
        public static void HandleFileOpen(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException();
                }

                try
                {
                    Process.Start(new ProcessStartInfo(filePath)
                    {
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    // Try to run with UAC prompt
                    ProcessStartInfo myProcess = new ProcessStartInfo(filePath);
                    myProcess.WorkingDirectory = @"C:\Windows\System32";
                    myProcess.UseShellExecute = true;
                    myProcess.Verb = "runas";
                    Process.Start(myProcess);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occured trying to open the file: {ex.Message}");
            }

        }

        public static void HandleViewFileInExplorer(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException();
                }

                // combine the arguments together
                // it doesn't matter if there is a space after ','
                string argument = "/select, \"" + filePath + "\"";

                Process.Start("explorer.exe", argument);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occured trying to open the file: {ex.Message}");
            }
        }
        public static Bitmap GetFileThumb(string filePath)
        {
            using (var shellFile = ShellFile.FromFilePath(filePath))
                return shellFile.Thumbnail.LargeBitmap;
        }
    }
}
