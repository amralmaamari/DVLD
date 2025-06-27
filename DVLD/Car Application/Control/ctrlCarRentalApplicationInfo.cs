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
    public partial class ctrlCarRentalApplicationInfo : UserControl
    {

        private clsCarRentalApplication _CarRentalApplication;
        private int _CarRentalApplicationID= -1;


        public int CarRentalApplicationID
        {
            get => _CarRentalApplicationID;
        }
        public ctrlCarRentalApplicationInfo()
        {
            InitializeComponent();
        }

        public void _ResetCarRentalApplicationInfo()
        {
            _CarRentalApplicationID = -1;
            ctrlCarRentalApplicationCard1.ResetCarRentalApplicationInfo();
            ctrlRentalApplicationCard1.ResetRentalApplicationInfo();
           
        }

        private void _FillCarRentalApplicationInfo()
        {
            ctrlCarRentalApplicationCard1.LoadCarRentalApplicationInfo(_CarRentalApplicationID);
            ctrlRentalApplicationCard1.LoadRentalApplicationInfo(ctrlCarRentalApplicationCard1.RentalApplicationID);
     
        }




        public bool LoadCarRentalApplicationInfoByID(int CarRentalApplicationID)
        {

            _CarRentalApplication = clsCarRentalApplication.FindByCarRentalApplicationID(CarRentalApplicationID);

            if (_CarRentalApplication == null)
            {
                _ResetCarRentalApplicationInfo();
                MessageBox.Show("No Application with Car Rental  ApplicationID = " + CarRentalApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            _CarRentalApplicationID = CarRentalApplicationID;
            _FillCarRentalApplicationInfo();

            return true;

        }
    }
}
