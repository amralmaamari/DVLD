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
    public partial class frmShowCustomerInfo : Form
    {
        private int _CustomerID;
        public frmShowCustomerInfo(int CustomerID)
        {
            InitializeComponent();
            _CustomerID=CustomerID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowCustomerInfo_Load(object sender, EventArgs e)
        {
            ctrlCustomerInfo1.LoadCustomerInfo(_CustomerID);
        }
    }
}
