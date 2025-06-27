using DvldDataBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmTakeTest : Form
    {
      

       private int _TestAppointmentID = -1;
        private clsTestAppointment _TestAppointmentInfo;

        clsTestType.enTestType _TestTypeID;

        private clsTest _TestInfo;


        public frmTakeTest(int TestAppointmentID,clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            this._TestAppointmentID = TestAppointmentID;
            this._TestTypeID = TestTypeID;


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        private void btnSave_Click(object sender, EventArgs e)
        {
           

            if (MessageBox.Show("Are you sure you want to save? After that you cannot change the Pass/Fail results after you save?.",
                        "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No
               )
            {
                return;
            }
               

            _TestInfo.TestAppointmentID = _TestAppointmentID;
            _TestInfo.TestResult = (rbPass.Checked);
            _TestInfo.Notes = (string.IsNullOrEmpty(rtxNotes.Text) ? "" : rtxNotes.Text.Trim());
            _TestInfo.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (_TestInfo.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);






        }

        private void _SetWritenTest()
        {
            if (_TestTypeID == clsTestType.enTestType.WrittenTest)
            {
                llTakeTest.Visible = true;
                rbPass.Enabled = false;
                rbFail.Enabled = false;
                rbPass.Checked = false;
                rbFail.Checked = false;
                btnSave.Enabled = false;
            }
        }
        private void frmTakeTest_Load(object sender, EventArgs e)
        {

            //_TestAppointmentInfo = clsTestAppointment.Find(_TestAppointmentID);

            //if (_TestAppointmentInfo == null)
            //{
            //    MessageBox.Show("Error: No  Appointment ID = " + _TestAppointmentID.ToString(),
            //       "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    _TestAppointmentID = -1;
            //    return;
            //}


            ctrlSecheduledTest1.TestTypeID=_TestTypeID;
            ctrlSecheduledTest1.LoadInfo(_TestAppointmentID);

            if (ctrlSecheduledTest1.TestAppointmentID == -1)           
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;


            int _TestID = ctrlSecheduledTest1.TestID;
            
            if(_TestID != -1)
            {
                _TestInfo=clsTest.Find(_TestID);

                if(_TestInfo.TestResult) {
                    rbPass.Checked=true;
                }else                
                    rbFail.Checked=true;
                    rtxNotes.Text = _TestInfo.Notes;

                lblUserMessage.Visible = true;
                rbFail.Enabled = false;
                rbPass.Enabled = false;


                
            }else
            {

                _TestInfo = new clsTest();

                _SetWritenTest();
            }


        }
        private void WrittenTestResult_DataBack(object sender, bool IsPassTest,string Notes)
        {
            if (IsPassTest == true)
            {
                rbPass.Checked =true ;

            }else
            {
                rbFail.Checked =true ;
            }
            rtxNotes.Text = Notes;
            btnSave.Enabled = true;
            llTakeTest.Visible = false ;
        }

        private void llTakeTest_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmWrittenTest frm = new frmWrittenTest();
            frm.DataBack += WrittenTestResult_DataBack;
            frm.Show();

        }
    }
}
