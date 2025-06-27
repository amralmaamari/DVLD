using DVLD.Global_Classes;
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
    public partial class frmMain : Form
    {
        frmLogin frmLogin;
        public frmMain(frmLogin frmLogin)
        {
            InitializeComponent();
            this.frmLogin = frmLogin;   
        }

        private void peopleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmListPeople managePeople = new frmListPeople();
            managePeople.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string loggerMessage = String.Concat("UserID:", clsGlobal.CurrentUser.UserID, "is Logout ,At Time:", DateTime.Now);
            clsLogging.AddNewLogging(this.Name, loggerMessage, clsLogging.enLevel.Information);

            clsGlobal.CurrentUser = null;
            frmLogin.ShowDialog();
            this.Close();
        }

        private void uSerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListUsers frm = new frmListUsers();
            frm.ShowDialog();
        }



        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {

            clsGlobal.CurrentUser = null;
            frmLogin.ShowDialog();
        }

        private void mangeApplictionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListApplicationTypes frm = new frmListApplicationTypes();
            frm.ShowDialog();
        }

        private void manageTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestTypes frm = new frmListTestTypes();
            frm.ShowDialog();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void localDrivingLicensesApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicenseApplications frm =new frmListLocalDrivingLicenseApplications();
            frm.ShowDialog();
        }

        private void reTakeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListLocalDrivingLicenseApplications frm = new frmListLocalDrivingLicenseApplications();
            frm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLocalDrivingLicense frm =new frmRenewLocalDrivingLicense();
            frm.ShowDialog();
        }

        private void replacementForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplacementForDamagedLicense frm=new frmReplacementForDamagedLicense();
            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDrivers frm = new frmListDrivers();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm =new frmDetainLicense();
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDetaintedLicenses frm = new frmListDetaintedLicenses();
            frm.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
        }

        private void intrnationalLicensesApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListInternatioanlLicenseApplications frm=
                new frmListInternatioanlLicenseApplications();

            frm.ShowDialog();
        }

        private void addNewCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateCustomer frm = new frmAddUpdateCustomer();
            frm.ShowDialog();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCustomerList frm =new frmCustomerList();
            frm.ShowDialog();
        }

        private void addNewCustomerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmAddUpdateCustomer frm = new frmAddUpdateCustomer();
            frm.ShowDialog();
        }

        private void manageRentTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListRentTypes frm = new frmListRentTypes();
            frm.ShowDialog(this);
        }

       

        private void manageTransmissionTypesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmListTransmissionTypes frm = new frmListTransmissionTypes();
            frm.ShowDialog();
        }

        private void manageListCarTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListCarTypes frm = new frmListCarTypes();
            frm.ShowDialog();
        }

        private void listCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCustomerList frm = new frmCustomerList();
            frm.ShowDialog();
        }

        private void addNewCarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateCar frm=new frmAddUpdateCar();
            frm.ShowDialog();
        }

        private void listCarsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListCar frm = new frmListCar();
            frm.ShowDialog();
        }

        private void NewCararRentalApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateCarRentalApplication frm = new frmAddUpdateCarRentalApplication();
            frm.ShowDialog();
        }

        private void carRentalApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListCarRentalApplication frm = new frmListCarRentalApplication();
            frm.ShowDialog();
        }

        private void SetMenuItemsEnabled(bool isEnabled, params ToolStripMenuItem[] menuItems)
        {
            foreach(ToolStripMenuItem item in menuItems)
            {
                item.Enabled= isEnabled;
            }
        }

        private void _PermissionForCreateAppLicense()
        {
            SetMenuItemsEnabled(false, carRentalApplicationsToolStripMenuItem
                , RentalServicesToolStripMenuItem
                , customerToolStripMenuItem1
                , carToolStripMenuItem
                , uSerToolStripMenuItem);
          
        }

        private void _PermissionForCreateAndRentCar()
        {

            SetMenuItemsEnabled(false, drivingToolStripMenuItem
                , localDrivingLicensesApplicationsToolStripMenuItem
                , intrnationalLicensesApplicationsToolStripMenuItem
                , detToolStripMenuItem
                , mangeApplictionToolStripMenuItem
                , manageTestToolStripMenuItem
                , peopleToolStripMenuItem
                , uSerToolStripMenuItem);

        }

        private void _PermissionForCreateUserAndManageUsers()
        {
           


            SetMenuItemsEnabled(false,
                peopleToolStripMenuItem
               , applicationsToolStripMenuItem
               , driversToolStripMenuItem
               , manageApplicationToolStripMenuItem
             );


            uSerToolStripMenuItem.Enabled = true;


        }

        private void _SetPermissions()
        {
            switch (clsGlobal.CurrentUser.Permission)
            {
                case clsUser.enPermissions.eAll:
                    return;

                case clsUser.enPermissions.pCreateAppLicense:
                    _PermissionForCreateAppLicense();
                    break;
                case clsUser.enPermissions.pCreateAndRentCar:
                    _PermissionForCreateAndRentCar();
                    break;
                case clsUser.enPermissions.pCreateUserAndManageUsers:
                    _PermissionForCreateUserAndManageUsers();
                    break;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            _SetPermissions();
        }
    }
}
