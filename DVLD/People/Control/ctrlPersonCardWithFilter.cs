using DvldDataBusinessLayer;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DVLD.People.Control
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public event Action<int> OnPersonSelected;
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if(handler != null)
            {
                handler(PersonID);
            }
        }

        //هذي طريقة اظهار واخفى الكنترول من خارج من خلال الفورم لو تشتي
        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get { return _ShowAddPerson; }
            set
            {
                _ShowAddPerson = value;
                btnAddNewPerson.Enabled = _ShowAddPerson;
            }

        }


        private bool _FilterEnable = true;

        public bool FilterEnable
        {
            get { return _FilterEnable; }

            set
            {
                _FilterEnable = value;
                gbFilter.Enabled= _FilterEnable;
            }
        }


        //this way to get info from other control 
        //the below will take the person id from the ctrlPerson 
        public int PersonID
        {
             get{ return ctrlPersonCard1.PersonID; }
        }
        public clsPeople Person
        {
            get { return ctrlPersonCard1.SelectedPersonInfo; }
        }


        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }


        public void LoadPersonInfo(int PersonID)
        {
            cmbFilterBy.SelectedIndex = 0;
            txtFilterValue.Text = PersonID.ToString();
            FindNow();
        }



        private void FindNow()
        {
            switch (cmbFilterBy.Text)
            {
                case "Person ID":
                    ctrlPersonCard1.LoadPersonInfo(int.Parse(txtFilterValue.Text.Trim()));
                    break;

                case "National No":
                    ctrlPersonCard1.LoadPersonInfo(txtFilterValue.Text.Trim());
                    break;
                default:
                    return;


            }

            // اي شخص يكون منادية من خارج هذي الفونشكن  والفلتر مايكون مغلق
            if (OnPersonSelected != null && FilterEnable )
                //Raise the Event With Parmeter
                OnPersonSelected(ctrlPersonCard1.PersonID);
        }



        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }



        private void BtnSearchPerson_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Fileds are not valide!,put the mouse over the red icon", "Error");
                return;
            }
            FindNow();
        }


        private void ctrlPersonCardWithFilter_Load_1(object sender, EventArgs e)
        {
            cmbFilterBy.SelectedIndex = 0;
            txtFilterValue.Focus();

        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm1 = new frmAddUpdatePerson();
            //here call the delgate to return the ID
            frm1.DataBack += DataBackEvent;
            frm1.ShowDialog();
        }



        private void DataBackEvent(object sender, int PersonID)
        {
            //handle the data retrive
            cmbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text=PersonID.ToString();
            ctrlPersonCard1.LoadPersonInfo(PersonID);
        }
      

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilterValue.Text))
            {
                e.Cancel=true;
                errorProvider1.SetError(txtFilterValue, "Enter Value!");
            }
            else
            {
                errorProvider1.SetError(txtFilterValue, null);

            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // (Enter)Key =13 if press
            if(e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
            }

            //if it is person id allow only NUMBERS
            //if NationalNO number and text

            if(cmbFilterBy.Text == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }


        public void FilterFoucs()
        {
            txtFilterValue.Focus();
        }



       

    }
}
