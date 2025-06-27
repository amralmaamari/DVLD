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
    public partial class frmListDrivers : Form
    {

        public frmListDrivers()
        {
            InitializeComponent();
        }

        DataTable _dataTable;
   
        



        private void frmListDrivers_Load(object sender, EventArgs e)
        {

            _dataTable = clsDriver.GetAllDrivers();
            dgvDrivers.DataSource = _dataTable;


            if (dgvDrivers.Rows.Count > 0)
            {                

                dgvDrivers.Columns[0].HeaderText = "Driver ID";
                dgvDrivers.Columns[1].HeaderText = "Person ID";
                dgvDrivers.Columns[2].HeaderText = "National No.";

                dgvDrivers.Columns[3].Width = 220;
                dgvDrivers.Columns[3].HeaderText = "Full Name";

                dgvDrivers.Columns[4].Width = 120;
                dgvDrivers.Columns[4].HeaderText = "Date";

                dgvDrivers.Columns[5].HeaderText = "Actice License";

            }

            lblCountRecords.Text=dgvDrivers.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtFilter.Visible = (cmbFilter.Text != "None");

                txtFilter.Focus();
                txtFilter.Clear();
            

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cmbFilter.SelectedItem.ToString().Trim())
            {
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;


                case "Person ID":
                    FilterColumn = "PersonID";
                    break;


                case "National No":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if (string.IsNullOrEmpty(txtFilter.Text) || FilterColumn == "None")
            {
                _dataTable.DefaultView.RowFilter = "";
                lblCountRecords.Text = dgvDrivers.Rows.Count.ToString();

                return;
            }


            if (FilterColumn == "DriverID" ||
                FilterColumn == "PersonID")

                _dataTable.DefaultView.RowFilter = $"{FilterColumn} = {txtFilter.Text.Trim()}";

            else
                _dataTable.DefaultView.RowFilter = $"{FilterColumn} LIKE '%{txtFilter.Text.Trim()}%'";

            lblCountRecords.Text = dgvDrivers.Rows.Count.ToString();


        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmbFilter.Text == "Driver ID" ||
               cmbFilter.Text == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            }
        }

        private void frmListDrivers_Activated(object sender, EventArgs e)
        {
            cmbFilter.SelectedIndex = 0;
        }

        private void showShowPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID=(int) dgvDrivers.CurrentRow.Cells[1].Value;
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
            
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvDrivers.CurrentRow.Cells[1].Value;
    
            frmLicenseHistory frm = new frmLicenseHistory(PersonID);
            frm.ShowDialog();
        }
    }
}
