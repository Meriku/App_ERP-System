using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Server__TCP_connection_
{
    public static class UserInterface
    {
        public static void ClearClientConsole(ClientObject client)       // Выводим список сотрудников
        {

            ServerObject.SendMessage("#", client);

        }

        public static void PrintMainMenu(ClientObject client)
        {
            ServerObject.SendMessage("\nДля выбора необходимого действия введите цифру: " +
                                                 "\n\n Внести информацию:" +
                                                 "\n\t1 - про новый полный рабочий день (8 часов) для сотрудника" +
                                                 "\n\t2 - про новый неполный рабочий день для сотрудника" +
                                                 "\n\n Получить информацию:" +
                                                 "\n\t3 - про количество отработанных сотрудником часов за дату" +
                                                 "\n\t4 - про количество отработанных сотрудником часов за период" +
                                                 "\n\t5 - про количество отработанных сотрудником часов за все время" +
                                                 "\n\t6 - про всех сотрудников компании и общее количество отработанных часов" +
                                                 "\n\n Отредактировать информацию:" +
                                                 "\n\t7 - о количестве отработанных сотрудником часов в определенную дату" +
                                                 "\n\n Уволить или нанять сотрудника:" +
                                                 "\n\t8 - уволить сотрудника" +
                                                 "\n\t9 - нанять сотрудника" +
                                                 "\n\n Завершить работу:" +
                                                 "\n\t10 - сохранить информацию про изменения и закрыть программу", client);
        }



        public static void PrintListOfEmployees(ClientObject client)       // Выводим список сотрудников
        {      
        
            ServerObject.SendMessage("\nСписок сотрудников:" + Employee.GetListOfEmployees(), client);
                 
        }

        public static void AskWhichEmployee(ClientObject client)       // Выбор интересующего сотрудника для дальнейших действий
        {

            ServerObject.SendMessage("\n\nВведите порядковый номер необходимого сотрудника:", client);

        }

        public static void AskWhatDate(ClientObject client)            // Выбор интересующей даты для дальнейших действий
        {
            
            ServerObject.SendMessage("\nВведите дату рабочего дня выбранного сотрудника в формате: 00.00.0000", client);

        }

        public static void AskQuitOrContinue(ClientObject client)          // Продолжить работу или выйти с приложения
        {
            ServerObject.SendMessage("\nПродолжить работу с системой? Y or N ", client);
      
        }







        public static int GetUserDecision()  
        {
            int UserDecision;               // Навигация в меню

            while (true) 
            {
                if (int.TryParse(Console.ReadLine(), out UserDecision) && UserDecision <= 10 && UserDecision >= 1)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Введите цифру от 1 до 10");
                   
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
                    Console.WriteLine($"Введите корректное число от 0 до 12");
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

        public static void PrintInfoWorkHoursAtDate(DateTime workday, double hours)
        {         
            if (hours == 0)
            {
            Console.WriteLine($"Данный сотрудник не был на работе {workday.ToString("d")}");
            }
            else
            {
                Console.WriteLine($"Данный сотрудник отработал {hours} часов {workday.ToString("d")}");
            }
        }


        #region LoadingInteface

        //public static async Task PrintLoadingAsync()
        //{
        //    Console.Write("\nИдет загрузка базы данных");
        //    Console.CursorVisible = false;

        //    await Task.Run(() => PrintLoading());

        //    Console.CursorVisible = true;           
        //}

        //public static void PrintLoading()
        //{
        //    while (FileReadAndWriteHandler.isReading)
        //    { 
        //        for (int i = 0; i <= 3; i++)
        //        {
        //            if (FileReadAndWriteHandler.isReading)
        //            {
        //                Console.SetCursorPosition(25, 2);
        //                Console.Write(new StringBuilder().Insert(0, ".", i)+"\t");
        //                Task.Delay(400).Wait();
        //            }               
        //        }
        //    }
        //}
        #endregion


    }
}
