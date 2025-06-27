using DVLD.Properties;
using DvldDataBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DvldDataBusinessLayer.clsTestType;

namespace DVLD
{
    public partial class ctrlScheduleTest : UserControl
    {

        enum enMode { AddNew = 1, Update = 2 };
        private enMode _Mode = enMode.AddNew;
        enum enCreationMode { FirstTimeSchedule = 0, RetakeTestSchedule = 1 };
        enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;


        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplicationInfo;


        //make it in enum is better than the number ♥☺

        int _LocalDrivingLicenseApplicationID = -1;

        clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        clsTestAppointment _TestAppointment;

        int _TestAppointmentID = -1;


        public clsTestType.enTestType TestTypeID
        {
            get { return _TestTypeID; }

            set
            {
                _TestTypeID = value;

                switch (_TestTypeID)
                {
                    case clsTestType.enTestType.VisionTest:
                        gbMain.Text = "Vision Test";
                        pictureBox1.Image = Resources.Vision_512;
                        break;
                    case clsTestType.enTestType.WrittenTest:
                        gbMain.Text = "Written Test";
                        pictureBox1.Image = Resources.Written_Test_512;
                        break;
                    case clsTestType.enTestType.StreetTest:
                        gbMain.Text = "Street Test";
                        pictureBox1.Image = Resources.driving_test_512;
                        break;

                    default:
                        return;
                }

            }

        }





        public ctrlScheduleTest()
        {
            InitializeComponent();
        }


        public void ResetAllValues()
        {

            lblDLAppID.Text = "[????]";
            lblDclass.Text = "[????]";
            lblFullName.Text = "[????]";
            lblTrial.Text = "[????]";
            dtpTestDate.Value = DateTime.Now;
            gbRetakeTestInfo.Enabled = false;
            lblRetakeAppFees.Text = "[????]";
            lblTotalFees.Text = "[????]";
            lblRetakeTestAppID.Text = "[????]";
        }

        public void LoadInfo(int LocalDrivingLicenseApplicationID, int TestAppointmentID = -1)
        {

            if (TestAppointmentID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestAppointmentID = TestAppointmentID;
            _LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);


            if (_LocalDrivingLicenseApplicationInfo == null)
            {
                ResetAllValues();
                MessageBox.Show("Error: No Local Driving License Application with ID = " + _LocalDrivingLicenseApplicationID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }

            //decide if the createion mode is retake test or not based if the person
            //even if he pass or fail 
            //attended this test before

            if (_LocalDrivingLicenseApplicationInfo.DoesAttendTestType(_TestTypeID))
            {
                _CreationMode = enCreationMode.RetakeTestSchedule;
            }
            else
            {
                _CreationMode = enCreationMode.FirstTimeSchedule;

            }



            if (_CreationMode == enCreationMode.RetakeTestSchedule)
            {
                lblRetakeAppFees.Text = clsApplicationType.Find((int)clsApplications.enApplicationTypeID.RetakeTest).ApplicationFees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = "0";


            }
            else
            {
                gbRetakeTestInfo.Enabled = false;
                lblTitle.Text = "Schedule Test";
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }


            lblDLAppID.Text = _LocalDrivingLicenseApplicationInfo.LocalDrivingLicenseApplicationID.ToString();
            lblDclass.Text = _LocalDrivingLicenseApplicationInfo.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplicationInfo.PersonFullName;
            lblTrial.Text = _LocalDrivingLicenseApplicationInfo.TotalTrialsPerTest(_TestTypeID).ToString();


            if (_Mode == enMode.AddNew)
            {
                lblFees.Text = clsTestType.Find(_TestTypeID).TestTypeFees.ToString();
                dtpTestDate.MinDate = DateTime.Now;
                lblRetakeTestAppID.Text = "N/A";

                _TestAppointment = new clsTestAppointment();
            }
            else
            {
                if (!_LoadTestAppointmentData())
                    return;
            }

            lblTotalFees.Text = ((Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRetakeAppFees.Text))).ToString();


            if (!_HandleActiveTestAppointmentConstraint())
                return;


            if (!_HandleAppointmentLockedConstraint())
                return;




            if (!_HandlePrviousTestConstraint())
                return;

        }

        private bool _LoadTestAppointmentData()
        {
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);

            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No Appointment with ID = " + _TestAppointmentID.ToString(),
               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }

            //the Test fees in the Update Not loded
            lblFees.Text = _TestAppointment.PaidFees.ToString();

            // If the current date and time is earlier than _TestAppointment.AppointmentDate, the result is negative.
            // If they are equal, the result is zero.
            // If the current date and time is later than _TestAppointment.AppointmentDate, the result is positive.

            if (DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate) < 0)
            {

                dtpTestDate.MinDate = DateTime.Now;
            }
            else
            {
                dtpTestDate.MinDate = _TestAppointment.AppointmentDate;
            }

            dtpTestDate.Value = _TestAppointment.AppointmentDate;


            if (_TestAppointment.RetakeTestApplicationID == -1)
            {
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
                gbRetakeTestInfo.Enabled = false;
            }
            else
            {
                lblRetakeAppFees.Text = _TestAppointment.RetakeTestAppInfo.PaidFees.ToString();
                lblRetakeTestAppID.Text = _TestAppointment.TestAppointmentID.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
               

            }
           
            return true;
        }


        private bool _HandleActiveTestAppointmentConstraint()
        {
            if ((_Mode == enMode.AddNew) && clsLocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(_LocalDrivingLicenseApplicationID, _TestTypeID))
            {

                lblUserMessage.Text = "Person Already have an active appointment for this test";
                btnSave.Enabled = false;
                gbRetakeTestInfo.Enabled = false;
                return false;
            }

            return true;
        }


        private bool _HandleAppointmentLockedConstraint()
        {
            //if appointment is locked that means the person already sat for this test
            //we cannot update locked appointment

            if (_TestAppointment.IsLocked)
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "Person already sat for the test, appointment loacked.";
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                return false;
            }
            else
            {
                lblUserMessage.Enabled = false;
            }
            return true;
        }


        private bool _HandlePrviousTestConstraint()
        {
            //we need to make sure that this person passed the prvious required test before apply to the new test.
            //person cannno apply for written test unless s/he passes the vision test.
            //person cannot apply for street test unless s/he passes the written test.

            switch (_TestTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    //in this case no required prvious test to pass.
                    lblUserMessage.Visible = false;
                    return true;

                case clsTestType.enTestType.WrittenTest:
                    if (!_LocalDrivingLicenseApplicationInfo.DoesPassTestType(clsTestType.enTestType.VisionTest))
                    {
                        lblUserMessage.Enabled = true;
                        lblUserMessage.Text = "Cannot Sechule, Vision Test should be passed first";
                        btnSave.Enabled=false;
                        dtpTestDate.Enabled=false;
                        return false;

                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }
                    return true;
                case clsTestType.enTestType.StreetTest:
                    if (!_LocalDrivingLicenseApplicationInfo.DoesPassTestType(clsTestType.enTestType.WrittenTest))
                    {
                        lblUserMessage.Enabled = true;
                        lblUserMessage.Text = "Cannot Sechule, Written Test should be passed first";
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;

                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }
                    return true;

                default:
                    return false;
            }


        }


        private bool _HandleRetakeApplication()
        {
            //this will decide to create a seperate application for retake test or not.
            // and will create it if needed , then it will linkit to the appoinment.
            if (_Mode == enMode.AddNew && _CreationMode == enCreationMode.RetakeTestSchedule)
            {
                clsApplications Application = new clsApplications();

                Application.ApplicantPersonID = _LocalDrivingLicenseApplicationInfo.ApplicantPersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID =(int) clsApplications.enApplicationTypeID.RetakeTest;
                Application.ApplicationStatus = clsApplications.enApplicationStatus.Completed;
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees =(float) clsApplicationType.Find((int)clsApplications.enApplicationTypeID.RetakeTest).ApplicationFees;
                Application.CreatedByUserID = clsGlobal.CurrentUser.UserID;

                if (!Application.Save())
                {
                    _TestAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _TestAppointment.RetakeTestApplicationID = Application.ApplicationID;
            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!_HandleRetakeApplication())
                return;


            _TestAppointment.TestTypeID = _TestTypeID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationInfo.LocalDrivingLicenseApplicationID;
            _TestAppointment.AppointmentDate = DateTime.Now;
            _TestAppointment.PaidFees = Convert.ToSingle(lblFees.Text);
            _TestAppointment.AppointmentDate = dtpTestDate.Value;
            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;


            if (_TestAppointment.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);



        }
    }

    }
