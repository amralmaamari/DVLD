using DvldDataBusinessLayer;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD
{
    
    public partial class frmAddUpdateLocalDrivingLicenseApplication : Form
    {
        

        enum enMode { AddNew = 0,Update=1};
        private enMode _Mode;
        private int _LocalDrivingLicenseApplicationID = -1;
        private int _SelectedPersonID = -1;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;


        public frmAddUpdateLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }



  
        public frmAddUpdateLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();    
            
            _Mode = enMode.Update;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }

        private void _FillLicenseClassesInComoboBox()
        {
            DataTable dtLicenseClasses=clsLicenseClass.GetAllLicenseClasses();
            if (dtLicenseClasses == null)
                return;

            foreach (DataRow Row in dtLicenseClasses.Rows)
            {
                cmbLicenseClass.Items.Add(Row["ClassName"].ToString());
            }
        }

        private void _ResetDefaultValue()
        {

            _FillLicenseClassesInComoboBox();
            if(_Mode == enMode.AddNew)
            {
                lblTitle.Text = "New Local Driving License Application";
                this.Text = "New Local Driving License Application";
                ctrlPersonCardWithFilter1.FilterEnable = true;
                ctrlPersonCardWithFilter1.FilterFoucs();
                _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
                tpApplicationInfo.Enabled = false;
                btnSave.Enabled = false;

                cmbLicenseClass.SelectedIndex = 2;
                lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
                lblApplicationFees.Text = clsApplicationType.Find((int)clsApplications.enApplicationTypeID.NewDrivingLicense).ApplicationFees.ToString();
                lblCreatedBy.Text = clsGlobal.CurrentUser.Username;


            }
            else
            {
                //Complete the other Process Here
                lblTitle.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";

                btnSave.Enabled = true;
                tcLocalDrivingLicensesApplication.Enabled = true;
            }

       
           

           
        }

        private void _LoadData()
        {
            ctrlPersonCardWithFilter1.FilterEnable = false;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Application with ID = " + _LocalDrivingLicenseApplicationID, "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }

            ctrlPersonCardWithFilter1.LoadPersonInfo(_LocalDrivingLicenseApplication.ApplicantPersonID);
            lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = clsFormat.DateToShort(_LocalDrivingLicenseApplication.ApplicationDate);
            cmbLicenseClass.SelectedIndex = cmbLicenseClass.FindString(clsLicenseClass.Find(_LocalDrivingLicenseApplication.LicenseClassID).ClassName);
            lblApplicationFees.Text = _LocalDrivingLicenseApplication.PaidFees.ToString();

            lblCreatedBy.Text = clsUser.FindByUserID(_LocalDrivingLicenseApplication.CreatedByUserID).ToString();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {


            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcLocalDrivingLicensesApplication.SelectedTab = tcLocalDrivingLicensesApplication.TabPages["tpApplicationInfo"];
                return;
            }

            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {
                
                tcLocalDrivingLicensesApplication.SelectedTab = tcLocalDrivingLicensesApplication.TabPages["tpApplicationInfo"];
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFoucs();
            }
        }

        private void frmNewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _ResetDefaultValue();


            if(_Mode == enMode.Update)
            {
                _LoadData();
            }


        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            //first of all will check if there is not ALready License
            int LicenseClassID =(int) clsLicenseClass.Find(cmbLicenseClass.Text).LicenseClassID;

            int ActiveApplicationID= clsApplications.GetActiveApplicationIDForLicenseClass(_SelectedPersonID, clsApplications.enApplicationTypeID.NewDrivingLicense,(clsLicenseClass.enLicenseClasse) LicenseClassID);

            if(ActiveApplicationID != -1)
            {
                 MessageBox.Show($"Choose another License Class, the selected Person Already have an active application for the selected class with id-{ActiveApplicationID}",
                     "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                cmbLicenseClass.Focus();    
                return;

            }

            //check if user already have issued license of the same driving  class.

           if(clsLicense.IsLicenseExistByPersonID(_SelectedPersonID, LicenseClassID))
            {
                MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LocalDrivingLicenseApplication.ApplicantPersonID = _SelectedPersonID;
            _LocalDrivingLicenseApplication.ApplicationDate = DateTime.Now;
            _LocalDrivingLicenseApplication.ApplicationTypeID = (int)clsApplications.enApplicationTypeID.NewDrivingLicense;
            _LocalDrivingLicenseApplication.ApplicationStatus = clsApplications.enApplicationStatus.New;
            _LocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;
            _LocalDrivingLicenseApplication.PaidFees = (float)clsApplicationType.Find(_LocalDrivingLicenseApplication.ApplicationTypeID).ApplicationFees;
            _LocalDrivingLicenseApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _LocalDrivingLicenseApplication.LicenseClassID = LicenseClassID;


            if (_LocalDrivingLicenseApplication.Save())
            {
                lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                _Mode = enMode.Update;
                lblTitle.Text = "Update Local Driving License Application";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

           
               
           

          

            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _SelectedPersonID = obj;
        }

        //what is the differnet bet (Activated VS Loaded)
        // Activated it come afer loded first will load and then Activated(when start to open the form)
        // Loades(Perper before Open the form)
        private void frmAddUpdateLocalDrivingLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFoucs();
        }
    }
}
