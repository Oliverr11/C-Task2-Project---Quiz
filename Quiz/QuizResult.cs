using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2Exam
{
    public class QuizResult
    {
        public string Category { get; set; }
        public int Score { get; set; }

        public QuizResult(string category, int score)
        {
            Category = category;
            Score = score;
        }

    }
}
