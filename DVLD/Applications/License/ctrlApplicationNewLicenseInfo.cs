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
    public partial class ctrlApplicationNewLicenseInfo : UserControl
    {

        int _LicenseID;
        clsLicense _LicenseInfo;

        public int _RLApplicationID { get; set; }
        public int RLApplicationID {
            set{

                _RLApplicationID = value;

                if (_RLApplicationID != -1)
                {
                    lblRLApplicationID.Text=_RLApplicationID.ToString();
                }
            }
        }


        public int _RenewdLicenseID { get; set; }
        public int RenewdLicenseID
        {
            set
            {

                _RenewdLicenseID = value;

                if (_RenewdLicenseID != -1)
                {
                    lblRenewdLicenseID.Text = _RenewdLicenseID.ToString();
                }
            }
        }



        public int ValidityYear { get; set; }
    
        public ctrlApplicationNewLicenseInfo()
        {
            InitializeComponent();
        }
        public  void ResetControlValue()
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = clsFormat.DateToShort(DateTime.Now);
            
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(ValidityYear));
            lblCreatedBy.Text = clsGlobal.CurrentUser.Username.ToString();
        }

       public void FillAllInformation(int LicenseID)
        {

            _LicenseInfo = clsLicense.GetLicenseInfoByID(LicenseID);

            if(_LicenseInfo == null) {
                return;
            }
            // lblRenewdLicenseID.Text = _LicenseInfo.LicenseID.ToString();

            ResetControlValue();

            lblApplicationFees.Text = ((int)clsApplicationType.Find((int)clsApplications.enApplicationTypeID.RenewDrivingLicense).ApplicationFees).ToString();
            lblLicenseFees.Text= clsLicenseClass.Find((int)_LicenseInfo.LicenseInfo.LicenseClassID).ClassFees.ToString(); 
            lblOldLicenseID.Text=  LicenseID.ToString();

           
            lblTotalFees.Text= (int.Parse(lblApplicationFees.Text) + int.Parse(lblLicenseFees.Text)).ToString();
            txtNotes.Text = clsLicense.GetLicenseInfoByID(_LicenseInfo.LicenseID).Notes.ToString();

        }
    }
}
