using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Server__TCP_connection_
{
    public class Answer
    {
        public virtual ClientObject Client { get; set; }
        public int IntAnswer { get; set; }

        public double DoubleAnswer { get; set; }

        public Employee EmployeeAnswer { get; set; }

        public DateTime DatetimeAnswer { get; set; }

        public string StringAnswer { get; set; }

        public ConsoleKey ConsolekeyAnswer { get; set; }

        public Type AnswerType;

        public Answer()
        {

        }

        public Answer(int intanswer, ClientObject client)
        {
            IntAnswer = intanswer;
            AnswerType = typeof(int);
            Client = client;
            Client.Answers.Add(this);
        }
        public Answer(double doubleanswer, ClientObject client)
        {
            DoubleAnswer = doubleanswer;
            AnswerType = typeof(double);
            Client = client;
            Client.Answers.Add(this);
        }
        public Answer(Employee Employeeanswer, ClientObject client)
        {
            EmployeeAnswer = Employeeanswer;
            AnswerType = typeof(Employee);
            Client = client;
            Client.Answers.Add(this);
        }
        public Answer(DateTime DateTimeanswer, ClientObject client)
        {
            DatetimeAnswer = DateTimeanswer;
            AnswerType = typeof(DateTime);
            Client = client;
            Client.Answers.Add(this);
        }
        public Answer(ConsoleKey Consolekeyanswer, ClientObject client)
        {
            ConsolekeyAnswer = Consolekeyanswer;
            AnswerType = typeof(ConsoleKey);
            Client = client;
            Client.Answers.Add(this);
        }

        public Answer(string stringanswer, ClientObject client)
        {
            StringAnswer = stringanswer;
            AnswerType = typeof(string);
            Client = client;
            Client.Answers.Add(this);
        }

        public override string ToString()
        {    
            if (AnswerType == typeof(int))
            {
                return IntAnswer.ToString();
            }
            if (AnswerType == typeof(double))
            {
                return DoubleAnswer.ToString();
            }
            if (AnswerType == typeof(Employee))
            {
                return EmployeeAnswer.ToString();
            }
            if (AnswerType == typeof(DateTime))
            {
                return DatetimeAnswer.ToString("d");
            }
            if (AnswerType == typeof(ConsoleKey))
            {
                return ConsolekeyAnswer.ToString();
            }
            if (AnswerType == typeof(string))
            {
                return StringAnswer;
            }

            return "Empty answer";
        }
    }
}
