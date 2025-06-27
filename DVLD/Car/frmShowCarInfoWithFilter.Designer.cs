namespace DVLD
{
    partial class frmShowCarInfoWithFilter
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
            this.ctrlCarCardWithFilter1 = new DVLD.ctrlCarCardWithFilter();
            this.SuspendLayout();
            // 
            // ctrlCarCardWithFilter1
            // 
            this.ctrlCarCardWithFilter1.EnableFilter = true;
            this.ctrlCarCardWithFilter1.Location = new System.Drawing.Point(1, 6);
            this.ctrlCarCardWithFilter1.Name = "ctrlCarCardWithFilter1";
            this.ctrlCarCardWithFilter1.Size = new System.Drawing.Size(761, 361);
            this.ctrlCarCardWithFilter1.TabIndex = 0;
            // 
            // frmShowCarInfoWithFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 372);
            this.Controls.Add(this.ctrlCarCardWithFilter1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmShowCarInfoWithFilter";
            this.Text = "Show Car Info With Filter";
            this.Load += new System.EventHandler(this.frmShowCarInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlCarCardWithFilter ctrlCarCardWithFilter1;
    }
}