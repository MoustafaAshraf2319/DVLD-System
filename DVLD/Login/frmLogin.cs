using DVLD.People;
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
    public partial class frmLogin : Form
    {
        private clsUser _User;
        public frmLogin()
        {
            InitializeComponent();          
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            _User = clsUser.FindByUsernameAndPassword(txtUsername.Text, txtPassword.Text);
            if (_User == null)
            {
                MessageBox.Show("Wrong Username or password !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }

            if (!_User.IsActive)
            {
                MessageBox.Show("User is not Active !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
   
            frmMain frm = new frmMain(_User);
            frm.ShowDialog();
        }
    }
}
