using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Task2Exam
{
    public class User
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string DOB { get; set; }

        private List<User> userList = new List<User>();
        public Dictionary<string, List<int>> QuizHistory { get; set; }

        public User()
        {

        }
        public User(string userName, string password, string dOB)
        {
            this.userName = userName;
            this.password = password;
            this.DOB = dOB;
            this.QuizHistory = new Dictionary<string, List<int>>();

        }
        public bool Login(string userName, string password)
        {
            IOManager iOManager = new IOManager();
            User user = iOManager.ReadUserFromFile(userName);


            if (user.userName == userName && user.password == password)
            {
                return true;
            }

            return false;
        }
        public void Register(string newUserName, string password, string DOB)
        {
            IOManager iOManager = new IOManager();

            User newUser = new User(newUserName, password, DOB);

            List<User> users = iOManager.ReadAllUsers(); 

            if (users.Any(u => u.userName == newUserName))
            {
                Console.WriteLine("Username already exists!");
            }
            else
            {
                iOManager.WriteUserToFile(newUserName,newUser);
                Console.WriteLine("User registered successfully!");
            }
        }

        public void ChangePassword(string userName,string newPassword)
        {
            IOManager iOManager = new IOManager();
            User user = iOManager.ReadUserFromFile(userName);
            user.password = newPassword;
            iOManager.WriteUserToFile(userName,user);
        }
        public void ChangeDOB(string userName, string newDOB)
        {
            IOManager iOManager = new IOManager();
            User user = iOManager.ReadUserFromFile(userName);
            user.DOB = newDOB;
            iOManager.WriteUserToFile(userName, user);
        }
        public void AddQuizResult(string category, int score)
        {
            if (!QuizHistory.ContainsKey(category))
            {
                QuizHistory[category] = new List<int>();
            }
            QuizHistory[category].Add(score); 
        }
        public int GetScoreForCategory(string categoryName)
        {
            if (QuizHistory.ContainsKey(categoryName))
            {
                return QuizHistory[categoryName].Max();
            }
            return 0;  
        }
        public void ViewPreviousQuizzes()
        {
            if (QuizHistory.Count == 0)
            {
                Console.WriteLine("No previous quiz results found.");
                return;
            }

            Console.WriteLine("Previous Quiz Results:");
            foreach (var category in QuizHistory)
            {
                Console.WriteLine("Category: {0}", category.Key);

                for (int i = 0; i < category.Value.Count; i++)
                {
                    Console.WriteLine(" {0}) Score: {1}", i + 1, category.Value[i]);
                }
            }
        }
        public void DeleteUser(string userName)
        {
            IOManager iOManager = new IOManager();
            List<User> users = iOManager.ReadAllUsers();

            User userToDelete = users.FirstOrDefault(u => u.userName.Equals(userName, StringComparison.OrdinalIgnoreCase));
            if (userToDelete != null)
            {
                users.Remove(userToDelete);
                iOManager.DeleteUserFile(userName); 
                Console.WriteLine("User {0} has been deleted successfully.",userName);
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }
        public void Display()
        {
            Console.WriteLine("Username : {0} ", userName);
        }
        public void DisplayAllUsers()
        {
            IOManager iOManager = new IOManager();
            List<User> users = iOManager.ReadAllUsers();
            if (users.Count == 0)
            {
                Console.WriteLine("No users found.");
                return;
            }
            foreach (var user in users)
            {
                user.Display();
                
            }
            Console.WriteLine("Total user : {0} ", users.Count);
        }
    }
}