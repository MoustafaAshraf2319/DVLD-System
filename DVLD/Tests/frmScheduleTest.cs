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

namespace DVLD.Tests
{
    public partial class frmScheduleTest : Form
    {
        
        private int _LocalDrivingLicenseApplicationID = -1;
        private int _AppointmentID = -1;
        private clsTestType.enTestType _TestTypeID;
        
        public frmScheduleTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType, int TestAppointment = -1)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestTypeID = TestType;
            _AppointmentID = TestAppointment;
        }

        

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            ctrlScheduleTest1.TestTypeID = _TestTypeID;

            ctrlScheduleTest1.LoadInfo(_LocalDrivingLicenseApplicationID, _AppointmentID);
        }
    }
}
