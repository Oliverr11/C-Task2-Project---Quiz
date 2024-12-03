using DemoJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2Exam
{
    public class LoginAndRegister
    {
        public void FirstMenu()
        {
            Console.WriteLine("========= Welcome =========");
            Console.WriteLine("1 ) Register As User");
            Console.WriteLine("2 ) Login As User");
            Console.WriteLine("3 ) Login As Admin");
            Console.Write("Enter Opt : ");
            string opt = Console.ReadLine();
            switch (opt)
            {
                case "1":Register(); break;
                case "2":Login(); break;
                case "3":LoginAsAdmin(); break;
                default: Console.Clear(); FirstMenu();  break;
            }
        }
        string GetValidInput(string prompt)
        {
            string input;
            do
            {
                Console.WriteLine(prompt);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    Console.WriteLine("Input cannot be empty. Please try again.");
            } while (string.IsNullOrWhiteSpace(input));
             return input;
        }
        public void Register()
        {
            Console.Clear();
            Console.WriteLine("========= Registration =========");
            User user = new User();


            string userName = GetValidInput("Enter Username : ");
            string password = GetValidInput("Enter Password : ");
            string DOB = GetValidInput("Enter DOB : ");

            user.Register(userName, password, DOB);

            FirstMenu();
        }

        public void Login()
        {
            Console.WriteLine("========= Login ========= ");
            
                string userName = GetValidInput("Enter Username : ");
                string password = GetValidInput("Enter Password : ");
            try
            {
                IOManager oManager = new IOManager();
                User user = oManager.ReadUserFromFile(userName);
                user.Login(userName, password);
                if (user.Login(userName, password))
                {
                    UserConsole userConsole = new UserConsole(user);
                    userConsole.FirstMenu();
                }
                else
                {

                    Console.Clear();
                    Console.WriteLine("Login Failed. Incorrect username or password.");
                    FirstMenu();

                }
            }
            catch (Exception )
            {
                Console.Clear();
                Console.WriteLine("Login Failed.");
                FirstMenu();
            }
            
        }

        public void LoginAsAdmin()
        {
            
            string userName = GetValidInput("Enter Username : ");
            string password = GetValidInput("Enter Passowrd : ");

            Admin admin = new Admin();
            admin.Login(userName, password);
            if (admin.Login(userName,password))
            {
                Console.WriteLine("vv");
                AdminConsole adminConsole = new AdminConsole();
                adminConsole.Start();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Incorrect!");
                FirstMenu();
            }
        }
    }
}
