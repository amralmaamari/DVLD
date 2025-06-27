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
    public partial class frmListApplicationTypes : Form
    {
        private DataTable _dtAllApplicationTypes = new DataTable();

        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        private void frmManageApplictionTypes_Load(object sender, EventArgs e)
        {
            _dtAllApplicationTypes=clsApplicationType.GetAllApplictionTypes();
            dgvApplictionTypes.DataSource = _dtAllApplicationTypes;

            if(dgvApplictionTypes.Rows.Count > 0 )
            {
                dgvApplictionTypes.Columns[0].HeaderText = "ID";
                dgvApplictionTypes.Columns[0].Width = 220;


                dgvApplictionTypes.Columns[1].HeaderText = "Title";
                dgvApplictionTypes.Columns[1].Width = 320;



                dgvApplictionTypes.Columns[2].HeaderText = "Fees";
                dgvApplictionTypes.Columns[2].Width = 220;
            }

            lblCountRecords.Text = dgvApplictionTypes.Rows.Count.ToString();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editApplictionTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditApplicationType frm = new frmEditApplicationType((int)dgvApplictionTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmManageApplictionTypes_Load(null, null);
        }
    }
}
