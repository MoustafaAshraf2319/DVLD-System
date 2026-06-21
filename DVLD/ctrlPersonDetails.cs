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
    public partial class ctrlPersonDetails : UserControl
    {
        private int _PersonID;
        private clsPerson _Person;

    
        public ctrlPersonDetails()
        {
            InitializeComponent();
        }
        public void LoadPersonInfo(int  PersonID)
        {
            _PersonID = PersonID;
            _Person = clsPerson.Find(_PersonID);
            if (_Person == null)
            {
                MessageBox.Show($"No Person with ID = {PersonID} was found in the system!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }
            lblPersonID.Text = _PersonID.ToString();
            lblName.Text = (_Person.FirstName + " " + _Person.SecondName + " " + _Person.ThirdName + " "
                + _Person.LastName).ToString();
            lblNationalNo.Text = _Person.NationalNo.ToString();
            lblGendor.Text = (_Person.Gendor == 0) ? "Male" : "Female";
            lblEmail.Text = _Person.Email.ToString();
            lblAddress.Text = _Person.Address.ToString();
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString();
            lblPhone.Text = _Person.Phone.ToString();
            lblCountry.Text = clsCountry.FindCountryNameByNationalityCountryID(_Person.NationalityCountryID);
            pbPersonalImage.ImageLocation = _Person.ImagePath;
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void ctrlPersonDetails_Load(object sender, EventArgs e)
        {
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPerson Edit = new frmAddEditPerson(_PersonID);
            Edit.ShowDialog();
        }

        private void llEditPersonInfo_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPerson Edit = new frmAddEditPerson(_PersonID);
            Edit.ShowDialog();
        }
    }
}
