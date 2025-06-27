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
    public partial class frmListInternatioanlLicenseApplications : Form
    {

        DataTable _dtAllInternationalLicenseApplications;
        public frmListInternatioanlLicenseApplications()
        {
            InitializeComponent();
        }

        private void _ResetDefaultForm()
        {
            txtFilter.Text = string.Empty;
            txtFilter.Visible = false;
            cmbFilter.SelectedIndex = 0;

        }

        private void _LoadData()
        {


            _dtAllInternationalLicenseApplications = clsInternationalLicense.GetAllInternationalLicense();
            dgvInternationalLicenseApplications.DataSource = _dtAllInternationalLicenseApplications;

            lblCountRecords.Text = dgvInternationalLicenseApplications.Rows.Count.ToString();

            if (dgvInternationalLicenseApplications.Rows.Count > 0)
            {
                dgvInternationalLicenseApplications.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicenseApplications.Columns[0].Width = 120;

                dgvInternationalLicenseApplications.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicenseApplications.Columns[1].Width = 120;


                dgvInternationalLicenseApplications.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicenseApplications.Columns[2].Width = 120;

                dgvInternationalLicenseApplications.Columns[3].HeaderText = "L.License ID";
                dgvInternationalLicenseApplications.Columns[3].Width = 120;

                dgvInternationalLicenseApplications.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicenseApplications.Columns[4].Width = 120;

                dgvInternationalLicenseApplications.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLicenseApplications.Columns[5].Width = 120;

                dgvInternationalLicenseApplications.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicenseApplications.Columns[6].Width = 120;


            }

            _ResetDefaultForm();
        }

        private void frmListInternatioanlLicenseApplications_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewInternationalLicenseApp_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
            frmListInternatioanlLicenseApplications_Load(null, null);
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow numbers only becasue all fiters are numbers.


            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cmbFilter.Text)
            {
                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;

                case "Application ID":
                    FilterColumn = "ApplicationID";
                    break;

                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Local Liceense ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;
                case "Is Active":
                    FilterColumn = "IsActive";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if (FilterColumn == "None" || txtFilter.Text.Trim() == "")
            {
                _dtAllInternationalLicenseApplications.DefaultView.RowFilter = "";
                lblCountRecords.Text = dgvInternationalLicenseApplications.Rows.Count.ToString();
                return;
            }

            txtFilter.Focus();

            
                _dtAllInternationalLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilter.Text.Trim());

           
            lblCountRecords.Text = dgvInternationalLicenseApplications.Rows.Count.ToString();
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedText = cmbFilter.Text;

            if (selectedText == "Is Active")
            {
                txtFilter.Visible = false;
                cmbIsActive.Visible = true;
                cmbIsActive.Focus();
                cmbIsActive.SelectedIndex = 0;
            }
            else
            {

            txtFilter.Visible = (selectedText != "None" && selectedText != "Is Active");
              
           
                cmbIsActive.Visible = false;

                if (selectedText == "None")
                {
                    txtFilter.Visible = false;
                }else
                    txtFilter.Visible = true;

                txtFilter.Text = "";
                txtFilter.Focus();
            }

            
        }

        private void showShowPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvInternationalLicenseApplications.CurrentRow.Cells[2].Value;
            int PersonID = clsDriver.FindByDriverID(DriverID).PersonID;
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InternatioanlID = (int)dgvInternationalLicenseApplications.CurrentRow.Cells[0].Value;

            frmInternationalDriveInfo frm = new frmInternationalDriveInfo(InternatioanlID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvInternationalLicenseApplications.CurrentRow.Cells[2].Value;
            int PersonID = clsDriver.FindByDriverID(DriverID).PersonID;
            frmLicenseHistory frm =new frmLicenseHistory(PersonID);

            frm.ShowDialog();

        }

        private void cmbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string IsActive = cmbIsActive.Text;

            switch (IsActive)
            {
                case "All":
                    break;

                case "Yes":
                    IsActive = "1";
                    break;
                case "No":
                    IsActive = "0";
                    break;
                default:
                    return;


            }

            if(IsActive == "All")
                    _dtAllInternationalLicenseApplications.DefaultView.RowFilter = "";
            else
                 _dtAllInternationalLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, IsActive); ;
           
            
            lblCountRecords.Text = dgvInternationalLicenseApplications.Rows.Count.ToString();
        }

        }
    
    }
