using DVLD.Classes;
using DVLD.Licenses;
using DVLD.Licenses.International_Licenses;
using DVLD_Buisness;
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

namespace DVLD.Applications.International_License
{
    public partial class frmNewInternationalLicenseApplication : Form
    {
        private int _InternationalLicenseID = -1;
        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
        }
      
        private void frmNewInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = lblApplicationDate.Text;
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(1));
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            lblFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).Fees.ToString();
        }

        private void ctrlDriverLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int selectedLicenseID = obj;

            lblLocalLicenseID.Text = selectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = (selectedLicenseID != -1);

            if (selectedLicenseID == -1)
                return;

            if (ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClass != 3)
            {
                MessageBox.Show("Selected License should be Class 3, Select Another one", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int ActiveInternationalLicenseID = clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID);

            if (ActiveInternationalLicenseID != -1)
            {
                MessageBox.Show("Person already have an active international license with ID = " + ActiveInternationalLicenseID.ToString(), "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowLicenseInfo.Enabled = true;
                _InternationalLicenseID = ActiveInternationalLicenseID;
                btnIssueLicense.Enabled = false;
                return;
            }

            btnIssueLicense.Enabled = true;
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsInternationalLicense InternationalLicense = new clsInternationalLicense();

            InternationalLicense.ApplicantPersonID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
            InternationalLicense.ApplicationDate = DateTime.Now;
            InternationalLicense.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            InternationalLicense.LastStatusDate = DateTime.Now;
            InternationalLicense.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).Fees;
            InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            InternationalLicense.DriverID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID;
            InternationalLicense.IssuedUsingLocalLicenseID = ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseID;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);

            if(!InternationalLicense.Save())
            {
                MessageBox.Show("Failed to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _InternationalLicenseID = InternationalLicense.InternationalLicenseID;
            lblApplicationID.Text = InternationalLicense.ApplicationID.ToString();
            lblInternationalLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
            MessageBox.Show("International License Issued Successfully with ID = " + InternationalLicense.InternationalLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssueLicense.Enabled = false;
            ctrlDriverLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;
        }

       

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm =
               new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(_InternationalLicenseID);
            frm.ShowDialog();
        }

        private void frmNewInternationalLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFilter1.txtLicenseIDFocus();
        }
    }
}
