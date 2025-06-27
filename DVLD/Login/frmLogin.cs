using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using DVLD.Global_Classes;
using DvldDataBusinessLayer;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD
{
    public partial class frmLogin : Form
    {


       private  clsUser _User;


      



        public frmLogin()
        {
            InitializeComponent();
        }

       
        private void _ResetFiled()
        {
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtUsername.Focus();

        }

       



        private void frmLogin_Load(object sender, EventArgs e)
        {
            
            _ResetFiled();
            string Username = "";
            string Password = "";
            if (clsGlobal.GetStoredCredentialFromRegistry(ref Username, ref Password))
            {
                txtUsername.Text = Username;
                txtPassword.Text = Password;
                chkRememberMe.Checked = true;
            }
            else
                chkRememberMe.Checked = false;

        }

        private void _ValidateFiled(object sender, CancelEventArgs e)
        {
            if (((TextBox)sender).Text.Trim() == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(((TextBox)sender), "Enter Correct Value");
            }
            else
                errorProvider1.SetError(((TextBox)sender), null);
        }



    

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
                return;


        
            _User = clsUser.FindByUsernameAndPassword(txtUsername.Text.Trim(), txtPassword.Text.Trim());

            if (_User != null)
            {

                if (_User.IsActive == false)
                {
                    txtUsername.Focus();
                    MessageBox.Show("Your Account is Not Active Please Contact Your Branch", "Not Active", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                if (chkRememberMe.Checked == true)
                {
                    clsGlobal.RememberUsernameAndPasswordToRegistry(txtUsername.Text.Trim(), txtPassword.Text.Trim());
                }
                else
                {
                    clsGlobal.RememberUsernameAndPasswordToRegistry("", "");
                }
                clsGlobal.CurrentUser = _User;
                clsLogging.AddNewLogging(this.Name,"Successfuly Enter", clsLogging.enLevel.Warning);
               
                this.Hide();
                frmMain frm = new frmMain(this);
                frm.ShowDialog();


            }
            else { 
            
            
                MessageBox.Show("Null Object", "Wrong Credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsLogging.AddNewLogging(this.Name, "Not Login Null Object", clsLogging.enLevel.Warning);
                
                _ResetFiled();
                return;
            
            
            
            }

          



           






        }
    }
}
