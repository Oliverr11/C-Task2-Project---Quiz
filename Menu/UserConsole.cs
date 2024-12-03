using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task2Exam
{
    public class UserConsole
    {
        public User currentUser;

        public UserConsole (User user)
        {
            currentUser = user;
        }
        public void FirstMenu()
        {
            Console.Clear();

            Console.WriteLine("=======  Welcome " + currentUser.userName + " =======");
            Console.WriteLine("1 ) Take Specific Quiz");
            Console.WriteLine("2 ) Take Mixed Quiz");
            Console.WriteLine("3 ) View the results of the previous quizzes");
            Console.WriteLine("4 ) View top 20");
            Console.WriteLine("5 ) Edit Profile");
            Console.WriteLine("6 ) Log Out");
            Console.Write("Enter Opt : ");
            string opt = Console.ReadLine();
            switch (opt)
            {
                case "1":
                    TakeQuizz();
                    break;
                case "2":
                    TakeMixedQuizz();
                    break;
                case "3":
                    ViewPreviousQuizzes();
                    break;
                case "4":
                    ViewTop();
                    break;
                case "5":
                    EditProfile();
                    break;
                case "6":
                    Console.Clear();
                    LogOut(); 
                    break;
                default:
                    FirstMenu();
                    break;
            }
        }
        public void ViewTop()
        {
            Console.Clear();
            Console.WriteLine("======== View Top ========");
            Console.WriteLine("1 ) View top");
            Console.WriteLine("2 ) Back");
            Console.WriteLine("Enter Opt : ");
            string opt = Console.ReadLine();
            switch(opt)
            {
                case "1": ViewTopForAllCataeroies(); break;
                case "2": FirstMenu(); break;
                default: ViewTop(); break;
            }
        }
        public void TakeMixedQuizz()
        {
            Console.WriteLine("Are you sure to take Mixed Quizz ? [yes/no]");
            string  decide = Console.ReadLine().ToLower();
            if (decide == "yes")
            {
                Console.Clear() ;
                Console.WriteLine("======== Mixed Quiz ========");
                TakeQuiz takeQuiz = new TakeQuiz(currentUser.userName);
                takeQuiz.StartMixedQuiz();
                Console.ReadLine();
                FirstMenu();
            }
            else
            {
                FirstMenu();
            }
        }
        public void ViewTopForAllCataeroies()
        {
            Console.Clear();
            Console.WriteLine("======== View Top ========");
            
            TakeQuiz takeQuiz = new TakeQuiz(currentUser);
            QuizControl quizControl = new QuizControl();
            quizControl.ViewCategories();

            Console.Write("Enter Category (Name): ");
            string category = Console.ReadLine();
            
            takeQuiz.ViewTop20(category);
            Console.ReadLine();
            ViewTop();
        }
     

        public void EditProfile()
        {
            Console.Clear();
            Console.WriteLine("======== Edit Profile ========");
            Console.WriteLine("1 ) Change Password");
            Console.WriteLine("2 ) Change Date Of Birth");
            Console.WriteLine("3 ) Back");
            Console.Write("Enter Opt : ");
            string opt =Console.ReadLine();
            switch (opt)
            {
                case "1": ChangePassword();  break;
                case "2": ChangeDOB(); break;
                case "3": FirstMenu(); break;
                default: EditProfile(); break;

            }
        }
        public void ChangePassword()
        {
            Console.Clear();
            Console.WriteLine("======== Change Password ========");
            Console.WriteLine("Enter New Password : ");
            string password = Console.ReadLine();
            currentUser.ChangePassword(currentUser.userName,password);
            EditProfile();
        }
        public void ChangeDOB()
        {
            Console.Clear();
            Console.WriteLine("======== Change DOB ========");
            Console.WriteLine("Enter New Date of birth: ");
            string DOB = Console.ReadLine();
            currentUser.ChangeDOB(currentUser.userName,DOB);
            EditProfile();
        }
        public void ViewPreviousQuizzes()
        {
            Console.Clear();
            Console.WriteLine("======== View Previous Quizzes Of {0} ========",currentUser.userName );
            TakeQuiz takeQuiz = new TakeQuiz(currentUser.userName);
            takeQuiz.ViewPreviousQuiz();
            Console.ReadLine();
            FirstMenu();
        }
        public void TakeQuizz()
        {
           
                Console.Clear();
                TakeQuiz takeQuiz = new TakeQuiz(currentUser.userName);
                takeQuiz.StartSpecificQuiz();
                Console.ReadLine();
                FirstMenu();
           
        }
        public void LogOut()
        {
            LoginAndRegister loginAndRegister = new LoginAndRegister();
            loginAndRegister.FirstMenu();
        }

    }
}
