using System.Windows.Forms;

namespace DropTop
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.generalTabPage = new System.Windows.Forms.TabPage();
            this.folderListBox = new System.Windows.Forms.ListBox();
            this.removeButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.appearanceTabPage = new System.Windows.Forms.TabPage();
            this.updateTabPage = new System.Windows.Forms.TabPage();
            this.donateTabPage = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.generalTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.generalTabPage);
            this.tabControl1.Controls.Add(this.appearanceTabPage);
            this.tabControl1.Controls.Add(this.updateTabPage);
            this.tabControl1.Controls.Add(this.donateTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(429, 343);
            this.tabControl1.TabIndex = 0;
            // 
            // generalTabPage
            // 
            this.generalTabPage.Controls.Add(this.folderListBox);
            this.generalTabPage.Controls.Add(this.removeButton);
            this.generalTabPage.Controls.Add(this.addButton);
            this.generalTabPage.Controls.Add(this.label1);
            this.generalTabPage.Location = new System.Drawing.Point(4, 22);
            this.generalTabPage.Name = "generalTabPage";
            this.generalTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.generalTabPage.Size = new System.Drawing.Size(421, 317);
            this.generalTabPage.TabIndex = 0;
            this.generalTabPage.Text = "General";
            this.generalTabPage.UseVisualStyleBackColor = true;
            // 
            // folderListBox
            // 
            this.folderListBox.FormattingEnabled = true;
            this.folderListBox.Location = new System.Drawing.Point(7, 18);
            this.folderListBox.Name = "folderListBox";
            this.folderListBox.Size = new System.Drawing.Size(409, 82);
            this.folderListBox.TabIndex = 4;
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(76, 104);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(64, 20);
            this.removeButton.TabIndex = 3;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(7, 104);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(64, 20);
            this.addButton.TabIndex = 2;
            this.addButton.Text = "Add...";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Folder to watch";
            // 
            // appearanceTabPage
            // 
            this.appearanceTabPage.Location = new System.Drawing.Point(4, 22);
            this.appearanceTabPage.Name = "appearanceTabPage";
            this.appearanceTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.appearanceTabPage.Size = new System.Drawing.Size(421, 317);
            this.appearanceTabPage.TabIndex = 1;
            this.appearanceTabPage.Text = "Appearance";
            this.appearanceTabPage.UseVisualStyleBackColor = true;
            // 
            // updateTabPage
            // 
            this.updateTabPage.Location = new System.Drawing.Point(4, 22);
            this.updateTabPage.Name = "updateTabPage";
            this.updateTabPage.Size = new System.Drawing.Size(421, 317);
            this.updateTabPage.TabIndex = 2;
            this.updateTabPage.Text = "Updates";
            this.updateTabPage.UseVisualStyleBackColor = true;
            // 
            // donateTabPage
            // 
            this.donateTabPage.Location = new System.Drawing.Point(4, 22);
            this.donateTabPage.Name = "donateTabPage";
            this.donateTabPage.Size = new System.Drawing.Size(421, 317);
            this.donateTabPage.TabIndex = 3;
            this.donateTabPage.Text = "Donate";
            this.donateTabPage.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 343);
            this.Controls.Add(this.tabControl1);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.tabControl1.ResumeLayout(false);
            this.generalTabPage.ResumeLayout(false);
            this.generalTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private TabControl tabControl1;
        private TabPage generalTabPage;
        private Button removeButton;
        private Button addButton;
        private Label label1;
        private TabPage appearanceTabPage;
        private TabPage updateTabPage;
        private TabPage donateTabPage;
        private ListBox folderListBox;
    }
}