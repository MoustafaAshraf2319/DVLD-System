using DVLD.Classes;
using DVLD.Properties;
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
using System.IO;

namespace DVLD.Licenses.International_Licenses.Controls
{
    public partial class ctrlDriverInternationalLicenseInfo : UserControl
    {
        private int _InternationalLicenseID;
        private clsInternationalLicense _InternationalLicense;

        public int InternationalLicenseID
        {
            get { return _InternationalLicenseID; }
            set { _InternationalLicenseID = value; }
        }
        public ctrlDriverInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        private void _LoadPersonImage()
        {
            if (_InternationalLicense.DriverInfo.PersonInfo.Gendor == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _InternationalLicense.DriverInfo.PersonInfo.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.Load(ImagePath);
                else
                    MessageBox.Show("Couldn't Find Image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void LoadInfo(int InternationalLicenseID)
        {
            _InternationalLicenseID = InternationalLicenseID;
            _InternationalLicense = clsInternationalLicense.Find(_InternationalLicenseID);
            if(_InternationalLicense == null)
            {
                MessageBox.Show("Couldn't Find International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblFullName.Text = _InternationalLicense.ApplicantFullName;
            lblInternationalLicenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();
            lblLocalLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
            lblNationalNo.Text = _InternationalLicense.DriverInfo.PersonInfo.NationalNo;
            lblGendor.Text = _InternationalLicense.DriverInfo.PersonInfo.Gendor == 0 ? "Male" : "Female";
            lblIssueDate.Text = clsFormat.DateToShort(_InternationalLicense.IssueDate);
            lblApplicationID.Text = _InternationalLicense.ApplicationID.ToString();
            lblIsActive.Text = _InternationalLicense.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = clsFormat.DateToShort(_InternationalLicense.DriverInfo.PersonInfo.DateOfBirth);
            lblDriverID.Text = _InternationalLicense.DriverID.ToString();
            lblExpirationDate.Text = clsFormat.DateToShort(_InternationalLicense.ExpirationDate);

            _LoadPersonImage();
        }
    }
}
