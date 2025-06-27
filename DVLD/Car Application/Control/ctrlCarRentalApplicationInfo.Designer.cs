namespace DVLD
{
    partial class ctrlCarRentalApplicationInfo
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ctrlRentalApplicationCard1 = new DVLD.ctrlRentalApplicationCard();
            this.ctrlCarRentalApplicationCard1 = new DVLD.ctrlCarRentalApplicationCard();
            this.SuspendLayout();
            // 
            // ctrlRentalApplicationCard1
            // 
            this.ctrlRentalApplicationCard1.Location = new System.Drawing.Point(3, 161);
            this.ctrlRentalApplicationCard1.Name = "ctrlRentalApplicationCard1";
            this.ctrlRentalApplicationCard1.Size = new System.Drawing.Size(714, 265);
            this.ctrlRentalApplicationCard1.TabIndex = 1;
            // 
            // ctrlCarRentalApplicationCard1
            // 
            this.ctrlCarRentalApplicationCard1.Location = new System.Drawing.Point(3, 3);
            this.ctrlCarRentalApplicationCard1.Name = "ctrlCarRentalApplicationCard1";
            this.ctrlCarRentalApplicationCard1.Size = new System.Drawing.Size(714, 161);
            this.ctrlCarRentalApplicationCard1.TabIndex = 0;
            // 
            // ctrlCarRentalApplicationInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctrlRentalApplicationCard1);
            this.Controls.Add(this.ctrlCarRentalApplicationCard1);
            this.Name = "ctrlCarRentalApplicationInfo";
            this.Size = new System.Drawing.Size(712, 433);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlCarRentalApplicationCard ctrlCarRentalApplicationCard1;
        private ctrlRentalApplicationCard ctrlRentalApplicationCard1;
    }
}
