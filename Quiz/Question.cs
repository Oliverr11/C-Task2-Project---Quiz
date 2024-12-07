
using Task2Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Task2Exam
{
    public class Question
    {
        public string Text { get; set; }
        public List<Answer> Answers { get; set; } = new List<Answer>();
        public Question() { }
        public Question(string text, List<Answer> answers)
        {
            Text = text;
            Answers = answers;
        }
        public void DisplayQuestion()
        {
            Console.WriteLine("Question : {0}",Text);
        }
    }
}
