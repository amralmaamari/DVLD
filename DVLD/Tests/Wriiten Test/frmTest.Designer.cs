namespace DVLD
{
    partial class frmWrittenTest
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblQustionCount = new System.Windows.Forms.Label();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.rbOptionA = new System.Windows.Forms.RadioButton();
            this.rbOptionB = new System.Windows.Forms.RadioButton();
            this.rbOptionC = new System.Windows.Forms.RadioButton();
            this.rbOptionD = new System.Windows.Forms.RadioButton();
            this.lblLevel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(321, 145);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(165, 31);
            this.lblTitle.TabIndex = 13;
            this.lblTitle.Text = "WrittenTest";
            // 
            // lblQustionCount
            // 
            this.lblQustionCount.AutoSize = true;
            this.lblQustionCount.BackColor = System.Drawing.SystemColors.Control;
            this.lblQustionCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQustionCount.ForeColor = System.Drawing.Color.Black;
            this.lblQustionCount.Location = new System.Drawing.Point(650, 461);
            this.lblQustionCount.Name = "lblQustionCount";
            this.lblQustionCount.Size = new System.Drawing.Size(140, 24);
            this.lblQustionCount.TabIndex = 14;
            this.lblQustionCount.Text = "Qustion 1 of 10.";
            // 
            // lblQuestion
            // 
            this.lblQuestion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(45)))), ((int)(((byte)(61)))));
            this.lblQuestion.Location = new System.Drawing.Point(11, 221);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(788, 63);
            this.lblQuestion.TabIndex = 15;
            this.lblQuestion.Text = "When pedestrians are crossing the road near a pedestrian crossing, you should";
            // 
            // rbOptionA
            // 
            this.rbOptionA.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.rbOptionA.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbOptionA.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbOptionA.ForeColor = System.Drawing.Color.White;
            this.rbOptionA.Location = new System.Drawing.Point(15, 283);
            this.rbOptionA.Name = "rbOptionA";
            this.rbOptionA.Size = new System.Drawing.Size(775, 39);
            this.rbOptionA.TabIndex = 16;
            this.rbOptionA.TabStop = true;
            this.rbOptionA.Tag = "1";
            this.rbOptionA.Text = "Slow down, sound the horn, and then continue";
            this.rbOptionA.UseVisualStyleBackColor = false;
            // 
            // rbOptionB
            // 
            this.rbOptionB.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.rbOptionB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbOptionB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbOptionB.ForeColor = System.Drawing.Color.White;
            this.rbOptionB.Location = new System.Drawing.Point(15, 326);
            this.rbOptionB.Name = "rbOptionB";
            this.rbOptionB.Size = new System.Drawing.Size(775, 39);
            this.rbOptionB.TabIndex = 22;
            this.rbOptionB.TabStop = true;
            this.rbOptionB.Tag = "2";
            this.rbOptionB.Text = "Sound the horn, and then continue";
            this.rbOptionB.UseVisualStyleBackColor = false;
            // 
            // rbOptionC
            // 
            this.rbOptionC.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.rbOptionC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbOptionC.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbOptionC.ForeColor = System.Drawing.Color.White;
            this.rbOptionC.Location = new System.Drawing.Point(15, 368);
            this.rbOptionC.Name = "rbOptionC";
            this.rbOptionC.Size = new System.Drawing.Size(775, 39);
            this.rbOptionC.TabIndex = 24;
            this.rbOptionC.TabStop = true;
            this.rbOptionC.Tag = "3";
            this.rbOptionC.Text = "Stop the vehicle and wait for pedestrians to cross the road before continuing";
            this.rbOptionC.UseVisualStyleBackColor = false;
            // 
            // rbOptionD
            // 
            this.rbOptionD.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.rbOptionD.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbOptionD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbOptionD.ForeColor = System.Drawing.Color.White;
            this.rbOptionD.Location = new System.Drawing.Point(15, 411);
            this.rbOptionD.Name = "rbOptionD";
            this.rbOptionD.Size = new System.Drawing.Size(775, 39);
            this.rbOptionD.TabIndex = 25;
            this.rbOptionD.TabStop = true;
            this.rbOptionD.Tag = "4";
            this.rbOptionD.Text = "All Above";
            this.rbOptionD.UseVisualStyleBackColor = false;
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLevel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblLevel.Location = new System.Drawing.Point(153, 163);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(111, 24);
            this.lblLevel.TabIndex = 26;
            this.lblLevel.Text = "Level Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(12, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 24);
            this.label2.TabIndex = 27;
            this.label2.Text = "Qustion Level:";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSubmit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSubmit.Enabled = false;
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Image = global::DVLD.Properties.Resources.Submit_321;
            this.btnSubmit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSubmit.Location = new System.Drawing.Point(16, 456);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(109, 36);
            this.btnSubmit.TabIndex = 76;
            this.btnSubmit.Text = "     Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Visible = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnNext
            // 
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNext.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Image = global::DVLD.Properties.Resources.next;
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNext.Location = new System.Drawing.Point(16, 456);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(109, 36);
            this.btnNext.TabIndex = 75;
            this.btnNext.Text = "     Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox1.Image = global::DVLD.Properties.Resources.exam512;
            this.pictureBox1.Location = new System.Drawing.Point(290, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(225, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // frmWrittenTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 498);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.rbOptionD);
            this.Controls.Add(this.rbOptionC);
            this.Controls.Add(this.rbOptionB);
            this.Controls.Add(this.rbOptionA);
            this.Controls.Add(this.lblQuestion);
            this.Controls.Add(this.lblQustionCount);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmWrittenTest";
            this.Text = "Written Test";
            this.Load += new System.EventHandler(this.frmTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblQustionCount;
        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.RadioButton rbOptionA;
        private System.Windows.Forms.RadioButton rbOptionB;
        private System.Windows.Forms.RadioButton rbOptionC;
        private System.Windows.Forms.RadioButton rbOptionD;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnSubmit;
    }
}