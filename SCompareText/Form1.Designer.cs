namespace SCompareText
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label2 = new System.Windows.Forms.Label();
            this.LbDiff1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LbDiff0 = new System.Windows.Forms.ListBox();
            this.MenuMain = new System.Windows.Forms.MenuStrip();
            this.ReloadMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdMain = new System.Windows.Forms.OpenFileDialog();
            this.bwSideBySideDiffScreen = new System.ComponentModel.BackgroundWorker();
            this.tlpSelectFiles = new System.Windows.Forms.TableLayoutPanel();
            this.TxtSelectFile0 = new System.Windows.Forms.TextBox();
            this.TxtSelectFile1 = new System.Windows.Forms.TextBox();
            this.BtnLoadFile0 = new System.Windows.Forms.Button();
            this.scDualTextView = new System.Windows.Forms.SplitContainer();
            this.FctbFile0 = new FastColoredTextBoxNS.FastColoredTextBox();
            this.FctbFile1 = new FastColoredTextBoxNS.FastColoredTextBox();
            this.PbLoading = new System.Windows.Forms.PictureBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.BtnLoadFile1 = new System.Windows.Forms.Button();
            this.btnCopyOldFileDiff = new System.Windows.Forms.Button();
            this.btnCopyNewFileDiff = new System.Windows.Forms.Button();
            this.MenuMain.SuspendLayout();
            this.tlpSelectFiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scDualTextView)).BeginInit();
            this.scDualTextView.Panel1.SuspendLayout();
            this.scDualTextView.Panel2.SuspendLayout();
            this.scDualTextView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FctbFile0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FctbFile1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbLoading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "New File Differences:";
            // 
            // LbDiff1
            // 
            this.LbDiff1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LbDiff1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbDiff1.FormattingEnabled = true;
            this.LbDiff1.HorizontalScrollbar = true;
            this.LbDiff1.ItemHeight = 16;
            this.LbDiff1.Location = new System.Drawing.Point(3, 35);
            this.LbDiff1.Name = "LbDiff1";
            this.LbDiff1.ScrollAlwaysVisible = true;
            this.LbDiff1.Size = new System.Drawing.Size(256, 276);
            this.LbDiff1.TabIndex = 1;
            this.LbDiff1.Click += new System.EventHandler(this.LbDiff1_Click);
            this.LbDiff1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.LbDiff1_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Old File Differences:";
            // 
            // LbDiff0
            // 
            this.LbDiff0.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LbDiff0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbDiff0.FormattingEnabled = true;
            this.LbDiff0.HorizontalScrollbar = true;
            this.LbDiff0.ItemHeight = 16;
            this.LbDiff0.Location = new System.Drawing.Point(3, 37);
            this.LbDiff0.Name = "LbDiff0";
            this.LbDiff0.ScrollAlwaysVisible = true;
            this.LbDiff0.Size = new System.Drawing.Size(256, 228);
            this.LbDiff0.TabIndex = 0;
            this.LbDiff0.Click += new System.EventHandler(this.LbDiff0_Click);
            this.LbDiff0.KeyUp += new System.Windows.Forms.KeyEventHandler(this.LbDiff0_KeyUp);
            // 
            // MenuMain
            // 
            this.MenuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ReloadMenuItem,
            this.ClearAllMenuItem});
            this.MenuMain.Location = new System.Drawing.Point(0, 0);
            this.MenuMain.Name = "MenuMain";
            this.MenuMain.Size = new System.Drawing.Size(1154, 24);
            this.MenuMain.TabIndex = 6;
            this.MenuMain.Text = "menuStrip1";
            // 
            // ReloadMenuItem
            // 
            this.ReloadMenuItem.Name = "ReloadMenuItem";
            this.ReloadMenuItem.Size = new System.Drawing.Size(136, 20);
            this.ReloadMenuItem.Text = "Re-Load Selected Files";
            this.ReloadMenuItem.Click += new System.EventHandler(this.ReloadMenuItem_Click);
            // 
            // ClearAllMenuItem
            // 
            this.ClearAllMenuItem.Name = "ClearAllMenuItem";
            this.ClearAllMenuItem.Size = new System.Drawing.Size(63, 20);
            this.ClearAllMenuItem.Text = "Clear All";
            this.ClearAllMenuItem.Click += new System.EventHandler(this.ClearAllMenuItem_Click);
            // 
            // bwSideBySideDiffScreen
            // 
            this.bwSideBySideDiffScreen.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwSideBySideDiffScreen_DoWork_1);
            this.bwSideBySideDiffScreen.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwSideBySideDiffScreen_RunWorkerCompleted);
            // 
            // tlpSelectFiles
            // 
            this.tlpSelectFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpSelectFiles.ColumnCount = 4;
            this.tlpSelectFiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.34722F));
            this.tlpSelectFiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpSelectFiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.65278F));
            this.tlpSelectFiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpSelectFiles.Controls.Add(this.TxtSelectFile0, 0, 0);
            this.tlpSelectFiles.Controls.Add(this.BtnLoadFile1, 3, 0);
            this.tlpSelectFiles.Controls.Add(this.TxtSelectFile1, 2, 0);
            this.tlpSelectFiles.Controls.Add(this.BtnLoadFile0, 1, 0);
            this.tlpSelectFiles.Location = new System.Drawing.Point(12, 27);
            this.tlpSelectFiles.Name = "tlpSelectFiles";
            this.tlpSelectFiles.RowCount = 1;
            this.tlpSelectFiles.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSelectFiles.Size = new System.Drawing.Size(840, 30);
            this.tlpSelectFiles.TabIndex = 7;
            // 
            // TxtSelectFile0
            // 
            this.TxtSelectFile0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtSelectFile0.Location = new System.Drawing.Point(3, 3);
            this.TxtSelectFile0.Name = "TxtSelectFile0";
            this.TxtSelectFile0.ReadOnly = true;
            this.TxtSelectFile0.Size = new System.Drawing.Size(284, 20);
            this.TxtSelectFile0.TabIndex = 2;
            // 
            // TxtSelectFile1
            // 
            this.TxtSelectFile1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtSelectFile1.Location = new System.Drawing.Point(425, 3);
            this.TxtSelectFile1.Name = "TxtSelectFile1";
            this.TxtSelectFile1.ReadOnly = true;
            this.TxtSelectFile1.Size = new System.Drawing.Size(280, 20);
            this.TxtSelectFile1.TabIndex = 4;
            // 
            // BtnLoadFile0
            // 
            this.BtnLoadFile0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnLoadFile0.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLoadFile0.Location = new System.Drawing.Point(293, 3);
            this.BtnLoadFile0.Name = "BtnLoadFile0";
            this.BtnLoadFile0.Size = new System.Drawing.Size(126, 22);
            this.BtnLoadFile0.TabIndex = 1;
            this.BtnLoadFile0.Text = "Select Old File";
            this.BtnLoadFile0.UseVisualStyleBackColor = true;
            this.BtnLoadFile0.Click += new System.EventHandler(this.BtnLoadFile0_Click);
            // 
            // scDualTextView
            // 
            this.scDualTextView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scDualTextView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scDualTextView.Location = new System.Drawing.Point(3, 3);
            this.scDualTextView.Name = "scDualTextView";
            // 
            // scDualTextView.Panel1
            // 
            this.scDualTextView.Panel1.Controls.Add(this.FctbFile0);
            // 
            // scDualTextView.Panel2
            // 
            this.scDualTextView.Panel2.Controls.Add(this.FctbFile1);
            this.scDualTextView.Size = new System.Drawing.Size(836, 603);
            this.scDualTextView.SplitterDistance = 418;
            this.scDualTextView.SplitterWidth = 7;
            this.scDualTextView.TabIndex = 8;
            this.scDualTextView.SizeChanged += new System.EventHandler(this.scDualTextView_SizeChanged);
            // 
            // FctbFile0
            // 
            this.FctbFile0.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FctbFile0.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.FctbFile0.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.FctbFile0.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FctbFile0.BackBrush = null;
            this.FctbFile0.CharHeight = 14;
            this.FctbFile0.CharWidth = 8;
            this.FctbFile0.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.FctbFile0.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.FctbFile0.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.FctbFile0.IsReplaceMode = false;
            this.FctbFile0.Location = new System.Drawing.Point(3, 3);
            this.FctbFile0.Name = "FctbFile0";
            this.FctbFile0.Paddings = new System.Windows.Forms.Padding(0);
            this.FctbFile0.ReadOnly = true;
            this.FctbFile0.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.FctbFile0.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("FctbFile0.ServiceColors")));
            this.FctbFile0.Size = new System.Drawing.Size(410, 595);
            this.FctbFile0.TabIndex = 1;
            this.FctbFile0.Zoom = 100;
            this.FctbFile0.VisibleRangeChanged += new System.EventHandler(this.FctbFile0_VisibleRangeChanged);
            // 
            // FctbFile1
            // 
            this.FctbFile1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FctbFile1.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.FctbFile1.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.FctbFile1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.FctbFile1.BackBrush = null;
            this.FctbFile1.CharHeight = 14;
            this.FctbFile1.CharWidth = 8;
            this.FctbFile1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.FctbFile1.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.FctbFile1.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.FctbFile1.IsReplaceMode = false;
            this.FctbFile1.Location = new System.Drawing.Point(3, 3);
            this.FctbFile1.Name = "FctbFile1";
            this.FctbFile1.Paddings = new System.Windows.Forms.Padding(0);
            this.FctbFile1.ReadOnly = true;
            this.FctbFile1.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.FctbFile1.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("FctbFile1.ServiceColors")));
            this.FctbFile1.Size = new System.Drawing.Size(403, 595);
            this.FctbFile1.TabIndex = 1;
            this.FctbFile1.Zoom = 100;
            this.FctbFile1.VisibleRangeChanged += new System.EventHandler(this.FctbFile1_VisibleRangeChanged);
            // 
            // PbLoading
            // 
            this.PbLoading.Image = global::SCompareText.Properties.Resources.loading;
            this.PbLoading.Location = new System.Drawing.Point(1113, 27);
            this.PbLoading.Name = "PbLoading";
            this.PbLoading.Size = new System.Drawing.Size(29, 30);
            this.PbLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PbLoading.TabIndex = 9;
            this.PbLoading.TabStop = false;
            this.PbLoading.Visible = false;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Location = new System.Drawing.Point(7, 7);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btnCopyOldFileDiff);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.LbDiff0);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btnCopyNewFileDiff);
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Panel2.Controls.Add(this.LbDiff1);
            this.splitContainer2.Size = new System.Drawing.Size(267, 595);
            this.splitContainer2.SplitterDistance = 271;
            this.splitContainer2.TabIndex = 10;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer3.Location = new System.Drawing.Point(12, 63);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.scDualTextView);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer3.Size = new System.Drawing.Size(1130, 611);
            this.splitContainer3.SplitterDistance = 844;
            this.splitContainer3.SplitterWidth = 7;
            this.splitContainer3.TabIndex = 11;
            this.splitContainer3.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer3_SplitterMoved);
            // 
            // BtnLoadFile1
            // 
            this.BtnLoadFile1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnLoadFile1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLoadFile1.Location = new System.Drawing.Point(712, 3);
            this.BtnLoadFile1.Name = "BtnLoadFile1";
            this.BtnLoadFile1.Size = new System.Drawing.Size(125, 22);
            this.BtnLoadFile1.TabIndex = 3;
            this.BtnLoadFile1.Text = "Select New File";
            this.BtnLoadFile1.UseVisualStyleBackColor = true;
            this.BtnLoadFile1.Click += new System.EventHandler(this.BtnLoadFile1_Click);
            // 
            // btnCopyOldFileDiff
            // 
            this.btnCopyOldFileDiff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyOldFileDiff.Location = new System.Drawing.Point(184, 3);
            this.btnCopyOldFileDiff.Name = "btnCopyOldFileDiff";
            this.btnCopyOldFileDiff.Size = new System.Drawing.Size(75, 23);
            this.btnCopyOldFileDiff.TabIndex = 2;
            this.btnCopyOldFileDiff.Text = "Copy All";
            this.btnCopyOldFileDiff.UseVisualStyleBackColor = true;
            this.btnCopyOldFileDiff.Click += new System.EventHandler(this.btnCopyOldFileDiff_Click);
            // 
            // btnCopyNewFileDiff
            // 
            this.btnCopyNewFileDiff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyNewFileDiff.Location = new System.Drawing.Point(184, 3);
            this.btnCopyNewFileDiff.Name = "btnCopyNewFileDiff";
            this.btnCopyNewFileDiff.Size = new System.Drawing.Size(75, 23);
            this.btnCopyNewFileDiff.TabIndex = 3;
            this.btnCopyNewFileDiff.Text = "Copy All";
            this.btnCopyNewFileDiff.UseVisualStyleBackColor = true;
            this.btnCopyNewFileDiff.Click += new System.EventHandler(this.btnCopyNewFileDiff_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 686);
            this.Controls.Add(this.splitContainer3);
            this.Controls.Add(this.PbLoading);
            this.Controls.Add(this.tlpSelectFiles);
            this.Controls.Add(this.MenuMain);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Text Compare by Scott Waldron, TheWayOfCoding.com";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MenuMain.ResumeLayout(false);
            this.MenuMain.PerformLayout();
            this.tlpSelectFiles.ResumeLayout(false);
            this.tlpSelectFiles.PerformLayout();
            this.scDualTextView.Panel1.ResumeLayout(false);
            this.scDualTextView.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scDualTextView)).EndInit();
            this.scDualTextView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FctbFile0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FctbFile1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbLoading)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox LbDiff1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox LbDiff0;
        private System.Windows.Forms.MenuStrip MenuMain;
        private System.Windows.Forms.ToolStripMenuItem ReloadMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ClearAllMenuItem;
        private System.Windows.Forms.OpenFileDialog ofdMain;
        private System.ComponentModel.BackgroundWorker bwSideBySideDiffScreen;
        private System.Windows.Forms.TableLayoutPanel tlpSelectFiles;
        private System.Windows.Forms.TextBox TxtSelectFile0;
        private System.Windows.Forms.TextBox TxtSelectFile1;
        private System.Windows.Forms.Button BtnLoadFile0;
        private System.Windows.Forms.SplitContainer scDualTextView;
        private FastColoredTextBoxNS.FastColoredTextBox FctbFile0;
        private FastColoredTextBoxNS.FastColoredTextBox FctbFile1;
        private System.Windows.Forms.PictureBox PbLoading;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button BtnLoadFile1;
        private System.Windows.Forms.Button btnCopyOldFileDiff;
        private System.Windows.Forms.Button btnCopyNewFileDiff;
    }
}

