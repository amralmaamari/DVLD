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
    public partial class frmListCar : Form
    {
        DataTable dtCar;
        public frmListCar()
        {
            InitializeComponent();
        }

        private void _ShowAllDataInDataGridView()
        {
           
            dtCar = clsCar.GetAllCars();
            dgvCar.DataSource = dtCar;
        }
        private void frmListCar_Load(object sender, EventArgs e)
        {
            cmbFilter.SelectedIndex = 0;
            _ShowAllDataInDataGridView();
            if (dgvCar.Rows.Count > 0)
            {
                dgvCar.Columns[0].Width = 70;
                dgvCar.Columns[0].HeaderText = "Car ID";

                dgvCar.Columns[1].Width = 120;
                dgvCar.Columns[1].HeaderText = "Car Type";


                dgvCar.Columns[2].Width = 120;
                dgvCar.Columns[2].HeaderText = "Model";


                dgvCar.Columns[3].Width = 70;
                dgvCar.Columns[3].HeaderText = "Year";

                dgvCar.Columns[4].Width = 160;
                dgvCar.Columns[4].HeaderText = "Engine";



                dgvCar.Columns[5].Width = 300;
                dgvCar.Columns[5].HeaderText = "Transmission Type";
            }
            lblCountRecords.Text = dgvCar.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void _FillCarTypeInComboBox()
        {
            DataTable CarTypes = clsCarTypes.GetAllCarTypes();

            cmbType.Items.Clear();
            cmbType.Items.Add("None");
            foreach (DataRow row in CarTypes.Rows)
            {
                string CarTypeName = row[1].ToString();
                cmbType.Items.Add(CarTypeName);
            }
            cmbType.SelectedIndex = 0;
        }

        private void _FillTransmissionTypeInComboBox()
        {
            DataTable TransmissionTypes = clsTransmissionTypes.GetAllTransmissionTypes();

            cmbType.Items.Clear();
            cmbType.Items.Add("None");
            foreach (DataRow row in TransmissionTypes.Rows)
            {
                string TransmissionTypeName = row[1].ToString();
                cmbType.Items.Add(TransmissionTypeName);
            }
            cmbType.SelectedIndex = 0;
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cmbFilter.Text)
            {
                case "Car ID":
                    FilterColumn = "CarID";
                    break;

                case "Car Type":
                    FilterColumn = "CarType";
                    break;

                case "Model":
                    FilterColumn = "Model";
                    break;

                case "Year":
                    FilterColumn = "Year";
                    break;

                case "Engine":
                    FilterColumn = "Engine";
                    break;

                case "Transmission Type":
                    FilterColumn = "TransmissionType";
                    
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if (FilterColumn == "None" || txtFilter.Text.Trim() == "")
            {
                dtCar.DefaultView.RowFilter = "";
                lblCountRecords.Text = dgvCar.Rows.Count.ToString();
                return;
            }

            txtFilter.Focus();

            if (FilterColumn == "CarID" || FilterColumn == "Year")
            {
                dtCar.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilter.Text.Trim());
            }

            if (FilterColumn == "Model" || FilterColumn == "Engine")
            {
                dtCar.DefaultView.RowFilter = $"{FilterColumn} LIKE '%{txtFilter.Text.Trim()}%'";

            }

            


            lblCountRecords.Text = dgvCar.Rows.Count.ToString();
        }


        private void _FillFilterComboBox(string selectedText)
        {
            if (selectedText == "Car Type")
            {
                _FillCarTypeInComboBox();
            }
            else if (selectedText == "Transmission Type")
            {
                _FillTransmissionTypeInComboBox();
            }


        }

        private void _HandleComboBoxSelection(string selectedText)
        {
            _FillFilterComboBox(selectedText);

            cmbType.SelectedIndex = 0;
            _SetVisibility(txtFilter, false);
            _SetVisibility(cmbType, true);
            cmbType.Focus();


        }

        private void _HandleTextBoxSelection(string selectedText)
        {
            if (selectedText == "None")
            {
                _SetVisibility(txtFilter, false);
                _ShowAllDataInDataGridView();
            }
            else
            {
                _SetVisibility(txtFilter, true);
                txtFilter.Text = "";
                txtFilter.Focus();

            }

            _SetVisibility(cmbType, false);

        }

        private void _SetVisibility(Control control, bool isVisible)
        {
            control.Visible= isVisible;
        }

        //Here to Change 
        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedText = cmbFilter.Text;

            if (selectedText == "Car Type" || selectedText == "Transmission Type")
            {
                _HandleComboBoxSelection(selectedText);
              
            }
            else
            {
                _HandleTextBoxSelection(selectedText);


            }
        }

        private void btnAddNewCar_Click(object sender, EventArgs e)
        {
            frmAddUpdateCar frm = new frmAddUpdateCar();
            frm.ShowDialog();
            frmListCar_Load(null, null);
        }

        private void addNewCarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateCar frm = new frmAddUpdateCar();
            frm.ShowDialog();
            frmListCar_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateCar frm = new frmAddUpdateCar((int)dgvCar.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void showDetailesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowCarInfo frm = new frmShowCarInfo((int)dgvCar.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {

            string FilterColumn = "";
            switch (cmbFilter.Text)
            {
                case "Car Type":
                    FilterColumn = "CarType";
                    break;

                case "Transmission Type":
                    FilterColumn = "TransmissionType";
                    break;

                
            }


           

            string selectedText = cmbType.Text;

            if (FilterColumn == "None" || selectedText.Trim() == "" || selectedText == "None" )
            {
                dtCar.DefaultView.RowFilter = "";
                lblCountRecords.Text = dgvCar.Rows.Count.ToString();
                return;
            }

            dtCar.DefaultView.RowFilter = $"{FilterColumn} LIKE '%{selectedText.Trim()}%'";








            lblCountRecords.Text = dgvCar.Rows.Count.ToString();
        }
    }
}
