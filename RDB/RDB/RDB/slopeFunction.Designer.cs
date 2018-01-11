namespace RDB
{
    partial class slopeFunction
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
            this.btn_SlopeOK = new System.Windows.Forms.Button();
            this.txb_SlopeZfactor = new System.Windows.Forms.TextBox();
            this.label_SlopeZfactor = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_SlopeOK
            // 
            this.btn_SlopeOK.Location = new System.Drawing.Point(244, 263);
            this.btn_SlopeOK.Margin = new System.Windows.Forms.Padding(4);
            this.btn_SlopeOK.Name = "btn_SlopeOK";
            this.btn_SlopeOK.Size = new System.Drawing.Size(100, 29);
            this.btn_SlopeOK.TabIndex = 0;
            this.btn_SlopeOK.Text = "Finish";
            this.btn_SlopeOK.UseVisualStyleBackColor = true;
            this.btn_SlopeOK.Click += new System.EventHandler(this.btn_SlopeOK_Click);
            // 
            // txb_SlopeZfactor
            // 
            this.txb_SlopeZfactor.Location = new System.Drawing.Point(91, 110);
            this.txb_SlopeZfactor.Margin = new System.Windows.Forms.Padding(4);
            this.txb_SlopeZfactor.Name = "txb_SlopeZfactor";
            this.txb_SlopeZfactor.Size = new System.Drawing.Size(178, 25);
            this.txb_SlopeZfactor.TabIndex = 1;
            // 
            // label_SlopeZfactor
            // 
            this.label_SlopeZfactor.AutoSize = true;
            this.label_SlopeZfactor.Location = new System.Drawing.Point(16, 116);
            this.label_SlopeZfactor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_SlopeZfactor.Name = "label_SlopeZfactor";
            this.label_SlopeZfactor.Size = new System.Drawing.Size(63, 15);
            this.label_SlopeZfactor.TabIndex = 2;
            this.label_SlopeZfactor.Text = "Zfactor";
            // 
            // slopeFunction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 311);
            this.Controls.Add(this.label_SlopeZfactor);
            this.Controls.Add(this.txb_SlopeZfactor);
            this.Controls.Add(this.btn_SlopeOK);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "slopeFunction";
            this.Text = "slopeFunction";
            this.Load += new System.EventHandler(this.slopeFunction_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_SlopeOK;
        private System.Windows.Forms.TextBox txb_SlopeZfactor;
        private System.Windows.Forms.Label label_SlopeZfactor;
    }
}