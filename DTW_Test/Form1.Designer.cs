namespace DTW_Test
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.checkBoxIsNumeric = new System.Windows.Forms.CheckBox();
            this.textBoxDistance = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSubject = new System.Windows.Forms.TextBox();
            this.textBoxOriginal = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pictureBoxCorrView = new System.Windows.Forms.PictureBox();
            this.pictureBoxMatchingFieldView = new System.Windows.Forms.PictureBox();
            this.checkBoxArray = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCorrView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMatchingFieldView)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxArray);
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxIsNumeric);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxDistance);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxSubject);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxOriginal);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(775, 480);
            this.splitContainer1.SplitterDistance = 239;
            this.splitContainer1.TabIndex = 0;
            // 
            // checkBoxIsNumeric
            // 
            this.checkBoxIsNumeric.AutoSize = true;
            this.checkBoxIsNumeric.Location = new System.Drawing.Point(14, 154);
            this.checkBoxIsNumeric.Name = "checkBoxIsNumeric";
            this.checkBoxIsNumeric.Size = new System.Drawing.Size(117, 16);
            this.checkBoxIsNumeric.TabIndex = 8;
            this.checkBoxIsNumeric.Text = "数値としてマッチング";
            this.checkBoxIsNumeric.UseVisualStyleBackColor = true;
            // 
            // textBoxDistance
            // 
            this.textBoxDistance.Location = new System.Drawing.Point(79, 306);
            this.textBoxDistance.Name = "textBoxDistance";
            this.textBoxDistance.ReadOnly = true;
            this.textBoxDistance.Size = new System.Drawing.Size(100, 19);
            this.textBoxDistance.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 309);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "distance =";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 252);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(213, 48);
            this.button1.TabIndex = 5;
            this.button1.Text = "DP Matching";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "データをカンマ区切りで入力\r\n配列の配列にする場合は「[」で区切る";
            // 
            // textBoxSubject
            // 
            this.textBoxSubject.Location = new System.Drawing.Point(14, 86);
            this.textBoxSubject.Name = "textBoxSubject";
            this.textBoxSubject.Size = new System.Drawing.Size(211, 19);
            this.textBoxSubject.TabIndex = 3;
            // 
            // textBoxOriginal
            // 
            this.textBoxOriginal.Location = new System.Drawing.Point(15, 29);
            this.textBoxOriginal.Name = "textBoxOriginal";
            this.textBoxOriginal.Size = new System.Drawing.Size(210, 19);
            this.textBoxOriginal.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "比較対象データ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "比較元データ";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.AutoScroll = true;
            this.splitContainer2.Panel1.Controls.Add(this.pictureBoxCorrView);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.AutoScroll = true;
            this.splitContainer2.Panel2.Controls.Add(this.pictureBoxMatchingFieldView);
            this.splitContainer2.Size = new System.Drawing.Size(531, 480);
            this.splitContainer2.SplitterDistance = 241;
            this.splitContainer2.TabIndex = 0;
            // 
            // pictureBoxCorrView
            // 
            this.pictureBoxCorrView.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pictureBoxCorrView.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxCorrView.Name = "pictureBoxCorrView";
            this.pictureBoxCorrView.Size = new System.Drawing.Size(522, 233);
            this.pictureBoxCorrView.TabIndex = 0;
            this.pictureBoxCorrView.TabStop = false;
            this.pictureBoxCorrView.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxCorrView_Paint_1);
            // 
            // pictureBoxMatchingFieldView
            // 
            this.pictureBoxMatchingFieldView.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pictureBoxMatchingFieldView.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxMatchingFieldView.Name = "pictureBoxMatchingFieldView";
            this.pictureBoxMatchingFieldView.Size = new System.Drawing.Size(522, 225);
            this.pictureBoxMatchingFieldView.TabIndex = 0;
            this.pictureBoxMatchingFieldView.TabStop = false;
            this.pictureBoxMatchingFieldView.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxMatchingFieldView_Paint_1);
            // 
            // checkBoxArray
            // 
            this.checkBoxArray.AutoSize = true;
            this.checkBoxArray.Location = new System.Drawing.Point(14, 176);
            this.checkBoxArray.Name = "checkBoxArray";
            this.checkBoxArray.Size = new System.Drawing.Size(135, 16);
            this.checkBoxArray.TabIndex = 9;
            this.checkBoxArray.Text = "配列の配列でマッチング";
            this.checkBoxArray.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 480);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "DTW Test";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCorrView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMatchingFieldView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox textBoxDistance;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSubject;
        private System.Windows.Forms.TextBox textBoxOriginal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.CheckBox checkBoxIsNumeric;
        private System.Windows.Forms.PictureBox pictureBoxCorrView;
        private System.Windows.Forms.PictureBox pictureBoxMatchingFieldView;
        private System.Windows.Forms.CheckBox checkBoxArray;
    }
}

