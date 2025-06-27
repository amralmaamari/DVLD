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
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {
        // Define a custom event handler delegate with parameters
        public event Action<int> OnLicenseSelected;
        // Create a protected method to raise the event with a parameter
        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;
            if (handler != null)
            {
                handler(LicenseID);
            }
        }

        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();

        }



        private bool _ShowFindLicense = true;

        public bool ShowFindLicense
        {
            get { return _ShowFindLicense; }

            set { _ShowFindLicense = value;
                btnFind.Enabled = _ShowFindLicense;
                }
        }

        private bool _FilterEnable = true;
        public bool EnableFilter
        {
            get {  return _FilterEnable; } 
            set { 
                _FilterEnable = value;
                gbFilter.Enabled = _FilterEnable;
            }
        }

        
        private int _LicenseID=-1;
        public int LicenseID
        {
            get { return ctrlDriverLicenseInfo1.LicenseID; }
        }
        public clsLicense LicenseInfo
        {
            get { return ctrlDriverLicenseInfo1.SelectedLicenseInfo; }
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
            }
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        public void FoucsOnLicensID()
        {

            txtLicenseID.Focus();
        }

       

        private void ctrlDriverLicenseInfoWithFilter_Load(object sender, EventArgs e)
        {
            FoucsOnLicensID();
        }


        public void LoadLicenseInfo(int LicenseID)
        {
            txtLicenseID.Text=LicenseID.ToString();

            clsLicense license = clsLicense.GetLicenseInfoByID(LicenseID);
            if (license == null)
            {
                MessageBox.Show($"There is no License ID = {LicenseID}", "Wrong License",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);                
                FoucsOnLicensID();
                return;
            }


            ctrlDriverLicenseInfo1.LoadLicenseCardInfo(LicenseID);
            _LicenseID= ctrlDriverLicenseInfo1.LicenseID;
          

            // اي شخص يكون منادية من خارج هذي الفونشكن  والفلتر مايكون مغلق
            if (OnLicenseSelected != null && _FilterEnable)
                //Raise the Event With Parmeter
                OnLicenseSelected(_LicenseID);
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FoucsOnLicensID();
                return;
            }


            _LicenseID = int.Parse(txtLicenseID.Text.Trim());
            LoadLicenseInfo(_LicenseID);

        }

        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLicenseID.Text))
            {
                e.Cancel=true;
                errorProvider1.SetError(txtLicenseID, "This Con't be Empty");
            }
            else
            {
                errorProvider1.SetError(txtLicenseID, null);
                
            }
        }
    }
}
