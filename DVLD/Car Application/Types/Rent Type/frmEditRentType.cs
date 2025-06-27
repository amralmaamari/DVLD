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
    public partial class frmEditRentType : Form
    {
        private clsRentType.enRentType _RentTypeID;
        private clsRentType _RentType;
        public frmEditRentType(clsRentType.enRentType RentTypeID)
        {
            InitializeComponent();
            _RentTypeID = RentTypeID;
        }

        private void _FillData()
        {
            lblID.Text =((int) _RentTypeID).ToString();
            txtName.Text = _RentType.RentTypeName;
            txtDescription.Text = _RentType.RentTypeDescription;
            txtFees.Text = _RentType.RentTypeFees.ToString();
        }

        private void frmEditRentType_Load(object sender, EventArgs e)
        {
            _RentType = clsRentType.Find(_RentTypeID);
            if(_RentType == null)
            {
                MessageBox.Show($"Can't Find TestTypeID:{(int)_RentTypeID}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillData();
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void _FillRentTypeObject()
        {
            _RentType.RentTypeName=txtName.Text;
            _RentType.RentTypeDescription=txtDescription.Text;
            _RentType.RentTypeFees=Convert.ToDecimal(txtFees.Text);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Move Mouse Over Icon To See Waht Is The Wrong", "Validate",
                    MessageBoxButtons.OK);
                return;
            }

            _FillRentTypeObject();
            
            if (_RentType.Save())
            {
                MessageBox.Show("Update Suceessfully", "Update",
                    MessageBoxButtons.OK);
                btnSave.Enabled = false;
            }
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text))
            {
                errorProvider1.SetError(txtFees, "Should Have Fees");
            }else
            {
                errorProvider1.SetError(txtFees, null);

            }
        }
    }
}
