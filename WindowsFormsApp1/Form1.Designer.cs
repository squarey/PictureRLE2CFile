namespace WindowsFormsApp1
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.EditFileSelect = new System.Windows.Forms.TextBox();
            this.PicPreview = new System.Windows.Forms.PictureBox();
            this.ListboxCompress = new System.Windows.Forms.ListBox();
            this.BtnSwitch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LablePixelFormat = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.LabelPicSize = new System.Windows.Forms.Label();
            this.BtnScan = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.EditSavePath = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LabelPicShowNotice = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PicPreview)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 35);
            this.label1.TabIndex = 1;
            this.label1.Text = "已选择文件:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // EditFileSelect
            // 
            this.EditFileSelect.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.EditFileSelect.Location = new System.Drawing.Point(175, 12);
            this.EditFileSelect.Name = "EditFileSelect";
            this.EditFileSelect.Size = new System.Drawing.Size(223, 35);
            this.EditFileSelect.TabIndex = 2;
            // 
            // PicPreview
            // 
            this.PicPreview.Location = new System.Drawing.Point(-1, -1);
            this.PicPreview.Name = "PicPreview";
            this.PicPreview.Size = new System.Drawing.Size(450, 310);
            this.PicPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PicPreview.TabIndex = 4;
            this.PicPreview.TabStop = false;
            // 
            // ListboxCompress
            // 
            this.ListboxCompress.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ListboxCompress.FormattingEnabled = true;
            this.ListboxCompress.ItemHeight = 25;
            this.ListboxCompress.Items.AddRange(new object[] {
            "RGB565 RLE压缩",
            "BGR565 RLE压缩",
            "RGB565 BMP",
            "BGR565 BMP"});
            this.ListboxCompress.Location = new System.Drawing.Point(563, 47);
            this.ListboxCompress.Name = "ListboxCompress";
            this.ListboxCompress.Size = new System.Drawing.Size(246, 129);
            this.ListboxCompress.TabIndex = 5;
            // 
            // BtnSwitch
            // 
            this.BtnSwitch.AutoSize = true;
            this.BtnSwitch.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnSwitch.Location = new System.Drawing.Point(571, 288);
            this.BtnSwitch.Name = "BtnSwitch";
            this.BtnSwitch.Size = new System.Drawing.Size(116, 35);
            this.BtnSwitch.TabIndex = 6;
            this.BtnSwitch.Text = "转换";
            this.BtnSwitch.UseVisualStyleBackColor = true;
            this.BtnSwitch.Click += new System.EventHandler(this.BtnSwitch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(557, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 35);
            this.label2.TabIndex = 7;
            this.label2.Text = "压缩方式:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(557, 237);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 35);
            this.label3.TabIndex = 8;
            this.label3.Text = "像素格式:";
            // 
            // LablePixelFormat
            // 
            this.LablePixelFormat.AutoSize = true;
            this.LablePixelFormat.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LablePixelFormat.Location = new System.Drawing.Point(687, 237);
            this.LablePixelFormat.Name = "LablePixelFormat";
            this.LablePixelFormat.Size = new System.Drawing.Size(51, 35);
            this.LablePixelFormat.TabIndex = 9;
            this.LablePixelFormat.Text = "---";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(18, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 35);
            this.label5.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(557, 189);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(124, 35);
            this.label6.TabIndex = 11;
            this.label6.Text = "大      小:";
            // 
            // LabelPicSize
            // 
            this.LabelPicSize.AutoSize = true;
            this.LabelPicSize.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabelPicSize.Location = new System.Drawing.Point(687, 189);
            this.LabelPicSize.Name = "LabelPicSize";
            this.LabelPicSize.Size = new System.Drawing.Size(51, 35);
            this.LabelPicSize.TabIndex = 12;
            this.LabelPicSize.Text = "---";
            // 
            // BtnScan
            // 
            this.BtnScan.AutoSize = true;
            this.BtnScan.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnScan.Location = new System.Drawing.Point(413, 12);
            this.BtnScan.Name = "BtnScan";
            this.BtnScan.Size = new System.Drawing.Size(75, 35);
            this.BtnScan.TabIndex = 13;
            this.BtnScan.Text = "浏览";
            this.BtnScan.UseVisualStyleBackColor = true;
            this.BtnScan.Click += new System.EventHandler(this.BtnScan_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(12, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(184, 35);
            this.label4.TabIndex = 14;
            this.label4.Text = "文件保存路径:";
            // 
            // EditSavePath
            // 
            this.EditSavePath.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.EditSavePath.Location = new System.Drawing.Point(202, 60);
            this.EditSavePath.Name = "EditSavePath";
            this.EditSavePath.Size = new System.Drawing.Size(286, 35);
            this.EditSavePath.TabIndex = 15;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.LabelPicShowNotice);
            this.panel1.Controls.Add(this.PicPreview);
            this.panel1.Location = new System.Drawing.Point(24, 101);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 310);
            this.panel1.TabIndex = 16;
            // 
            // LabelPicShowNotice
            // 
            this.LabelPicShowNotice.AutoSize = true;
            this.LabelPicShowNotice.BackColor = System.Drawing.Color.Transparent;
            this.LabelPicShowNotice.Font = new System.Drawing.Font("微软雅黑", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LabelPicShowNotice.Location = new System.Drawing.Point(114, 135);
            this.LabelPicShowNotice.Name = "LabelPicShowNotice";
            this.LabelPicShowNotice.Size = new System.Drawing.Size(195, 46);
            this.LabelPicShowNotice.TabIndex = 0;
            this.LabelPicShowNotice.Text = "图片预览区";
            this.LabelPicShowNotice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(838, 442);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.EditSavePath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.BtnScan);
            this.Controls.Add(this.LabelPicSize);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.LablePixelFormat);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnSwitch);
            this.Controls.Add(this.ListboxCompress);
            this.Controls.Add(this.EditFileSelect);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.PicPreview)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox EditFileSelect;
        private System.Windows.Forms.PictureBox PicPreview;
        private System.Windows.Forms.ListBox ListboxCompress;
        private System.Windows.Forms.Button BtnSwitch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LablePixelFormat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label LabelPicSize;
        private System.Windows.Forms.Button BtnScan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox EditSavePath;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label LabelPicShowNotice;
    }
}

