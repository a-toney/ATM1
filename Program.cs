using System;
using BOLayer;
using DataLayer;
using LogicLayer;
using ViewLayer;

namespace MyAtm
{
    class Program
    {
        static void Main(string[] args)
        {
            // used to write an administrator to the file
            /*
             Logic logic = new Logic();
            Admin admin = new Admin {Username = logic.EncryptionDecryption("admin"), Ping = logic.EncryptionDecryption("12345") };
            Data obj = new Data();
            obj.AddToFile(admin);
            */

            View view = new View();
            view.loginScreen();
        }
    }

}