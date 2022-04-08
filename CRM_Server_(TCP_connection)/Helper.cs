using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Server__TCP_connection_
{
    public static class Helper
    {
        public static bool IsInt(this string input, out int result)
        {
            result = 0;

            if (int.TryParse(input, out var value) && value >= 0)
            {
                result = value;
                return true;
            }
            else
            { 
                return false;
            }
        }

        public static bool IsDouble(this string input, out double result)
        {
            result = 0;

            if (double.TryParse(input, out var value) && value >= 0 && value <= 12)
            {
                result = value;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsEmployee(this string input, out Employee result)
        {
            result = new Employee();

            if (int.TryParse(input, out var Id) && Id >= 0 && Employee.GetAllPossibleIds().Contains(Id))
            {
                result = Employee.GetById(Id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsDateTime(this string input, out DateTime result)
        {
            result = new DateTime();

            if (DateTime.TryParse(input, out var value) && value >= DateTime.Parse("01.01.2010") && value <= DateTime.Now)
            {
                result = value;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsKey(this string input, out ConsoleKey result)
        {
            result = ConsoleKey.Spacebar;

            if (string.Equals(input.ToLower(), "y"))
            {
                result = ConsoleKey.Y;
                return true;
            }
            else if (string.Equals(input.ToLower(), "n"))
            {
                result = ConsoleKey.N;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string String(this Type input)
        {
            if (input == typeof(int))
            {
                return "Вы ввели некорректное число от 1 до 9.";
            }
            if (input == typeof(double))
            {
                return "Корректное число для отработанного времени от 0 до 12.";
            }
            if (input == typeof(Employee))
            {
                return "Необходимо ввести Id нужного сотрудника.";
            }
            if (input == typeof(DateTime))
            {
                return "Необходимо ввести любую дату от 01.01.2010 по сегодняшний день";
            }
            if (input == typeof(ConsoleKey))
            {
                return "Введите N или Y";
            }
            if (input == typeof(string))
            {
                return "Введите корректное строковое значение.";
            }
            return "";
        }


    }
}
