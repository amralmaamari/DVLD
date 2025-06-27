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
    public partial class ctrlDriverLicenses : UserControl
    {
        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }

        private int _DriverID = -1;
        private clsDriver _Driver;
        private DataTable _dtDriverLocalLicensesHistory;
        private DataTable _dtDriverInternationalLicensesHistory;
        public int DriverID
        {
            get { return _DriverID; }
        }

        //here Whn you make the Internationl Class Logic and Bussines so apply
        private void _LoadLocalLicenseInfo()
        {

            _dtDriverLocalLicensesHistory = clsDriver.GetLicenses(DriverID);
            
            dgvLocal.DataSource= _dtDriverLocalLicensesHistory;

            if(dgvLocal.Rows.Count > 0 ) {
                dgvLocal.Columns[0].HeaderText = "Lic.ID";
                dgvLocal.Columns[1].HeaderText = "App.ID";

                dgvLocal.Columns[2].HeaderText = "Class Name";
                dgvLocal.Columns[2].Width = 150;

                dgvLocal.Columns[3].HeaderText = "Issue Date";
                dgvLocal.Columns[3].Width = 120;

                dgvLocal.Columns[4].HeaderText = "Exipration Date";
                dgvLocal.Columns[4].Width = 120;

                dgvLocal.Columns[5].HeaderText = "Is Active";
            }
            lblLocalRecordCount.Text = dgvLocal.Rows.Count.ToString();


        }


        private void _LoadInternationalLicenseInfo()
        {

            _dtDriverInternationalLicensesHistory = clsInternationalLicense.GetDriverInternationalLicenses(DriverID) ;

            dgvInternational.DataSource = _dtDriverInternationalLicensesHistory;

            if (dgvInternational.Rows.Count > 0)
            {
                dgvInternational.Columns[0].HeaderText = "Int.License ID";
                dgvInternational.Columns[0].Width = 120;

                dgvInternational.Columns[1].HeaderText = "Application ID";
                dgvInternational.Columns[1].Width = 120;


              

                dgvInternational.Columns[2].HeaderText = "L.License ID";
                dgvInternational.Columns[2].Width = 120;

                dgvInternational.Columns[3].HeaderText = "Issue Date";
                dgvInternational.Columns[3].Width = 120;

                dgvInternational.Columns[4].HeaderText = "Expiration Date";
                dgvInternational.Columns[4].Width = 120;

                dgvInternational.Columns[5].HeaderText = "Is Active";
                dgvInternational.Columns[5].Width = 120;
               
            }
            lblInternationalRecordCount.Text = dgvInternational.Rows.Count.ToString();

           

        }
        public void LoadInfo(int DriverID)
        {
            _DriverID=DriverID;
             _Driver = clsDriver.FindByDriverID(DriverID);

            if (_Driver == null) {
                MessageBox.Show($"Driver ID: {DriverID} Is not found it",
                    "Not Found",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();
        



            }


        public void LoadInfoByPersonID(int PersonID)
        {
            _Driver = clsDriver.FindByPersonID(PersonID);

            if (_Driver == null)
            {
                MessageBox.Show($"Driver ID: {DriverID} Is not found it",
                    "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _DriverID = _Driver.DriverID;


            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();


        }

        public void Clear()
        {
            _dtDriverLocalLicensesHistory.Clear();
            _dtDriverInternationalLicensesHistory.Clear();
        }

        private void showLocalLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID =(int) dgvLocal.CurrentRow.Cells[0].Value;
            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID = (int)dgvInternational.CurrentRow.Cells[0].Value;
            frmInternationalDriveInfo frm = new frmInternationalDriveInfo(InternationalLicenseID);
            frm.ShowDialog();
        }
    }
}
