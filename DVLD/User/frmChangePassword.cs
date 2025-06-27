using DvldDataBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmChangePassword : Form
    {
        private clsUser _User;


        private int _UserID;


        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            this._UserID = UserID;
            
            
        }

        private void _ResetDefualtValues()
        {
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtCurrentPassword.Focus();
        }
        




        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            _User = clsUser.FindByUserID(_UserID);
            if(_User == null )
            {
                MessageBox.Show($"No User Witth ID ,{_User}","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
                return;
            }
            ctrlUserInfocs1.LoadUserInfo(_User.UserID);

           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                 "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

          

                _User.Password= txtNewPassword.Text.Trim();
            if (_User.Save())
            {
                MessageBox.Show("Password Changed Successfully.",
                  "Saved.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _ResetDefualtValues();

            }
            else
            {
                MessageBox.Show("An Erro Occured, Password did not change.",
                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtCurrentPassword.Text.Trim()))
            {

                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Password cannot be blank");


            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, "");

            }

           
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim() ))
            {

                e.Cancel = true;

                errorProvider1.SetError(txtNewPassword, "New Password cannot be blank");


            }
            else
            {
                errorProvider1.SetError(txtNewPassword, "");

            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
            {

                e.Cancel = true;

                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match New Password!");


            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, "");

            }
        }
    }
}
