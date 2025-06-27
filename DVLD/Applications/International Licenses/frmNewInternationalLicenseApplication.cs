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
    public partial class frmNewInternationalLicenseApplication : Form
    {
        clsInternationalLicense InternationalLicense;
        private int _InternationalLicenseID = -1;
        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;

            if (SelectedLicenseID == -1)
                return;

            llShowLicenseInfo.Enabled = (SelectedLicenseID != -1);

            clsLicense license = clsLicense.GetLicenseInfoByID(SelectedLicenseID);

            if (license.LicenseClass != (int)clsLicenseClass.enLicenseClasse.Ordinary)
            {
                MessageBox.Show("Selected License Should be Class 3,Select Another One",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            int activeInternationalLicenseID = clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(license.DriverID) ;

            if (activeInternationalLicenseID != -1)
            {
                MessageBox.Show($"Person already have an active international License With ID = {activeInternationalLicenseID}","Not allowed"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowLicenseInfo.Enabled = true;
                _InternationalLicenseID = activeInternationalLicenseID;
                btnIssue.Enabled = false;
                return;
            }

            btnIssue.Enabled = true;
            ctrlDriverLicenseInfoWithFilter1.EnableFilter = false;


        }

        private void frmNewInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(1));
            lblFees.Text = clsApplicationType.Find((int)clsApplications.enApplicationTypeID.NewInternationalLicense).ApplicationFees.ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.Username;

        }

        private void frmNewInternationalLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.FoucsOnLicensID();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are Sure Want To Issue International License?", "Conform"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;


            InternationalLicense = new clsInternationalLicense();
            InternationalLicense.ApplicantPersonID = ctrlDriverLicenseInfoWithFilter1.LicenseInfo.ApplictionInfo.ApplicantPersonID;
            InternationalLicense.ApplicationDate = DateTime.Now;
            InternationalLicense.ApplicationTypeID = (int)clsApplications.enApplicationTypeID.NewInternationalLicense;
            InternationalLicense.ApplicationStatus = clsApplications.enApplicationStatus.Completed;
            InternationalLicense.LastStatusDate = DateTime.Now;
            InternationalLicense.PaidFees = clsApplicationType.Find((int)clsApplications.enApplicationTypeID.NewInternationalLicense).ApplicationFees;

            InternationalLicense.DriverID = ctrlDriverLicenseInfoWithFilter1.LicenseInfo.DriverID;
            InternationalLicense.IssuedUsingLocalLicenseID =ctrlDriverLicenseInfoWithFilter1.LicenseID;
            InternationalLicense.IssueDate= DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);
            InternationalLicense.IsActive = true;
            InternationalLicense.CreatedByUserID= clsGlobal.CurrentUser.UserID;
            


            if (!InternationalLicense.Save())
            {
                MessageBox.Show("Faild TO Issue International License", "Error"
                , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _InternationalLicenseID = InternationalLicense.InternationalLicenseID;
            lblILApplicationID.Text = InternationalLicense.ApplicationID.ToString();
            lblInternationlLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();

            MessageBox.Show($"International License Issued Successflly With ID ={InternationalLicense.InternationalLicenseID}", "License Issued"
               , MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssue.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.EnableFilter = false;
            llShowLicenseInfo.Enabled = true;


        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInternationalDriveInfo frm = new frmInternationalDriveInfo(_InternationalLicenseID);
            frm.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseHistory frm = new frmLicenseHistory(ctrlDriverLicenseInfoWithFilter1.LicenseInfo.ApplictionInfo.ApplicantPersonID);
            frm.ShowDialog();
        }
    }
}
