using DVLD_Business;
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
    public partial class frmManagePeople : Form
    {
        public frmManagePeople()
        {
            InitializeComponent();
        }

        private void _RefreshPeopleList()
        {
            dgvPeopleList.DataSource = clsPerson.GetAllPeople();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddEditPerson addNew = new frmAddEditPerson(-1);
            addNew.ShowDialog();
            _RefreshPeopleList();
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _RefreshPeopleList();
            cbFilterBy.SelectedIndex = 0;
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPerson addNew = new frmAddEditPerson(-1);
            addNew.ShowDialog();
            _RefreshPeopleList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPerson Edit = new frmAddEditPerson((int)dgvPeopleList.CurrentRow.Cells[0].Value);
            Edit.ShowDialog();
            _RefreshPeopleList();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "None")
            {
                txtFilterBy.Visible = false;
                _RefreshPeopleList();
                return;
            }
            else
            {
                txtFilterBy.Visible = true;
            }
                
          
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to delete this person ?","Sure",MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (clsPerson.DeletePerson((int)dgvPeopleList.CurrentRow.Cells[0].Value))
                    MessageBox.Show("Person deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Failed to Delete Person", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

            _RefreshPeopleList();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature will be added soon", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature will be added soon", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonDetails frmPersonDetails = new frmPersonDetails((int)dgvPeopleList.CurrentRow.Cells[0].Value);
            frmPersonDetails.ShowDialog();
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = cbFilterBy.Text;
            string SearchText = txtFilterBy.Text;
            dgvPeopleList.DataSource = clsPerson.SearchPeople(filterColumn, SearchText);
        }
    }
}
