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
    public partial class ctrlUserCard : UserControl
    {




        private clsUser _User;

        // i don't know what is this for in Teacher Use
        private int _UserID = -1;
        public int UserID
        {
            get { return this._UserID; }
        }

        public ctrlUserCard()
        {
            InitializeComponent();
          
        }

        private void _ResetUserInfo()
        {
            ctrlPersonCard1.ResetPersonInfo();
            lblUserID.Text = string.Empty;
            lblUserName.Text = string.Empty;
            lblIsActive.Text = string.Empty;
        }

        private void _FillUserInfo() {
            ctrlPersonCard1.LoadPersonInfo(_User.PersonID);
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.Username.ToString();

            lblIsActive.Text = (_User.IsActive == true) ? "Yes" : "No";

        }
        public void LoadUserInfo(int UserID)
        {
            _UserID = UserID;
          _User=clsUser.FindByUserID(UserID);

            if(_User == null) {
                _ResetUserInfo();
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
           _FillUserInfo();

        }

     


        }
}
