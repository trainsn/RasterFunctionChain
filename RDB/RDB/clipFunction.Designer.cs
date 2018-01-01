namespace RDB
{
    partial class clipFunction
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
            this.label1 = new System.Windows.Forms.Label();
            this.txb_inputfeatherlayer = new System.Windows.Forms.TextBox();
            this.btn_inputclick2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择裁剪所依据的矢量图层";
            // 
            // txb_inputfeatherlayer
            // 
            this.txb_inputfeatherlayer.Location = new System.Drawing.Point(25, 55);
            this.txb_inputfeatherlayer.Name = "txb_inputfeatherlayer";
            this.txb_inputfeatherlayer.Size = new System.Drawing.Size(100, 21);
            this.txb_inputfeatherlayer.TabIndex = 2;
            this.txb_inputfeatherlayer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txb_inputfeatherlayer_MouseDown);
            // 
            // btn_inputclick2
            // 
            this.btn_inputclick2.Location = new System.Drawing.Point(163, 53);
            this.btn_inputclick2.Name = "btn_inputclick2";
            this.btn_inputclick2.Size = new System.Drawing.Size(75, 23);
            this.btn_inputclick2.TabIndex = 3;
            this.btn_inputclick2.Text = "确认";
            this.btn_inputclick2.UseVisualStyleBackColor = true;
            this.btn_inputclick2.Click += new System.EventHandler(this.btn_inputclick2_Click);
            // 
            // clipFunction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 97);
            this.Controls.Add(this.btn_inputclick2);
            this.Controls.Add(this.txb_inputfeatherlayer);
            this.Controls.Add(this.label1);
            this.Name = "clipFunction";
            this.Text = "clipFunction";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txb_inputfeatherlayer;
        private System.Windows.Forms.Button btn_inputclick2;
    }
}