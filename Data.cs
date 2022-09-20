using System;
using BOLayer;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace DataLayer
{
    public class Data
    {
        // Appends object to file in json format
        public void AddToFile<T>(T obj)
        {
            string jsonOutput = JsonSerializer.Serialize(obj);
            if (obj is Admin)
            {
                File.AppendAllText("admins.txt", jsonOutput + Environment.NewLine);
            }
            else if (obj is Customer)
            {
                File.AppendAllText("customers.txt", jsonOutput + Environment.NewLine);
            }
            else if (obj is Transaction)
            {
                File.AppendAllText("transaction.txt", jsonOutput + Environment.NewLine);
            }
        }

        // Clears previous data and saves new list to file in json format
        public void SaveToFile<T>(List<T> list)
        {
            // overwrite file with first object in list
            string jsonOutput = JsonSerializer.Serialize(list[0]);
            if (list[0] is Admin)
            {
                File.WriteAllText("admins.txt", jsonOutput + Environment.NewLine);
            }
            else if (list[0] is Customer)
            {
                File.WriteAllText("customers.txt", jsonOutput + Environment.NewLine);
            }

            for (int i = 1; i < list.Count; i++)
            {
                AddToFile(list[i]);
            }
        }

        // Returns list of obj from file
        public List<T> ReadFile<T>(string FileName)
        {
            List<T> list = new List<T>();
            string FilePath = Path.Combine(Environment.CurrentDirectory, FileName);
            StreamReader sr = new StreamReader(FilePath);

            string line = String.Empty;
            while ((line = sr.ReadLine()) != null)
            {
                list.Add(JsonSerializer.Deserialize<T>(line));
            }
            sr.Close();

            return list;
        }

        // Deletes a customer obj from file
        public void DeleteFromFile (Customer customer)
        {
            List<Customer> list = ReadFile<Customer>("customers.txt");
            // Checking and remove the required object from list
            foreach (Customer item in list)
            {
                if (item.AccountNum == customer.AccountNum)
                {
                    list.Remove(item);
                    break;
                }
            }
            // overwriting
            SaveToFile<Customer>(list);
        }

        // updates customer obj in file

        public void UpdateInFile (Customer customer)
        {
            List<Customer> list = ReadFile<Customer>("customers.txt");
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].AccountNum == customer.AccountNum)
                {
                    list[i] = customer;
                }
            }

            // overwriting list to file
            SaveToFile<Customer>(list);
             
        }

        // Checks if admin obj is in file
        public bool isInFile(Admin user)
        {
            List<Admin> list = ReadFile<Admin>("admins.txt");
            foreach (Admin admin in list)
            {
                if (admin.Username == user.Username && admin.Pin == user.Pin)
                {
                    return true;
                }
            }
            return false;
        }


        // check if customer obj in file
        public int userActive(string user)
        {
            List<Customer> list = ReadFile<Customer>("customers.txt");
            foreach (Customer customer in list)
            {
                if (customer.Username == user && customer.Status == "Active")
                {
                    return 1;
                }
                else if (customer.Username == user && customer.Status == "Disabled")
                {
                    return 2;
                }
            }
            return 0;
        }


        // is user inactive
        public bool canLogin(Customer customer)
        {
            List<Customer> list = ReadFile<Customer>("customer.txt");
            foreach (Customer user in list)
            {
                if (customer.Username == user.Username && customer.Pin == user.Pin && user.Status == "Active")
                {
                    return true;
                }
            }
            return false;
        }

        // check if account # in file

        public bool isInFile(int accNum, out Customer outCustomer)
        {
            List<Customer> list = ReadFile<Customer>("customer.txt");
            foreach (Customer customer in list)
            {
                if (Customer.AccNum == accNum)
                {
                    outCustomer = customer;
                    return true;
                }
            }
            outCustomer = null;
            return false;
        }
    


        // Check username on file
        public bool isInFile(string username)
        {
            List<Customer> list = ReadFile<Customer>("customer.txt");
            foreach (Customer customer in list)
            {
                if (customer.Username == username)
                {
                    return true;
                }
            }
            return false;

        }


        // returns object if username is provided
        public Customer GetCustomer(string username)
        {
            List<Customer> list = ReadFile<Customer>("customers.txt");
            foreach (Customer customer in list)
            {
                if (customer.Username == username)
                {
                    return customer;
                }
            }
            return null;
        }

        // returns if account number is provided
        public Customer GetCustomer(int accNum)
        {
            List<Customer> list = ReadFile<Customer>("customers.txt");
            foreach (Customer customer in list)
            {
                if (customer.AccountNum == accNum)
                {
                    return customer;
                }
            }
            return null;
        }

        // method to retrieve last account num
        public int getLastAccNum()
        {
            List<Customer> list = ReadFile<Customer>("customers.txt");
            if (list.Count > 0)
            {
                Customer customer = list[list.Count - 1];
                return customer.AccountNum;
            }
            return 0;
        }


            // remove amount from balance of an acct and update in file

        public void RemoveBal(Customer c, int amount)
        {
                c.Balance -= amount;
                UpdateInFile(c);
        }

            // Add an amount and update to file

        public void AddAmount(Customer c, int amount)
        {
            c.Balance += amount;
            UpdateInFile(c);
        }

        // gives total amount customer has withdrawn today
        public int TodaysTransactionsAmt(int accNum)
        {
            List<Transaction> list = ReadFile<Transaction>("transactions.txt");
            int totalAmt = 0;

            foreach(Transaction t in list)
            {
                if (t.AccountNum == accNum)
                {
                    totalAmt += t.TransactionAmount;
                }
            }
            return totalAmt;
        }
    }
}