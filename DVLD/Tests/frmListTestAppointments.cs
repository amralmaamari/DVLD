using DVLD.Properties;
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
using static DvldDataBusinessLayer.clsTestType;

namespace DVLD
{
    public partial class frmListTestAppointments : Form
    {
       

       private DataTable _dtLicenseTestAppointments;
        private int _LocalDrivingLicenseApplicationID ;
        private  clsTestType.enTestType _TestTypeID;

        private  clsLocalDrivingLicenseApplication clsLocalDrivingLicenseApplication;


        public frmListTestAppointments(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType)
        {
            InitializeComponent();
            this._LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this._TestTypeID = TestType;
        }

        private void _LoadAppointmentsDataTable()
        {
            _dtLicenseTestAppointments = clsTestAppointment.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationID, (clsTestType.enTestType)_TestTypeID);
            dataGridView1.DataSource = _dtLicenseTestAppointments;

            if(dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "Appointment ID";
                dataGridView1.Columns[0].Width = 150;

                dataGridView1.Columns[1].HeaderText = "Appointment Date";
                dataGridView1.Columns[1].Width = 200;

                dataGridView1.Columns[2].HeaderText = "Paid Fees";
                dataGridView1.Columns[2].Width = 150;

                dataGridView1.Columns[3].HeaderText = "Is Locked";
                dataGridView1.Columns[3].Width = 120;
            }
            lblCountRecords.Text = dataGridView1.Rows.Count.ToString();
        }

        private void _LoadTestTypeImageAndTitle()
        {

            switch (_TestTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    lblTitle.Text = "Vision Test Appointments";
                    this.Text = lblTitle.Text;
                    pictureBox1.Image = Resources.Vision_512;
                    break;

                case clsTestType.enTestType.WrittenTest:
                    lblTitle.Text = "Written Test Appointments";
                    this.Text = lblTitle.Text;
                    pictureBox1.Image = Resources.Written_Test_512;
                    break;


                case clsTestType.enTestType.StreetTest:
                    lblTitle.Text = "Street Test Appointments";
                    this.Text = lblTitle.Text;
                    pictureBox1.Image = Resources.driving_test_512;
                    break;
                default:
                    return;
            }

           
        }
        private void frmTestAppointments_Load(object sender, EventArgs e)
        {
            _LoadTestTypeImageAndTitle();


            clsLocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);
            if (clsLocalDrivingLicenseApplication == null)
                return;


            ctrlDrivingLicenseApplicationInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseApplicationID);
            _LoadAppointmentsDataTable();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            //we have to check if he hvae perv appointemet and if  it is active should not allow to addd
            //make condition later on about if it is there is application and is not active if actice rejected  


            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);
            


            if (LocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(_TestTypeID)   )
            {
                MessageBox.Show("Person Already have an active appointment for this test," +
                  " You cannot add new appointment", "Not allowed", MessageBoxButtons.OK,
                  MessageBoxIcon.Error);
                return;
            }



            clsTest lastTest = LocalDrivingLicenseApplication.GetLastTestPerTestType( _TestTypeID);

            if(lastTest == null)
            {
                frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestTypeID);
                frm.ShowDialog();
                frmTestAppointments_Load(null, null);
                return;               

            }

            //if person already passed the test s/he cannot retak it.
            if(lastTest.TestResult == true)
            {
                MessageBox.Show("This person already passed this test before, you can only retake faild test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // I don't why he send the prevoius exam faill to the form
            frmScheduleTest frm2 = new frmScheduleTest
               (lastTest.TestAppointmentInfo.LocalDrivingLicenseApplicationID, _TestTypeID);
            frm2.ShowDialog();
            frmTestAppointments_Load(null, null);



  
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID =(int) dataGridView1.CurrentRow.Cells[0].Value;
            frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestTypeID, TestAppointmentID); ;
            frm.ShowDialog();
            frmTestAppointments_Load(null, null);
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            frmTakeTest frm = new frmTakeTest(TestAppointmentID, _TestTypeID);
            frm.ShowDialog();
            frmTestAppointments_Load(null, null);

        }
    }
}
