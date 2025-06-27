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
    public partial class ctrlCustomerCard : UserControl
    {

        private int _CustomerID = -1;
        private clsCustomer _Customer;
        public ctrlCustomerCard()
        {
            InitializeComponent();
        }


        public void ResetCustomerCardInfo()
        {

            llViewPersonInfo.Enabled = false;
            lblName.Text ="[????]";
            lblCreatedByUser.Text ="[????]";
            lblPersonID.Text ="[????]";
            lblLicenseID.Text ="[????]";
            lblCustomerID.Text ="[????]";
            lblIsActive.Text ="[????]";
        }
        private void _FillData()
        {
            llViewPersonInfo.Enabled = true;
            lblName.Text = _Customer.LiceseInfo.ApplictionInfo.ApplicantFullName;
            lblCreatedByUser.Text=_Customer.CreatedByUserID.ToString();
            lblPersonID.Text = _Customer.LiceseInfo.ApplictionInfo.ApplicantPersonID.ToString();
            lblLicenseID.Text = _Customer.LicenseID.ToString();
            lblCustomerID.Text = _CustomerID.ToString();
            lblIsActive.Text = (_Customer.IsActive) ? "Yes" : "No";

        }

        public void LoadCustomerInfo(int CustomerID )
        {
            _CustomerID=CustomerID;

            _Customer=clsCustomer.GetCustomerInfoByID(CustomerID);

            if( _Customer == null )
            {
                MessageBox.Show("No Customer with ID = " + _CustomerID, "Error",
                 MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                return;           
              }


            _FillData();


        }

        private void linlblViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo(_Customer.LiceseInfo.ApplictionInfo.ApplicantPersonID);
            frm.ShowDialog();
            LoadCustomerInfo(_CustomerID );

        }
    }
}
