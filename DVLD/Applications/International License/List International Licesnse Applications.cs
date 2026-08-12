using DVLD.Licenses;
using DVLD.Licenses.International_Licenses;
using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.International_License
{
    public partial class frmListInternationalLicensesApplications : Form
    {
        private DataTable _dtAllInternationalLicensesApplications;
        public frmListInternationalLicensesApplications()
        {
            InitializeComponent();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "None":
                    FilterColumn = "None";
                    break;
                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;
                case "Application ID":
                    FilterColumn = "ApplicationID";
                    break;
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;
                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;
                case "Is Active":
                    FilterColumn = "IsActive";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if (FilterColumn == "None" || txtFilterValue.Text.Trim() == "")
            {
                _dtAllInternationalLicensesApplications.DefaultView.RowFilter = "";
                lblInternationalLicensesRecords.Text = _dtAllInternationalLicensesApplications.Rows.Count.ToString();
            }
            else if (FilterColumn == "IsActive")
            {
                _dtAllInternationalLicensesApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, cbFilterBy.Text.Trim());
            }
            else
                _dtAllInternationalLicensesApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());

            lblInternationalLicensesRecords.Text = _dtAllInternationalLicensesApplications.Rows.Count.ToString();
        }

        private void frmListInternationalLicensesApplications_Load(object sender, EventArgs e)
        {
            _dtAllInternationalLicensesApplications = clsInternationalLicense.GetAllInternationalLicenses();
            dgvInternationalLicenses.DataSource = _dtAllInternationalLicensesApplications;
            lblInternationalLicensesRecords.Text = dgvInternationalLicenses.Rows.Count.ToString();
            cbFilterBy.SelectedIndex = 0;

            if(dgvInternationalLicenses.Rows.Count > 0)
            {
                dgvInternationalLicenses.Columns[0].HeaderText = "Int License ID";
                dgvInternationalLicenses.Columns[0].Width = 150;

                dgvInternationalLicenses.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicenses.Columns[1].Width = 120;

                dgvInternationalLicenses.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicenses.Columns[2].Width = 120;

                dgvInternationalLicenses.Columns[3].HeaderText = "L.License ID";
                dgvInternationalLicenses.Columns[3].Width = 120;

                dgvInternationalLicenses.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns[4].Width = 120;

                dgvInternationalLicenses.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns[5].Width = 120;

                dgvInternationalLicenses.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicenses.Columns[6].Width = 120;

            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "IsActive")
            {
                cbIsActive.Visible = true;
                txtFilterValue.Visible = false;
                cbIsActive.SelectedIndex = 0;
                cbIsActive.Focus();
            }
            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsActive.Visible = false;
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = "";
            switch (cbIsActive.Text)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }

            if (FilterValue == "All")
                _dtAllInternationalLicensesApplications.DefaultView.RowFilter = "";
            else
                _dtAllInternationalLicensesApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsDriver.FindByDriverID((int)dgvInternationalLicenses.CurrentRow.Cells[2].Value).PersonID;
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
            frmListInternationalLicensesApplications_Load(null, null);
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo((int)dgvInternationalLicenses.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsDriver.FindByDriverID((int)dgvInternationalLicenses.CurrentRow.Cells[2].Value).PersonID;
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void btnNewApplication_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
            frmListInternationalLicensesApplications_Load(null,null);
        }
    }
}
