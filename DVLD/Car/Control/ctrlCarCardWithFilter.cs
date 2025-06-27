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
    public partial class ctrlCarCardWithFilter : UserControl
    {


        public event Action<int> OnSelectedCar;


        protected virtual void SelectedCar(int CarID)
        {
            //Action<int> Handler = OnSelectedCar;

            //if (Handler != null)
            //{
            //    Handler(CarID);
            //}

            //all the same
            OnSelectedCar?.Invoke(CarID);
               
        }


        clsCar _Car;

        private bool _EnableFilter = true;
        public bool EnableFilter
        {
            set
            {
                _EnableFilter = value;
                gbFilter.Enabled = _EnableFilter;
            }
            get => _EnableFilter;
        }

        private bool _FoucsFilterText = true;

        //The Foucs Filter Not Work Properly
        public bool FoucsFilterText
        {
            set
            {
                _FoucsFilterText = value;
                if (_FoucsFilterText)
                {
                    txtFilterValue.Text = "";
                    txtFilterValue.Focus();
                }
            }
            
        }

        public ctrlCarCardWithFilter()
        {
            InitializeComponent();
        }

        

        private void _FindNow()
        {
           

            switch (cmbFilterBy.Text)
            {
                case "Car ID":
                    ctrlCarCard1.LoadCarInfo(_Car.CarID);
                    break;

               
            }


            if (OnSelectedCar != null)
            {
                OnSelectedCar(_Car.CarID);
            }


        }

        private void ctrlCarCardWithFilter_Load(object sender, EventArgs e)
        {
            cmbFilterBy.SelectedIndex = 0;
            
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
                return;

            int CarID=Convert.ToInt32(txtFilterValue.Text.Trim());

            _Car=clsCar.GetCarInfoByID(CarID);
            
            if (_Car == null)
            {
                MessageBox.Show($"There is no Car With ID {CarID}", "Not Found",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FindNow();





        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilterValue.Text))
            {
                errorProvider1.SetError(txtFilterValue, "This Should not Be Empty");
                txtFilterValue.Focus();
                e.Cancel = true;
            }else
            {
                errorProvider1.SetError(txtFilterValue, null);
               

            }
        }
    }
}
