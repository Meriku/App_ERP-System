using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Server__TCP_connection_
{
    public static class UserInterface
    {

        public static void FirstGreeting()
        {
            // Возможно создать List строк "PossibleDecisions", и внести туда возможные варианты ответа
            // Эти варианты вывести с помощью цикла на консоль, и ограничить возможные варианты ответа длинной списка (от 1 до List.Count)
            // В таком случае, если необходимо будет масштабировать проект, это можно будет сделать проще
            // Но поскольку это "учебный проект", и я масштабировать его точно не буду выведем меню следующим образом:

            Console.WriteLine("Вас приветствует приложение CRM2000v1.0");
            Console.WriteLine("\nДля выбора необходимого действия введите цифру: ");

            Console.WriteLine("\n Внести информацию:");
            Console.WriteLine("\t1 - про новый полный рабочий день (8 часов) для сотрудника");          //AddFullWorkDay
            Console.WriteLine("\t2 - про новый неполный рабочий день для сотрудника");                  //AddPartWorkDay

            Console.WriteLine("\n Получить информацию:");
            Console.WriteLine("\t3 - про количество отработанных сотрудником часов за дату");           //GetInfoWorkHoursPerDate
            Console.WriteLine("\t4 - про количество отработанных сотрудником часов за период");         //GetInfoWorkHoursPerPeriod
            Console.WriteLine("\t5 - про количество отработанных сотрудником часов за все время");      //GetInfoWorkHoursAllTime

            Console.WriteLine("\n Отредактировать информацию:");
            Console.WriteLine("\t6 - о количестве отработанных сотрудником часов в определенную дату"); //ChangeWorkHoursAtDate

            Console.WriteLine("\n Уволить или нанять сотрудника:");
            Console.WriteLine("\t7 - уволить сотрудника");                                              //ToFireEmployee
            Console.WriteLine("\t8 - нанять сотрудника");                                               //ToHireEmployee

        }

        public static int GetUserDecision()  
        {
            int UserDecision;               // Навигация в меню

            while (true) 
            {
                if (int.TryParse(Console.ReadLine(), out UserDecision) && UserDecision <= 8 && UserDecision >= 1)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Введите цифру от 1 до 8");
                }
            }
            return UserDecision;
        }



        public static string AskFirstNameForNewEmployee() 
        {
            string result;                  // Имя для найма сотрудника

            Console.Clear();
            Console.WriteLine("Введите имя нового сотрудника:");

            result = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(result))
            {
                Console.WriteLine("Введите имя нового сотрудника:");
                result = Console.ReadLine();
            }
           
            result.Trim();      
            result = char.ToUpper(result[0]) + result.Substring(1);

            return result;
        }

        public static string AskLastNameForNewEmployee() 
        {
            string result;                  // Фамилия для найма сотрудника

            Console.WriteLine("Введите фамилию нового сотрудника:");

            result = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(result))
            {
                Console.WriteLine("Введите фамилию нового сотрудника:");
                result = Console.ReadLine();
            }

            result.Trim();
            result = char.ToUpper(result[0]) + result.Substring(1);

            return result;
        }





        public static Employee AskWhichEmployee()       // Выбор интересующего сотрудника для дальнейших действий
        {                       
            if (Employee.EvenOneEmployeeInListExist())
            {
                Employee.PrintListOfEmployees();
                Console.WriteLine("\nВведите порядковый номер необходимого сотрудника:");

                int result;

                while (true)                            // Проверка на корректность введенной информации
                {
                    if (int.TryParse(Console.ReadLine(), out result) && result >= 0 && result < Employee.GetNumberOfEmployees())
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Введите цифру от 0 до {Employee.GetNumberOfEmployees() - 1}");
                    }
                }

                Console.Clear();
                Console.WriteLine($"Вы выбрали сотрудника {Employee.GetEmployeeFNameLNameByIndex(result)}");

                return Employee.GetEmployeeByIndex(result);
            }
            else
            {
                Console.WriteLine("Список сотрудников пуст. Наймите хотя бы одного человека для работы.");
                Console.ReadLine();
                return null;
            }

        }

    
        public static DateTime AskWhatDate()            // Выбор интересующей даты для дальнейших действий
        {
            Console.Clear();
            Console.WriteLine("Введите дату рабочего дня выбранного сотрудника в формате: 00.00.0000");

            DateTime result;

            while (true)                                // Проверка на корректность введенной информации
            {
                if (DateTime.TryParse(Console.ReadLine(), out result) && result.Year >= 2000 && result <= DateTime.Now)
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Введите корректную дату в формате: 00.00.0000");
                }
            }

            Console.Clear();
            return result;

        }

        public static double AskWorkHoursCount()        // Запрос к пользователю на количество часов 
        {
            Console.Clear();
            Console.WriteLine("Введите число часов отработанных сотрудником в этот день");

            double result;

            while (true)                                // Проверка на корректность введенной информации
            {
                if (double.TryParse(Console.ReadLine(), out result) && result > 0 && result <= 12)
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Введите корректное число от 0 до 12)");
                }
            }

            return result;

        }

        public static double AskWorkHoursToEditInfo()   // Запрос к пользователю на количество часов  (для редактирования информации)
        {
            Console.WriteLine("\nСколько часов он отработал в этот день на самом деле:");

            double result;

            while (true)                                // Проверка на корректность введенной информации
            {
                if (double.TryParse(Console.ReadLine(), out result) && result > 0 && result <= 12)
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Введите корректное число от 0 до 12)");
                }
                    
            }

            return result;

        }

        public static void AskQuitOrContinue()          // Продолжить работу или выйти с приложения
        {
            Console.WriteLine("\nПродолжить работу с системой? Y or N ");
            while (true)
            {
                var answer = Console.ReadLine().ToLower();
                if (string.Equals(answer, "y"))
                {
                    Console.WriteLine("Продолжаем работу");
                    break;
                }
                else if (string.Equals(answer, "n"))
                {
                    Environment.Exit(0);
                    break;
                }
                else
                {
                    Console.WriteLine("Введите Y для продолжение работы или N для выхода из приложения");
                }

            }      
        }


        public static DateTime AskWhatDateForPeriod(int firstorseconddate)  // Запрос на Первую (1) и Вторую (2) дату для периода
        {
            Console.Clear();
            if (firstorseconddate == 1)
            {
                Console.WriteLine("Введите первую дату для необходимого периода в формате: 00.00.0000");
            }
            if (firstorseconddate == 2)
            {
                Console.WriteLine("Введите вторую дату для необходимого периода в формате: 00.00.0000");
            }

            DateTime result;

            while (true)                                                    // Проверка на корректность введенной информации
            {
                if (DateTime.TryParse(Console.ReadLine(), out result) && result.Year >= 2000 && result <= DateTime.Now)
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Введите корректную дату в формате: 00.00.0000");
                }                
            }

            Console.Clear();
            return result;

        }

 
    }
}
