namespace DVLD
{
    partial class frmShowCarInfo
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
            this.ctrlCarCard1 = new DVLD.ctrlCarCard();
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ctrlCarCard1
            // 
            this.ctrlCarCard1.Location = new System.Drawing.Point(12, 46);
            this.ctrlCarCard1.Name = "ctrlCarCard1";
            this.ctrlCarCard1.Size = new System.Drawing.Size(753, 268);
            this.ctrlCarCard1.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(313, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(160, 31);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Car Details";
            // 
            // frmShowCarInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 322);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.ctrlCarCard1);
            this.Name = "frmShowCarInfo";
            this.Text = "Show Car Info";
            this.Load += new System.EventHandler(this.frmShowCarInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlCarCard ctrlCarCard1;
        private System.Windows.Forms.Label lblTitle;
    }
}