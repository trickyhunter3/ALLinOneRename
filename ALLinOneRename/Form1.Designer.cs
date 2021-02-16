
namespace ALLinOneRename
{
    partial class Form1
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
            this.BtnRenameVOne = new System.Windows.Forms.Button();
            this.BtnSubtractName = new System.Windows.Forms.Button();
            this.BtnRenameVTwo = new System.Windows.Forms.Button();
            this.BtnCheckFiles = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.RtbRenamedText = new System.Windows.Forms.RichTextBox();
            this.TbxPath = new System.Windows.Forms.TextBox();
            this.TbxFilterNumbers = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CbxIsNumberFirst = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // BtnRenameVOne
            // 
            this.BtnRenameVOne.Location = new System.Drawing.Point(52, 118);
            this.BtnRenameVOne.Name = "BtnRenameVOne";
            this.BtnRenameVOne.Size = new System.Drawing.Size(75, 23);
            this.BtnRenameVOne.TabIndex = 0;
            this.BtnRenameVOne.Text = "Change V1";
            this.BtnRenameVOne.UseVisualStyleBackColor = true;
            this.BtnRenameVOne.Click += new System.EventHandler(this.BtnRenameVOne_Click);
            // 
            // BtnSubtractName
            // 
            this.BtnSubtractName.Location = new System.Drawing.Point(52, 317);
            this.BtnSubtractName.Name = "BtnSubtractName";
            this.BtnSubtractName.Size = new System.Drawing.Size(75, 23);
            this.BtnSubtractName.TabIndex = 1;
            this.BtnSubtractName.Text = "Subtract Name";
            this.BtnSubtractName.UseVisualStyleBackColor = true;
            // 
            // BtnRenameVTwo
            // 
            this.BtnRenameVTwo.Location = new System.Drawing.Point(585, 124);
            this.BtnRenameVTwo.Name = "BtnRenameVTwo";
            this.BtnRenameVTwo.Size = new System.Drawing.Size(75, 23);
            this.BtnRenameVTwo.TabIndex = 2;
            this.BtnRenameVTwo.Text = "Change V2";
            this.BtnRenameVTwo.UseVisualStyleBackColor = true;
            this.BtnRenameVTwo.Click += new System.EventHandler(this.BtnRenameVTwo_Click);
            // 
            // BtnCheckFiles
            // 
            this.BtnCheckFiles.Location = new System.Drawing.Point(585, 317);
            this.BtnCheckFiles.Name = "BtnCheckFiles";
            this.BtnCheckFiles.Size = new System.Drawing.Size(75, 23);
            this.BtnCheckFiles.TabIndex = 3;
            this.BtnCheckFiles.Text = "Check Files";
            this.BtnCheckFiles.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Rename files to numbers";
            // 
            // RtbRenamedText
            // 
            this.RtbRenamedText.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.RtbRenamedText.Location = new System.Drawing.Point(190, 81);
            this.RtbRenamedText.Name = "RtbRenamedText";
            this.RtbRenamedText.ReadOnly = true;
            this.RtbRenamedText.Size = new System.Drawing.Size(357, 357);
            this.RtbRenamedText.TabIndex = 5;
            this.RtbRenamedText.Text = "";
            // 
            // TbxPath
            // 
            this.TbxPath.Location = new System.Drawing.Point(190, 41);
            this.TbxPath.Name = "TbxPath";
            this.TbxPath.Size = new System.Drawing.Size(357, 20);
            this.TbxPath.TabIndex = 6;
            // 
            // TbxFilterNumbers
            // 
            this.TbxFilterNumbers.Location = new System.Drawing.Point(585, 153);
            this.TbxFilterNumbers.Name = "TbxFilterNumbers";
            this.TbxFilterNumbers.Size = new System.Drawing.Size(100, 20);
            this.TbxFilterNumbers.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(691, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Filter Numbers";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(591, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "separate by space";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(187, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Enter Path";
            // 
            // CbxIsNumberFirst
            // 
            this.CbxIsNumberFirst.AutoSize = true;
            this.CbxIsNumberFirst.Location = new System.Drawing.Point(585, 192);
            this.CbxIsNumberFirst.Name = "CbxIsNumberFirst";
            this.CbxIsNumberFirst.Size = new System.Drawing.Size(146, 17);
            this.CbxIsNumberFirst.TabIndex = 11;
            this.CbxIsNumberFirst.Text = "Number is First in the file?";
            this.CbxIsNumberFirst.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.CbxIsNumberFirst);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TbxFilterNumbers);
            this.Controls.Add(this.TbxPath);
            this.Controls.Add(this.RtbRenamedText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnCheckFiles);
            this.Controls.Add(this.BtnRenameVTwo);
            this.Controls.Add(this.BtnSubtractName);
            this.Controls.Add(this.BtnRenameVOne);
            this.Name = "Form1";
            this.Text = "Renamer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnRenameVOne;
        private System.Windows.Forms.Button BtnSubtractName;
        private System.Windows.Forms.Button BtnRenameVTwo;
        private System.Windows.Forms.Button BtnCheckFiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox RtbRenamedText;
        private System.Windows.Forms.TextBox TbxPath;
        private System.Windows.Forms.TextBox TbxFilterNumbers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox CbxIsNumberFirst;
    }
}

