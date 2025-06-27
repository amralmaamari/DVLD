using DVLD.People.Control;
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
    public partial class frmAddUpdateCarRentalApplication : Form
    {
        enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        private int _CarRentalApplicationID = -1;
        private int _SelectedCustomerID = -1;
        clsCarRentalApplication _CarRentalApplication;

        enum enRentStatus { NoActive=0, Active=1};
        enRentStatus _RentStatus=enRentStatus.NoActive;
        public frmAddUpdateCarRentalApplication()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdateCarRentalApplication(int CarRentalApplicationID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _CarRentalApplicationID = CarRentalApplicationID;
        }

        private void _FillCarNamesInComoboBox()
        {
            DataTable dtCarNames = clsCar.GetAllCars();
            if (dtCarNames == null)
                return;

            foreach (DataRow Row in dtCarNames.Rows)
            {
                cmbCarModel.Items.Add(Row["Model"].ToString());
            }
        }


        private void _FillRentTypesInComoboBox()
        {
            DataTable dtRentTypes = clsRentType.GetAllRentTypes();
            if (dtRentTypes == null)
                return;

            foreach (DataRow Row in dtRentTypes.Rows)
            {
                cmbRentType.Items.Add(Row["Name"].ToString());
            }
        }


        private void _ResetDefaultValue()
        {

            _FillCarNamesInComoboBox();
            _FillRentTypesInComoboBox();


            dtpStartDate.MinDate = DateTime.Now;
            dtpEndDate.MinDate = DateTime.Now.AddDays(1);

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "New Add Car Rental Application";
                this.Text = "New Add Car Rental Application";
                ctrlCustomerCardFilter1.EnableFilter = true;
                ctrlCustomerCardFilter1.FilterFoucs();
                _CarRentalApplication = new clsCarRentalApplication();
                tpApplicationInfo.Enabled = false;
                btnSave.Enabled = false;

                cmbCarModel.SelectedIndex = 0;
                cmbRentType.SelectedIndex = 0;

                dtpStartDate.Value = DateTime.Now;
                dtpEndDate.Value = DateTime.Now.AddMonths(1);
                lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);

                int RentTypeID =(int) clsRentType.Find((cmbRentType.Text)).RentTypeID;
                lblApplicationFees.Text = clsRentType.Find((clsRentType.enRentType)RentTypeID).RentTypeFees.ToString();
                //lblApplicationFees.Text = clsApplicationType.Find((int)clsApplications.enApplicationTypeID.NewDrivingLicense).ApplicationFees.ToString();
                lblCreatedBy.Text = clsGlobal.CurrentUser.Username;
                txtNotes.Text = string.Empty;
            }
            else
            {
                //Complete the other Process Here
                lblTitle.Text = "Update Car Rental Application";
                this.Text = "Update Car Rental Application";

                btnSave.Enabled = true;
                tcCarRentalApplication.Enabled = true;
            }





        }


        private void _LoadData()
        {
            ctrlCustomerCardFilter1.EnableFilter = false;
            _CarRentalApplication = clsCarRentalApplication.FindByCarRentalApplicationID(_CarRentalApplicationID);

            if (_CarRentalApplication == null)
            {
                MessageBox.Show("No Application with ID = " + _CarRentalApplicationID, "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }

            ctrlCustomerCardFilter1.LoadCustomerInfo(_CarRentalApplication.CustomerID);
            lblCarRentalApplicationID.Text = _CarRentalApplication.CarRentalApplicationID.ToString();
            cmbCarModel.Text = clsCar.GetCarInfoByID(_CarRentalApplication.CarID).Model;
            if (_Mode == enMode.Update)
            {
                dtpStartDate.MinDate = _CarRentalApplication.StartDateTime;
                dtpStartDate.Value = _CarRentalApplication.StartDateTime;                
            }
            else
                dtpStartDate.Value = _CarRentalApplication.StartDateTime;

            dtpEndDate.Value = _CarRentalApplication.EndDateTime;
            txtCurrentLocation.Text = _CarRentalApplication.CurrentLocation;
            cmbRentType.Text = clsRentType.Find((clsRentType.enRentType)_CarRentalApplication.RentTypeID).RentTypeName;
            lblApplicationFees.Text= _CarRentalApplication.PaidFees.ToString();
            lblCreatedBy.Text =_CarRentalApplication.CreatedByUserID.ToString();
            lblApplicationDate.Text = clsFormat.DateToShort(_CarRentalApplication.CreatedDate);
            txtNotes.Text= _CarRentalApplication.Notes.ToString();
          
        }


        private void frmAddUpdateCarRentalApplication_Load(object sender, EventArgs e)
        {
            _ResetDefaultValue();
            if (_Mode == enMode.Update)
            {
                _LoadData();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //here i have to check if the Number of car not equal to zero and when i rent car have to 
            // decrease the number 

            clsCarAvailability CarAvailability = clsCarAvailability.GetCarAvailabilityInfoByCarID(clsCar.GetCarInfoByModel(cmbCarModel.Text).CarID);

            if (!(CarAvailability.AvailableCars >0))
            {
                MessageBox.Show("This car not available, Choose diffrent car", "Not available", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }





          

            //will make the EndDate after the Startdate
            if (!ValidateChildren())
                return;


            _CarRentalApplication.CarID = clsCar.GetCarInfoByModel(cmbCarModel.Text).CarID;
            _CarRentalApplication.CustomerID = ctrlCustomerCardFilter1.CustomerID;
            _CarRentalApplication.StartDateTime = dtpStartDate.Value;
            _CarRentalApplication.EndDateTime = dtpEndDate.Value;
            _CarRentalApplication.CurrentLocation=txtCurrentLocation.Text.Trim();
            _CarRentalApplication.RentTypeID =(int) clsRentType.Find(cmbRentType.Text).RentTypeID;
            _CarRentalApplication.CreatedByUserID =clsGlobal.CurrentUser.UserID;
            _CarRentalApplication.CreatedDate = DateTime.Now;
            _CarRentalApplication.PaidFees =(float) clsRentType.Find(cmbRentType.Text).RentTypeFees;
            _CarRentalApplication.LastStatusDate = DateTime.Now;
            _CarRentalApplication.Notes = txtNotes.Text.Trim();
            _CarRentalApplication.IsActive = true;

            if (_CarRentalApplication.Save())
            {
                lblCarRentalApplicationID.Text = _CarRentalApplication.CarRentalApplicationID.ToString();
                _Mode = enMode.Update;
                lblTitle.Text = "Update Car Rental Application";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcCarRentalApplication.SelectedTab = tcCarRentalApplication.TabPages["tpApplicationInfo"];
                return;
            }


            if (ctrlCustomerCardFilter1.CustomerID == -1)
            {
                MessageBox.Show("Please Select  Correct  Customer", "Select a Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlCustomerCardFilter1.FilterFoucs();
                return;
            }
           

            if (!ctrlCustomerCardFilter1.CustomerInfo.IsActive)
            {
                MessageBox.Show("Customer Is Not Active", "Not Active", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlCustomerCardFilter1.ResetCustomerCardFilterInfo();
                return;
            }

            _SelectedCustomerID = ctrlCustomerCardFilter1.CustomerID;
            tcCarRentalApplication.SelectedTab = tcCarRentalApplication.TabPages["tpApplicationInfo"];
            btnSave.Enabled = true;
            tpApplicationInfo.Enabled = true;

        }

    

        private void dtpEndDate_Validating(object sender, CancelEventArgs e)
        {
            //here will make it atlest one day after
            int Compire = DateTime.Compare(dtpStartDate.Value, dtpEndDate.Value);
            //same Date
            if (Compire == 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(dtpEndDate, "The StartDate Should Not Be Same EndDate");
            }
            else if (Compire > 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(dtpStartDate, "The StartDate Should  Be Less Then  EndDate ");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(dtpEndDate, null);
                errorProvider1.SetError(dtpStartDate, null);
            }
            
        }
    }
}
