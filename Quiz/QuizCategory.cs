using DemoJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoJson
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
