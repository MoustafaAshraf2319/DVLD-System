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
    public partial class frmPersonDetails : Form
    {
        private int _PersonID;
        public frmPersonDetails(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        private void frmPersonDetails_Load(object sender, EventArgs e)
        {
            ctrlPersonDetails1.LoadPersonInfo(_PersonID);
        }

        private void ctrlPersonDetails1_Load(object sender, EventArgs e)
        {

        }
    }
}
