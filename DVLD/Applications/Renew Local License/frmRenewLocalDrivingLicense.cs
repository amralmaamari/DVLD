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
    public partial class frmRenewLocalDrivingLicense : Form
    {
        int _NewLicenseID = -1;
       
        public frmRenewLocalDrivingLicense()
        {
            InitializeComponent();
        }


        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
               int  SelectedLicenseID=obj;

            if (ctrlDriverLicenseInfoWithFilter1.LicenseID == -1)
                return;


             
               
               
            
              int ValidityYear = ctrlDriverLicenseInfoWithFilter1.LicenseInfo.LicenseInfo.DefaultValidityLength;
               
            ctrlApplicationNewLicenseInfo1.ValidityYear=ValidityYear;
            ctrlApplicationNewLicenseInfo1.FillAllInformation(SelectedLicenseID);

            
            
                

                

                if (!ctrlDriverLicenseInfoWithFilter1.LicenseInfo.IsLicenseExpired())
                {
                    MessageBox.Show($"Selected License is not yet expiared,it will expire on {clsFormat.DateToShort(ctrlDriverLicenseInfoWithFilter1.LicenseInfo.ExpirationDate)}",
                        "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   
                
                

                return;
                }
               

                if (!ctrlDriverLicenseInfoWithFilter1.LicenseInfo.IsActive)
                {
                    MessageBox.Show($"Selected License is Not Active,Choose an Active License.",
                       "Not Active License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   
                    return;
                }
            
            tabControl1.SelectTab("tabPage2");
            btnRenew.Enabled = true;
            btnNext.Enabled = true;






        }




        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure Want To Renew The License?", "Conform",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;


            string RenewLicenseNote = clsApplicationType.Find((int)clsLicense.enIssueReason.Renew).ApplicationTypeTitle;
            
            clsLicense NewLicense = ctrlDriverLicenseInfoWithFilter1.LicenseInfo.RenewLicense(RenewLicenseNote, clsGlobal.CurrentUser.UserID);

            if(NewLicense == null)
            {
                MessageBox.Show("Faild to Renew the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            ctrlApplicationNewLicenseInfo1.RLApplicationID = NewLicense.ApplicationID;
            _NewLicenseID = NewLicense.LicenseID;
            ctrlApplicationNewLicenseInfo1.RenewdLicenseID=NewLicense.LicenseID;

            MessageBox.Show("Licensed Renewed Successfully with ID=" + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
            

            btnRenew.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.Enabled = false;
            btnNext.Enabled = false;

            llShowLicenseInfo.Enabled=true;


        }

        private void frmRenewLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            btnNext.Enabled = false;
            btnRenew.Enabled = false;
           
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("tabPage2");
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_NewLicenseID);
            frm.ShowDialog();
        }

        private void frmRenewLocalDrivingLicense_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.FoucsOnLicensID();
        }
    }
}
