using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsCountry
    {
        public static string FindCountryNameByNationalityCountryID(int NationalityCountryID)
        {
            return clsCountryData.GetCountryNameByNationalityCountryID(NationalityCountryID);
        }
        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }
    }
}
