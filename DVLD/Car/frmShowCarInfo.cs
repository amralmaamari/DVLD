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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace DVLD
{
    public partial class frmShowCarInfo : Form
    {

        private int _CarID;
        private clsCar _Car;
        public frmShowCarInfo(int carID)
        {
            InitializeComponent();
            this._CarID = carID;
        }

        private void frmShowCarInfo_Load(object sender, EventArgs e)
        {
            _Car = clsCar.GetCarInfoByID(_CarID);

            if (_Car == null)
            {
                MessageBox.Show($"There is no Car With ID {_CarID}", "Not Found",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ctrlCarCard1.LoadCarInfo(_CarID);
        }
    }
}
