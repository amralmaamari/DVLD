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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD
{
    public partial class frmCarRentalApplicationInfo : Form
    {

        private int _CarRentalApplictionID;
        public frmCarRentalApplicationInfo(int CarRentalApplictionID)
        {
            InitializeComponent();
            _CarRentalApplictionID = CarRentalApplictionID;
        }

        private void frmTry_Load(object sender, EventArgs e)
        {
            ctrlCarRentalApplicationInfo1.LoadCarRentalApplicationInfoByID(_CarRentalApplictionID);
        }
    }
}

