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
    public partial class frmListRentTypes : Form
    {
        private DataTable _dtAllRentTypes = new DataTable();

        public frmListRentTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListRentTypes_Load(object sender, EventArgs e)
        {
            _dtAllRentTypes = clsRentType.GetAllRentTypes();
            dgvRentTypes.DataSource = _dtAllRentTypes;

            if (dgvRentTypes.Rows.Count > 0)
            {
                dgvRentTypes.Columns[0].HeaderText = "ID";
                dgvRentTypes.Columns[0].Width = 220;


                dgvRentTypes.Columns[1].HeaderText = "Name";
                dgvRentTypes.Columns[1].Width = 220;

                dgvRentTypes.Columns[2].HeaderText = "Fees";
                dgvRentTypes.Columns[2].Width = 100;


                dgvRentTypes.Columns[3].Width = 180;
            }

            lblCountRecords.Text = dgvRentTypes.Rows.Count.ToString();

        }

        private void editApplictionTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditRentType frm = new frmEditRentType((clsRentType.enRentType)dgvRentTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListRentTypes_Load(null,null);
        }
    }
}
