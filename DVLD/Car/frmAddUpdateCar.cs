using DvldDataBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DVLD
{
    public partial class frmAddUpdateCar : Form
    {


        enum enMode { AddNew=0, Update=1 }
        enMode Mode = enMode.AddNew;
        private int _CarID;
        private clsCar _Car;
        public frmAddUpdateCar()
        {
            InitializeComponent();
            Mode = enMode.AddNew;
        }

        public frmAddUpdateCar(int CarID)
        {
            InitializeComponent();
            _CarID = CarID;
            Mode = enMode.Update;
        }

        private void _FillCarTypeInComboBox()
        {
            DataTable CarTypes= clsCarTypes.GetAllCarTypes();

            cmbCarTypes.Items.Clear();

            foreach (DataRow row in CarTypes.Rows)
            {
                string CarTypeName = row[1].ToString();
                cmbCarTypes.Items.Add(CarTypeName);
            }
            cmbCarTypes.SelectedIndex = 0;
        }


        private void _FillTransmissionTypeInComboBox()
        {
            DataTable TransmissionTypes = clsTransmissionTypes.GetAllTransmissionTypes();

            cmbTransmissionType.Items.Clear();

            foreach (DataRow row in TransmissionTypes.Rows)
            {
                string TransmissionTypeName = row[1].ToString();
                cmbTransmissionType.Items.Add(TransmissionTypeName);
            }
            cmbTransmissionType.SelectedIndex = 0;
        }

        private void _ResetDefaultValue()
        {
            _FillCarTypeInComboBox();
            _FillTransmissionTypeInComboBox();

            if (Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Car";
                _Car = new clsCar();
                this.Text = lblTitle.Text;
            }
            else
            {
                lblTitle.Text = "Update Car";
                this.Text = lblTitle.Text;
            }

            lblCarID.Text = "N/A";
            txtModel.Text= string.Empty;
            dtpYear.Text= clsFormat.DateToShort(DateTime.Now);
            txtEngine.Text= string.Empty;
           // lblUserIDCreated.Text= clsGlobal.CurrentUser.UserID.ToString();

           

        }
        private void _SetColor()
        {
            Color temp = Color.FromName( btnColorPicked.Tag.ToString());
            if(temp.IsNamedColor)
            {
                btnColorPicked.BackColor = temp;
               
                return;

            }
            else
                btnColorPicked.BackColor =Color.Black;

         

        }

        private void _LodaData()
        {
            _Car = clsCar.GetCarInfoByID(_CarID);
            if (_Car == null)
            {
                MessageBox.Show("This form will be closed because No Car with ID = " + _CarID, "Car Not Found");
                this.Close();

                return;
            }


              lblCarID.Text = _Car.CarID.ToString();
              cmbCarTypes.Text=_Car.CarTypeInfo.Name; 
              txtModel.Text=_Car.Model.ToString();
              dtpYear.Value = _Car.Year;
              btnColorPicked.Tag= _Car.Color;
              cmbTransmissionType.Text=_Car.TransmissionTypeInfo.Name;
              txtEngine.Text=_Car.Engine.ToString();
              lblUserIDCreated.Text = _Car.UserIDCreated.ToString();

              _SetColor();

            MessageBox.Show($"BacgroundColor{_Car.Color}");


        }


        private void txt_Validating(object sender, CancelEventArgs e)
        {
            TextBox txtbox=(TextBox) sender;

            if (string.IsNullOrEmpty(txtbox.Text))
            {
                errorProvider1.SetError(txtbox, "The Filed Should Not Empty");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtbox, null);
                e.Cancel = false;
            }
        }

        private void frmAddUpdateCar_Load(object sender, EventArgs e)
        {
            _ResetDefaultValue();

            if (Mode == enMode.Update)
            {
                _LodaData();
            }




        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {

                //here the logic

                Color colorName = Color.FromName(colorDialog1.Color.Name);
                
                Color color = colorDialog1.Color;

                btnColorPicked.Tag = color;
                
                _Car.Color = colorName.Name;
             
            }
           

            if (btnColorPicked.Tag != null)
            {
                btnColorPicked.BackColor =(Color) btnColorPicked.Tag;
            }

        }

        private void _AddCar()
        {
            
            _Car.CarTypeID = clsCarTypes.Find(cmbCarTypes.Text).CarTypeID;
            _Car.Model = txtModel.Text.Trim();
            _Car.Year = dtpYear.Value;
            _Car.Engine= txtEngine.Text.Trim();
            _Car.TransmissionTypeID = clsTransmissionTypes.Find(cmbTransmissionType.Text).TransmissionTypeID;
            _Car.UserIDCreated = 38;
            //_Car.UserIDCreated = clsGlobal.CurrentUser.UserID;
            _Car.DateOfCreated = DateTime.Now;
            _Car.Color=btnColorPicked.Tag.ToString();
            _Car.TotalCars = (int)nudTotalCars.Value;


        }

  



        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
                return;

            _AddCar();

            if (_Car.Save())
            {              

                lblCarID.Text = _Car.CarID.ToString();
                lblTitle.Text = "Update Car";
                this.Text = lblTitle.Text;
                MessageBox.Show("Data Saved Successfully", "Saved");

        


            }
            else
            {

                MessageBox.Show("Error: Data NOT Saved Successfully", "Not Saved");

            }

            


        }

    }
}
