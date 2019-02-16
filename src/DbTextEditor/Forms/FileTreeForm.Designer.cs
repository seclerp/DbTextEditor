namespace DbTextEditor.Forms
{
    partial class FileTreeForm
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
            this.ExplorerTree = new WindowsExplorer.ExplorerTree();
            this.SuspendLayout();
            // 
            // ExplorerTree
            // 
            this.ExplorerTree.BackColor = System.Drawing.SystemColors.Control;
            this.ExplorerTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExplorerTree.Location = new System.Drawing.Point(5, 5);
            this.ExplorerTree.Name = "ExplorerTree";
            this.ExplorerTree.SelectedPath = "C:\\";
            this.ExplorerTree.ShowAddressbar = false;
            this.ExplorerTree.ShowMyDocuments = true;
            this.ExplorerTree.ShowMyFavorites = false;
            this.ExplorerTree.ShowMyNetwork = false;
            this.ExplorerTree.ShowToolbar = false;
            this.ExplorerTree.Size = new System.Drawing.Size(397, 746);
            this.ExplorerTree.TabIndex = 0;
            // 
            // FileTreeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 756);
            this.Controls.Add(this.ExplorerTree);
            this.Name = "FileTreeForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "File tree";
            this.ResumeLayout(false);

        }

        #endregion

        internal WindowsExplorer.ExplorerTree ExplorerTree;
    }
}