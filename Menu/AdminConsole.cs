using System;
using System.Collections.Generic;
using System.Linq;
using Task2Exam;

namespace DemoJson
{
    public class AdminConsole
    {
        private QuizControl quizControl = new QuizControl();
        private IOManager iOManager;
        public AdminConsole()
        {
            iOManager = new IOManager();
        }

        public void Start()
        {
                Console.Clear();
                Console.WriteLine("========= Admin Console =========");
                Console.WriteLine("1. Add Category");
                Console.WriteLine("2. Add Question to Category");
                Console.WriteLine("3. View Categories and Questions");
                Console.WriteLine("4. Delete Category");
                Console.WriteLine("5. Delete Question from Category");
                Console.WriteLine("6. Log Out");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

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
                        Console.Clear();
                        LoginAndRegister loginAndRegister = new LoginAndRegister();
                        loginAndRegister.FirstMenu();
                        return;
                    default:
                        Console.WriteLine("Invalid choice, try again.");
                         Start();        
                        break;
                
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

        private void AddCategory()
        {
            Console.Clear();
            Console.WriteLine ("====== Add Category ======");
            Console.WriteLine("# 0 to Exit");
            string categoryName = GetValidInput("Enter Category Name : ");
            if(categoryName == "0")
            {
                Start();
            }
            quizControl.AddCategory(categoryName);   
            Console.ReadLine();
            Start();
        }

        private void AddQuestionToCategory()
        {
            quizControl.ViewCategoriesNoMixedQuiz();
            Console.WriteLine("# 0 to Exit");
            string categoryName = GetValidInput("Enter Category name To Add Question: ");
            if (categoryName =="0")
            {
                Start();
            }
            quizControl.AddQuestionToCategory(categoryName);
            Console.ReadLine();
            Start();

        }

        private void ViewCategoriesAndQuestions()
        {

            quizControl.ViewCategoriesNoMixedQuiz();
            Console.WriteLine("# 0 to Exit");
            string categories = GetValidInput("Enter Category name : ");
            if (categories == "0")
            {
                Start();
            }
            quizControl.ViewQuestions(categories);
            Console.ReadLine();
            Start();
        }

        private void DeleteCategory()
        {

            quizControl.ViewCategoriesNoMixedQuiz();
            Console.WriteLine("# 0 to Exit");
            string categoryName = GetValidInput("Enter Category name to delete : ");
            if (categoryName == "0")
            {
                Start();
            }
            quizControl.DeleteCategory(categoryName);
            Console.ReadKey();
            Start();

        }

        private void DeleteQuestionFromCategory()
        {

            quizControl.ViewCategoriesNoMixedQuiz();
            Console.WriteLine("# 0 to Exit");
            string categoryName = GetValidInput("Enter Category name : ");
            if (categoryName == "0")
            {
                Start();
            }
            quizControl.DeleteQuestionFromCategory(categoryName);
            Console.ReadKey();
            Start();

        }
    }
}
