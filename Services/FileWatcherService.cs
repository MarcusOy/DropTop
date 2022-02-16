using DropTop.Renderer.Elements;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropTop.Services
{
    public class FileWatcherService
    {

        #region Singleton
        private static FileWatcherService instance;
        public static FileWatcherService Current
        {
            get
            {
                if (instance == null)
                    instance = new FileWatcherService();
                return instance;
            }
        }
        #endregion

        public List<FileSystemWatcher> Watchers { get; set; }

        public List<Drop> CreatedFiles { get; set; }
        public List<string> FilesBeingCreated { get; set; }

        private FileWatcherService()
        {
            this.Watchers = new List<FileSystemWatcher>();
            this.CreatedFiles = new List<Drop>();
            SettingsService.FoldersToWatchChanged += SettingsService_FoldersToWatchChanged;
        }

        public void InitializeWatchers()
        {
            Debug.WriteLine("Initializing new watchers...");
            foreach(FolderToWatch folder in SettingsService.FoldersToWatch)
            {
                if (!folder.IsEnabled) continue;

                var watcher = new FileSystemWatcher(folder.Path)
                {
                    NotifyFilter = NotifyFilters.Attributes
                     | NotifyFilters.CreationTime
                     | NotifyFilters.DirectoryName
                     | NotifyFilters.FileName
                     | NotifyFilters.LastAccess
                     | NotifyFilters.LastWrite
                     | NotifyFilters.Security
                     | NotifyFilters.Size,
                    IncludeSubdirectories = true,
                    EnableRaisingEvents = true
                };

                watcher.Changed += OnChanged;
                watcher.Created += OnCreated;
                //watcher.Deleted += OnDeleted;
                //watcher.Renamed += OnRenamed;
                watcher.Error += OnError;

                this.Watchers.Add(watcher);
            }
        }
        private void SettingsService_FoldersToWatchChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("Folders to watch changed. Restarting FileWatcherService...");
            DestroyWatchers();
            InitializeWatchers();
        }

        public void DestroyWatchers()
        {
            Debug.WriteLine("Destroying all watchers...");
            foreach(FileSystemWatcher watcher in this.Watchers)
            {
                watcher.Dispose();
            }
            this.Watchers.Clear();
            Debug.WriteLine("Watchers destroyed.");
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {

            // Check if file has size greater than 0
            FileInfo fi = new FileInfo(e.FullPath);
            //if (fi.Length >= 0)
            //{
            //    this.CreatedFiles.Add(new Drop(e.FullPath));
            //}
            Debug.WriteLine($"Created: {e.FullPath} - {fi.Length}");
            NotificationService.CreateNotification(e.FullPath);
        }
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }
            FileInfo fi = new FileInfo(e.FullPath);
            Debug.WriteLine($"Changed: {e.FullPath} - {fi.Length}");
        }

        //private static void OnDeleted(object sender, FileSystemEventArgs e) =>
        //    Debug.WriteLine($"Deleted: {e.FullPath}");

        //private static void OnRenamed(object sender, RenamedEventArgs e)
        //{
        //    Debug.WriteLine($"Renamed:");
        //    Debug.WriteLine($"    Old: {e.OldFullPath}");
        //    Debug.WriteLine($"    New: {e.FullPath}");
        //}

        private static void OnError(object sender, ErrorEventArgs e) =>
            PrintException(e.GetException());

        private static void PrintException(Exception ex)
        {
            if (ex != null)
            {
                Debug.WriteLine($"Message: {ex.Message}");
                Debug.WriteLine("Stacktrace:");
                Debug.WriteLine(ex.StackTrace);
                Debug.WriteLine("");
                PrintException(ex.InnerException);
            }
        }
    }
}
