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
    public partial class frmListCarTypes : Form
    {
        private DataTable _dtAllCarTypes = new DataTable();
        public frmListCarTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListCarTypes_Load(object sender, EventArgs e)
        {
            _dtAllCarTypes = clsCarTypes.GetAllCarTypes();
            dgvCarTypes.DataSource = _dtAllCarTypes;

            if (dgvCarTypes.Rows.Count > 0)
            {
                dgvCarTypes.Columns[0].HeaderText = "ID";
                dgvCarTypes.Columns[0].Width = 100;


                dgvCarTypes.Columns[1].HeaderText = "Name";
                dgvCarTypes.Columns[1].Width = 576;

                
            }

            lblCountRecords.Text = dgvCarTypes.Rows.Count.ToString();
        }
    }
}
