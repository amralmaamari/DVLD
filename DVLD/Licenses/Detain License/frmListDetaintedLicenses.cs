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
    public partial class frmListDetaintedLicenses : Form
    {

        private static DataTable _dtDetaintedLicenses;
        public frmListDetaintedLicenses()
        {
            InitializeComponent();
        }

        private void _LoadDataToTable()
        {
            _dtDetaintedLicenses = clsDetainedLicensecs.GetAllDetainedLicense();
            dgvDetaintedLicenses.DataSource = _dtDetaintedLicenses;
            lblCountRecords.Text = dgvDetaintedLicenses.Rows.Count.ToString();
            if (dgvDetaintedLicenses.Rows.Count > 0)
            {
                dgvDetaintedLicenses.Columns[0].HeaderText = "D.ID";
                dgvDetaintedLicenses.Columns[1].HeaderText = "L.ID";
                dgvDetaintedLicenses.Columns[2].HeaderText = "D.Date";
                dgvDetaintedLicenses.Columns[3].HeaderText = "Is Released";
                dgvDetaintedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvDetaintedLicenses.Columns[5].HeaderText = "Release Date";
                dgvDetaintedLicenses.Columns[6].HeaderText = "N.No.";
                dgvDetaintedLicenses.Columns[7].HeaderText = "Full Name";
                dgvDetaintedLicenses.Columns[8].HeaderText = "Release App ID";
                dgvDetaintedLicenses.Columns[8].Width = 150;

            }
        }
        private void frmListDetaintedLicenses_Load(object sender, EventArgs e)
        {
            cmbFilter.SelectedIndex = 0;
            cmbIsReleased.SelectedIndex=0;

            _LoadDataToTable();

        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedText = cmbFilter.Text;

            txtFilter.Visible = (selectedText != "None" && selectedText != "Is Released");
            cmbIsReleased.Visible = (selectedText == "Is Released");

            

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cmbFilter.Text)
            {
                case "Detain ID":
                    FilterColumn = "DetainID";
                    break;

                case "Is Released":
                    FilterColumn = "IsReleased";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Release Application ID":
                    FilterColumn = "ReleaseApplicationID";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if ( FilterColumn == "None" || txtFilter.Text.Trim() == "")
            {
                _dtDetaintedLicenses.DefaultView.RowFilter = "";
                _LoadDataToTable();
                lblCountRecords.Text = dgvDetaintedLicenses.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "DetainID" || FilterColumn == "ReleaseApplicationID")
            {
                _dtDetaintedLicenses.DefaultView.RowFilter =  string.Format("[{0}] = {1}", FilterColumn, txtFilter.Text.Trim()); ;
            }
            
            else
            {
                _dtDetaintedLicenses.DefaultView.RowFilter =  string.Format("[{0}] like '%{1}%'", FilterColumn, txtFilter.Text.Trim()); ;

            }
            txtFilter.Focus();
            lblCountRecords.Text = dgvDetaintedLicenses.Rows.Count.ToString();
        }

        private void cmbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilter.Text == "Is Released")
            {
                bool Released = false;
                switch (cmbIsReleased.Text)
                {
                    case "Yes":
                        Released = true;
                        break;
                    case "No":
                        Released = false;
                        break;
                    default:
                        _dtDetaintedLicenses.DefaultView.RowFilter = "";
                        _LoadDataToTable();
                        return;
                }


                _dtDetaintedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", "IsReleased", Released); ;
                lblCountRecords.Text = dgvDetaintedLicenses.Rows.Count.ToString();
            }

        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
            frmListDetaintedLicenses_Load(null, null);
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
            frmListDetaintedLicenses_Load(null, null);
        }

        private void showShowPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetaintedLicenses.CurrentRow.Cells[1].Value;
            frmShowPersonInfo frm = new frmShowPersonInfo(clsLicense.GetLicenseInfoByID(LicenseID).ApplictionInfo.ApplicantPersonID);
            frm.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
           

            releaseDetaintedLicencseToolStripMenuItem.Enabled= ((bool)dgvDetaintedLicenses.CurrentRow.Cells[3].Value==false);


        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetaintedLicenses.CurrentRow.Cells[1].Value;
            frmShowLicenseInfo frm= new frmShowLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetaintedLicenses.CurrentRow.Cells[1].Value;
            clsLicense license = clsLicense.GetLicenseInfoByID(LicenseID); 
                
            frmLicenseHistory frm = new frmLicenseHistory(license.ApplictionInfo.ApplicantPersonID);
            frm.ShowDialog();
        }

        private void releaseDetaintedLicencseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetaintedLicenses.CurrentRow.Cells[1].Value;

            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense(LicenseID);
            frm.ShowDialog();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id or user id is selected.
            if (cmbFilter.Text == "Detain ID" || cmbFilter.Text == "Release Application ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
