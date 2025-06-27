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
    public partial class frmInternationalDriveInfo : Form
    {
        private int _InternationalLicenseID = -1;


        public frmInternationalDriveInfo(int InternationalLicenseID)
        {
            InitializeComponent();
            _InternationalLicenseID=InternationalLicenseID;
           
        }

        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmInternationalDriveInfo_Load(object sender, EventArgs e)
        {
            ctrlDriverInternationalLicenseInfo1.LoadLicenseCardInfo(_InternationalLicenseID);
        }
    }
}
