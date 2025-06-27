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
    public partial class ctrlCarRentalApplicationCard : UserControl
    {

        private int _CarRentalApplicationID;
        private clsCarRentalApplication _CarRentalApplication;

        public int RentalApplicationID
        {
            get => _CarRentalApplication.RentalApplicationID;
        }
        public ctrlCarRentalApplicationCard()
        {
            InitializeComponent();
        }

        public void ResetCarRentalApplicationInfo()
        {
            _CarRentalApplicationID = -1;
            lblCarRentalApplicationID.Text = "[????]";
            lblIsRentActive.Text = "[????]";          
            lblShowLicenseInfo.Enabled = false;
        }


        private string _CheckedIfRentIsActive()
        {
            return (_CarRentalApplication.IsActive ? "Active" : "NoActive");
        }
        private void _FillData()
        {
            lblShowLicenseInfo.Enabled = true;
            lblCarRentalApplicationID.Text = _CarRentalApplication.CarRentalApplicationID.ToString();
            lblIsRentActive.Text = _CheckedIfRentIsActive();
        }

        public void LoadCarRentalApplicationInfo(int CarRentalApplicationID)
        {
            _CarRentalApplication = clsCarRentalApplication.FindByCarRentalApplicationID(CarRentalApplicationID);
            if (_CarRentalApplication == null)
            {
                MessageBox.Show($"There is no Car Reantl Application with ID={CarRentalApplicationID}",
                    "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _CarRentalApplicationID = -1;
                return;
            }

            _CarRentalApplicationID = CarRentalApplicationID;

            _FillData();
        }

        private void lblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int LicenseID = _CarRentalApplication.CustomerInfo.LicenseID;
            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
            frm.ShowDialog();
            LoadCarRentalApplicationInfo(_CarRentalApplicationID );

        }
    }
}
