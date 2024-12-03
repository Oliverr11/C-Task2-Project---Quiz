using DemoJson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Task2Exam;

namespace Task2Exam
{
    public class TakeQuiz
    {
        private string currentUserName;
        private string filePath = "quizData.json";
        private User currentUser;
        private IOManager iOManager;

        public TakeQuiz(string userName)
        {
            currentUserName = userName;
            iOManager = new IOManager();
        }
        public TakeQuiz(User user)
        {
            currentUser = user;
            iOManager = new IOManager();

        }
        private void StartQuizQuestions(List<Question> questions, string categoryName)
        {
            int score = 0;
            List<Question> askedQuestions = new List<Question>();  

            foreach (var question in questions)
            {
                if (askedQuestions.Contains(question))
                {
                    continue; 
                }
                askedQuestions.Add(question);
                Console.WriteLine("\nQuestion: {0}", question.Text);
                for (int i = 0; i < question.Answers.Count; i++)
                {
                    Console.WriteLine("{0}. {1}", i + 1, question.Answers[i].Text);
                }

                List<int> userAnswers = new List<int>();

                while (true)
                {
                    Console.WriteLine("Select the correct answers by number, by commas (e.g., 1,3): ");
                    string userInput = Console.ReadLine();

                    if (string.IsNullOrEmpty(userInput))
                    {
                        Console.WriteLine("Input cannot be empty. Please try again.");
                        continue;
                    }
                    try
                    {
                        userAnswers = userInput.Split(',')
                            .Select(int.Parse)
                            .Select(i => i - 1)  
                            .ToList();

                        if (userAnswers.All(i => i >= 0 && i < question.Answers.Count))
                        {
                            break;  
                        }
                        else
                        {
                            Console.WriteLine("Some answers are out of range. Please enter valid answer numbers.");
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid input. Please enter numbers separated by commas.");
                    }
                }

                if (IsCorrectAnswer(question, userAnswers))
                {
                    Console.WriteLine("Correct!");
                    score++;
                }
                else
                {
                    Console.WriteLine("Incorrect.");
                }
            }

            Console.WriteLine("\nQuiz completed! Your score is: {0}/{1}", score, questions.Count);

            User user = iOManager.ReadUserFromFile(currentUserName);

            if (!user.QuizHistory.ContainsKey("Mixed Quiz") && (categoryName == "Mixed Quiz"))
            {
                user.QuizHistory["Mixed Quiz"] = new List<int>();
            }
            user.AddQuizResult(categoryName, score);

            iOManager.WriteUserToFile(currentUserName, user);
        }

        public void StartSpecificQuiz()
        {

                List<QuizCategory> categories = iOManager.ReadJson<List<QuizCategory>>(filePath);

                categories = categories.Where(c => c.QuizName != "Mixed Quiz").ToList();

                QuizControl quizControl = new QuizControl();
                quizControl.ViewCategoriesNoMixedQuiz();

            
                Console.Write("Enter Category (number): ");
            try
            {
                int categoryIndex = int.Parse(Console.ReadLine()) - 1;

                if (categoryIndex < 0 || categoryIndex >= categories.Count)
                {
                    Console.WriteLine("Invalid category selection.");
                    return;
                }
                QuizCategory selectedCategory = categories[categoryIndex];

                Console.WriteLine("You selected: {0}", selectedCategory.QuizName);

                var random = new Random();
                var shuffledQuestions = selectedCategory.Questions.OrderBy(q => random.Next()).Take(10).ToList();

                StartQuizQuestions(shuffledQuestions, selectedCategory.QuizName);

            }catch (Exception)
            {
                Console.WriteLine("Invalid Category!");
                return;
            }
        }

        public void StartMixedQuiz()
        {
            List<QuizCategory> categories = iOManager.ReadJson<List<QuizCategory>>(filePath);

            List<Question> allQuestions = categories.SelectMany(c => c.Questions).ToList();
            if (allQuestions.Count == 0)
            {
                Console.WriteLine("No questions available in any category.");
                return;
            }

            var random = new Random();
            var shuffledQuestions = allQuestions.OrderBy(q => random.Next()).Take(10).ToList();

            StartQuizQuestions(shuffledQuestions, "Mixed Quiz");
        }


        private bool IsCorrectAnswer(Question question, List<int> userAnswers)
        {
            List<int> correctAnswers = question.Answers
                .Select((answer, index) => answer.IsCorrect ? index : -1)
                .Where(index => index != -1)
                .ToList();

            return userAnswers.Count == correctAnswers.Count && !userAnswers.Except(correctAnswers).Any();
        }

        public void ViewTop20(string categoryName)
        {

            List<QuizCategory> categories = iOManager.ReadJson<List<QuizCategory>>(filePath);

            bool categoryExists = categories.Any(c => c.QuizName.Equals(categoryName, StringComparison.OrdinalIgnoreCase));

            if (!categoryExists)
                {
                    Console.WriteLine("The category '{0}' does not exist.", categoryName);
                    return;
                }
            
            Console.Clear();
            Console.WriteLine("======= Top 20 =======");
                List<User> users = iOManager.ReadAllUsers();

                var allUsersWithScores = users.Select(user => new { userName = user.userName, score = user.GetScoreForCategory(categoryName) })
                                                .OrderByDescending(u => u.score)
                                                .ToList();

                Console.WriteLine("Top Users for category : {0} ", categoryName);
                int rank = 1;
                foreach (var user in allUsersWithScores.Take(20))
                {
                    Console.WriteLine("{0} . {1} : {2} scores", rank, user.userName, user.score);
                    rank++;
                }
                var currentUserScore = allUsersWithScores.FirstOrDefault(u => u.userName == currentUser.userName);

                if (currentUserScore.score == 0 || allUsersWithScores.IndexOf(currentUserScore) >= 20)
                {
                    int currentUserRank = allUsersWithScores.IndexOf(currentUserScore) + 1;
                    Console.WriteLine("...");
                    Console.WriteLine("{0}. {1} : {2} s", currentUserRank, currentUser.userName, currentUser.GetScoreForCategory(categoryName));
                }
         
        }
        public void ViewPreviousQuiz()
        {
            User user = iOManager.ReadUserFromFile(currentUserName);
            user.ViewPreviousQuizzes();
        }
    }
}
