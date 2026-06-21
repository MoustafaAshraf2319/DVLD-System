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
    public partial class frmAddEditPerson : Form
    {
        enum enMode { AddNew=0, Edit =1}
        enMode Mode = enMode.AddNew;
        private int _PersonID;
        public frmAddEditPerson(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
            if (_PersonID == -1)
                Mode = enMode.AddNew;
            else
                Mode = enMode.Edit;
        }

        private void CtrlAddEditPerson1_OnPersonSaved(object sender, int PersonID)
        {
            lblPersonID.Text = PersonID.ToString();
            lblAddEdit.Text = "Update Person";
        }

        private void frmAddEditPerson_Load(object sender, EventArgs e)
        {
            ctrlAddEditPerson1.LoadPersonData(_PersonID);

            if (Mode == enMode.AddNew)
            {
                lblAddEdit.Text = "Add New Person";
            }
            else
            {
                lblAddEdit.Text = "Update Person";
            }
            ctrlAddEditPerson1.OnPersonSaved += CtrlAddEditPerson1_OnPersonSaved;

        }
    }
}
