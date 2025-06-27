using DVLD.Properties;
using DvldDataBusinessLayer;
using System;
using System.IO;
using System.Windows.Forms;

namespace DVLD
{
    public partial class ctrlPersonCard : UserControl
    {
        public ctrlPersonCard()
        {
            InitializeComponent();
        }


        private clsPeople _Person;

        // this is the solution of the teacher i cann't access from outside so i didn't how the teacher access from outside
        private int _PersonID = -1;

        public int PersonID
        {
            get { return _PersonID; }
        }

        //this use in futer which can access the property of the Person from this control if it is Exisit
        public clsPeople SelectedPersonInfo
        {
            get { return _Person; }
        }

        enum enGendor { Male = 0, Famle = 1 };

        private string _GenderName(byte Gendor)
        {
            return (Gendor == (int)enGendor.Male) ? "Male" : (Gendor == (int)enGendor.Famle) ? "Female" : "Unknow";
         
        }

        private void _LoadPersonImage()
        {
            if (_Person.Gendor == (int)enGendor.Male)
                pbPersonImage.ImageLocation = Resources.person_boy.ToString();

            if (_Person.Gendor == (int)enGendor.Famle)
                pbPersonImage.ImageLocation = Resources.person_woman.ToString();

            string ImagePath = _Person.ImagePath;
            if (ImagePath != "")
            {
                
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show($"Could Not Find this image: = {ImagePath}", "Error", MessageBoxButtons.OK);
            }
        }


        private void _FillPersonInfo()
        {
            llEditPersonInfo.Enabled = true;
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _Person.PersonID.ToString();
            lblName.Text = _Person.FullName;
            lblNationalNo.Text = _Person.NationalNo;
            lblGendor.Text = _GenderName(_Person.Gendor);            

            lblCountry.Text = _Person.CountryInfo.CountryName.ToString();

            lblEmail.Text = _Person.Email.ToString();
            lblAddress.Text = _Person.Address.ToString();
            lblDOB.Text = _Person.DateOfBirth.ToShortDateString();
            lblPhone.Text = _Person.Phone.ToString();
            _LoadPersonImage();
        }

        public void ResetPersonInfo()
        {
            lblPersonID.Text = "[????]";
            lblName.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblGendor.Text = "[????]";
            lblCountry.Text = "[????]";
            lblEmail.Text = "[????]";
            lblAddress.Text = "[????]";
            lblDOB.Text = "[????]";
            lblPhone.Text = "[????]";
        }
        public void LoadPersonInfo(int PersonID)
        {
          
            _Person = clsPeople.Find(PersonID);

            if (_Person == null)
            {
                //this teacher Do it 
                ResetPersonInfo();
                MessageBox.Show($"There is No Person With ID {PersonID}", "Error");
                return;
            }

            _FillPersonInfo();

           



        }


        public void LoadPersonInfo(string NationalNo)
        {

            _Person = clsPeople.Find(NationalNo);

            if (_Person == null)
            {
                //this teacher Do it 
                ResetPersonInfo();
                MessageBox.Show($"There is No NationalNO With NationalNo {NationalNo}", "Error");
                return;
            }

            _FillPersonInfo();



        }

     

        private void linlblEditPerson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_Person == null) return;
            frmAddUpdatePerson frmAddEditPersonInfo = new frmAddUpdatePerson(_PersonID);
            frmAddEditPersonInfo.ShowDialog();
            LoadPersonInfo(_PersonID);

        }
    }
}
