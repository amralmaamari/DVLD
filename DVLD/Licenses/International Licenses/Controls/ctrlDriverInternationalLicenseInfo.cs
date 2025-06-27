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
    public partial class ctrlDriverInternationalLicenseInfo : UserControl
    {

        clsInternationalLicense InternationalLicense;
        public ctrlDriverInternationalLicenseInfo()
        {
            InitializeComponent();
        }


        private void _LoadPersonImage()
        {
            if (InternationalLicense.DriverInfo.PersonInfo.Gendor == 0)
                pbPersonImage.Image = Properties.Resources.Male_512;
            else
                pbPersonImage.Image = Properties.Resources.Female_512;
            string ImagePath = InternationalLicense.PersonInfo.ImagePath;
            if (ImagePath != "")
            {
                if (System.IO.File.Exists(ImagePath))
                    pbPersonImage.Load(ImagePath);
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }
            public void LoadLicenseCardInfo(int InternationalLicenseID)
        {
            InternationalLicense = clsInternationalLicense.Find(InternationalLicenseID);
            if (InternationalLicense == null)
            {
                MessageBox.Show($"Could Not find International License License ID {InternationalLicense} ", "No International License", MessageBoxButtons.OK,
                  MessageBoxIcon.Error);
                return;
            }


            lblName.Text = InternationalLicense.ApplicantFullName;
            lblIntLicenseID.Text= InternationalLicense.InternationalLicenseID.ToString();
            lblLicenseID.Text=InternationalLicense.IssuedUsingLocalLicenseID.ToString();
           lblNationalNo.Text = InternationalLicense.DriverInfo.PersonInfo.NationalNo;
            lblGendor.Text = (InternationalLicense.DriverInfo.PersonInfo.Gendor == 0)?"Male":"Famle";
            lblIssueDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblApplicationID.Text=InternationalLicense.ApplicationID.ToString();
            lblIsActive.Text=(InternationalLicense.IsActive)?"Yes":"No";
            lblDateOfBirth.Text= clsFormat.DateToShort (InternationalLicense.PersonInfo.DateOfBirth);
            lblDriverID.Text = InternationalLicense.DriverID.ToString();
            lblExpirationDate.Text=clsFormat.DateToShort(InternationalLicense.ExpirationDate);
            _LoadPersonImage();

            MessageBox.Show(InternationalLicense.PersonInfo.FullName);






        }
    }
}
