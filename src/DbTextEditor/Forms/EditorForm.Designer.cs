using System.Windows.Forms;
using ScintillaNET;

namespace DbTextEditor.Forms
{
    partial class EditorForm
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
            this.SuspendLayout();
            this.TextEditor = new ScintillaNET.Scintilla();
            //
            // TextEditor
            //
            this.TextEditor.Name = "TextEditor";
            this.TextEditor.IndentWidth = 4;
            this.TextEditor.TabWidth = 4;
            this.TextEditor.BorderStyle = BorderStyle.None;
            this.TextEditor.Dock = DockStyle.Fill;
            this.TextEditor.WrapMode = WrapMode.None;
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TextEditor);
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "EditorForm";
            this.Padding = new Padding(10, 0, 0, 0);
            this.Text = "EditorForm";
            this.ResumeLayout(false);

        }

        #endregion
        internal Scintilla TextEditor;
    }
}