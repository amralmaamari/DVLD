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
    public partial class frmAddUpdateUser : Form
    {
        enum enMode { Update = 0, AddNew = 1 };
        enMode _Mode; 
        private int UserID=-1;
        clsUser _User;

        public frmAddUpdateUser()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            this.UserID = UserID;
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnNext_Click(object sender, EventArgs e)
        {

            if(_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpLoginInfo.Enabled = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                return;
            }

            //incase of add new mode.
            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {

                if (clsUser.IsUserExistForPersonID(ctrlPersonCardWithFilter1.PersonID))
                {
                    MessageBox.Show("Selected Person already has a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter1.FilterFoucs();
                }
                else {
                    btnSave.Enabled = true;
                    tpLoginInfo.Enabled = true;

                    tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                }

            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFoucs();
            }
            
            //if(clsUser.IsUserExistForPersonID(PersonID) && (_Mode == enMode.AddNew))
            //if(clsUser.IsUserExistForPersonID(PersonID) && (_Mode == enMode.AddNew))
            //{
            //    MessageBox.Show("Selected Person already has a user,choose another one.", "Select Another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

         
            //tcUserInfo.SelectedTab = tpLoginInfo;
            //btnSave.Enabled = true;

           
        }


        private void ValidatePassword_Validating(object sender, CancelEventArgs e)
        {
            if (((TextBox)sender).Text.Trim() == "")
            {
                e.Cancel = true;
                errorProvider1.SetError((TextBox)sender, "Enter Password");

            }
            else
                errorProvider1.SetError((TextBox)sender, "");






        }


        private void FillUser()
        {
            _User.PersonID=ctrlPersonCardWithFilter1.PersonID;
           _User.Username = txtUserName.Text.Trim();
           _User.Password = txtPassword.Text.Trim();
           _User.IsActive = cbIsActive.Checked;



        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {

            if (clsUser.IsUserExist(txtUserName.Text.Trim()) && txtUserName.Text.Trim() != _User.Username)
            {
              
                errorProvider1.SetError(txtUserName, "Already Exsist, Enter Another User");
            }
            else
            {
                errorProvider1.SetError(txtUserName, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

         
            if (!ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

      
           

            FillUser();


            if (_User.Save())
            {
                lblUserID.Text = _User.UserID.ToString();
                _Mode = enMode.Update;
                lblTitle.Text = "Update User";
                this.Text= "Update User";
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }


        private void _LoadData()
        {
            
            _User=clsUser.FindByUserID(UserID);
           ctrlPersonCardWithFilter1.FilterEnable = false;


            if (_User == null)
            {
                MessageBox.Show("No User with ID = " + _User, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

   


           lblUserID.Text= _User.UserID.ToString();
           txtUserName.Text=_User.Username;
           txtPassword.Text=_User.Password;
           txtConfirmPassword.Text=_User.Password;
            cbIsActive.Checked = _User.IsActive;
           ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);


        }
        private void _ResetDefualtValues()
        {
            if(_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New User";
                this.Text= "Add New User";
                _User = new clsUser();
                tpLoginInfo.Enabled = false;
                ctrlPersonCardWithFilter1.FilterFoucs();
            }
            else
            {
                lblTitle.Text = "Update User";
                this.Text = "Update User";
                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;

            }
            lblUserID.Text = string.Empty;
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            cbIsActive.Checked = true;



        }
        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            //the sane person cann't have more than one user 
            if (_Mode ==enMode.Update)
                    _LoadData();


        }

        private void txtUserName_Validating_1(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true;

                errorProvider1.SetError(txtUserName, "Username cannot be blank");
            }
            else
            {
                errorProvider1.SetError(txtUserName, null);
            };

            if(_Mode == enMode.AddNew)
            {
                if (clsUser.IsUserExist(txtUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "username is used by another user");
                }
                else
                {
                    errorProvider1.SetError(txtUserName, null);
                };
            }
            else
            {
                if(_User.Username != txtUserName.Text.Trim())
                {
                    if (clsUser.IsUserExist(txtUserName.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txtUserName, "username is used by another user");
                        return;
                    }
                    else
                    {
                        errorProvider1.SetError(txtUserName, null);
                    };
                }

            }
           
        
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                e.Cancel = true;

                errorProvider1.SetError(txtPassword, "Password cannot be blank");
            }
            else
            {
                errorProvider1.SetError(txtPassword, null);
            };
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                e.Cancel = true;

                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match Password!");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            };

          
        }

        private void frmAddUpdateUser_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFoucs();
        }
    }
}
