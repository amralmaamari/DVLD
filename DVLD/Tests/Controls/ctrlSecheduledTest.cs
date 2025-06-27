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

namespace DVLD
{
    public partial class ctrlSecheduledTest : UserControl
    {
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplicationInfo;
        public clsTestType.enTestType  TestTypeID
        {
            get { return _TestTypeID; }
            set {
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

        //is present clsTest
        private int _TestID = -1;

        private clsTestAppointment _TestAppointmentInfo;
        private int _TestAppointmentID=-1;

        public int TestAppointmentID
        {
            get { return _TestAppointmentID; }
        }

        public int TestID
        {
            get { return _TestID; }
        }

        public ctrlSecheduledTest()
        {
            InitializeComponent();
        }


        private void _LoadScheduledTestData()
        {
            lblDLAppID.Text = _LocalDrivingLicenseApplicationInfo.LocalDrivingLicenseApplicationID.ToString();
            lblDclass.Text = _LocalDrivingLicenseApplicationInfo.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplicationInfo.ApplicantFullName;
            lblTrial.Text = _LocalDrivingLicenseApplicationInfo.TotalTrialsPerTest(_TestAppointmentInfo.TestTypeID).ToString();
            lblDate.Text =clsFormat.DateToShort(_TestAppointmentInfo.AppointmentDate);
            lblFees.Text= _TestAppointmentInfo.PaidFees.ToString();

            lblTestId.Text=(_TestAppointmentInfo.TestID == -1)? "Not Taken Yet":_TestAppointmentInfo.TestID.ToString();




        }

        public void LoadInfo(int TestAppointmentID)
        {
            _TestAppointmentID = TestAppointmentID;



            _TestAppointmentInfo = clsTestAppointment.Find(_TestAppointmentID);

            _LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_TestAppointmentInfo.LocalDrivingLicenseApplicationID);
            if(_TestAppointmentInfo == null)
            {
                MessageBox.Show("Error: No  Appointment ID = " + _TestAppointmentID.ToString(),
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _TestAppointmentID = -1;
                return;
            }

            _TestID = _TestAppointmentInfo.TestID;

            _LoadScheduledTestData();
            
        }



    }
}
