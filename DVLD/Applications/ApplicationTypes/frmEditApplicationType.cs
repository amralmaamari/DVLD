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
    public partial class frmEditApplicationType : Form
    {
        private int _ApplicationTypeID=-1;

        clsApplicationType _ApplicationType;

        public frmEditApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;
        }

        private void _ResetFileds()
        {
            lblID.Text = "???";
            txtTitle.Text = string.Empty;
            txtFees.Text = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled=!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void frmUpdateApplictionType_Load(object sender, EventArgs e)
        {
            _ResetFileds();
            _ApplicationType = clsApplicationType.Find(_ApplicationTypeID);
            if( _ApplicationType == null)
            {
                MessageBox.Show($"There is No Appliction Type With ID {_ApplicationTypeID}","Erroe",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
                _ResetFileds();
                return;

            }

            lblID.Text= _ApplicationType.ApplicationTypeID.ToString();
            txtTitle.Text= _ApplicationType.ApplicationTypeTitle.ToString();
            txtFees.Text=((int) _ApplicationType.ApplicationFees).ToString();

            txtTitle.Focus();




        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _ApplicationType.ApplicationTypeTitle = txtTitle.Text.Trim();
            _ApplicationType.ApplicationFees = float.Parse(txtFees.Text.Trim());


            //Save
            if (_ApplicationType.Save())
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
