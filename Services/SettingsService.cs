using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropTop.Services
{
    public static class SettingsService
    {
        public static List<FolderToWatch> FoldersToWatch
        {
            get
            {
                try { 
                    var s = Properties.Settings.Default.FoldersToWatch;
                    return JsonConvert.DeserializeObject<List<FolderToWatch>>(s) ?? new List<FolderToWatch>();
                } catch
                {
                    return new List<FolderToWatch>();
                }
            }
            set
            {
                Properties.Settings.Default.FoldersToWatch = JsonConvert.SerializeObject(value);
                Properties.Settings.Default.Save();
                OnFoldersToWatchChanged(new EventArgs());
            }
        }
        public static event EventHandler FoldersToWatchChanged;
        private static void OnFoldersToWatchChanged(EventArgs e)
        {
            EventHandler handler = FoldersToWatchChanged;
            handler.Invoke(null, e);
        }
    }


}
