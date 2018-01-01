namespace RDB
{
    partial class hillshadeFunction
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
            this.txb_inputazimuth = new System.Windows.Forms.TextBox();
            this.btn_inputclick = new System.Windows.Forms.Button();
            this.txb_inputzfactor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txb_inputazimuth
            // 
            this.txb_inputazimuth.Location = new System.Drawing.Point(12, 58);
            this.txb_inputazimuth.Name = "txb_inputazimuth";
            this.txb_inputazimuth.Size = new System.Drawing.Size(160, 21);
            this.txb_inputazimuth.TabIndex = 0;
            // 
            // btn_inputclick
            // 
            this.btn_inputclick.Location = new System.Drawing.Point(97, 184);
            this.btn_inputclick.Name = "btn_inputclick";
            this.btn_inputclick.Size = new System.Drawing.Size(75, 23);
            this.btn_inputclick.TabIndex = 1;
            this.btn_inputclick.Text = "确定";
            this.btn_inputclick.UseVisualStyleBackColor = true;
            this.btn_inputclick.Click += new System.EventHandler(this.btn_inputclick_Click);
            // 
            // txb_inputzfactor
            // 
            this.txb_inputzfactor.Location = new System.Drawing.Point(12, 135);
            this.txb_inputzfactor.Name = "txb_inputzfactor";
            this.txb_inputzfactor.Size = new System.Drawing.Size(160, 21);
            this.txb_inputzfactor.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Azimuth(Default 50)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Z-factor（Default 1/11111.0)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "请输入参数";
            // 
            // hillshadeFunction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(188, 252);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txb_inputzfactor);
            this.Controls.Add(this.btn_inputclick);
            this.Controls.Add(this.txb_inputazimuth);
            this.Name = "hillshadeFunction";
            this.Text = "hillshadeFunction";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txb_inputazimuth;
        private System.Windows.Forms.Button btn_inputclick;
        private System.Windows.Forms.TextBox txb_inputzfactor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}