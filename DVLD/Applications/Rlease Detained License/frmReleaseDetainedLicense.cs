using DvldDataBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmReleaseDetainedLicense : Form
    {
        private clsDetainedLicensecs _detainedLicensecs;
        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
        }

        public frmReleaseDetainedLicense(int LicenseID)
        {
            InitializeComponent();
            ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(LicenseID);
            ctrlDriverLicenseInfoWithFilter1.EnableFilter = false;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReleaseDetainedLicense_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.FoucsOnLicensID();
        }

        

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int LicenseID = obj;

            if (LicenseID == -1)
                return;

            llShowLicenseHistory.Enabled = (LicenseID != -1);

            if (!ctrlDriverLicenseInfoWithFilter1.LicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License i is not detained, choose another one."
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;

            }
            lblLicenseID.Text = LicenseID.ToString();
           
             _detainedLicensecs= clsDetainedLicensecs.FindByLicenseID(LicenseID);
            lblDetainID.Text = _detainedLicensecs.DetainID.ToString();
            lblDetainDate.Text = clsFormat.DateToShort(_detainedLicensecs.DetainDate);
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplications.enApplicationTypeID.ReleaseDetainedDrivingLicsense).ApplicationFees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserID.ToString();
            lblFineFees.Text = ((int)_detainedLicensecs.FineFees).ToString();
            lblTotalFees.Text= ( Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblFineFees.Text) ).ToString();

            btnRelease.Enabled = true;
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are Sure Want To Release this detainted license?","Confirm",
                MessageBoxButtons.YesNo,MessageBoxIcon.Asterisk) == DialogResult.No) {
                return;
            }
            int ApplicationID = -1;

           if (!ctrlDriverLicenseInfoWithFilter1.LicenseInfo.ReleaseDetainedLicense(clsGlobal.CurrentUser.UserID, ref ApplicationID))
            {

                MessageBox.Show("Faild to Release Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

           lblApplicationID.Text = ApplicationID.ToString();
           MessageBox.Show("Detained License Released Successfully", "Detained License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRelease.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.EnableFilter = false;
            llShowLicenseInfo.Enabled = true ;

        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseHistory frm = new frmLicenseHistory(ctrlDriverLicenseInfoWithFilter1.LicenseInfo.ApplictionInfo.ApplicantPersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(ctrlDriverLicenseInfoWithFilter1.LicenseID);
            frm.ShowDialog();
        }
    }
}
