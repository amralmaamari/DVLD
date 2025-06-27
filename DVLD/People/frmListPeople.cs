using System;
using DvldDataBusinessLayer;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics.Eventing.Reader;

namespace DVLD
{
    public partial class frmListPeople : Form
    {

        private static DataTable _dtAllPeople= clsPeople.GetAllPeople();

        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName", "SecondName", "ThirdName"
                                                                    , "LastName", "Gendor", "DateOfBirth", "Nationality", "Phone","Email");
        private void _RefreshPeoplList()
        {

            _dtAllPeople = clsPeople.GetAllPeople();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName", "SecondName", "ThirdName"
                                                                    , "LastName", "Gendor", "DateOfBirth", "Nationality", "Phone", "Email");

            dgvPeople.DataSource=_dtPeople;
            _RefreshPeopleCount();
           
        }
        private void _RefreshPeopleCount()
        {
            lblCountRecords.Text=dgvPeople.Rows.Count.ToString();
            
        }
        public frmListPeople()
        {
            InitializeComponent();
         
        }

      
        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            
            _RefreshPeoplList();
            cmbFilter.SelectedIndex = 0;

            //format the name as you want and there WIDTH
            if(dgvPeople.Rows.Count > 0)
            {
                dgvPeople.Columns[0].HeaderText = "Person ID";
                dgvPeople.Columns[0].Width = 110;

                dgvPeople.Columns[1].HeaderText = "National No";
                dgvPeople.Columns[1].Width = 120;

                dgvPeople.Columns[2].HeaderText = "First Name";
                dgvPeople.Columns[2].Width = 120;


                dgvPeople.Columns[3].HeaderText = "Second Name";
                dgvPeople.Columns[3].Width = 120;


                dgvPeople.Columns[4].HeaderText = "Third Name";
                dgvPeople.Columns[4].Width = 120;

                dgvPeople.Columns[5].HeaderText = "Last Name";
                dgvPeople.Columns[5].Width = 120;


                dgvPeople.Columns[6].HeaderText = "Gendor";
                dgvPeople.Columns[6].Width = 120;

                dgvPeople.Columns[7].HeaderText = "Date Of Birth";
                dgvPeople.Columns[7].Width = 120;


                dgvPeople.Columns[8].HeaderText = "Nationality";
                dgvPeople.Columns[8].Width = 120;


                dgvPeople.Columns[9].HeaderText = "Phone";
                dgvPeople.Columns[9].Width = 120;


                dgvPeople.Columns[10].HeaderText = "Email";
                dgvPeople.Columns[10].Width = 120;
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson AddEditPersonInfo = new frmAddUpdatePerson();
            AddEditPersonInfo.ShowDialog();
            _RefreshPeoplList();
        }

        private void showDetailesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // we can not use variable PersonID  to SAVE MEOMRY
            int PersonID = (int)dgvPeople.CurrentRow.Cells["PersonID"].Value;
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ShowDialog();
            _RefreshPeoplList();

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmAddUpdatePerson frm = new frmAddUpdatePerson((int)dgvPeople.CurrentRow.Cells["PersonID"].Value);
            frm.ShowDialog();
            _RefreshPeoplList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int SelectedPersonId = (int)dgvPeople.CurrentRow.Cells["PersonID"].Value;
            if(MessageBox.Show($"Are Sure Want To Delete Person ID {SelectedPersonId} ","Sure To Delete",MessageBoxButtons.YesNo) == DialogResult.Yes){
            
                    if (clsPeople.DeletePerson(SelectedPersonId))
                    {
                        MessageBox.Show("Delete Successfully ");
                        _RefreshPeoplList();
                    }
                    else                   
                        MessageBox.Show("Has Refernssial Integritey ");

              
                
                
                
            }
  
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is NOT Impelemneted Yet!","NOT Ready!",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature is NOT Impelemneted Yet!", "NOT Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

     

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cmbFilter.Text)
            {
                case"PersonID":
                    FilterColumn = "PersonID";
                    break;

                case "NationalNo":
                    FilterColumn = "NationalNo";
                    break;

                case "FirstName":
                    FilterColumn = "FirstName";
                    break;


                case "SecondName":
                    FilterColumn = "SecondName";
                    break;

                case "ThirdName":
                    FilterColumn = "ThirdName";
                    break;

                case "LastName":
                    FilterColumn = "LastName";
                    break;


                case "Nationality":
                    FilterColumn = "Nationality";
                    break;

                case "Gendor":
                    FilterColumn = "Gendor";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if(txtFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllPeople.DefaultView.RowFilter = "";
                _RefreshPeoplList();
                lblCountRecords.Text = dgvPeople.Rows.Count.ToString();
                return;
            }


            if(FilterColumn == "PersonID") 
               _dtPeople.DefaultView.RowFilter= string.Format("[{0}] = {1}", FilterColumn, txtFilter.Text.Trim());
            else
            _dtPeople.DefaultView.RowFilter = string.Format("[{0}] like '%{1}%'", FilterColumn, txtFilter.Text.Trim());


 
            lblCountRecords.Text = dgvPeople.Rows.Count.ToString();


           


        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmbFilter.Text == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtFilter.Visible=cmbFilter.Text != "None";
            if (txtFilter.Visible == true)
            {
             txtFilter.Text = "";
             txtFilter.Focus();

            }

    
        }
    }
}
