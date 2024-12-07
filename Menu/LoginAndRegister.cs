using Task2Exam;
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
            Console.WriteLine("======================= Welcome =======================\n");
            Console.WriteLine("1 ) Register As User\n");
            Console.WriteLine("2 ) Login As User\n");
            Console.WriteLine("3 ) Login As Admin\n");
            Console.WriteLine("4 ) Exit\n");
            Console.WriteLine("=======================================================\n");

            Console.Write("Enter Opt : ");
            string opt = Console.ReadLine();
            switch (opt)
            {
                case "1":Register(); break;
                case "2":Login(); break;
                case "3":LoginAsAdmin(); break;
                case "4": break;
                default: Console.Clear(); FirstMenu();  break;
            }
        }
        string GetValidInput(string prompt)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    Console.WriteLine("Input cannot be empty. Please try again.");
            } while (string.IsNullOrWhiteSpace(input));
             return input;
        }
        public void Register()
        {
            Console.Clear();
            Console.WriteLine("======================= Registeration =======================");
            User user = new User();


            string userName = GetValidInput("\nEnter Username : ");
            string password = GetValidInput("\nEnter Password : ");
            string DOB = GetValidInput("\nEnter DOB : ");

            user.Register(userName, password, DOB);
            Console.ReadLine();
            Console.Clear();
            FirstMenu();
        }

        public void Login()
        {
            Console.Clear();
            Console.WriteLine("=========================== User Login ===========================");

            string userName = GetValidInput("\nEnter Username : ");
                string password = GetValidInput("\nEnter Password : ");
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
            Console.Clear();
            Console.WriteLine("=========================== Admin Login ===========================");

            string userName = GetValidInput("\nEnter Username : ");
            string password = GetValidInput("\nEnter Passowrd : ");

            Admin admin = new Admin();
            admin.Login(userName, password);
            if (admin.Login(userName,password))
            {
                Console.WriteLine("vv");
                AdminConsole adminConsole = new AdminConsole();
                adminConsole.Menu();
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
