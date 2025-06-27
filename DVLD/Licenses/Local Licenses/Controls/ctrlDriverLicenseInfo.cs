using DVLD.Properties;
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
using System.IO;

namespace DVLD
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
        private int _LicenseID =-1;
        private clsLicense _License;

        public int LicenseID
        {
            get { return _LicenseID; } 

        }
        public clsLicense SelectedLicenseInfo
        {
            get { return _License; }

        }
        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }

        private void _LoadPersonImage()
        {
            if (_License.ApplictionInfo.PersonInfo.Gendor == 0)
            {
                pbPersonImage.Image = Resources.Male_512;
            }
            else
            {
                pbPersonImage.Image = Resources.Female_512;
            }

            string ImagePath = _License.ApplictionInfo.PersonInfo.ImagePath;
            if (ImagePath != "")
            {
                if(File.Exists(ImagePath))
                {
                    pbPersonImage.Load(ImagePath);
                }
                else
                {
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void _LoadLicense(clsLicense license)
        {
            lblClass.Text = license.LicenseInfo.ClassName;
            lblName.Text = license.ApplictionInfo.ApplicantFullName;
            lblLicenseID.Text = license.LicenseID.ToString();
            lblNationalNo.Text = license.ApplictionInfo.PersonInfo.NationalNo;
            lblGendor.Text = (license.ApplictionInfo.PersonInfo.Gendor==0?"Male":"Famle");
            lblIssueDate.Text= clsFormat.DateToShort(license.IssueDate);
            lblIssueReason.Text=license.IssueReasonText;
            lblNotes.Text = (string.IsNullOrEmpty(license.Notes) ? "Not Notes" : license.Notes.ToString());
            lblIsActive.Text = ( (license.IsActive == true) ? "Yes" : "No");
            lblDateOfBirth.Text = clsFormat.DateToShort(license.ApplictionInfo.PersonInfo.DateOfBirth);
            lblDriverID.Text=license.DriverID.ToString();
            lblExpirationDate.Text = clsFormat.DateToShort( license.ExpirationDate);
            lbllDetained.Text = (license.IsDetained ? "Yes": "No");
            _LoadPersonImage();
        }

        public void LoadLicenseCardInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            _License=clsLicense.GetLicenseInfoByID(_LicenseID);
            

            
          



            

            if (_License == null)
            {
                MessageBox.Show("There is No License  ", "No License", MessageBoxButtons.OK,
                  MessageBoxIcon.Error);
                _LicenseID = -1;
                return;
            }

            _LoadLicense(_License);










        }
        private void ctrlDriverLicenseInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
