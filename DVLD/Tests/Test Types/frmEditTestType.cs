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
    public partial class frmEditTestType : Form
    {

        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;

        clsTestType _TestType;
        public frmEditTestType(clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            this._TestTypeID=TestTypeID;
        }

        private void _ResetFileds()
        {
            lblID.Text = "???";
            txtTitle.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtFees.Text = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "Title cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtTitle, "");

            }
        }
        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDescription, "Description cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtTitle, "");

            }
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFees, "");

            }

            if (!clsValidatoin.IsNumber(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid Number.");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFees, "");

            }
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void frmEditTestType_Load(object sender, EventArgs e)
        {
            _ResetFileds();
            _TestType = clsTestType.Find(_TestTypeID);
            if (_TestType == null)
            {
                MessageBox.Show("Could not find Test Type with id = " + _TestTypeID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                _ResetFileds();
                return;

            }

            lblID.Text = ((int)_TestType.TestTypeID).ToString();
            txtTitle.Text = _TestType.TestTypeTitle.ToString();
            txtDescription.Text = _TestType.TestTypeDescription.ToString();
            txtFees.Text = ((int)_TestType.TestTypeFees).ToString();

            txtTitle.Focus();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _TestType.TestTypeTitle = txtTitle.Text.Trim();
            _TestType.TestTypeDescription = txtDescription.Text.Trim();
            _TestType.TestTypeFees = decimal.Parse(txtFees.Text.Trim());


            //Save
            if (_TestType.Save())
            {
                MessageBox.Show("Update Successfully");
                return;
            }
            else
            {
                MessageBox.Show("NOT Update Successfully");
                return;

            }
        }
    }
}
