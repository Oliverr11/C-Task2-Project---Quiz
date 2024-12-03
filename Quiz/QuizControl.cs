using DemoJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2Exam
{
    public class QuizControl
    {
        private string filePath = "quizData.json";
        private IOManager iOManager;

        public QuizControl()
        {
            iOManager = new IOManager();
        }

        public void AddCategory(string categoryName)
        {
            
            List<QuizCategory> categories = GetCategories();

            if (categories.Any(c => c.QuizName.Equals(categoryName, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Category already exist!");
            }
            else
            {
                categories.Add(new QuizCategory(categoryName));

                iOManager.WriteJson(filePath, categories);
                Console.WriteLine("Category added successfully!");

            }
        }


        public void AddQuestionToCategory(string categoryName)
        {
            List<QuizCategory> categories = GetCategories();

            QuizCategory category = categories.FirstOrDefault(c => c.QuizName.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
            if (category == null)
            {
                Console.WriteLine("Category not found.");
                return;
            }

            Console.Write("Enter Question Text: ");
            string questionText = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(questionText))
            {
                Console.WriteLine("Question text cannot be empty.");
                return;
            }

            Console.Write("How many answers does this question have? ");
            int numAnswers;
            if (!int.TryParse(Console.ReadLine(), out numAnswers) || numAnswers <= 0)
            {
                Console.WriteLine("Invalid number of answers.");
                return;
            }

            List<Answer> answers = new List<Answer>();

            for (int i = 0; i < numAnswers; i++)
            {
                string answerText;
                do
                {
                    Console.Write("Enter Answer Text: ");
                    answerText = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(answerText))
                    {
                        Console.WriteLine("Answer text cannot be empty. Please enter a valid answer.");
                    }
                } while (string.IsNullOrWhiteSpace(answerText));

                answers.Add(new Answer { Text = answerText });
            }

            Console.WriteLine("Which answers are correct? Enter the numbers (comma separated, 1-based):");

            for (int i = 0; i < answers.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, answers[i].Text);
            }

            string userInput = Console.ReadLine();

            List<int> correctAnswerIndices = userInput.Split(',')
                                                      .Select(index => index.Trim())
                                                      .Where(index => int.TryParse(index, out int parsedIndex) && parsedIndex >= 1 && parsedIndex <= answers.Count)
                                                      .Select(index => int.Parse(index.Trim()) - 1) 
                                                      .ToList();

            // Check if any invalid indices were entered, such as an index greater than the number of answers
            if (correctAnswerIndices.Count != userInput.Split(',').Length || correctAnswerIndices.Any(index => index < 0 || index >= answers.Count))
            {
                Console.WriteLine("Invalid correct answer indices. Please enter valid numbers within the range of available answers.");
                return;
            }
            else
            {

                // Assign the correct answers based on the valid indices
                foreach (var index in correctAnswerIndices)
                {
                    answers[index].IsCorrect = true;
                }

                // Create a new question and add it to the category
                Question question = new Question(questionText, answers);
                category.Questions.Add(question);

                // Save the updated categories list to file
                iOManager.WriteJson(filePath, categories);

                Console.WriteLine("Question added successfully!");
            }
        }


        public void ViewCategoriesNoMixedQuiz()
        {
            List<QuizCategory> categories = iOManager.ReadJson<List<QuizCategory>>(filePath);

            categories = categories.Where(c => c.QuizName != "Mixed Quiz").ToList();

            Console.Clear();
            Console.WriteLine("======== Categories ========");

            for (int i = 0; i < categories.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, categories[i].QuizName);
            }
        }
        public void ViewCategories()
        {
            List<QuizCategory> categories = iOManager.ReadJson<List<QuizCategory>>(filePath);


            Console.Clear();
            Console.WriteLine("======== Categories ========");

            for (int i = 0; i < categories.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, categories[i].QuizName);
            }
        }


        public void ViewQuestions(string categoryName)
        {
            int index = 0;
            List<QuizCategory> questions = iOManager.ReadJson<List<QuizCategory>>(filePath);
            foreach (QuizCategory category in questions)
            {
                if (category.QuizName == categoryName)
                {
                    foreach (Question question in category.Questions)
                    {
                        index++;
                        Console.Write("{0} : ", index);
                        question.DisplayQuestion();
                    }
                }
            }
        }
     
        public void DeleteCategory(string categoryName)
        {
            List<QuizCategory> categories = GetCategories();
            QuizCategory category = categories.FirstOrDefault(c => c.QuizName.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
            if (category != null)
            {
                categories.Remove(category);
                iOManager.WriteJson(filePath, categories);
                Console.WriteLine("Category deleted successfully.");
            }
            else
            {
                Console.WriteLine("Category not found.");
            }
        }

        public void DeleteQuestionFromCategory(string categoryName)
        {
            List<QuizCategory> categories = GetCategories();

            QuizCategory category = categories.FirstOrDefault(c => c.QuizName.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
            ViewQuestions(categoryName);
            if (category != null)
            {
                Console.Write("Enter Question Text to Delete: ");
                string questionText = Console.ReadLine();
                Question question = category.Questions.FirstOrDefault(q => q.Text.Equals(questionText, StringComparison.OrdinalIgnoreCase));

                if (question != null)
                {
                    category.Questions.Remove(question);
                    iOManager.WriteJson(filePath, categories);
                    Console.WriteLine("Question deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Question not found.");
                }
            }
            else
            {
                Console.WriteLine("Category not found.");
            }
        }
        private List<QuizCategory> GetCategories()
        {
            List<QuizCategory> categories = iOManager.ReadJson<List<QuizCategory>>(filePath);
            if (categories == null)
                categories = new List<QuizCategory>();
            return categories;
        }

    }

}
