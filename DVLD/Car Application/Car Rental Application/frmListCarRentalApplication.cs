using DvldDataBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmListCarRentalApplication : Form
    {

        DataTable _dtAllCarRentalApplications;
        public frmListCarRentalApplication()
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


            _dtAllCarRentalApplications = clsCarRentalApplication.GetAllCarRentalApplications();
            dgvCarRentalApplications.DataSource = _dtAllCarRentalApplications;

            lblCountRecords.Text = dgvCarRentalApplications.Rows.Count.ToString();

            if (dgvCarRentalApplications.Rows.Count > 0)
            {
                dgvCarRentalApplications.Columns[0].HeaderText = "C.R AppID";
                dgvCarRentalApplications.Columns[0].Width = 100;

                dgvCarRentalApplications.Columns[1].HeaderText = "Car Model";
                dgvCarRentalApplications.Columns[1].Width = 150;


                dgvCarRentalApplications.Columns[2].HeaderText = "Customer ID";
                dgvCarRentalApplications.Columns[2].Width = 100;

                dgvCarRentalApplications.Columns[3].HeaderText = "Full Name";
                dgvCarRentalApplications.Columns[3].Width = 220;

                dgvCarRentalApplications.Columns[4].HeaderText = "Paid Fees";
                dgvCarRentalApplications.Columns[4].Width = 100;

                dgvCarRentalApplications.Columns[5].HeaderText = "Application Date";
                dgvCarRentalApplications.Columns[5].Width = 150;

                dgvCarRentalApplications.Columns[6].HeaderText = "Rent Type Name";
                dgvCarRentalApplications.Columns[6].Width = 150;


                dgvCarRentalApplications.Columns[7].HeaderText = "Application Status";
                dgvCarRentalApplications.Columns[7].Width = 130;


            }

            _ResetDefaultForm();
        }

        private void frmListCarRentalApplication_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedText = cmbFilter.Text;

            if (selectedText == "Application Status")
            {
                txtFilter.Visible = false;
                cmbStatus.Visible = true;
                cmbStatus.Focus();
                cmbStatus.SelectedIndex = 0;
            }
            else
            {

                cmbStatus.Visible = false;

                if (selectedText == "None")
                {
                    _dtAllCarRentalApplications.DefaultView.RowFilter = "";
                    lblCountRecords.Text = dgvCarRentalApplications.Rows.Count.ToString();
                    txtFilter.Visible = false;
                }
                else
                {
                    txtFilter.Text = "";
                    txtFilter.Visible = true;
                    txtFilter.Focus();

                }

            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";




            switch (cmbFilter.Text)
            {
                case "C.R AppID":
                    FilterColumn = "CarRentalApplicationID";
                    break;

                case "Customer ID":
                    FilterColumn = "CustomerID";
                    break;

                case "Application Status":
                    FilterColumn = "ApplicationStatus";
                    break;

                default:
                    FilterColumn = "None";                    
                    break;
            }

            if (FilterColumn == "None" || txtFilter.Text.Trim() == "")
            {
                
                _dtAllCarRentalApplications.DefaultView.RowFilter = "";
                lblCountRecords.Text = dgvCarRentalApplications.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "CarRentalApplicationID" || FilterColumn == "CustomerID")
            {

                _dtAllCarRentalApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilter.Text.Trim());

            }
            //the below no need because there is no Text
            //else
            //{

            //    _dtAllCarRentalApplications.DefaultView.RowFilter = string.Format("[{0}] like '%{1}%'", FilterColumn, txtFilter.Text.Trim());

            //}
            lblCountRecords.Text = dgvCarRentalApplications.Rows.Count.ToString();
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

            string FilterColumn = "ApplicationStatus";
            string Status = this.cmbStatus.Text;

            switch (Status)
            {

                case "All":
                    break;
                case "New":
                    Status = "New";
                    break;
                case "Complete":
                    Status = "Complete";
                    break;
                case "Cancelled":
                    Status = "Cancelled";
                break;
            default:
                    return;


            }

            if (Status == "All")
                _dtAllCarRentalApplications.DefaultView.RowFilter = "";
            else
                    _dtAllCarRentalApplications.DefaultView.RowFilter = string.Format("[{0}] like '%{1}%'", FilterColumn, Status);





                lblCountRecords.Text = dgvCarRentalApplications.Rows.Count.ToString();

        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmbFilter.Text == "C.R AppID"|| cmbFilter.Text == "Customer ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showDetailesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCarRentalApplicationInfo frm = new frmCarRentalApplicationInfo((int)dgvCarRentalApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void EditApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateCarRentalApplication frm = new frmAddUpdateCarRentalApplication((int)dgvCarRentalApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListCarRentalApplication_Load(null, null);

        }

        private void btnAddNewCarRentalApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateCarRentalApplication frm = new frmAddUpdateCarRentalApplication();
            frm.ShowDialog();
            frmListCarRentalApplication_Load(null, null);
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int CarRentalApplictionID = (int)dgvCarRentalApplications.CurrentRow.Cells[0].Value;

            if (MessageBox.Show($"Are Sure Want To Delete This ApplicationID {CarRentalApplictionID}",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            clsCarRentalApplication CarRentalApplication = clsCarRentalApplication.FindByCarRentalApplicationID(CarRentalApplictionID);
           if (CarRentalApplication.Delete())
            {
                MessageBox.Show("Delete Sucessfully", "Delte", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

            }else
            {
                MessageBox.Show("Delete NOT Sucessfully", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            }

            frmListCarRentalApplication_Load(null, null);




        }

        private void CancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int CarRentalApplicationID = (int)dgvCarRentalApplications.CurrentRow.Cells[0].Value;
            int RentalApplicationID = clsCarRentalApplication.FindByCarRentalApplicationID(CarRentalApplicationID).RentalApplicationID;

            if (MessageBox.Show($"Are Sure Want To Cancel This ApplicationID {CarRentalApplicationID}",
               "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;


            if (clsRentalApplication.UpdateStatus(RentalApplicationID, clsRentalApplication.enRentStatus.Cancelled))
            {
                MessageBox.Show("Canceled Sucessfully", "Canceled", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Canceled NOT Sucessfully", "Canceled", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            }

            frmListCarRentalApplication_Load(null, null);


        }

        private void cmsCarRentalApplication_Opening(object sender, CancelEventArgs e)
        {
            int CarRentalApplicationID = (int)dgvCarRentalApplications.CurrentRow.Cells[0].Value;
            int RentalApplicationID = clsCarRentalApplication.FindByCarRentalApplicationID(CarRentalApplicationID).RentalApplicationID;

            clsCarRentalApplication carRentalApplication = clsCarRentalApplication.FindByCarRentalApplicationID(CarRentalApplicationID);
            EditApplicationToolStripMenuItem.Enabled = (carRentalApplication.RentStatus == clsRentalApplication.enRentStatus.New);
            deleteApplicationToolStripMenuItem.Enabled = (carRentalApplication.RentStatus != clsRentalApplication.enRentStatus.New);
            CancelToolStripMenuItem.Enabled= (carRentalApplication.RentStatus == clsRentalApplication.enRentStatus.New);
        }
    }
}
