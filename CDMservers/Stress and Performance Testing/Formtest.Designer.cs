﻿namespace Stress_and_Performance_Testing
{
    partial class Formtest
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonstart = new System.Windows.Forms.Button();
            this.buttonstop = new System.Windows.Forms.Button();
            this.comboBoxmethods = new System.Windows.Forms.ComboBox();
            this.textBoxurl = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBoxeachthreadvolume = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxthreadcount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxusername = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxtype = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxcountcode = new System.Windows.Forms.TextBox();
            this.buttonstuff = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxdatavolume = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxordinal = new System.Windows.Forms.TextBox();
            this.buttonfiles = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxsourcefile = new System.Windows.Forms.TextBox();
            this.buttoncamera = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxcameras = new System.Windows.Forms.ComboBox();
            this.buttoncameracapture = new System.Windows.Forms.Button();
            this.buttoncameraclose = new System.Windows.Forms.Button();
            this.videoSourcePlayer = new AForge.Controls.VideoSourcePlayer();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonstart
            // 
            this.buttonstart.Location = new System.Drawing.Point(496, 44);
            this.buttonstart.Name = "buttonstart";
            this.buttonstart.Size = new System.Drawing.Size(75, 23);
            this.buttonstart.TabIndex = 0;
            this.buttonstart.Text = "start";
            this.buttonstart.UseVisualStyleBackColor = true;
            this.buttonstart.Click += new System.EventHandler(this.buttonstart_Click);
            // 
            // buttonstop
            // 
            this.buttonstop.Location = new System.Drawing.Point(496, 92);
            this.buttonstop.Name = "buttonstop";
            this.buttonstop.Size = new System.Drawing.Size(75, 23);
            this.buttonstop.TabIndex = 1;
            this.buttonstop.Text = "stop";
            this.buttonstop.UseVisualStyleBackColor = true;
            this.buttonstop.Click += new System.EventHandler(this.buttonstop_Click);
            // 
            // comboBoxmethods
            // 
            this.comboBoxmethods.FormattingEnabled = true;
            this.comboBoxmethods.Location = new System.Drawing.Point(172, 46);
            this.comboBoxmethods.Name = "comboBoxmethods";
            this.comboBoxmethods.Size = new System.Drawing.Size(121, 20);
            this.comboBoxmethods.TabIndex = 2;
            // 
            // textBoxurl
            // 
            this.textBoxurl.Location = new System.Drawing.Point(172, 19);
            this.textBoxurl.Name = "textBoxurl";
            this.textBoxurl.Size = new System.Drawing.Size(199, 21);
            this.textBoxurl.TabIndex = 3;
            this.textBoxurl.Text = "localhost:8000";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 314);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(652, 349);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // textBoxeachthreadvolume
            // 
            this.textBoxeachthreadvolume.Location = new System.Drawing.Point(172, 72);
            this.textBoxeachthreadvolume.Name = "textBoxeachthreadvolume";
            this.textBoxeachthreadvolume.Size = new System.Drawing.Size(199, 21);
            this.textBoxeachthreadvolume.TabIndex = 5;
            this.textBoxeachthreadvolume.Text = "10";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "each thread volume";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "thread count";
            // 
            // textBoxthreadcount
            // 
            this.textBoxthreadcount.Location = new System.Drawing.Point(172, 99);
            this.textBoxthreadcount.Name = "textBoxthreadcount";
            this.textBoxthreadcount.Size = new System.Drawing.Size(199, 21);
            this.textBoxthreadcount.TabIndex = 7;
            this.textBoxthreadcount.Text = "2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "server";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "method";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "username";
            // 
            // textBoxusername
            // 
            this.textBoxusername.Location = new System.Drawing.Point(172, 126);
            this.textBoxusername.Name = "textBoxusername";
            this.textBoxusername.Size = new System.Drawing.Size(199, 21);
            this.textBoxusername.TabIndex = 11;
            this.textBoxusername.Text = "un11";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "type";
            // 
            // textBoxtype
            // 
            this.textBoxtype.Location = new System.Drawing.Point(172, 153);
            this.textBoxtype.Name = "textBoxtype";
            this.textBoxtype.Size = new System.Drawing.Size(199, 21);
            this.textBoxtype.TabIndex = 13;
            this.textBoxtype.Text = "123";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 183);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "county code";
            // 
            // textBoxcountcode
            // 
            this.textBoxcountcode.Location = new System.Drawing.Point(172, 180);
            this.textBoxcountcode.Name = "textBoxcountcode";
            this.textBoxcountcode.Size = new System.Drawing.Size(199, 21);
            this.textBoxcountcode.TabIndex = 15;
            this.textBoxcountcode.Text = "haiyang";
            // 
            // buttonstuff
            // 
            this.buttonstuff.Location = new System.Drawing.Point(496, 124);
            this.buttonstuff.Name = "buttonstuff";
            this.buttonstuff.Size = new System.Drawing.Size(136, 23);
            this.buttonstuff.TabIndex = 17;
            this.buttonstuff.Text = "stuff data";
            this.buttonstuff.UseVisualStyleBackColor = true;
            this.buttonstuff.Click += new System.EventHandler(this.buttonstuff_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 210);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 12);
            this.label8.TabIndex = 19;
            this.label8.Text = "data volume";
            // 
            // textBoxdatavolume
            // 
            this.textBoxdatavolume.Location = new System.Drawing.Point(172, 207);
            this.textBoxdatavolume.Name = "textBoxdatavolume";
            this.textBoxdatavolume.Size = new System.Drawing.Size(199, 21);
            this.textBoxdatavolume.TabIndex = 18;
            this.textBoxdatavolume.Text = "100000";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 237);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 12);
            this.label9.TabIndex = 21;
            this.label9.Text = "start ordinal";
            // 
            // textBoxordinal
            // 
            this.textBoxordinal.Location = new System.Drawing.Point(172, 234);
            this.textBoxordinal.Name = "textBoxordinal";
            this.textBoxordinal.Size = new System.Drawing.Size(199, 21);
            this.textBoxordinal.TabIndex = 20;
            this.textBoxordinal.Text = "45286";
            // 
            // buttonfiles
            // 
            this.buttonfiles.Location = new System.Drawing.Point(496, 156);
            this.buttonfiles.Name = "buttonfiles";
            this.buttonfiles.Size = new System.Drawing.Size(136, 23);
            this.buttonfiles.TabIndex = 22;
            this.buttonfiles.Text = "stuff files";
            this.buttonfiles.UseVisualStyleBackColor = true;
            this.buttonfiles.Click += new System.EventHandler(this.buttonfiles_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 264);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 12);
            this.label10.TabIndex = 24;
            this.label10.Text = "source file";
            // 
            // textBoxsourcefile
            // 
            this.textBoxsourcefile.Location = new System.Drawing.Point(172, 261);
            this.textBoxsourcefile.Name = "textBoxsourcefile";
            this.textBoxsourcefile.Size = new System.Drawing.Size(253, 21);
            this.textBoxsourcefile.TabIndex = 23;
            this.textBoxsourcefile.Text = "F:\\tools\\swift-pocket-reference.pdf";
            this.textBoxsourcefile.TextChanged += new System.EventHandler(this.textBoxsourcefile_TextChanged);
            // 
            // buttoncamera
            // 
            this.buttoncamera.Location = new System.Drawing.Point(39, 25);
            this.buttoncamera.Name = "buttoncamera";
            this.buttoncamera.Size = new System.Drawing.Size(136, 23);
            this.buttoncamera.TabIndex = 25;
            this.buttoncamera.Text = "connect";
            this.buttoncamera.UseVisualStyleBackColor = true;
            this.buttoncamera.Click += new System.EventHandler(this.buttoncamera_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxcameras);
            this.groupBox1.Controls.Add(this.buttoncameracapture);
            this.groupBox1.Controls.Add(this.buttoncameraclose);
            this.groupBox1.Controls.Add(this.buttoncamera);
            this.groupBox1.Location = new System.Drawing.Point(829, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 182);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "camera";
            // 
            // comboBoxcameras
            // 
            this.comboBoxcameras.FormattingEnabled = true;
            this.comboBoxcameras.Location = new System.Drawing.Point(39, 118);
            this.comboBoxcameras.Name = "comboBoxcameras";
            this.comboBoxcameras.Size = new System.Drawing.Size(175, 20);
            this.comboBoxcameras.TabIndex = 28;
            // 
            // buttoncameracapture
            // 
            this.buttoncameracapture.Location = new System.Drawing.Point(39, 89);
            this.buttoncameracapture.Name = "buttoncameracapture";
            this.buttoncameracapture.Size = new System.Drawing.Size(136, 23);
            this.buttoncameracapture.TabIndex = 27;
            this.buttoncameracapture.Text = "capture";
            this.buttoncameracapture.UseVisualStyleBackColor = true;
            this.buttoncameracapture.Click += new System.EventHandler(this.buttoncameracapture_Click);
            // 
            // buttoncameraclose
            // 
            this.buttoncameraclose.Location = new System.Drawing.Point(39, 59);
            this.buttoncameraclose.Name = "buttoncameraclose";
            this.buttoncameraclose.Size = new System.Drawing.Size(136, 23);
            this.buttoncameraclose.TabIndex = 26;
            this.buttoncameraclose.Text = "close";
            this.buttoncameraclose.UseVisualStyleBackColor = true;
            this.buttoncameraclose.Click += new System.EventHandler(this.buttoncameraclose_Click);
            // 
            // videoSourcePlayer
            // 
            this.videoSourcePlayer.Location = new System.Drawing.Point(683, 314);
            this.videoSourcePlayer.Name = "videoSourcePlayer";
            this.videoSourcePlayer.Size = new System.Drawing.Size(579, 314);
            this.videoSourcePlayer.TabIndex = 10;
            this.videoSourcePlayer.Text = "videoSourcePlayer";
            this.videoSourcePlayer.VideoSource = null;
            // 
            // Formtest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1310, 675);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBoxsourcefile);
            this.Controls.Add(this.buttonfiles);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxordinal);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxdatavolume);
            this.Controls.Add(this.buttonstuff);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxcountcode);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxtype);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxusername);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxthreadcount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxeachthreadvolume);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.textBoxurl);
            this.Controls.Add(this.comboBoxmethods);
            this.Controls.Add(this.buttonstop);
            this.Controls.Add(this.buttonstart);
            this.Controls.Add(this.videoSourcePlayer);
            this.Name = "Formtest";
            this.Text = "Stress and Performance Testing";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Formtest_FormClosing);
            this.Load += new System.EventHandler(this.Formtest_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonstart;
        private System.Windows.Forms.Button buttonstop;
        private System.Windows.Forms.ComboBox comboBoxmethods;
        private System.Windows.Forms.TextBox textBoxurl;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textBoxeachthreadvolume;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxthreadcount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxusername;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxtype;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxcountcode;
        private System.Windows.Forms.Button buttonstuff;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxdatavolume;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxordinal;
        private System.Windows.Forms.Button buttonfiles;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxsourcefile;
        private System.Windows.Forms.Button buttoncamera;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttoncameracapture;
        private System.Windows.Forms.Button buttoncameraclose;
        private System.Windows.Forms.ComboBox comboBoxcameras;
        private AForge.Controls.VideoSourcePlayer videoSourcePlayer;
    }
}

