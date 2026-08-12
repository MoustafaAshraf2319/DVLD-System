using DVLD_Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace DVLD
{
    internal static class clsGlobal
    {
        public static clsUser CurrentUser;
        private const string RegistryKeyPath = @"HKEY_CURRENT_USER\Software\DVLD";
        public static bool RememberUsernameAndPassword(string Username, string Password)
        {
            try
            {
                if (Username == "")
                {
                    Registry.SetValue(RegistryKeyPath, "Username", "");
                    Registry.SetValue(RegistryKeyPath, "Password", "");
                    return true;
                }

                Registry.SetValue(RegistryKeyPath, "Username", Username);
                Registry.SetValue(RegistryKeyPath, "Password", Password);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
            return true;
        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            try
            { 
                object storedUsername = Registry.GetValue(RegistryKeyPath, "Username", null);
                object storedPassword = Registry.GetValue(RegistryKeyPath, "Password", null);

                if (storedUsername == null || storedPassword == null || storedUsername.ToString() == "")
                    return false;

                Username = storedUsername.ToString();
                Password = storedPassword.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
            return true;
        }
    }
}
