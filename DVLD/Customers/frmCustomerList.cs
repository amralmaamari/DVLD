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
    public partial class frmCustomerList : Form
    {
        DataTable dtCustomer;
        public frmCustomerList()
        {
            InitializeComponent();
        }

        private void frmCustomerList_Load(object sender, EventArgs e)
        {
            cmbFilter.SelectedIndex = 0;
            dtCustomer = clsCustomer.GetAllCustomer();
            dgvCustomer.DataSource = dtCustomer;
            if (dgvCustomer.Rows.Count > 0)
            {
                dgvCustomer.Columns[0].Width = 100;
                dgvCustomer.Columns[0].HeaderText = "Customer ID";

                dgvCustomer.Columns[1].Width = 100;
                dgvCustomer.Columns[1].HeaderText = "Person ID";


                dgvCustomer.Columns[2].Width = 100;
                dgvCustomer.Columns[2].HeaderText = "License ID";


                dgvCustomer.Columns[3].Width = 100;
                dgvCustomer.Columns[3].HeaderText = "Driver ID";

                dgvCustomer.Columns[4].Width = 250;
                dgvCustomer.Columns[4].HeaderText = "Full Name";

           

                dgvCustomer.Columns[5].HeaderText = "Customer Is Active";
            }
            lblCountRecords.Text = dgvCustomer.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewCustomer_Click(object sender, EventArgs e)
        {
            frmAddUpdateCustomer frm = new frmAddUpdateCustomer();
            frm.ShowDialog();
            frmCustomerList_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateCustomer frm = new frmAddUpdateCustomer((int)dgvCustomer.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmCustomerList_Load(null, null);
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
           
            switch (cmbFilter.Text)
            {
                case "CustomerID":
                    FilterColumn = "CustomerID";
                    break;

                case "PersonID":
                    FilterColumn = "PersonID";
                    break;

                case "FullName":
                    FilterColumn = "FullName";
                    break;

               
                case "IsActive":
                    FilterColumn = "IsActive";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if (FilterColumn == "None" || txtFilter.Text.Trim() == "")
            {
                dtCustomer.DefaultView.RowFilter = "";
                lblCountRecords.Text = dgvCustomer.Rows.Count.ToString();
                return;
            }

            txtFilter.Focus();

            if (FilterColumn == "FullName")
            {
            dtCustomer.DefaultView.RowFilter = $"{FilterColumn} LIKE '%{txtFilter.Text.Trim()}%'";

            }

            dtCustomer.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilter.Text.Trim());


            lblCountRecords.Text = dgvCustomer.Rows.Count.ToString();
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedText = cmbFilter.Text;

            if (selectedText == "IsActive")
            {
                txtFilter.Visible = false;
                cmbIsActive.Visible = true;
                cmbIsActive.Focus();
                cmbIsActive.SelectedIndex = 0;
            }
            else
            {
                
                txtFilter.Visible = (selectedText != "None" && selectedText != "IsActive");


                cmbIsActive.Visible = false;

                if (selectedText == "None")
                {
                    txtFilter.Visible = false;
                }
                else
                    txtFilter.Visible = true;

                txtFilter.Text = "";
                txtFilter.Focus();
            }
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

            if (IsActive == "All")
                dtCustomer.DefaultView.RowFilter = "";
            else
                dtCustomer.DefaultView.RowFilter = $"{FilterColumn} = {IsActive}";

        
         


            lblCountRecords.Text = dgvCustomer.Rows.Count.ToString();
        }

        private void showDetailesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowCustomerInfo frm = new frmShowCustomerInfo((int)dgvCustomer.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int CustomerID = (int)dgvCustomer.CurrentRow.Cells[0].Value;
            clsCustomer Customer = clsCustomer.GetCustomerInfoByID(CustomerID);


            if (Customer == null)
            {
                MessageBox.Show("This form will be closed because No Customer with ID = " + CustomerID, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (MessageBox.Show($"Are sure want delete this CustomerID: {CustomerID}", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (Customer.IsActive)
                {
                    MessageBox.Show("Customer Is Active Con't Delte it = " + CustomerID, "Error",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Customer.DeleteCustomer();
                
            }

            frmCustomerList_Load(null,null);
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateCustomer frm = new frmAddUpdateCustomer();
            frm.ShowDialog();
            frmCustomerList_Load(null, null);
        }
    }

}
