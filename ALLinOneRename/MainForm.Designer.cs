
namespace ALLinOneRename
{
    partial class MainForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TbxFilterNumbersV1 = new System.Windows.Forms.TextBox();
            this.CbxIsNumberFirstV1 = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TbxSubtractNumber = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.RtbCheckFiles = new System.Windows.Forms.RichTextBox();
            this.BtnClearRenamedRtb = new System.Windows.Forms.Button();
            this.BtnClearCheckFilesRtb = new System.Windows.Forms.Button();
            this.BtnShowFolderFiles = new System.Windows.Forms.Button();
            this.BtnOpenExplorer = new System.Windows.Forms.Button();
            this.BtnTransferFilesFromDownload = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.BtnCreateHashTable = new System.Windows.Forms.Button();
            this.CbxIsNumberLast = new System.Windows.Forms.CheckBox();
            this.CbxIsV2Func = new System.Windows.Forms.CheckBox();
            this.CbxIsNumberSecond = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // BtnRenameVOne
            // 
            this.BtnRenameVOne.Location = new System.Drawing.Point(15, 108);
            this.BtnRenameVOne.Name = "BtnRenameVOne";
            this.BtnRenameVOne.Size = new System.Drawing.Size(75, 23);
            this.BtnRenameVOne.TabIndex = 0;
            this.BtnRenameVOne.Text = "Change V1";
            this.BtnRenameVOne.UseVisualStyleBackColor = true;
            this.BtnRenameVOne.Click += new System.EventHandler(this.BtnRenameVOne_Click);
            // 
            // BtnSubtractName
            // 
            this.BtnSubtractName.Location = new System.Drawing.Point(56, 386);
            this.BtnSubtractName.Name = "BtnSubtractName";
            this.BtnSubtractName.Size = new System.Drawing.Size(75, 23);
            this.BtnSubtractName.TabIndex = 1;
            this.BtnSubtractName.Text = "Subtract Name";
            this.BtnSubtractName.UseVisualStyleBackColor = true;
            this.BtnSubtractName.Click += new System.EventHandler(this.BtnSubtractName_Click);
            // 
            // BtnRenameVTwo
            // 
            this.BtnRenameVTwo.Location = new System.Drawing.Point(109, 108);
            this.BtnRenameVTwo.Name = "BtnRenameVTwo";
            this.BtnRenameVTwo.Size = new System.Drawing.Size(75, 23);
            this.BtnRenameVTwo.TabIndex = 2;
            this.BtnRenameVTwo.Text = "Change V2";
            this.BtnRenameVTwo.UseVisualStyleBackColor = true;
            this.BtnRenameVTwo.Click += new System.EventHandler(this.BtnRenameVTwo_Click);
            // 
            // BtnCheckFiles
            // 
            this.BtnCheckFiles.Location = new System.Drawing.Point(644, 412);
            this.BtnCheckFiles.Name = "BtnCheckFiles";
            this.BtnCheckFiles.Size = new System.Drawing.Size(75, 23);
            this.BtnCheckFiles.TabIndex = 3;
            this.BtnCheckFiles.Text = "Check Files";
            this.BtnCheckFiles.UseVisualStyleBackColor = true;
            this.BtnCheckFiles.Click += new System.EventHandler(this.BtnCheckFiles_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Files To Num";
            // 
            // RtbRenamedText
            // 
            this.RtbRenamedText.BackColor = System.Drawing.SystemColors.ControlDark;
            this.RtbRenamedText.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.RtbRenamedText.Location = new System.Drawing.Point(202, 108);
            this.RtbRenamedText.Name = "RtbRenamedText";
            this.RtbRenamedText.ReadOnly = true;
            this.RtbRenamedText.Size = new System.Drawing.Size(436, 327);
            this.RtbRenamedText.TabIndex = 5;
            this.RtbRenamedText.Text = "";
            // 
            // TbxPath
            // 
            this.TbxPath.BackColor = System.Drawing.SystemColors.ControlDark;
            this.TbxPath.Location = new System.Drawing.Point(202, 12);
            this.TbxPath.Multiline = true;
            this.TbxPath.Name = "TbxPath";
            this.TbxPath.Size = new System.Drawing.Size(436, 87);
            this.TbxPath.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(106, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Files To Format";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(135, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Enter Path/s";
            // 
            // TbxFilterNumbersV1
            // 
            this.TbxFilterNumbersV1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.TbxFilterNumbersV1.Location = new System.Drawing.Point(81, 153);
            this.TbxFilterNumbersV1.Name = "TbxFilterNumbersV1";
            this.TbxFilterNumbersV1.Size = new System.Drawing.Size(100, 20);
            this.TbxFilterNumbersV1.TabIndex = 12;
            // 
            // CbxIsNumberFirstV1
            // 
            this.CbxIsNumberFirstV1.AutoSize = true;
            this.CbxIsNumberFirstV1.Location = new System.Drawing.Point(4, 192);
            this.CbxIsNumberFirstV1.Name = "CbxIsNumberFirstV1";
            this.CbxIsNumberFirstV1.Size = new System.Drawing.Size(96, 17);
            this.CbxIsNumberFirstV1.TabIndex = 13;
            this.CbxIsNumberFirstV1.Text = "Is Number First";
            this.CbxIsNumberFirstV1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(78, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Separate By Space";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Filter Numbers";
            // 
            // TbxSubtractNumber
            // 
            this.TbxSubtractNumber.BackColor = System.Drawing.SystemColors.ControlDark;
            this.TbxSubtractNumber.Location = new System.Drawing.Point(47, 415);
            this.TbxSubtractNumber.Name = "TbxSubtractNumber";
            this.TbxSubtractNumber.Size = new System.Drawing.Size(100, 20);
            this.TbxSubtractNumber.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(-2, 344);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(183, 39);
            this.label7.TabIndex = 17;
            this.label7.Text = "the file names MUST be just numbers\r\nbefore using this button\r\n works only one li" +
    "ne";
            // 
            // RtbCheckFiles
            // 
            this.RtbCheckFiles.BackColor = System.Drawing.SystemColors.ControlDark;
            this.RtbCheckFiles.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.RtbCheckFiles.Location = new System.Drawing.Point(725, 12);
            this.RtbCheckFiles.Name = "RtbCheckFiles";
            this.RtbCheckFiles.ReadOnly = true;
            this.RtbCheckFiles.Size = new System.Drawing.Size(457, 426);
            this.RtbCheckFiles.TabIndex = 18;
            this.RtbCheckFiles.Text = "";
            // 
            // BtnClearRenamedRtb
            // 
            this.BtnClearRenamedRtb.Location = new System.Drawing.Point(4, 12);
            this.BtnClearRenamedRtb.Name = "BtnClearRenamedRtb";
            this.BtnClearRenamedRtb.Size = new System.Drawing.Size(75, 23);
            this.BtnClearRenamedRtb.TabIndex = 19;
            this.BtnClearRenamedRtb.Text = "Clear Text";
            this.BtnClearRenamedRtb.UseVisualStyleBackColor = true;
            this.BtnClearRenamedRtb.Click += new System.EventHandler(this.BtnClearRenamedRtb_Click);
            // 
            // BtnClearCheckFilesRtb
            // 
            this.BtnClearCheckFilesRtb.Location = new System.Drawing.Point(644, 10);
            this.BtnClearCheckFilesRtb.Name = "BtnClearCheckFilesRtb";
            this.BtnClearCheckFilesRtb.Size = new System.Drawing.Size(75, 23);
            this.BtnClearCheckFilesRtb.TabIndex = 20;
            this.BtnClearCheckFilesRtb.Text = "Clear Text";
            this.BtnClearCheckFilesRtb.UseVisualStyleBackColor = true;
            this.BtnClearCheckFilesRtb.Click += new System.EventHandler(this.BtnClearCheckFilesRtb_Click);
            // 
            // BtnShowFolderFiles
            // 
            this.BtnShowFolderFiles.Location = new System.Drawing.Point(644, 323);
            this.BtnShowFolderFiles.Name = "BtnShowFolderFiles";
            this.BtnShowFolderFiles.Size = new System.Drawing.Size(75, 23);
            this.BtnShowFolderFiles.TabIndex = 21;
            this.BtnShowFolderFiles.Text = "Folder Files";
            this.BtnShowFolderFiles.UseVisualStyleBackColor = true;
            this.BtnShowFolderFiles.Click += new System.EventHandler(this.BtnShowFolderFiles_Click);
            // 
            // BtnOpenExplorer
            // 
            this.BtnOpenExplorer.Location = new System.Drawing.Point(644, 360);
            this.BtnOpenExplorer.Name = "BtnOpenExplorer";
            this.BtnOpenExplorer.Size = new System.Drawing.Size(75, 23);
            this.BtnOpenExplorer.TabIndex = 22;
            this.BtnOpenExplorer.Text = "Open Folder";
            this.BtnOpenExplorer.UseVisualStyleBackColor = true;
            this.BtnOpenExplorer.Click += new System.EventHandler(this.BtnOpenExplorer_Click);
            // 
            // BtnTransferFilesFromDownload
            // 
            this.BtnTransferFilesFromDownload.Location = new System.Drawing.Point(644, 215);
            this.BtnTransferFilesFromDownload.Name = "BtnTransferFilesFromDownload";
            this.BtnTransferFilesFromDownload.Size = new System.Drawing.Size(75, 23);
            this.BtnTransferFilesFromDownload.TabIndex = 23;
            this.BtnTransferFilesFromDownload.Text = "Transfer";
            this.BtnTransferFilesFromDownload.UseVisualStyleBackColor = true;
            this.BtnTransferFilesFromDownload.Click += new System.EventHandler(this.BtnTransferFilesFromDownload_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(644, 307);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "only one line";
            // 
            // BtnCreateHashTable
            // 
            this.BtnCreateHashTable.Location = new System.Drawing.Point(644, 244);
            this.BtnCreateHashTable.Name = "BtnCreateHashTable";
            this.BtnCreateHashTable.Size = new System.Drawing.Size(75, 23);
            this.BtnCreateHashTable.TabIndex = 26;
            this.BtnCreateHashTable.Text = "Create ";
            this.BtnCreateHashTable.UseVisualStyleBackColor = true;
            this.BtnCreateHashTable.Click += new System.EventHandler(this.BtnCreateHashTable_Click);
            // 
            // CbxIsNumberLast
            // 
            this.CbxIsNumberLast.AutoSize = true;
            this.CbxIsNumberLast.Location = new System.Drawing.Point(4, 235);
            this.CbxIsNumberLast.Name = "CbxIsNumberLast";
            this.CbxIsNumberLast.Size = new System.Drawing.Size(97, 17);
            this.CbxIsNumberLast.TabIndex = 27;
            this.CbxIsNumberLast.Text = "Is Number Last";
            this.CbxIsNumberLast.UseVisualStyleBackColor = true;
            // 
            // CbxIsV2Func
            // 
            this.CbxIsV2Func.AutoSize = true;
            this.CbxIsV2Func.Checked = true;
            this.CbxIsV2Func.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbxIsV2Func.Location = new System.Drawing.Point(1, 39);
            this.CbxIsV2Func.Name = "CbxIsV2Func";
            this.CbxIsV2Func.Size = new System.Drawing.Size(156, 17);
            this.CbxIsV2Func.TabIndex = 28;
            this.CbxIsV2Func.Text = "Use GetNumberV2 function";
            this.CbxIsV2Func.UseVisualStyleBackColor = true;
            // 
            // CbxIsNumberSecond
            // 
            this.CbxIsNumberSecond.AutoSize = true;
            this.CbxIsNumberSecond.Location = new System.Drawing.Point(4, 212);
            this.CbxIsNumberSecond.Name = "CbxIsNumberSecond";
            this.CbxIsNumberSecond.Size = new System.Drawing.Size(114, 17);
            this.CbxIsNumberSecond.TabIndex = 29;
            this.CbxIsNumberSecond.Text = "Is Number Second";
            this.CbxIsNumberSecond.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1194, 450);
            this.Controls.Add(this.CbxIsNumberSecond);
            this.Controls.Add(this.CbxIsV2Func);
            this.Controls.Add(this.CbxIsNumberLast);
            this.Controls.Add(this.BtnCreateHashTable);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.BtnTransferFilesFromDownload);
            this.Controls.Add(this.BtnOpenExplorer);
            this.Controls.Add(this.BtnShowFolderFiles);
            this.Controls.Add(this.BtnClearCheckFilesRtb);
            this.Controls.Add(this.BtnClearRenamedRtb);
            this.Controls.Add(this.RtbCheckFiles);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TbxSubtractNumber);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CbxIsNumberFirstV1);
            this.Controls.Add(this.TbxFilterNumbersV1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TbxPath);
            this.Controls.Add(this.RtbRenamedText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnCheckFiles);
            this.Controls.Add(this.BtnRenameVTwo);
            this.Controls.Add(this.BtnSubtractName);
            this.Controls.Add(this.BtnRenameVOne);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TbxFilterNumbersV1;
        private System.Windows.Forms.CheckBox CbxIsNumberFirstV1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TbxSubtractNumber;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox RtbCheckFiles;
        private System.Windows.Forms.Button BtnClearRenamedRtb;
        private System.Windows.Forms.Button BtnClearCheckFilesRtb;
        private System.Windows.Forms.Button BtnShowFolderFiles;
        private System.Windows.Forms.Button BtnOpenExplorer;
        private System.Windows.Forms.Button BtnTransferFilesFromDownload;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button BtnCreateHashTable;
        private System.Windows.Forms.CheckBox CbxIsNumberLast;
        private System.Windows.Forms.CheckBox CbxIsV2Func;
        private System.Windows.Forms.CheckBox CbxIsNumberSecond;
    }
}

