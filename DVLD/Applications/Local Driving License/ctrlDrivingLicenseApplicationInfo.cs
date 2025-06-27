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
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {
       private  clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication ;
        private int _LocalDrivingLicenseApplicationID = -1 ;
        private int _LicenseID = -1 ;

        public int LocalDrivingLicenseApplicationID
        {
            get { return _LocalDrivingLicenseApplicationID; }
        }


        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }


     
        public void _ResetLocalDrivingLicenseApplicationInfo()
        {
            _LocalDrivingLicenseApplicationID = -1;
            ctrlApplicationBasicInfo1.ResetApplicationInfo();
            lblLocalDrivingLicenseApplicationID.Text = "[????]";
            lblLicenseName.Text = "[????]";
            lblPassedTests.Text = "0/3";
        }
 
        private void _FillLocalDrivingLicenseApplicationInfo()
        {
            _LicenseID = _LocalDrivingLicenseApplication.LicenseClassID;

            //incase there is license enable the show link.
            lblShowLicenseInfo.Enabled = (_LicenseID != -1);

            lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblLicenseName.Text =  clsLicenseClass.Find(_LocalDrivingLicenseApplication.LicenseClassID).ClassName;
            lblPassedTests.Text = $"{_LocalDrivingLicenseApplication.GetPassedTestCount()} / 3";
            ctrlApplicationBasicInfo1.LoadApplicationInfo(_LocalDrivingLicenseApplication.ApplicationID);

        }

  


        

     


     


        public bool LoadApplicationInfoByLocalDrivingAppID(int LocalDrivingLicenseApplicationID)
        {

            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                _ResetLocalDrivingLicenseApplicationInfo();
                MessageBox.Show("No Application with Local Driving License ApplicationID = " + LocalDrivingLicenseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            _FillLocalDrivingLicenseApplicationInfo();

            return true;
          
        }


        public bool LoadApplicationInfoByApplicationID(int ApplicationID)
        {

            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByApplicationID(ApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                _ResetLocalDrivingLicenseApplicationInfo();
                MessageBox.Show("No Application with ApplicationID = " + ApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            _FillLocalDrivingLicenseApplicationInfo();

            return true;

        }

        
    }
}
