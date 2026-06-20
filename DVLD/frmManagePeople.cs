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
            dgvListPeople.DataSource = clsPerson.GetAllPeople();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddEditPerson addNew = new frmAddEditPerson();
            addNew.ShowDialog();
            _RefreshPeopleList();
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _RefreshPeopleList();
        }
    }
}
