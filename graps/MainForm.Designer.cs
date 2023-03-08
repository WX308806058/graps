namespace graps
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_Upload = new System.Windows.Forms.ToolStripButton();
            this.sPictureBox_ShowPicture = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.richTextBox_ZplStr = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sPictureBox_ShowPicture)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_Upload});
            this.toolStrip1.Location = new System.Drawing.Point(20, 60);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(680, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton_Upload
            // 
            this.toolStripButton_Upload.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Upload.Image")));
            this.toolStripButton_Upload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Upload.Name = "toolStripButton_Upload";
            this.toolStripButton_Upload.Size = new System.Drawing.Size(93, 24);
            this.toolStripButton_Upload.Text = "上传图片";
            this.toolStripButton_Upload.Click += new System.EventHandler(this.toolStripButton_Upload_Click);
            // 
            // sPictureBox_ShowPicture
            // 
            this.sPictureBox_ShowPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sPictureBox_ShowPicture.Image = ((System.Drawing.Image)(resources.GetObject("sPictureBox_ShowPicture.Image")));
            this.sPictureBox_ShowPicture.Location = new System.Drawing.Point(3, 21);
            this.sPictureBox_ShowPicture.Name = "sPictureBox_ShowPicture";
            this.sPictureBox_ShowPicture.Size = new System.Drawing.Size(206, 186);
            this.sPictureBox_ShowPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.sPictureBox_ShowPicture.TabIndex = 0;
            this.sPictureBox_ShowPicture.TabStop = false;
            this.sPictureBox_ShowPicture.Click += new System.EventHandler(this.toolStripButton_Upload_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.richTextBox_ZplStr);
            this.panel2.Location = new System.Drawing.Point(24, 321);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(670, 241);
            this.panel2.TabIndex = 2;
            // 
            // richTextBox_ZplStr
            // 
            this.richTextBox_ZplStr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_ZplStr.Location = new System.Drawing.Point(0, 0);
            this.richTextBox_ZplStr.Name = "richTextBox_ZplStr";
            this.richTextBox_ZplStr.ReadOnly = true;
            this.richTextBox_ZplStr.Size = new System.Drawing.Size(670, 241);
            this.richTextBox_ZplStr.TabIndex = 0;
            this.richTextBox_ZplStr.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.sPictureBox_ShowPicture);
            this.groupBox1.Location = new System.Drawing.Point(24, 105);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 210);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "上传的图片";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(720, 585);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Purple;
            this.Text = "图片转ZPL";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sPictureBox_ShowPicture)).EndInit();
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_Upload;
        private System.Windows.Forms.PictureBox sPictureBox_ShowPicture;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox richTextBox_ZplStr;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

