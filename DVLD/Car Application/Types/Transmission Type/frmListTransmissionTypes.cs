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
    public partial class frmListTransmissionTypes : Form
    {
        private DataTable _dtAlldgvTransmissionTypes = new DataTable();
        public frmListTransmissionTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListTransmissionTypes_Load(object sender, EventArgs e)
        {
            _dtAlldgvTransmissionTypes = clsTransmissionTypes.GetAllTransmissionTypes();
            dgvTransmissionTypes.DataSource = _dtAlldgvTransmissionTypes;

            if (dgvTransmissionTypes.Rows.Count > 0)
            {
                dgvTransmissionTypes.Columns[0].HeaderText = "ID";
                dgvTransmissionTypes.Columns[0].Width = 200;


                dgvTransmissionTypes.Columns[1].HeaderText = "Name";
                dgvTransmissionTypes.Columns[1].Width =520;

            }

            lblCountRecords.Text = dgvTransmissionTypes.Rows.Count.ToString();
        }
    }
}
