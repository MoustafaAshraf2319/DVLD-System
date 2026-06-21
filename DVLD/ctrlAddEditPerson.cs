using DVLD.Properties;
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
    public partial class ctrlAddEditPerson : UserControl
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);
        public event DataBackEventHandler OnPersonSaved;

        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        private int _PersonID = -1;
        private clsPerson _Person;

       
        public ctrlAddEditPerson()
        {
            InitializeComponent();
        }

        public int PersonID { get { return _Person.PersonID; } }

        private void FillCountriesComboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();
            cbCountries.DataSource = dtCountries;
            cbCountries.DisplayMember = "CountryName";
            cbCountries.ValueMember = "CountryID";
            cbCountries.SelectedIndex = 0;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm()?.Close();
        }

        

        private void ctrlAddEditPerson_Load(object sender, EventArgs e)
        {
            FillCountriesComboBox();
            rbMale.Checked = true;
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            llRemove.Visible = false;
        }

        public void LoadPersonData(int PersonID)
        {
            _PersonID = PersonID;
            if(_PersonID == -1)
            {
                Mode = enMode.AddNew;
                _Person = new clsPerson();
                return;
            }
            Mode = enMode.Update;
            _Person = clsPerson.Find(_PersonID);
            if(_Person == null)
            {
                MessageBox.Show("No Person with ID = " + _PersonID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtNationalNo.Text = _Person.NationalNo;
            txtEmail.Text = _Person.Email;
            txtPhone.Text = _Person.Phone;
            txtAddress.Text = _Person.Address;
            cbCountries.SelectedValue = _Person.NationalityCountryID;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            pbPersonImage.ImageLocation = _Person.ImagePath;
            if (_Person.Gendor == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (clsPerson.IsPersonExistsByNationalNo(txtNationalNo.Text) || string.IsNullOrEmpty(txtNationalNo.Text))
            {
                e.Cancel = true;
                txtNationalNo.Focus();
                errorProvider1.SetError(txtNationalNo, "National Number is used for another person");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNationalNo, "");
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            string Email = txtEmail.Text.Trim();
            if (string.IsNullOrEmpty(Email))
            {
                e.Cancel = false;
                errorProvider1.SetError(txtEmail, "");
                return;
            }
            if (!Email.Contains("@"))
            {
                e.Cancel = true;
                txtEmail.Focus();
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtEmail, "");
            }
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMale.Checked)
            {
                pbPersonImage.Image = DVLD.Properties.Resources.Male_512;
            }
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFemale.Checked)
            {
                pbPersonImage.Image = DVLD.Properties.Resources.Female_512;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                MessageBox.Show("National No cannot be empty", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNationalNo.Focus();
                return; 
            }
            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.NationalNo = txtNationalNo.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.Address = txtAddress.Text.Trim();
            _Person.Gendor = rbMale.Checked ? 0 : 1;
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.NationalityCountryID = Convert.ToInt32(cbCountries.SelectedValue);
            _Person.ImagePath = pbPersonImage.ImageLocation ?? "";
            if (_Person.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);               
                OnPersonSaved?.Invoke(this, _Person.PersonID);              
            }
            else
            {
                MessageBox.Show("Failed to Save ", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                pbPersonImage.ImageLocation = openFileDialog1.FileName;
                llRemove.Visible = true;
            }
        }

        private void llRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.Image = Resources.Male_512;
            llRemove.Visible = false;
        }
    }
}
