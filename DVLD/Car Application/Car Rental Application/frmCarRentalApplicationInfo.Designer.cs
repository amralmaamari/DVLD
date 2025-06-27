namespace DVLD
{
    partial class frmCarRentalApplicationInfo
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
            this.ctrlCarRentalApplicationInfo1 = new DVLD.ctrlCarRentalApplicationInfo();
            this.SuspendLayout();
            // 
            // ctrlCarRentalApplicationInfo1
            // 
            this.ctrlCarRentalApplicationInfo1.Location = new System.Drawing.Point(6, 9);
            this.ctrlCarRentalApplicationInfo1.Name = "ctrlCarRentalApplicationInfo1";
            this.ctrlCarRentalApplicationInfo1.Size = new System.Drawing.Size(709, 425);
            this.ctrlCarRentalApplicationInfo1.TabIndex = 0;
            // 
            // frmCarRentalApplicationInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 438);
            this.Controls.Add(this.ctrlCarRentalApplicationInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmCarRentalApplicationInfo";
            this.Text = "Car Rental Application Info";
            this.Load += new System.EventHandler(this.frmTry_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlCarRentalApplicationInfo ctrlCarRentalApplicationInfo1;
    }
}