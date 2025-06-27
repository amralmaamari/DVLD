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
using System.Text.RegularExpressions;

namespace DVLD
{
    public partial class frmAddUpdatePerson : Form
    {

        public delegate void DataBackEvenHandler(object sender, int PersonID);
        public event DataBackEvenHandler DataBack;
        private int _PersonID;



        enum enMode { AddNew = 0, Update = 1 }
        enum enGendor { Male=0,Famle=1};
        enMode _Mode = enMode.AddNew;

        clsPeople _Person;

        public frmAddUpdatePerson()
        {
            InitializeComponent();

           _Mode = enMode.AddNew;


        }

        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
            _Mode = enMode.Update;


        }


    




  
        private void rdbFamle_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Properties.Resources.person_woman;
        }

        private void rdbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null)
                pbPersonImage.Image = Properties.Resources.person_boy;
        }



        

        

        

      



        private byte _CheckGenderChoice()
        {
            if (rdbMale.Checked)
                return (int)enGendor.Male;
            else
                return (int)enGendor.Famle;
        }




        private void _AddPerosn()
        {
            int NationalityCountryID = clsCountry.Find(cmbCountry.Text).CountryID; 

            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.NationalNo = txtNationalNO.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.Gendor = _CheckGenderChoice();
            _Person.NationalityCountryID = NationalityCountryID;
            _Person.Address = txtAddress.Text;

            if(pbPersonImage.ImageLocation != null)
            _Person.ImagePath = pbPersonImage.ImageLocation.ToString();
            else
                _Person.ImagePath = "";

            //Teacher Not Use it 
      




        }

        private bool _HandlePersonImage()
        {
          
            //if i update the image will delete this will work in update adn delete image only
            if(_Person.ImagePath != pbPersonImage.ImageLocation)
            {
                if(_Person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {
                    }
                }
            }


            if(pbPersonImage.ImageLocation != null)
            {
                string SourceImageFile=pbPersonImage.ImageLocation.ToString();
                if(clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                {
                    pbPersonImage.ImageLocation = SourceImageFile;
                    return true;
                }
                {
                    MessageBox.Show("Error Coping Image File", "Error", MessageBoxButtons.OK);
                    return false;
                }
            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            //this is bult-in function will check all Validation Function by it self
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Fileds are not valide!,put the mouse over the red icon to see the error");
                return;
            }

            //Next Lesson
            if (!_HandlePersonImage())
                return;

            _AddPerosn();



            if (_Person.Save())
            {
                lblPersonID.Text = _Person.PersonID.ToString();
                lblTitle.Text = "Update";
                MessageBox.Show("Data Saved Successfully", "Saved");

                //Teacher Use it  whne add new person will return the ID
                DataBack?.Invoke(this, _Person.PersonID);
            }
            else
            {
                MessageBox.Show("Error: Data NOT Saved Successfully", "Not Saved");

            }



            //Teacher he didn't do it

            //_Mode = enMode.Update;
            //lblPersonID.Text = _Person.PersonID.ToString();

        }




        private void _CheckOnlyString(TextBox textBox)
        {
            if (!Regex.IsMatch(textBox.Text, @"^\D*$"))
            {
                MessageBox.Show("Please enter Characters only.");
                textBox.Clear();

            }

        }




        private void NameCheckr_TextChanged(object sender, EventArgs e)
        {

            _CheckOnlyString((TextBox)sender);


        }

        private void _FillCountriesInComboBox()
        {
            DataTable dataCountries = clsCountry.GetAllCountries();
            foreach (DataRow row in dataCountries.Rows)
            {
                cmbCountry.Items.Add(row["CountryName"]);
            }
         
        }

        //this mine before see the teacher _LoadData after _ResetDefualtValues
        private void _LoadData()
        {




            _Person = clsPeople.Find(_PersonID);
            if (_Person == null)
            {
                MessageBox.Show("This form will be closed because No Person with ID = " + _PersonID,"Person Not Found");
                this.Close();

                return;
            }




            lblPersonID.Text = _PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtNationalNO.Text = _Person.NationalNo;
            dtpDateOfBirth.Value = _Person.DateOfBirth;

            if (_Person.Gendor == (int)enGendor.Male)
                rdbMale.Checked = true;
            else
                rdbFamle.Checked = true;


            txtPhone.Text = _Person.Phone;
            txtEmail.Text = _Person.Email;
            cmbCountry.SelectedIndex = cmbCountry.FindString(clsCountry.Find(_Person.NationalityCountryID).CountryName) ;
            txtAddress.Text = _Person.Address;



            if(_Person.ImagePath != ""){

             pbPersonImage.ImageLocation = _Person.ImagePath;
            }

            linlblRemoveImage.Visible = (_Person.ImagePath != "");






        }
        
        private void _ResetDefualtValues()
        {
            _FillCountriesInComboBox();



            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Person";
                _Person = new clsPeople();
            }
            else
                lblTitle.Text = "Update Person";


            if (rdbMale.Checked)
                pbPersonImage.Image = Properties.Resources.person_boy;
            else
                pbPersonImage.Image = Properties.Resources.person_woman;


            linlblRemoveImage.Visible = (pbPersonImage.Location != null);

            dtpDateOfBirth.MaxDate = DateTime.Now.Date.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            dtpDateOfBirth.MinDate = DateTime.Now.Date.AddYears(-100);

            cmbCountry.SelectedIndex = cmbCountry.FindString("Yemen");




            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNO.Text = "";
            rdbMale.Checked = true;
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";





       
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Form Parentform = this.FindForm();
            if (ParentForm != null)
            {
                Parentform.Close();
            }
        }



        private void linlblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files (*.png; *.jpg; *.jpeg)|*.png;*.jpg;*.jpeg";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                string SelectFilePath = openFileDialog1.FileName;
                pbPersonImage.Load(SelectFilePath);
                linlblRemoveImage.Visible = true;


            }


        }

        private void linlblRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;


            if (File.Exists(_Person.ImagePath))
                File.Delete(_Person.ImagePath);

            if (rdbMale.Checked == true)
                pbPersonImage.Image = Properties.Resources.person_boy;
            else
                pbPersonImage.Image = Properties.Resources.person_woman;



            linlblRemoveImage.Visible = false;
        }

        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if(_Mode == enMode.Update)
                _LoadData();
        }


        
        private void _ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox Temp = ((TextBox)sender);

            if (string.IsNullOrWhiteSpace(Temp.Text.Trim()))
            {
                // it mean will not make leve from the filed e.Cancel=true;
                e.Cancel = true;
                errPro1.SetError(Temp, "This filed is required!");

            }
            else
            {
                errPro1.SetError(Temp, null);
            }


        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text.Trim() == "")
                return;

            if (!clsValidatoin.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errPro1.SetError(txtEmail, "Invalide Email Address Format!");
            }
            else
            {
                errPro1.SetError(txtEmail, null);
            }

        }


        private void txtNationalNO_Validating(object sender, CancelEventArgs e)
        {
           
            if (string.IsNullOrEmpty(txtNationalNO.Text.Trim()))
            {
                e.Cancel=true;
                errPro1.SetError(txtNationalNO, "This Filed Is Requried!");
                return;
            }
            else
            {
                errPro1.SetError(txtNationalNO, null);
            }


         //only check for other National Number and for the update it allow to update with same national no
        if (txtNationalNO.Text.Trim() != _Person.NationalNo && clsPeople.IsPersonExist(txtNationalNO.Text.Trim()))
        {
            e.Cancel = true;
        errPro1.SetError(txtNationalNO, "National Number is used for another person!");
        }

        else
            errPro1.SetError(txtNationalNO, null);
            
        }

        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
          
            if (!clsValidatoin.ValidatePhone(txtPhone.Text.Trim()))
            {
                e.Cancel = true;
                errPro1.SetError(txtPhone, "Invalide Phone Number Format!");
            }
            else
            {
                errPro1.SetError(txtPhone, null);

            }
        }





    
    }
}
