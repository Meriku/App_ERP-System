using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Server__TCP_connection_
{
    internal class MenuNavigation
    {
        public static void AddClientAnswer(string message, ClientObject client)
        {

            if (client.ExpectedAnswer[0].Equals(typeof(int)))      // Если мы ожидаем от пользователя ввод целого числа
            {
                if (int.TryParse(message, out var result) && result >= 0 && result <= (int)client.ExpectedAnswer[1])
                {

                    if (client.ClientAnswers.Count == 0)
                    {
                        client.AddAnswer(result);
                        Console.WriteLine($"{client.userName} сделал выбор в Главном Меню: '{GetTitleOfMenuItem(client)}'.");
                    }
                    else
                    {
                        client.AddAnswer(result);
                        Console.WriteLine($"{client.userName} сделал в пункте: '{GetTitleOfMenuItem(client)}' выбор: ({message}).");
                    }
                                                                    
                    ChooseMenuItem(client);
                    
                }
                else
                {
                    Console.WriteLine($"{client.userName} сделал некорректный выбор в пункте: '{GetTitleOfMenuItem(client)}', он ввел ({message}) вместо целого числа.");
                    ServerObject.SendMessage($"Некорректный выбор. Введите целое число", client);
                }
            }
            else if (client.ExpectedAnswer[0].Equals(typeof(double)))      // Если мы ожидаем от пользователя ввод отработанных сотрудником часов
            {
                if (double.TryParse(message, out var result) && result >= 0 && result <= 12)
                {
                    Console.WriteLine($"{client.userName} сделал в пункте: '{GetTitleOfMenuItem(client)}' выбор: ({message}) часов.");
                    client.AddAnswer(result);
                    ChooseMenuItem(client);
                }
                else
                {
                    Console.WriteLine($"{client.userName} сделал некорректный выбор в пункте: '{GetTitleOfMenuItem(client)}', он ввел ({message}) вместо числа отработанных часов.");
                    ServerObject.SendMessage($"Некорректный выбор. Введите число отработанных часов", client);
                }
            }
            else if (client.ExpectedAnswer[0].Equals(typeof(Employee)))      // Если мы ожидаем от пользователя выбор сотрудника
            {
                if (int.TryParse(message, out var result) && result >= 0 && result <= Employee.GetNumberOfEmployees() - 1)
                {
                    Console.WriteLine($"{client.userName} сделал в пункте: '{GetTitleOfMenuItem(client)}' выбор: ({Employee.GetEmployeeName(Employee.GetEmployeeByIndex(result))}).");
                    client.AddAnswer(Employee.GetEmployeeByIndex(result));
                    ChooseMenuItem(client);
                }
                else
                {
                    Console.WriteLine($"{client.userName} сделал некорректный выбор в пункте: '{GetTitleOfMenuItem(client)}', он ввел ({message}) вместо порядкового номера сотрудника.");
                    ServerObject.SendMessage($"Некорректный выбор. Введите порядковый номер сотрудника.", client);
                }
            }

            else if (client.ExpectedAnswer[0].Equals(typeof(DateTime)))      // Если мы ожидаем от пользователя ввод даты
            {
                if (DateTime.TryParse(message, out var result) && result >= DateTime.Parse("01.01.2010") && result <= DateTime.Now)
                {
                    Console.WriteLine($"{client.userName} сделал в пункте: '{GetTitleOfMenuItem(client)}' выбор: ({result:d}).");
                    client.AddAnswer(result);
                    ChooseMenuItem(client);
                }
                else
                {
                    Console.WriteLine($"{client.userName} сделал некорректный выбор в пункте: '{GetTitleOfMenuItem(client)}', он ввел ({message}) вместо корректной даты.");
                    ServerObject.SendMessage($"Некорректный выбор. Введите дату с 01.01.2010 по {DateTime.Now:d}", client);
                }
            }
            else if (client.ExpectedAnswer[0].Equals(typeof(ConsoleKey)))      // Если мы ожидаем от пользователя ввод Y or N для выхода или продолжения работы
            {
                if (string.Equals(message.ToLower(), "y"))          // Продолжить работу с системой
                {
                    client.ClientAnswers.Clear();               // Очищаем список ответов
                    UserInterface.ClearClientConsole(client);
                    UserInterface.PrintMainMenu(client);        // Выводим главное меню
                    client.ExpectedAnswer[0] = typeof(int);     // Ожидаем от пользователя выбор пункта главного меню
                    client.ExpectedAnswer[1] = 10;
                    Console.WriteLine($"{client.userName} вернулся в основное меню.");
                }
                else if (string.Equals(message.ToLower(), "n"))     // Завершить работу с системой
                {

                    FileReadAndWriteHandler.ToSaveDataBase();   // Сохраняем данные внесенные пользователем
                    ServerObject.SendMessage("%", client);      // Требование отключится
                    client.Close();
                }

            }
            else if (client.ExpectedAnswer[0].Equals(typeof(string)))      // Если мы ожидаем от пользователя ввод имени/ фамилии сотрудника / или подтверждение действия
            {
                if ((int)client.ClientAnswers[0] == 8 && (string.Equals(message.ToLower(), "y") || string.Equals(message.ToLower(), "n")))
                {
                    client.AddAnswer(message.ToLower());
                    ChooseMenuItem(client);
                }

                if ((int)client.ClientAnswers[0] == 9 && !string.IsNullOrWhiteSpace(message) && !int.TryParse(message, out var result) && !DateTime.TryParse(message, out var date))
                {
                    if (client.ClientAnswers.Count == 2)    // Сохраняем введенную фамилию нового сотрудника
                    {
                        message.Trim();
                        client.AddAnswer(char.ToUpper(message[0]) + message.Substring(1));
                        ChooseMenuItem(client);
                    }
                    if (client.ClientAnswers.Count == 1)    // Сохраняем введенное имя нового сотрудника
                    {
                        message.Trim();                        
                        client.AddAnswer(char.ToUpper(message[0]) + message.Substring(1));
                        ChooseMenuItem(client);
                    }              
                }
            }
        }





        public static void ChooseMenuItem(ClientObject client)
        {

            switch ((int)client.ClientAnswers[0])
            {
                case 1:  // Необходимы: сотрудник, дата                                 //AddFullWorkDay

                    ServerObject.SendMessage("#Вы выбрали внести информацию про новый полный рабочий день (8 часов) для сотрудника", client);
                    switch (client.ClientAnswers.Count)
                    {
                        case 1:
                            UserInterface.PrintListOfEmployees(client);
                            UserInterface.AskWhichEmployee(client);                                                       
                            break;

                        case 2:
                            UserInterface.AskWhatDate(client);                           
                            break;

                        case 3:
                            var result = Employee.AddFullWorkDay(client); // TODO: сделать так красиво везде
                            ServerObject.SendMessage(result, client);

                            UserInterface.AskQuitOrContinue(client);
                            break;
                    }

                    break;

                case 2: // Необходимы: сотрудник, дата, количество часов                 //AddPartWorkDay
                    ServerObject.SendMessage("#Вы выбрали внести информацию про новый неполный рабочий день для сотрудника", client);
                    switch (client.ClientAnswers.Count)
                    {
                        case 1:
                            UserInterface.PrintListOfEmployees(client);
                            UserInterface.AskWhichEmployee(client);
                            break;

                        case 2:
                            UserInterface.AskWhatDate(client);      
                            break;

                        case 3:
                            UserInterface.AskWorkHoursCount(client);                            
                            break;

                        case 4:
                            var result = Employee.AddPartWorkDay(client);
                            ServerObject.SendMessage(result, client);

                            UserInterface.AskQuitOrContinue(client);                           
                            break;
                    }
                    break;

                case 3: // Необходимы: сотрудник и дата                                 //GetInfoWorkHoursPerDate
                    ServerObject.SendMessage("#Вы выбрали получить информацию про количество отработанных сотрудником часов за дату", client);
                    switch (client.ClientAnswers.Count)
                    {
                        case 1:
                            UserInterface.PrintListOfEmployees(client);
                            UserInterface.AskWhichEmployee(client);
                            break;

                        case 2:
                            UserInterface.AskWhatDate(client);
                            break; 
                            
                        case 3:                          
                            string result = $"\nСотрудник {Employee.GetEmployeeName(client.ClientAnswers[1])} отработал за {client.ClientAnswers[2]:d} {Employee.GetInfoWorkHoursAtDate(client)} часов.";
                            ServerObject.SendMessage(result, client);

                            UserInterface.AskQuitOrContinue(client);
                            break;
                    }
                    break;


                case 4: // Необходимы: сотрудник и период                               //GetInfoWorkHoursPerPeriod
                    ServerObject.SendMessage("#Вы выбрали получить информацию про количество отработанных сотрудником часов за период", client);
                    switch (client.ClientAnswers.Count) // TODO: не работает
                    {
                        case 1:
                            UserInterface.PrintListOfEmployees(client);
                            UserInterface.AskWhichEmployee(client);
                            break;

                        case 2:
                            UserInterface.AskWhatDateForPeriod(client, 1);
                            break;

                        case 3:
                            UserInterface.AskWhatDateForPeriod(client, 2);
                            break;

                        case 4:           
                            var result = Employee.GetInfoWorkHoursPerPeriod(client);
                            ServerObject.SendMessage(result, client);

                            UserInterface.AskQuitOrContinue(client);
                            break;
                    }
                    break;


                case 5: // Необходимы: сотрудник                                        //GetInfoWorkHoursAllTime
                    ServerObject.SendMessage("#Вы выбрали получить информацию про количество отработанных сотрудником часов за все время", client);
                    switch (client.ClientAnswers.Count)
                    {
                        case 1:
                            UserInterface.PrintListOfEmployees(client);
                            UserInterface.AskWhichEmployee(client);
                            break;
                        case 2:
                            var result = Employee.GetInfoWorkHoursAllTime(client);
                            ServerObject.SendMessage(result, client);

                            UserInterface.AskQuitOrContinue(client);
                            break;                
                    }
                    break;

                case 6: // Необходимо: ничего                                           //GetInfoWorkHoursAllTime
                    ServerObject.SendMessage("#Вы выбрали получить информацию про всех сотрудников компании и общее количество отработанных часов", client);
                         
                    ServerObject.SendMessage(Employee.GetListOfEmployeesAndWorkHours(), client);

                    UserInterface.AskQuitOrContinue(client);
                    break;

                case 7: // Необходимы: сначала сотрудник и дата, потом сколько часов    //ChangeWorkHoursAtDate
                    ServerObject.SendMessage("#Вы выбрали отредактировать информацию о количестве отработанных сотрудником часов в определенную дату", client);
                    switch (client.ClientAnswers.Count)
                    {
                        case 1:
                            UserInterface.PrintListOfEmployees(client);
                            UserInterface.AskWhichEmployee(client);
                            break;


                        case 2:
                            UserInterface.AskWhatDate(client);
                            break;


                        case 3:
                            UserInterface.PrintInfoWorkHoursAtDate(client);
                            UserInterface.AskWorkHoursToEditInfo(client);                     
                            break;

                        case 4:
                            var result = Employee.EditInfoAboutWorkDay(client);
                            ServerObject.SendMessage(result, client);

                            UserInterface.AskQuitOrContinue(client);
                            break;
                    }

                    break;

                case 8: // Необходимы: Сотрудник                                        //ToFireEmployee
                    ServerObject.SendMessage("#Вы выбрали уволить сотрудника", client);       
                    switch (client.ClientAnswers.Count)
                    {
                        case 1:
                            UserInterface.PrintListOfEmployees(client);
                            UserInterface.AskWhichEmployee(client);
                            break;

                        case 2:
                            UserInterface.IsUserSureToFireEmployee(client);
                            break;

                        case 3:
                            var result = Employee.ToFireEmployee(client);
                            ServerObject.SendMessage(result, client);

                            UserInterface.AskQuitOrContinue(client);
                            break;
                    }


                    break;

                case 9: // Необходимы: Имя, Фамилия                                     //ToHireEmployee
                    ServerObject.SendMessage("#Вы выбрали нанять сотрудника", client);
                    switch (client.ClientAnswers.Count)
                    {
                        case 1:
                            ServerObject.SendMessage("\nВведите имя нового сотрудника:", client);
                            client.ExpectedAnswer[0] = typeof(string);
                            break;

                        case 2:
                            ServerObject.SendMessage("\nВведите фамилию нового сотрудника:", client);
                            client.ExpectedAnswer[0] = typeof(string);
                            break;

                        case 3:
                            new Employee(client);
                            UserInterface.PrintInfoAboutNewEmployee(client);

                            UserInterface.AskQuitOrContinue(client);
                            break;
                    }
                    break;

                case 10: //"Сохранить информацию"                                       //ToSaveAndExit
                    ServerObject.SendMessage("#Сохранить информацию", client);
                    FileReadAndWriteHandler.ToSaveDataBase();
                    ServerObject.SendMessage("\nИнформация успешно сохранена на сервере", client);

                    UserInterface.AskQuitOrContinue(client);
                    break;
            }
        }



        public static string GetTitleOfMenuItem (ClientObject client)
        {
            switch ((int)client.ClientAnswers[0])
            {
                case 0:
                    return "Главное меню";
                case 1:
                    return "Внести информацию про новый полный рабочий день";
                case 2:
                    return "Внести информацию про новый неполный рабочий день";
                case 3:
                    return "Получить информацию про количество часов за дату";
                case 4:
                    return "Получить информацию про количество часов за период";
                case 5:
                    return "Получить информацию про количество часов за все время";
                case 6:
                    return "Получить информацию про количество часов у всех сотрудников";
                case 7:
                    return "Отредактировать информацию";
                case 8:
                    return "Уволить сотрудника";
                case 9:
                    return "Нанять сотрудника";
                case 10:
                    return "Сохранить информацию";
                default:
                    return "Ошибка. Такого пункта не существует ERR 101";
            }
        }
    }
}
