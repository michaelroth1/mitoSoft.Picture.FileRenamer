namespace mitoSoft.Picture.FileRenamer
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.button1 = new System.Windows.Forms.Button();
            this.FileListBox = new System.Windows.Forms.ListBox();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FormatTextBox = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.FileContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openContainingFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ResultListBox = new System.Windows.Forms.ListBox();
            this.ResultContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openContainingFolderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.shiftToLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenLocalFolderButton = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.FileContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.ResultContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1255, 754);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(225, 46);
            this.button1.TabIndex = 0;
            this.button1.Text = "Rename";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Renaming_Click);
            // 
            // FileListBox
            // 
            this.FileListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileListBox.FormattingEnabled = true;
            this.FileListBox.HorizontalScrollbar = true;
            this.FileListBox.ItemHeight = 32;
            this.FileListBox.Location = new System.Drawing.Point(0, 0);
            this.FileListBox.Name = "FileListBox";
            this.FileListBox.Size = new System.Drawing.Size(654, 741);
            this.FileListBox.TabIndex = 1;
            this.FileListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FileListBox_MouseDown);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "openFileDialog";
            this.OpenFileDialog.Multiselect = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(1330, 64);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 46);
            this.button2.TabIndex = 2;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.SearchFiles_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 820);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1495, 42);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(250, 30);
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(237, 32);
            this.toolStripStatusLabel.Text = "toolStripStatusLabel1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::mitoSoft.Picture.FileRenamer.Properties.Resources.Mitosoft_Logo_transparent;
            this.pictureBox1.Location = new System.Drawing.Point(1330, 155);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 88);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 761);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 32);
            this.label1.TabIndex = 5;
            this.label1.Text = "Format:";
            // 
            // FormatTextBox
            // 
            this.FormatTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FormatTextBox.Location = new System.Drawing.Point(114, 759);
            this.FormatTextBox.Name = "FormatTextBox";
            this.FormatTextBox.Size = new System.Drawing.Size(590, 39);
            this.FormatTextBox.TabIndex = 6;
            this.FormatTextBox.Text = "yyyyMMdd_HHmmss";
            this.FormatTextBox.TextChanged += new System.EventHandler(this.FormatTextBox_TextChanged);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(1330, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(150, 46);
            this.button3.TabIndex = 7;
            this.button3.Text = "Clear";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Clear_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(1330, 301);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(150, 99);
            this.button4.TabIndex = 8;
            this.button4.Text = "Sort to Folder";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.FolderSort_Click);
            // 
            // FileContextMenuStrip
            // 
            this.FileContextMenuStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.FileContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openContainingFolderToolStripMenuItem});
            this.FileContextMenuStrip.Name = "contextMenuStrip";
            this.FileContextMenuStrip.Size = new System.Drawing.Size(338, 42);
            // 
            // openContainingFolderToolStripMenuItem
            // 
            this.openContainingFolderToolStripMenuItem.Name = "openContainingFolderToolStripMenuItem";
            this.openContainingFolderToolStripMenuItem.Size = new System.Drawing.Size(337, 38);
            this.openContainingFolderToolStripMenuItem.Text = "Open containing folder";
            this.openContainingFolderToolStripMenuItem.Click += new System.EventHandler(this.OpenContainingFolderToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.FileListBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ResultListBox);
            this.splitContainer1.Size = new System.Drawing.Size(1312, 741);
            this.splitContainer1.SplitterDistance = 654;
            this.splitContainer1.TabIndex = 9;
            // 
            // ResultListBox
            // 
            this.ResultListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultListBox.FormattingEnabled = true;
            this.ResultListBox.HorizontalScrollbar = true;
            this.ResultListBox.ItemHeight = 32;
            this.ResultListBox.Location = new System.Drawing.Point(0, 0);
            this.ResultListBox.Name = "ResultListBox";
            this.ResultListBox.Size = new System.Drawing.Size(654, 741);
            this.ResultListBox.TabIndex = 0;
            this.ResultListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ResultListBox_MouseDown);
            // 
            // ResultContextMenuStrip
            // 
            this.ResultContextMenuStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ResultContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openContainingFolderToolStripMenuItem1,
            this.shiftToLeftToolStripMenuItem});
            this.ResultContextMenuStrip.Name = "ResultContextMenuStrip";
            this.ResultContextMenuStrip.Size = new System.Drawing.Size(338, 80);
            // 
            // openContainingFolderToolStripMenuItem1
            // 
            this.openContainingFolderToolStripMenuItem1.Name = "openContainingFolderToolStripMenuItem1";
            this.openContainingFolderToolStripMenuItem1.Size = new System.Drawing.Size(337, 38);
            this.openContainingFolderToolStripMenuItem1.Text = "Open containing folder";
            this.openContainingFolderToolStripMenuItem1.Click += new System.EventHandler(this.OpenContainingFolderToolStripMenuItem1_Click);
            // 
            // shiftToLeftToolStripMenuItem
            // 
            this.shiftToLeftToolStripMenuItem.Name = "shiftToLeftToolStripMenuItem";
            this.shiftToLeftToolStripMenuItem.Size = new System.Drawing.Size(337, 38);
            this.shiftToLeftToolStripMenuItem.Text = "← shift to left";
            this.shiftToLeftToolStripMenuItem.Click += new System.EventHandler(this.ShiftToLeftToolStripMenuItem_Click);
            // 
            // OpenLocalFolderButton
            // 
            this.OpenLocalFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenLocalFolderButton.Location = new System.Drawing.Point(1333, 406);
            this.OpenLocalFolderButton.Name = "OpenLocalFolderButton";
            this.OpenLocalFolderButton.Size = new System.Drawing.Size(150, 99);
            this.OpenLocalFolderButton.TabIndex = 10;
            this.OpenLocalFolderButton.Text = "Open local Folder...";
            this.OpenLocalFolderButton.UseVisualStyleBackColor = true;
            this.OpenLocalFolderButton.Click += new System.EventHandler(this.OpenLocalFolder_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1495, 862);
            this.Controls.Add(this.OpenLocalFolderButton);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.FormatTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1118, 698);
            this.Name = "MainForm";
            this.Text = "mitoSoft.Picture.FileRenamer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.FileContextMenuStrip.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResultContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button1;
        private ListBox FileListBox;
        private OpenFileDialog OpenFileDialog;
        private Button button2;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel;
        private PictureBox pictureBox1;
        private ToolStripProgressBar toolStripProgressBar;
        private Label label1;
        private TextBox FormatTextBox;
        private Button button3;
        private Button button4;
        private ContextMenuStrip FileContextMenuStrip;
        private ToolStripMenuItem openContainingFolderToolStripMenuItem;
        private SplitContainer splitContainer1;
        private ListBox ResultListBox;
        private ContextMenuStrip ResultContextMenuStrip;
        private ToolStripMenuItem openContainingFolderToolStripMenuItem1;
        private ToolStripMenuItem shiftToLeftToolStripMenuItem;
        private Button OpenLocalFolderButton;
    }
}