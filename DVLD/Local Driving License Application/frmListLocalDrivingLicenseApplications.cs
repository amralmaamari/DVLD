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
    public partial class frmListLocalDrivingLicenseApplications : Form
    {
        DataTable _dtAllLocalDrivingLicenseApplications;
        public frmListLocalDrivingLicenseApplications()
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
            
           
            _dtAllLocalDrivingLicenseApplications =clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvLocalDrivingLicenseApplications.DataSource = _dtAllLocalDrivingLicenseApplications;

            lblCountRecords.Text= dgvLocalDrivingLicenseApplications.Rows.Count.ToString();

            if(dgvLocalDrivingLicenseApplications.Rows.Count > 0 )
            {
                dgvLocalDrivingLicenseApplications.Columns[0].HeaderText = "L D.L AppID";
                dgvLocalDrivingLicenseApplications.Columns[0].Width=100;

                dgvLocalDrivingLicenseApplications.Columns[1].HeaderText = "Driving CLass";
                dgvLocalDrivingLicenseApplications.Columns[1].Width = 250;

               
                dgvLocalDrivingLicenseApplications.Columns[2].Width = 100;

                dgvLocalDrivingLicenseApplications.Columns[3].Width = 250;

                dgvLocalDrivingLicenseApplications.Columns[4].Width = 200;

                dgvLocalDrivingLicenseApplications.Columns[5].Width = 150;

       

            }

            _ResetDefaultForm();
        }

        private void frmListLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = (cmbFilter.Text != "None");

            if (txtFilter.Visible)
            {
               
                txtFilter.Text = string.Empty;
                txtFilter.Focus();

            }
            _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
            lblCountRecords.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cmbFilter.Text)
            {
                case "L D.L AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Status":
                    FilterColumn = "Status";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if (FilterColumn == "None" || txtFilter.Text.Trim() == "")
            {
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
                lblCountRecords.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "LocalDrivingLicenseApplicationID")
            {
        
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilter.Text.Trim());

            }
            else
            {

                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] like '%{1}%'", FilterColumn, txtFilter.Text.Trim());

            }
            lblCountRecords.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cmbFilter.Text == "L D.L AppID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewLDLicenseApp_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication();
            frm.ShowDialog();
            frmListLocalDrivingLicenseApplications_Load(null, null);
        }


        private void _ScheduleTest(clsTestType.enTestType TestType)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            frmListTestAppointments frm = new frmListTestAppointments(LocalDrivingLicenseApplicationID, TestType);
            frm.ShowDialog();
            frmListLocalDrivingLicenseApplications_Load(null, null);

        }

        private void scheduleVisiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.VisionTest);
        }

        private void scheduleWritenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.WrittenTest);
            
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.StreetTest);
           
        }

        //you have to complete this to show the wright exam in test he had 
        //and make the test in the ctrlDrivingLicenseA... make the right test count♥
       

        private void cmsLocalDrivingLicenseApplication_Opening(object sender, CancelEventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            int TotalPassedTests = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[5].Value;
            //Teacher Down line write it in differentway
            bool LicenseExists = LocalDrivingLicenseApplication.IsLicenseIssued();

            //Enabled only if person passed all tests and Does not have license. 
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = (TotalPassedTests == 3) && (!LicenseExists);
            showLicenseToolStripMenuItem.Enabled = LicenseExists && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplications.enApplicationStatus.Completed);


            EditApplicationToolStripMenuItem.Enabled =(!LicenseExists) && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplications.enApplicationStatus.New);

            SechduleTestsToolStripMenuItem.Enabled = (!LicenseExists);

            //Enable/Disable Cancel Menue Item
            //We only canel the applications with status=new.
            CancelToolStripMenuItem.Enabled = (LocalDrivingLicenseApplication.ApplicationStatus == clsApplications.enApplicationStatus.New);

            //Enable/Disable Delete Menue Item
            //We only allow delete incase the application status is new not complete or Cancelled.
            deToolStripMenuItem.Enabled = (LocalDrivingLicenseApplication.ApplicationStatus == clsApplications.enApplicationStatus.New);

            //Enable Disable Schedule menue and it's sub menue

            bool PassedVisionTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.VisionTest);
            bool PassedWrittenTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.WrittenTest);
            bool PassedStreetTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.StreetTest);

            SechduleTestsToolStripMenuItem.Enabled = (!PassedVisionTest || (!PassedWrittenTest) ||  (!PassedStreetTest)) && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplications.enApplicationStatus.New);

            if (SechduleTestsToolStripMenuItem.Enabled)
            {
                scheduleVisiToolStripMenuItem.Enabled = (!PassedVisionTest);
                scheduleWritenTestToolStripMenuItem.Enabled = (!PassedWrittenTest) && PassedVisionTest;
                scheduleStreetTestToolStripMenuItem.Enabled = (!PassedStreetTest) && PassedWrittenTest && PassedVisionTest;
            }
        }

        private void showDetailesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplicationInfo frm =new frmLocalDrivingLicenseApplicationInfo((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListLocalDrivingLicenseApplications_Load(null, null);


        }

        private void EditApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmListLocalDrivingLicenseApplications_Load(null, null);
        }

        private void CancelToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are sure want to Cancle this Application?", "Confrim", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
                clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = 
                    clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

                if (LocalDrivingLicenseApplication != null)
                {
                    if (LocalDrivingLicenseApplication.Cancel())
                    {
                        MessageBox.Show("Application Cancelled Succesfullly?", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmListLocalDrivingLicenseApplications_Load(null, null);
                    }
                }
                else
                {
                    MessageBox.Show("Could't Cancel Application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                   
                }

            }
        }

        private void deToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are sure want to Delete this Application?", "Confrim", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
                clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                    clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

                if (LocalDrivingLicenseApplication != null)
                {
                    if (LocalDrivingLicenseApplication.Delete())
                    {
                        MessageBox.Show("Application Deleted Succesfullly?", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmListLocalDrivingLicenseApplications_Load(null, null);
                    }
                }
                else
                {
                    MessageBox.Show("Could't Delete Application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
                return;
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            frmIssueDriverLicenseForTheFirstTime frm = new frmIssueDriverLicenseForTheFirstTime(LocalDrivingLicenseApplicationID);
            frm.ShowDialog();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            int LicenseID = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID).GetActiveLicenseID();

           

            if(LicenseID != -1)
            {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
            frm.ShowDialog();

            }
            else
            {
                MessageBox.Show("No License Found!", "No License", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
                return;
            }
        
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
                int PersonID = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(
                ((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value)
                ).ApplicantPersonID;
            if (PersonID == -1)
                return;

            frmLicenseHistory frm=new frmLicenseHistory(PersonID);
            frm.ShowDialog();
        }
    }
}
