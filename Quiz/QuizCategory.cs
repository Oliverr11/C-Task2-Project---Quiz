using Task2Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2Exam
{
    public class QuizCategory
    {
        public string QuizName { get; set; }
        public QuizCategory() { }
        public QuizCategory(string name)
        {
            this.QuizName = name;
        }
        public List<Question> Questions { get; set; } = new List<Question>();

    }
}
