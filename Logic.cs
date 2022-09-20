using System;
using BOLayer;
using DataLayer;
using System.Collections.Generic;
using System.Globalization;
using System.Text;


namespace LogicLayer
{
    public class Logic
    {
        // verify login of Admin
        public bool VerifyLogin(Admin admin)
        {
            Data adminData = new Data();
            return adminData.isInFile(admin);
        }

        // verify if username is in file
        public bool userActive (string user)
        {
            Data data = new Data();
            return data.userActive(user);
        }

        // verify login of customer
        public bool LoginVerify(Customer customer)
        {
            Data customerInfo = new Data();
            return customerInfo.canLogin(customer);
        }

        // check if valid username (A-Z, a-z, 0-9)

        public bool validUsername(string s)
        {
            foreach (char c in s)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        // check if pin is valid 5 digit 0-9
        public bool validPin(string s)
        {
            if (s.Length != 5)
            {
                return false;
            }
            foreach (char c in s)
            {
                if (c >= '0' && c <= '9')
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        // Encryption
        // for letters, a is swapped with z, b with y, etc.
        // numbers 0 with 9, 8 with 1, etc.

        public string EncryptDecrypt(string username)
        {
            string output = "";
            foreach (char c in username)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    output += Convert.ToChar(('Z' - (c - 'A')));
                }
                else if (c >= 'a' && c <= 'z')
                {
                    output += Convert.ToChar(('z' - (c - 'a')));
                }
                else if (c >= '0' && c <= '9')
                {
                    output += (9 - char.GetNumericValue(c));
                }


            }

        }
        
    }
}

