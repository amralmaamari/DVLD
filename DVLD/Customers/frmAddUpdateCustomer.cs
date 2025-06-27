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
    public partial class frmAddUpdateCustomer : Form
    {
        enum enMode { AddNew=0, Update=1};
        enMode Mode= enMode.AddNew;

        private clsCustomer _Customer;
        private int LicenseID = -1;
        private int _CustomerID = -1;

        public frmAddUpdateCustomer()
        {
            InitializeComponent();
            Mode = enMode.AddNew;

        }

        public frmAddUpdateCustomer(int CustomerID)
        {
            InitializeComponent();
            this._CustomerID = CustomerID;
            Mode = enMode.Update;

        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int LicenseID = obj;

            if (LicenseID == -1)
                return;



                    if (!ctrlDriverLicenseInfoWithFilter1.LicenseInfo.IsActive)
                    {
                        MessageBox.Show($"Customer Con't Add Because License Is Not Activate"
                           , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                   


                    if (Mode == enMode.AddNew)
                    {

                        if (clsCustomer.IsThereCustomerActiveByLicenseID(LicenseID))
                        {
                            MessageBox.Show($"The License Has ALready Active Customer"
                               , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                         _Customer.LicenseID = ctrlDriverLicenseInfoWithFilter1.LicenseID;
                        _Customer.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                         btnSave.Enabled = true;
                    }

                
            

           



            // here will carete Customers
            

           








        }

        private void frmAddUpdateCustomer_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.FoucsOnLicensID();
        
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            

            if (!_Customer.Save())
            {
                MessageBox.Show($"Customer Not Save"
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            ctrlDriverLicenseInfoWithFilter1.EnableFilter = false;
            MessageBox.Show($"Customer Add Save Successfully ID:{_Customer.CustomerID}"
                  , "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnSave.Enabled = false;
            lblCustomerID.Text = _Customer.CustomerID.ToString();
        }

        private void _ResetDefaultValue()
        {

            if (Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Customer";
                this.Name = lblTitle.Text;
                _Customer = new clsCustomer();
                ctrlDriverLicenseInfoWithFilter1.FoucsOnLicensID();
            }
            else
            {
                lblTitle.Text = "Update  Customer";
                this.Name = lblTitle.Text;                
            }

        }

  

        private void _loadData()
        {
            gbCustomerActivate.Visible = true;
            _Customer = clsCustomer.GetCustomerInfoByID(_CustomerID);


            if (_Customer == null)
            {
                MessageBox.Show("This form will be closed because No Customer with ID = " + _CustomerID,"Error",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
                return;
            }


            lblCustomerID.Text = _CustomerID.ToString();
            ctrlDriverLicenseInfoWithFilter1.LoadLicenseInfo(_Customer.LicenseID);
            ctrlDriverLicenseInfoWithFilter1.EnableFilter = false;

            if (_Customer.IsActive == false)
            {
                gbCustomerActivate.Enabled = false;
                rbDeActivate.Checked = true;               
                btnSave.Enabled = false;
                return;
            }

            if (_Customer.IsThereCustomerActiveByLicenseID())
            {
                gbCustomerActivate.Enabled = true;
                rbActivate.Checked = true;
                _Customer.IsActive = true;
                btnSave.Enabled = true;
            }



          
        }
        private void frmAddUpdateCustomer_Load(object sender, EventArgs e)
        {
            _ResetDefaultValue();

            if (Mode == enMode.Update)
            {
                _loadData();
            }
        }

        private void rbActivate_CheckedChanged(object sender, EventArgs e)
        {

            if (rbActivate.Checked)
                _Customer.IsActive = true;
            else
                _Customer.IsActive = false;
        }
    }
}
