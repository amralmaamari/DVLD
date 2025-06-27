using DvldDataBusinessLayer;
using System;
using System.Data;

using System.Windows.Forms;

namespace DVLD
{
    // solve the problem of the filter if i search after change to None the DGV will not show the Data
    public partial class frmListUsers : Form
    {
        private static DataTable _dtAllUser;
        public frmListUsers()
        {
            InitializeComponent();
        }


   

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

      

        private void cmbFilter_SelectedIndexChanged(object sender, System.EventArgs e)
        {
         


            if (cmbFilter.Text == "IsActive")
            {
                //Make here DropMEan for Yes Or Nor if Yes show will the Actives USERS otherwise show all not ACtive
                cmbIsActive.Visible = true;
                txtFilter.Visible = false;
                cmbIsActive.Focus();
                cmbIsActive.SelectedIndex = 0;

            }
            else
            {
                txtFilter.Visible = (cmbFilter.Text != "None");
                cmbIsActive.Visible = false;

          
                txtFilter.Text = string.Empty;
                txtFilter.Focus();
            }

          
        

        }

        private void frmManageUsers_Load(object sender, System.EventArgs e)
        {

            _dtAllUser = clsUser.GetAllUser();
            dgvUsers.DataSource = _dtAllUser;
            cmbFilter.SelectedIndex = 0;
            lblCountRecords.Text = dgvUsers.Rows.Count.ToString();

            if (dgvUsers.Rows.Count > 0)
            {

                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[0].Width = 200;

                dgvUsers.Columns[1].HeaderText = "Person ID";
                dgvUsers.Columns[1].Width = 170;

                dgvUsers.Columns[2].HeaderText = "Full Name";
                dgvUsers.Columns[2].Width = 300;

                dgvUsers.Columns[3].HeaderText = "User Name";
                dgvUsers.Columns[3].Width = 120;


                dgvUsers.Columns[4].HeaderText = "Is Active";
                dgvUsers.Columns[4].Width = 110;

            }
        }

        private void txtFilter_TextChanged(object sender, System.EventArgs e)
        {

            string FilterColumn = "";
            switch (cmbFilter.Text)
            {
                case "UserID":
                    FilterColumn = "UserID";
                    break;

                case "UserName":
                    FilterColumn = "UserName";
                    break;
                case "PersonID":
                    FilterColumn = "PersonID";
                    break;
                case "FullName":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if (FilterColumn == "None" || txtFilter.Text.Trim() == "")
            {
                _dtAllUser.DefaultView.RowFilter = "";
                lblCountRecords.Text = dgvUsers.Rows.Count.ToString();
                return;
            }


           


            if (_dtAllUser.Columns[FilterColumn].DataType == typeof(string))
            {
                _dtAllUser.DefaultView.RowFilter = string.Format("[{0}] like '%{1}%'", FilterColumn, txtFilter.Text.Trim());
            }
            else
            {
                _dtAllUser.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilter.Text.Trim());
            }

            lblCountRecords.Text = dgvUsers.Rows.Count.ToString();


        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            //allow only the Number
            if(cmbFilter.Text == "PersonID" ||  cmbFilter.Text == "UserID")
            {
                e.Handled = ! char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void showDetailesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo((int)dgvUsers.CurrentRow.Cells["UserID"].Value);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Not Work Properply
            frmChangePassword frm = new frmChangePassword((int)dgvUsers.CurrentRow.Cells["UserID"].Value);
            
            frm.ShowDialog();
            frmManageUsers_Load(null, null);
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.ShowDialog();
            //_UploadData();
            frmManageUsers_Load(null, null);

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser((int)dgvUsers.CurrentRow.Cells["UserID"].Value);
            frm.ShowDialog();
            //_UploadData();

            frmManageUsers_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int) dgvUsers.CurrentRow.Cells["UserID"].Value;

            if (MessageBox.Show($"Asre Sure Want To Delete UserID{UserID} ?","Delete",MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (clsUser.DeleteUser(UserID))
                {
                    MessageBox.Show("Delete Susccfully");
                }
                {
                    MessageBox.Show("User is not Deleted due to data connected to it.","Faild",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        private void cmbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterCoulmn = "IsActive";
            string FilterValue = cmbIsActive.Text;
            switch (FilterValue)
            {
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
                default:
                    break;
            }


             if (FilterValue == "All")
            {
                _dtAllUser.DefaultView.RowFilter = "";
            }
           else
            {

                _dtAllUser.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterCoulmn, FilterValue);

            }
           
            lblCountRecords.Text = _dtAllUser.Rows.Count.ToString();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.ShowDialog();
            //this is refresh the form 
            frmManageUsers_Load(null, null);
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is NOT Impelemneted Yet!", "NOT Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is NOT Impelemneted Yet!", "NOT Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }
    }
}
