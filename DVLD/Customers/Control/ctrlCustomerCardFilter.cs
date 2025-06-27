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

namespace DVLD.Customers.Control
{
    public partial class ctrlCustomerCardFilter : UserControl
    {
        public event Action<int> OnSelectedCustomer;

        protected virtual void SelectedCustomer(int CustomerID)
        {
            OnSelectedCustomer?.Invoke(CustomerID);
        }

        private clsCustomer _Customer;
        private int _CustomerID=-1;

        public int CustomerID
        {
            get { return _CustomerID; }
        }
        public clsCustomer CustomerInfo
        {
            get => _Customer;
        }

        private bool _EnableFilter = true;
        public bool EnableFilter
        {
            set
            {
                _EnableFilter = value;
                gbFilter.Enabled = _EnableFilter;
            }
            get => _EnableFilter;
        }

      
        public ctrlCustomerCardFilter()
        {
            InitializeComponent();
        }


        public void ResetCustomerCardFilterInfo()
        {
            EnableFilter = true;
            FilterFoucs();
            ctrlCustomerCard1.ResetCustomerCardInfo();
        }

        private void _FindNow()
        {


            switch (cmbFilterBy.Text)
            {
                case "Customer ID":
                    ctrlCustomerCard1.LoadCustomerInfo(_Customer.CustomerID);
                    break;


            }

            EnableFilter = false;

            if (OnSelectedCustomer != null)
            {
                OnSelectedCustomer(_Customer.CustomerID);
            }




        }

        private void ctrlCustomerCardFilter_Load(object sender, EventArgs e)
        {
            cmbFilterBy.SelectedIndex = 0;
            txtFilterValue.Focus();
            
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
                return;

            int CustomerID = Convert.ToInt32(txtFilterValue.Text.Trim());

            _Customer = clsCustomer.GetCustomerInfoByID(CustomerID);

            if (_Customer == null)
            {
                MessageBox.Show($"There is no Customer With ID {CustomerID}", "Not Found",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _CustomerID = CustomerID;
            _FindNow();
        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilterValue.Text))
            {
                errorProvider1.SetError(txtFilterValue, "This Should not Be Empty");
                txtFilterValue.Focus();
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtFilterValue, null);


            }
        }

        private void btnAddNewCustomer_Click(object sender, EventArgs e)
        {
            frmAddUpdateCustomer frm = new frmAddUpdateCustomer();
            frm.ShowDialog();
        }


        public void FilterFoucs()
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }


        public void LoadCustomerInfo(int CustomerID)
        {
            _Customer = clsCustomer.GetCustomerInfoByID(CustomerID);

            if (_Customer == null)
            {
                MessageBox.Show($"There is no Customer With ID {CustomerID}", "Not Found",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _CustomerID = CustomerID;
            txtFilterValue.Text=_CustomerID.ToString();
            ctrlCustomerCard1.LoadCustomerInfo(CustomerID);
        }
    }
}
