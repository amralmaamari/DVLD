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
    public partial class ctrlCarCard : UserControl
    {
        clsCar _Car;

        private int _CarID;

        public int SelectedCarID
        {
            
            get => _CarID;
        }

        public clsCar SelectedCarInfo
        {
            get => _Car;
        }
        public ctrlCarCard()
        {
            InitializeComponent();
        }


        public void ResetCarInfo()
        {
            lblCarID.Text = "";
            lblCarType.Text = "";
            lblModel.Text = "";
            lblYear.Text = "";
            lblColor.Text = "";
            lblEngine.Text = "";
            lblTransmissionType.Text = "";
            lblUserID.Text = "";
            lblDateOfCreated.Text = "";
        }
        private void _FillCarInfo()
        {
            lblCarID.Text=_Car.CarID.ToString();
            _CarID = _Car.CarID;
            lblCarType.Text=_Car.CarTypeInfo.Name;
            lblModel.Text=_Car.Model.ToString();
            lblYear.Text=clsFormat.DateToShort(_Car.Year);
            lblColor.Text = _Car.Color;
            lblEngine.Text=_Car.Engine.ToString();
            lblTransmissionType.Text = _Car.TransmissionTypeInfo.Name;
            lblUserID.Text=_Car.UserIDCreated.ToString();
            lblDateOfCreated.Text = clsFormat.DateToShort(_Car.DateOfCreated);
        }

        public void LoadCarInfo(int CarID)
        {
            _Car = clsCar.GetCarInfoByID(CarID);

            if( _Car == null ) {
                MessageBox.Show($"There is no Car With ID {CarID}", "Not Found",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResetCarInfo();
                return;
            }

            _FillCarInfo();

        }
    }
}
