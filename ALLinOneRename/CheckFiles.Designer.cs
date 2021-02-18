
namespace ALLinOneRename
{
    partial class CheckFiles
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
            this.RtbCheckFiles = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // RtbCheckFiles
            // 
            this.RtbCheckFiles.Location = new System.Drawing.Point(13, 13);
            this.RtbCheckFiles.Name = "RtbCheckFiles";
            this.RtbCheckFiles.ReadOnly = true;
            this.RtbCheckFiles.Size = new System.Drawing.Size(406, 425);
            this.RtbCheckFiles.TabIndex = 0;
            this.RtbCheckFiles.Text = "";
            // 
            // CheckFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 450);
            this.Controls.Add(this.RtbCheckFiles);
            this.Name = "CheckFiles";
            this.Text = "CheckFiles";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox RtbCheckFiles;
    }
}