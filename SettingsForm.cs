using DropTop.Services;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DropTop
{
    public partial class SettingsForm : Form
    {
        public BindingList<FolderToWatch> FoldersToWatch { get; set; }
        public SettingsForm()
        {
            InitializeComponent();
            this.FoldersToWatch = new BindingList<FolderToWatch>(SettingsService.FoldersToWatch);
            folderListBox.DataSource = FoldersToWatch;
            folderListBox.DisplayMember = "Path";
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                FoldersToWatch.Add(new FolderToWatch
                {
                    Path = dialog.FileName,
                    IsEnabled = true
                });
                SettingsService.FoldersToWatch = this.FoldersToWatch.ToList();
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (folderListBox.SelectedIndex >= -1)
                this.FoldersToWatch.RemoveAt(folderListBox.SelectedIndex);
            else
                MessageBox.Show("Select a folder to remove.");

            SettingsService.FoldersToWatch = this.FoldersToWatch.ToList();
        }
    }

    public class FolderToWatch
    {
        public string Path { get; set; }
        public bool IsEnabled { get; set; }
    }
}
