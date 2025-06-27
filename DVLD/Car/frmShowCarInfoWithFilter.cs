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
    public partial class frmShowCarInfoWithFilter : Form
    {
        public frmShowCarInfoWithFilter()
        {
            InitializeComponent();
        }

        private void frmShowCarInfo_Load(object sender, EventArgs e)
        {
            ctrlCarCardWithFilter1.FoucsFilterText = true;
        }

        private void ctrlCarCardWithFilter1_OnSelectedCar(int obj)
        {
            MessageBox.Show(obj.ToString());
        }
    }
}
