using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Task2Exam;

namespace Task2Exam
{
    public class AdminConsole
    {
        private QuizControl quizControl = new QuizControl();
        private IOManager iOManager;
        public AdminConsole()
        {
            iOManager = new IOManager();
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
        public void Menu()
        {
            Console.Clear();
            Console.WriteLine("=========================== ADMIN CONSOLE ===========================\n");
            Console.WriteLine("1 ) Manage Quiz\n");
            Console.WriteLine("2 ) Manage User\n");
            Console.WriteLine("3 ) Log Out\n");
            Console.WriteLine("=====================================================================\n");

            string choice = GetValidInput("Enter your choice: ");
            switch (choice)
            {
                case "1":
                    ManageQuiz();
                    break;
                case "2":
                    ManageUser();
                    break;
                case "3":
                     LoginAndRegister loginAndRegister =new LoginAndRegister();
                    Console.Clear();
                    loginAndRegister.FirstMenu();

                    break;
                default:
                    Menu();
                    break;
            }
        }
        public void ManageUser()
        {
            Console.Clear();
            Console.WriteLine("=========================== MANAGE USER ===========================\n");
            Console.WriteLine("1 ) View Top 20\n");
            Console.WriteLine("2 ) Edit Information\n");
            Console.WriteLine("3 ) Delete User\n");
            Console.WriteLine("4 ) Display All Users\n");
            Console.WriteLine("5 ) Back\n");
            Console.WriteLine("===================================================================\n");

            string choice = GetValidInput("Enter your choice: ");
            switch (choice)
            {
                case "1":
                    ViewTop20();
                    break;
                case "2":
                    EditInformation(); 
                    break;
                case "3":
                    DeleteUser();
                    break;
                case "4":
                    DisplayAllUsers();
                    break;
                case "5":
                    Menu();
                    break;
                default:
                    ManageUser();
                    break;
            }
        }
        public void DeleteUser()
        {
            Console.Clear();
            Console.WriteLine("=========================== Delete User ===========================\n");
            string userName = GetValidInput("Enter Username to delete : ");
            User user = new User();
            user.DeleteUser(userName);
            Console.ReadLine();
            ManageUser();
        }
        public void DisplayAllUsers()
        {
            Console.Clear();
            Console.WriteLine("=========================== All TOP 20 ===========================\n");
            User user = new User();
            user.DisplayAllUsers();
            Console.ReadLine();
            ManageUser();
        }
        public void ViewTop20()
        {
            Console.Clear();
            Console.WriteLine("=========================== VIEW TOP 20 ===========================\n");

            QuizControl quizControl = new QuizControl();
            quizControl.ViewCategories();
            string category = GetValidInput("Enter Category (Name): ");
            Console.WriteLine("===========================  TOP 20 ===========================\n");
            TakeQuiz takeQuiz = new TakeQuiz();
            takeQuiz.ViewTop20Admin(category);
            Console.ReadLine();
            ManageUser();
        }
        public void EditInformation()
        {
            string userName = GetValidInput("Enter Username : ");
            try
            {
                 User user = iOManager.ReadUserFromFile(userName);
                if (user.userName == userName)
                {
                    Console.Clear();
                    Console.WriteLine("========= Change Information for [{0}] =========", userName);
                    Console.WriteLine("1 ) Password ");
                    Console.WriteLine("2 ) Date of birth");
                    Console.WriteLine("3 ) Back");
                    string opt = GetValidInput("Enter option : ");
                    switch (opt)
                    {
                        case "1":
                            string password = GetValidInput("Enter New Password : ");
                            user.ChangePassword(userName, password);
                            ManageUser();
                            break;
                        case "2":
                            string DOB = GetValidInput("Enter New Date of birth : ");
                            user.ChangeDOB(userName, DOB);
                            ManageUser();
                            break;
                        case "3":
                            ManageUser();
                            break;
                        default: Console.WriteLine("Unknown Option."); Console.ReadLine(); ManageUser(); break;

                    }
                }
                else{
                    Console.ReadLine();
                    Console.WriteLine("Can't found this User!");
                    ManageUser();
                }
            }
            catch (Exception)
            {
                Console.ReadLine() ;
                Console.WriteLine("Can't found this User!");
                ManageUser();
            }
        }
        
        public void ManageQuiz()
        {
                Console.Clear();
            Console.WriteLine("=========================== MANAGE QUIZ ===========================\n");
                Console.WriteLine("1. Add Category\n");
                Console.WriteLine("2. Add Question to Category\n");
                Console.WriteLine("3. View Categories and Questions\n");
                Console.WriteLine("4. Delete Category\n");
                Console.WriteLine("5. Delete Question from Category\n");
                Console.WriteLine("6. Back\n");
            Console.WriteLine("==================================================================\n");

            string choice = GetValidInput("Enter your choice: ");

                switch (choice)
                {
                    case "1":
                        AddCategory();
                        break;
                    case "2":
                        AddQuestionToCategory();
                        break;
                    case "3":
                        ViewCategoriesAndQuestions();
                        break;
                    case "4":
                        DeleteCategory();
                        break;
                    case "5":
                        DeleteQuestionFromCategory();
                        break;
                    case "6":
                    Menu();
                        break;
                    default:
                        Console.WriteLine("Invalid choice, try again.");
                         ManageQuiz();        
                        break;
                
            }
        }

        private void AddCategory()
        {
            Console.Clear();
            Console.WriteLine("=========================== ADD CATEGORY ===========================\n");
            Console.WriteLine("# 0 to Exit\n");
            string categoryName = GetValidInput("Enter Category Name : ");
            Console.WriteLine("\n");
            if (categoryName == "0")
            {
                ManageQuiz();
            }
            quizControl.AddCategory(categoryName);   
            Console.ReadLine();
            ManageQuiz();
        }

        private void AddQuestionToCategory()
        {
            Console.Clear();
            Console.WriteLine("=========================== ADD Question To Category ===========================\n");

            quizControl.ViewCategoriesNoMixedQuiz();
            Console.WriteLine("# 0 to Exit\n");
            string categoryName = GetValidInput("Enter Category name To Add Question: ");
            Console.WriteLine("\n");
            if (categoryName =="0")
            {
                ManageQuiz();
            }
            quizControl.AddQuestionToCategory(categoryName);
            ManageQuiz();

        }

        private void ViewCategoriesAndQuestions()
        {
            Console.Clear();
            Console.WriteLine("=========================== View Category And Questions ===========================\n");


            quizControl.ViewCategoriesNoMixedQuiz();
            Console.WriteLine("# 0 to Exit");
            string categories = GetValidInput("Enter Category name : ");
            if (categories == "0")
            {
                ManageQuiz();
            }
            quizControl.ViewQuestions(categories);
            Console.ReadLine();
            ManageQuiz();
        }

        private void DeleteCategory()
        {
            Console.Clear();
            Console.WriteLine("=========================== Delete Category ===========================\n");

            quizControl.ViewCategoriesNoMixedQuiz();
            Console.WriteLine("# 0 to Exit\n");
            string categoryName = GetValidInput("Enter Category name to delete : ");
            Console.WriteLine("\n");
            if (categoryName == "0")
            {
                ManageQuiz();
            }
            quizControl.DeleteCategory(categoryName);
            Console.ReadKey();
            ManageQuiz();

        }

        private void DeleteQuestionFromCategory()
        {

            Console.Clear();
            Console.WriteLine("=========================== Delete Questions From Categories ===========================\n");

            quizControl.ViewCategoriesNoMixedQuiz();
            Console.WriteLine("# 0 to Exit");
            string categoryName = GetValidInput("Enter Category name : ");
            if (categoryName == "0")
            {
                ManageQuiz();
            }
            quizControl.DeleteQuestionFromCategory(categoryName);
            Console.ReadKey();
            ManageQuiz();

        }
    }
}
