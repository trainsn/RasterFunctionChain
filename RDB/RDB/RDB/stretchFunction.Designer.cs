namespace RDB
{
    partial class stretchFunction
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
            this.stxb_num = new System.Windows.Forms.TextBox();
            this.txb_NumberOfStandardDeviations = new System.Windows.Forms.TextBox();
            this.cmb_StretchType = new System.Windows.Forms.ComboBox();
            this.stxb_min = new System.Windows.Forms.TextBox();
            this.stxb_max = new System.Windows.Forms.TextBox();
            this.txb_min = new System.Windows.Forms.TextBox();
            this.txb_max = new System.Windows.Forms.TextBox();
            this.btn_Stretch = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // stxb_num
            // 
            this.stxb_num.BackColor = System.Drawing.SystemColors.Menu;
            this.stxb_num.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.stxb_num.Font = new System.Drawing.Font("宋体", 11F);
            this.stxb_num.Location = new System.Drawing.Point(172, 160);
            this.stxb_num.Name = "stxb_num";
            this.stxb_num.ReadOnly = true;
            this.stxb_num.Size = new System.Drawing.Size(26, 34);
            this.stxb_num.TabIndex = 6;
            this.stxb_num.Text = "n";
            this.stxb_num.Visible = false;
            // 
            // txb_NumberOfStandardDeviations
            // 
            this.txb_NumberOfStandardDeviations.Location = new System.Drawing.Point(254, 160);
            this.txb_NumberOfStandardDeviations.Name = "txb_NumberOfStandardDeviations";
            this.txb_NumberOfStandardDeviations.Size = new System.Drawing.Size(211, 35);
            this.txb_NumberOfStandardDeviations.TabIndex = 7;
            this.txb_NumberOfStandardDeviations.Visible = false;
            // 
            // cmb_StretchType
            // 
            this.cmb_StretchType.FormattingEnabled = true;
            this.cmb_StretchType.Location = new System.Drawing.Point(254, 103);
            this.cmb_StretchType.Name = "cmb_StretchType";
            this.cmb_StretchType.Size = new System.Drawing.Size(320, 32);
            this.cmb_StretchType.TabIndex = 8;
            this.cmb_StretchType.SelectedIndexChanged += new System.EventHandler(this.cmb_StretchType_SelectedIndexChanged);
            // 
            // stxb_min
            // 
            this.stxb_min.BackColor = System.Drawing.SystemColors.Menu;
            this.stxb_min.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.stxb_min.Font = new System.Drawing.Font("宋体", 9F);
            this.stxb_min.Location = new System.Drawing.Point(68, 160);
            this.stxb_min.Name = "stxb_min";
            this.stxb_min.ReadOnly = true;
            this.stxb_min.Size = new System.Drawing.Size(123, 28);
            this.stxb_min.TabIndex = 9;
            this.stxb_min.Text = "MinPercent";
            this.stxb_min.Visible = false;
            // 
            // stxb_max
            // 
            this.stxb_max.BackColor = System.Drawing.SystemColors.Menu;
            this.stxb_max.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.stxb_max.Font = new System.Drawing.Font("宋体", 9F);
            this.stxb_max.Location = new System.Drawing.Point(307, 160);
            this.stxb_max.Name = "stxb_max";
            this.stxb_max.ReadOnly = true;
            this.stxb_max.Size = new System.Drawing.Size(133, 28);
            this.stxb_max.TabIndex = 10;
            this.stxb_max.Text = "MaxPercent";
            this.stxb_max.Visible = false;
            // 
            // txb_min
            // 
            this.txb_min.Location = new System.Drawing.Point(204, 153);
            this.txb_min.Name = "txb_min";
            this.txb_min.Size = new System.Drawing.Size(84, 35);
            this.txb_min.TabIndex = 11;
            this.txb_min.Visible = false;
            // 
            // txb_max
            // 
            this.txb_max.Location = new System.Drawing.Point(446, 153);
            this.txb_max.Name = "txb_max";
            this.txb_max.Size = new System.Drawing.Size(84, 35);
            this.txb_max.TabIndex = 12;
            this.txb_max.Visible = false;
            // 
            // btn_Stretch
            // 
            this.btn_Stretch.Location = new System.Drawing.Point(523, 398);
            this.btn_Stretch.Name = "btn_Stretch";
            this.btn_Stretch.Size = new System.Drawing.Size(124, 49);
            this.btn_Stretch.TabIndex = 13;
            this.btn_Stretch.Text = "Finish";
            this.btn_Stretch.UseVisualStyleBackColor = true;
            this.btn_Stretch.Click += new System.EventHandler(this.btn_Stretch_Click);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(68, 103);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(174, 28);
            this.textBox2.TabIndex = 14;
            this.textBox2.Text = "Stretch Type";
            // 
            // stretchFunction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 472);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.btn_Stretch);
            this.Controls.Add(this.txb_max);
            this.Controls.Add(this.txb_min);
            this.Controls.Add(this.stxb_max);
            this.Controls.Add(this.stxb_min);
            this.Controls.Add(this.cmb_StretchType);
            this.Controls.Add(this.txb_NumberOfStandardDeviations);
            this.Controls.Add(this.stxb_num);
            this.Name = "stretchFunction";
            this.Text = "StretchFunction Definition";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox stxb_num;
        private System.Windows.Forms.TextBox txb_NumberOfStandardDeviations;
        private System.Windows.Forms.ComboBox cmb_StretchType;
        private System.Windows.Forms.TextBox stxb_min;
        private System.Windows.Forms.TextBox stxb_max;
        private System.Windows.Forms.TextBox txb_min;
        private System.Windows.Forms.TextBox txb_max;
        private System.Windows.Forms.Button btn_Stretch;
        private System.Windows.Forms.TextBox textBox2;
    }
}