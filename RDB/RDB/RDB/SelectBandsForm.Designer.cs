namespace RDB
{
    partial class SelectBandsForm
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
            this.btn_DrawCompareHistogram = new System.Windows.Forms.Button();
            this.cklb_CompareHistogram = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // btn_DrawCompareHistogram
            // 
            this.btn_DrawCompareHistogram.Location = new System.Drawing.Point(327, 478);
            this.btn_DrawCompareHistogram.Name = "btn_DrawCompareHistogram";
            this.btn_DrawCompareHistogram.Size = new System.Drawing.Size(118, 44);
            this.btn_DrawCompareHistogram.TabIndex = 0;
            this.btn_DrawCompareHistogram.Text = "绘制";
            this.btn_DrawCompareHistogram.UseVisualStyleBackColor = true;
            this.btn_DrawCompareHistogram.Click += new System.EventHandler(this.btn_DrawCompareHistogram_Click);
            // 
            // cklb_CompareHistogram
            // 
            this.cklb_CompareHistogram.FormattingEnabled = true;
            this.cklb_CompareHistogram.Location = new System.Drawing.Point(103, 37);
            this.cklb_CompareHistogram.Name = "cklb_CompareHistogram";
            this.cklb_CompareHistogram.Size = new System.Drawing.Size(569, 424);
            this.cklb_CompareHistogram.TabIndex = 1;
            // 
            // SelectBandsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 543);
            this.Controls.Add(this.cklb_CompareHistogram);
            this.Controls.Add(this.btn_DrawCompareHistogram);
            this.Name = "SelectBandsForm";
            this.Text = "SelectBandsForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_DrawCompareHistogram;
        private System.Windows.Forms.CheckedListBox cklb_CompareHistogram;
    }
}