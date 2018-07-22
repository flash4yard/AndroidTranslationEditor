namespace AndroidTranslationEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.table = new System.Windows.Forms.DataGridView();
            this.C1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileOutput = new System.Windows.Forms.SaveFileDialog();
            this.fileInput = new System.Windows.Forms.OpenFileDialog();
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.OpenFile = new System.Windows.Forms.ToolStripButton();
            this.CreateFile = new System.Windows.Forms.ToolStripButton();
            this.About = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.table)).BeginInit();
            this.toolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // table
            // 
            this.table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.table.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.C1});
            this.table.Location = new System.Drawing.Point(0, 28);
            this.table.Name = "table";
            this.table.Size = new System.Drawing.Size(800, 419);
            this.table.TabIndex = 0;
            this.table.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.CellUpdated);
            // 
            // C1
            // 
            this.C1.HeaderText = "Key";
            this.C1.Name = "C1";
            this.C1.ReadOnly = true;
            // 
            // fileInput
            // 
            this.fileInput.FileName = "openFileDialog1";
            // 
            // toolBar
            // 
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFile,
            this.CreateFile,
            this.About});
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(800, 25);
            this.toolBar.TabIndex = 1;
            // 
            // OpenFile
            // 
            this.OpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenFile.Image = ((System.Drawing.Image)(resources.GetObject("OpenFile.Image")));
            this.OpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenFile.Name = "OpenFile";
            this.OpenFile.Size = new System.Drawing.Size(23, 22);
            this.OpenFile.Text = "&Open";
            this.OpenFile.Click += new System.EventHandler(this.OpenFile_Click);
            // 
            // CreateFile
            // 
            this.CreateFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CreateFile.Image = ((System.Drawing.Image)(resources.GetObject("CreateFile.Image")));
            this.CreateFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CreateFile.Name = "CreateFile";
            this.CreateFile.Size = new System.Drawing.Size(23, 22);
            this.CreateFile.Text = "&New";
            this.CreateFile.Click += new System.EventHandler(this.CreateFile_Click);
            // 
            // About
            // 
            this.About.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.About.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.About.Image = ((System.Drawing.Image)(resources.GetObject("About.Image")));
            this.About.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.About.Name = "About";
            this.About.Size = new System.Drawing.Size(23, 22);
            this.About.Text = "He&lp";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolBar);
            this.Controls.Add(this.table);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.table)).EndInit();
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView table;
        private System.Windows.Forms.DataGridViewTextBoxColumn C1;
        private System.Windows.Forms.SaveFileDialog fileOutput;
        private System.Windows.Forms.OpenFileDialog fileInput;
        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.ToolStripButton CreateFile;
        private System.Windows.Forms.ToolStripButton OpenFile;
        private System.Windows.Forms.ToolStripButton About;
    }
}

