using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2Exam
{
    public class Admin
    {
        public string userName { get; set; }
        public string password { get; set; }
        public Admin() { }
        public Admin(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }
        public bool Login(string userName, string password)
        {
            string path = "Admin.json";
            IOManager iOManager = new IOManager();
            Admin admin = iOManager.ReadJson<Admin>(path);
            
            if (userName == admin.userName && password == admin.password)
            {
               return true; 
            }
            
           return false; 
        }
    }
}
