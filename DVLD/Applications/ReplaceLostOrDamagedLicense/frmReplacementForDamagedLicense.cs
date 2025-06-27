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
using static DvldDataBusinessLayer.clsLicense;

namespace DVLD
{
    public partial class frmReplacementForDamagedLicense : Form
    {

        private int _NewLicenseID = -1;



       

        public frmReplacementForDamagedLicense()
        {
            InitializeComponent();
        }

        private int _GetApplicationTypeID()
        {
            if (rbDamagedLicense.Checked)
                return (int)clsApplications.enApplicationTypeID.ReplaceDamagedDrivingLicense;
            else
                return (int)clsApplications.enApplicationTypeID.ReplaceLostDrivingLicense;

        }
        private enIssueReason _GetIssueReasonID()
        {
            if (rbDamagedLicense.Checked)
            {
                return clsLicense.enIssueReason.DamagedReplacement;

            }else
                return clsLicense.enIssueReason.LostReplacement;
           

           
        }

        private void frmReplacementForDamagedLicense_Load(object sender, EventArgs e)
        {
            rbDamagedLicense.Checked = true;
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;
            llShowLicenseHistory.Enabled=(SelectedLicenseID != -1);

            if (SelectedLicenseID == -1)
                return;

        

            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblOldLicenseID.Text = ctrlDriverLicenseInfoWithFilter1.LicenseID.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.Username;
            lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationTypeID()).ApplicationFees.ToString();



            if (ctrlDriverLicenseInfoWithFilter1.LicenseInfo.IsLicenseExpired())
            {
                MessageBox.Show($"Selected License is expiared,Choose an Vaild License",
                       "Not Vaild", MessageBoxButtons.OK, MessageBoxIcon.Error);




                return;
            }

            if (!ctrlDriverLicenseInfoWithFilter1.LicenseInfo.IsActive)
            {
                MessageBox.Show($"Selected License is Not Active,Choose an Active License.",
                       "Not Active License", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            btnIssueReplacement.Enabled = true;


        }

        private void frmReplacementForDamagedLicense_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.FoucsOnLicensID();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_NewLicenseID);
            frm.ShowDialog();
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure Want To Replace a Replacement for the License?", "Conform",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

           
            clsLicense NewLicense = ctrlDriverLicenseInfoWithFilter1.LicenseInfo.Replace(_GetIssueReasonID(), clsGlobal.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Replace the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            _NewLicenseID = NewLicense.LicenseID;

            lblLRAppllicationID.Text = NewLicense.ApplicationID.ToString();
            lblReplacedLicenseID.Text = NewLicense.LicenseID.ToString();


            MessageBox.Show("Licensed Replace Successfully with ID=" + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);


            gbApplicationInfoForLicenseReplacement.Enabled = false;
            btnIssueReplacement.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.Enabled = false;
            llShowLicenseInfo.Enabled = true;
            gbReplacementFor.Enabled = false;



        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "Replacement for Damaged License";
            this.Text = lblTitle.Text;
            lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationTypeID()).ApplicationFees.ToString();

        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "Replacement for Lost License";
            this.Text = lblTitle.Text;
            lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationTypeID()).ApplicationFees.ToString();

        }


    }
}
