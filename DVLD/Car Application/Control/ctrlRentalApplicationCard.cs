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
    public partial class ctrlRentalApplicationCard : UserControl
    {

        private int _RentalApplicationID;
        private clsRentalApplication _RentalApplication;
        public ctrlRentalApplicationCard()
        {
            InitializeComponent();
        }

        public void ResetRentalApplicationInfo()
        {
            _RentalApplicationID = -1;

            lblRentalApplicationID.Text = "[????]";
            lblCardID.Text = "[????]";
            lblStatus.Text = "[????]";
            lblFees.Text = "[????]";
            lblType.Text = "[????]";
            lblCustomer.Text = "[????]";
            lblStartDate.Text = "[????]";
            lblEndDate.Text = "[????]";
            lblLocation.Text = "[????]";
            lblCreatedByUser.Text = "[????]";
            llViewCustomerInfo.Enabled = false;
        }

        private void _FillData()
        {
            llViewCustomerInfo.Enabled = true;
            lblRentalApplicationID.Text = _RentalApplication.RentalApplicationID.ToString();
            lblCardID.Text = _RentalApplication.CarID.ToString();
            lblStatus.Text = _RentalApplication.RentStatusName;
            lblFees.Text = _RentalApplication.PaidFees.ToString();
            lblType.Text = _RentalApplication.RentTypeInfo.RentTypeName;
            lblCustomer.Text = _RentalApplication.CustomerInfo.LiceseInfo.ApplictionInfo.ApplicantFullName;
            lblStartDate.Text=clsFormat.DateToShort (_RentalApplication.StartDateTime);
            lblEndDate.Text= clsFormat.DateToShort(_RentalApplication.EndDateTime);
            lblCreatedByUser.Text=_RentalApplication.CreatedByUserID.ToString();
            lblLocation.Text = _RentalApplication.CurrentLocation;


        }

        public void LoadRentalApplicationInfo(int RentalApplicationID)
        {
             _RentalApplication= clsRentalApplication.FindByRentalApplicationInfoByID(RentalApplicationID);
            if (_RentalApplication == null)
            {
                MessageBox.Show($"There is no Reantl Application with ID={RentalApplicationID}",
                    "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _RentalApplicationID = -1;
                return;
            }

            _RentalApplicationID = RentalApplicationID;

            _FillData();
        }

        private void linlblViewCustomerInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowCustomerInfo frm = new frmShowCustomerInfo(_RentalApplication.CustomerID);
            frm.ShowDialog();
            LoadRentalApplicationInfo(_RentalApplicationID);
        }
    }
}
