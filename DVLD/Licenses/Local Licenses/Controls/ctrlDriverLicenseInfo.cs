using DVLD.Classes;
using DVLD.Properties;
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
using System.IO;

namespace DVLD.Licenses.Local_Licenses.Controls
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
        private int _LicenseID = -1;
        private clsLicense _License;

        public clsLicense SelectedLicenseInfo
        { get { return _License; } }
        public int LicenseID
        {
            get { return _LicenseID; }
        }

        public ctrlDriverLicenseInfo()
        {
            InitializeComponent();
        }

        private void _LoadPersonImage()
        {
            if (_License.DriverInfo.PersonInfo.Gendor == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _License.DriverInfo.PersonInfo.ImagePath;

            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.Load(ImagePath);
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void LoadDriverLicenseInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            _License = clsLicense.Find(_LicenseID);

            if(_License == null)
            {
                MessageBox.Show("Couldn't Find License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblClass.Text = _License.LicenseClassInfo.ClassName;
            lblFullName.Text = _License.DriverInfo.PersonInfo.FullName;
            lblLicenseID.Text = _License.LicenseID.ToString();
            lblNationalNo.Text = _License.DriverInfo.PersonInfo.NationalNo;
            lblGendor.Text = _License.DriverInfo.PersonInfo.Gendor == 0 ? "Male" : "Female";
            lblIssueDate.Text = clsFormat.DateToShort(_License.IssueDate);
            lblIssueReason.Text = _License.IssueReasonText;
            lblNotes.Text = _License.Notes == "" ? "No Notes" : _License.Notes;
            lblIsActive.Text = _License.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = clsFormat.DateToShort(_License.DriverInfo.PersonInfo.DateOfBirth);
            lblDriverID.Text = _License.DriverID.ToString();
            lblExpirationDate.Text = clsFormat.DateToShort(_License.ExpirationDate);
            lblIsDetained.Text = _License.IsDetained ? "Yes" : "No";
            _LoadPersonImage();
        }
    }
}
