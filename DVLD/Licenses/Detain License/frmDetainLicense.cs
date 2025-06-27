using DvldDataBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DVLD
{
    public partial class frmDetainLicense : Form
    {


        clsDetainedLicensecs _detainedLicensecs;
        public frmDetainLicense()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int LicenseID = obj;

            if (LicenseID == -1)
            {
                MessageBox.Show($"Licese With ID: {LicenseID} Is Not Exist", "Not License");
                return;
            }

            llShowLicenseHistory.Enabled = (LicenseID != -1);

            lblLicenseID.Text = LicenseID.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.Username;
            
            

            //ToDo: make sure the license is not detained already.
            if (ctrlDriverLicenseInfoWithFilter1.LicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License i already detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtFineFees.Focus();

            btnDetain.Enabled = true;

        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserID.ToString();

        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
          
                e.Handled = (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar));

          
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFineFees.Text) ){
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Fees Con't be Empty");
            }
            else
                errorProvider1.SetError(txtFineFees, null);


            if (!clsValidatoin.IsNumber(txtFineFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Invalid Number.");
            }
            else
                errorProvider1.SetError(txtFineFees, null);


        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            MessageBox.Show("here error providrt not working ");
            if (!ValidateChildren())
                return;

            
            float FineFees = Convert.ToSingle(txtFineFees.Text.Trim());
            int _DetainID = ctrlDriverLicenseInfoWithFilter1.LicenseInfo.Detain(FineFees, clsGlobal.CurrentUser.UserID);

            if (_DetainID == -1)
            {
                MessageBox.Show("Faild to Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblDetainID.Text = _DetainID.ToString();
            MessageBox.Show("License Detained Successfully with ID=" + _DetainID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);


            btnDetain.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.EnableFilter = false;
            txtFineFees.Enabled = false;
            llShowLicenseInfo.Enabled = true;
        }

        //
        private void frmDetainLicense_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.FoucsOnLicensID();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(ctrlDriverLicenseInfoWithFilter1.LicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseHistory frm = new frmLicenseHistory(ctrlDriverLicenseInfoWithFilter1.LicenseInfo.ApplictionInfo.ApplicantPersonID);
            frm.ShowDialog();

        }
    }
}
