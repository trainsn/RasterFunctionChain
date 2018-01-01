namespace RDB
{
    partial class ndviFunction
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
            this.cmb_redBandindex = new System.Windows.Forms.ComboBox();
            this.cmb_infraredBandindex = new System.Windows.Forms.ComboBox();
            this.btn_init = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cmb_redBandindex
            // 
            this.cmb_redBandindex.FormattingEnabled = true;
            this.cmb_redBandindex.Location = new System.Drawing.Point(259, 91);
            this.cmb_redBandindex.Name = "cmb_redBandindex";
            this.cmb_redBandindex.Size = new System.Drawing.Size(220, 32);
            this.cmb_redBandindex.TabIndex = 5;
            // 
            // cmb_infraredBandindex
            // 
            this.cmb_infraredBandindex.FormattingEnabled = true;
            this.cmb_infraredBandindex.Location = new System.Drawing.Point(259, 143);
            this.cmb_infraredBandindex.Name = "cmb_infraredBandindex";
            this.cmb_infraredBandindex.Size = new System.Drawing.Size(220, 32);
            this.cmb_infraredBandindex.TabIndex = 6;
            // 
            // btn_init
            // 
            this.btn_init.Location = new System.Drawing.Point(450, 303);
            this.btn_init.Name = "btn_init";
            this.btn_init.Size = new System.Drawing.Size(113, 49);
            this.btn_init.TabIndex = 7;
            this.btn_init.Text = "Finish";
            this.btn_init.UseVisualStyleBackColor = true;
            this.btn_init.Click += new System.EventHandler(this.btn_init_Click);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(69, 146);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(174, 28);
            this.textBox2.TabIndex = 8;
            this.textBox2.Text = "InfraredBandID";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(69, 91);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(174, 28);
            this.textBox1.TabIndex = 9;
            this.textBox1.Text = "VisibleBandID";
            // 
            // ndviFunction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 374);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.btn_init);
            this.Controls.Add(this.cmb_infraredBandindex);
            this.Controls.Add(this.cmb_redBandindex);
            this.Name = "ndviFunction";
            this.Text = "NDVIFunction Definition";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_redBandindex;
        private System.Windows.Forms.ComboBox cmb_infraredBandindex;
        private System.Windows.Forms.Button btn_init;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
    }
}