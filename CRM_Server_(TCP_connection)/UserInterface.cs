using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Server__TCP_connection_
{
    public static class UserInterface
    {
        public static void ClearClientConsole(ClientObject client)       // Очищаем клиентскую консоль
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
        public static void PrintInfoAboutNewEmployee(ClientObject client)       // Информируем про успешный найм нового сотрудника
        {
            Console.WriteLine($"Пользователь {client.userName} добавил нового сотрудника: {(string)client.ClientAnswers[1]} {(string)client.ClientAnswers[2]}");
            ServerObject.SendMessage($"Вы успешно добавили нового сотрудника: {(string)client.ClientAnswers[1]} {(string)client.ClientAnswers[2]}", client);
        }

        public static void AskWhichEmployee(ClientObject client)       // Выбор интересующего сотрудника для дальнейших действий
        {

            ServerObject.SendMessage("\n\nВведите порядковый номер необходимого сотрудника:", client);
            client.ExpectedAnswer[0] = typeof(Employee);

        }

        public static void AskWhatDate(ClientObject client)            // Выбор интересующей даты для дальнейших действий
        {
            
            ServerObject.SendMessage("\nВведите дату рабочего дня выбранного сотрудника в формате: 00.00.0000", client);
            client.ExpectedAnswer[0] = typeof(DateTime);

        }

        public static void AskQuitOrContinue(ClientObject client)          // Продолжить работу или выйти с приложения
        {
            ServerObject.SendMessage("\n\n\tПродолжить работу с системой? Y or N ", client);
            client.ExpectedAnswer[0] = typeof(ConsoleKey);
        }

        public static void AskWorkHoursCount(ClientObject client)        // Запрос к пользователю на количество часов 
        {
            ServerObject.SendMessage("\nВведите число часов отработанных сотрудником в этот день", client);
            client.ExpectedAnswer[0] = typeof(double);

        }

        public static void AskWhatDateForPeriod(ClientObject client, int firstorseconddate)  // Запрос на Первую (1) и Вторую (2) дату для периода
        {
            client.ExpectedAnswer[0] = typeof(DateTime);

            if (firstorseconddate == 1)
            {
                ServerObject.SendMessage("Введите первую дату для необходимого периода в формате: 00.00.0000", client);
            }
            if (firstorseconddate == 2)
            {
                ServerObject.SendMessage("\nВведите вторую дату для необходимого периода в формате: 00.00.0000", client);
            }
        }

        public static void AskWorkHoursToEditInfo(ClientObject client)   // Запрос к пользователю на количество часов  (для редактирования информации)
        {
            ServerObject.SendMessage("\nСколько часов сотрудник отработал в этот день на самом деле:", client);
            client.ExpectedAnswer[0] = typeof(double);
        }


        public static void PrintInfoWorkHoursAtDate(ClientObject client)
        {
            var hours = Employee.GetInfoWorkHoursAtDate(client);

            var workdate = (DateTime)client.ClientAnswers[2];

            if (hours == 0)
            {
                ServerObject.SendMessage($"\nДанный сотрудник не был на работе {workdate:d}", client);
            }
            else
            {
                ServerObject.SendMessage($"\nДанный сотрудник отработал {hours} часов {workdate:d}", client);
            }
        }


        public static void IsUserSureToFireEmployee(ClientObject client)
        {
            ServerObject.SendMessage($"Вы действительно хотите уволить сотрудника {Employee.GetEmployeeName(client.ClientAnswers[1])} ? Y or N ", client);
            client.ExpectedAnswer[0] = typeof(string);
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
        //                Console.Write(new StringBuilder().Insert(0, ".", i) + "\t");
        //                Task.Delay(400).Wait();
        //            }
        //        }
        //    }
        //}
        #endregion


    }
}
