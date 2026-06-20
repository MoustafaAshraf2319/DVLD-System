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
        public ctrlAddEditPerson()
        {
            InitializeComponent();
        }
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
        }
    }
}
