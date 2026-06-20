using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public static class clsCountryData
    {
        public static string GetCountryNameByNationalityCountryID(int NationalityCountryID)
        {
            string CountryName = "";
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select * from Countries where CountryID = @NationalityCountryID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    CountryName = reader["CountryName"].ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return CountryName;
        }

        public static DataTable GetAllCountries()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select * from Countries";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
    }
}
