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

            if (client.messageType == typeof(int))      // Если мы ожидаем от пользователя ввод целого числа
            {
                if (message.IsInt(out var result) && result > 0 && result < 10)
                {
                    new Answer(result, client);

                    FileHandler.AddLogClientChoice(client, successful: true);

                    ChooseMenuItem(client);
                }
                else
                {
                    FileHandler.AddLogClientChoice(client, successful: false);
                }
            }
           
            else if (client.messageType == typeof(double))
            {
                if (message.IsDouble(out var result))
                {
                    new Answer(result, client);

                    FileHandler.AddLogClientChoice(client, successful: true);

                    ChooseMenuItem(client);
                }
                else
                {
                    FileHandler.AddLogClientChoice(client, successful: false);
                }
            }

            else if (client.messageType == typeof(Employee))
            {
                if (message.IsEmployee(out var result))
                {
                    new Answer(result, client);

                    FileHandler.AddLogClientChoice(client, successful: true);

                    ChooseMenuItem(client);
                }
                else
                {
                    FileHandler.AddLogClientChoice(client, successful: false);
                }
            }

            else if (client.messageType == typeof(DateTime))
            {
                if (message.IsDateTime(out var result))
                {
                    new Answer(result, client);

                    FileHandler.AddLogClientChoice(client, successful: true);

                    ChooseMenuItem(client);
                }
                else
                {
                    FileHandler.AddLogClientChoice(client, successful: false);
                }
            }

            else if (client.messageType == typeof(ConsoleKey))
            {
                if (message.IsKey(out var result))
                {                 
                    if (client.Answers[0].IntAnswer == 8 && client.Answers.Count == 2)
                    {   // Только при увольнении сотрудника
                        new Answer(result, client);
                        ChooseMenuItem(client);
                    }
                    else if (result.Equals(ConsoleKey.Y))
                    {
                        client.Answers.Clear();
                        client.messageType = typeof(int);
                        UserInterface.PrintMainMenu(client);
                        FileHandler.AddLogSwitchToMainMenu(client);
                    }
                    else if (result.Equals(ConsoleKey.N))
                    {
                        client.Close();
                    }

                }
                else
                {
                    FileHandler.AddLogClientChoice(client, successful: false);
                }
            }
            
            else if (client.messageType == typeof(string) && !string.IsNullOrWhiteSpace(message))
            {
                message.Trim();
                switch (client.Answers.Count)
                {
                    case 2:
                        var lastname = char.ToUpper(message[0]) + message.Substring(1);
                        new Answer(lastname, client);
                        ChooseMenuItem(client);
                        break;

                    case 1:
                        var firstname = char.ToUpper(message[0]) + message.Substring(1);
                        new Answer(firstname, client);
                        ChooseMenuItem(client);
                        break;
                }
            }
        }

        public static void ChooseMenuItem(ClientObject client)
        {

            switch (client.Answers[0].IntAnswer)
            {
                case 1: 

                    ServerObject.SendMessage("#Внести информацию про новый полный рабочий день (8 часов) для сотрудника", client);
                    switch (client.Answers.Count) 
                    {
                        case 1:
                            UserInterface.SendListOfEmployees(client);
                            UserInterface.AskWhichEmployee(client);                                                       
                            break;

                        case 2:
                            UserInterface.AskWhatDate(client);                           
                            break;

                        case 3:
                            var result = Employee.AddFullWorkDay(client);
                            ServerObject.SendMessage(result, client);

                            UserInterface.AskQuitOrContinue(client);
                            break;
                    }

                    break;

                case 2: // Необходимы: сотрудник, дата, количество часов                 //AddPartWorkDay
                    ServerObject.SendMessage("#Внести информацию про новый неполный рабочий день для сотрудника", client);
                    switch (client.Answers.Count)
                    {
                        case 1:
                            UserInterface.SendListOfEmployees(client);
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
                    ServerObject.SendMessage("#Получить информацию про количество отработанных сотрудником часов за дату", client);
                    switch (client.Answers.Count)
                    {
                        case 1:
                            UserInterface.SendListOfEmployees(client);
                            UserInterface.AskWhichEmployee(client);
                            break;

                        case 2:
                            UserInterface.AskWhatDate(client);
                            break; 
                            
                        case 3:                          
                            string result = Employee.GetInfoWorkHoursAtDateAsString(client);                           
                            ServerObject.SendMessage(result, client);

                            UserInterface.AskQuitOrContinue(client);
                            break;
                    }
                    break;


                case 4: // Необходимы: сотрудник и период                               //GetInfoWorkHoursPerPeriod
                    ServerObject.SendMessage("#Получить информацию про количество отработанных сотрудником часов за период", client);
                    switch (client.Answers.Count)
                    {
                        case 1:
                            UserInterface.SendListOfEmployees(client);
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
                    ServerObject.SendMessage("#Получить информацию про количество отработанных сотрудником часов за все время", client);
                    switch (client.Answers.Count)
                    {
                        case 1:
                            UserInterface.SendListOfEmployees(client);
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
                    ServerObject.SendMessage("#Получить информацию про всех сотрудников компании и общее количество отработанных часов", client);
                         
                    ServerObject.SendMessage(Employee.GetListOfEmployeesAndWorkHours(client), client);

                    UserInterface.AskQuitOrContinue(client);
                    break;

                case 7: // Необходимы: сначала сотрудник и дата, потом сколько часов    //ChangeWorkHoursAtDate
                    ServerObject.SendMessage("#Отредактировать информацию о количестве отработанных сотрудником часов в определенную дату", client);
                    switch (client.Answers.Count)
                    {
                        case 1:
                            UserInterface.SendListOfEmployees(client);
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
                    ServerObject.SendMessage("#Уволить сотрудника", client);       
                    switch (client.Answers.Count)
                    {
                        case 1:
                            UserInterface.SendListOfEmployees(client);
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
                    ServerObject.SendMessage("#Нанять сотрудника", client);
                    switch (client.Answers.Count)
                    {
                        case 1:
                            ServerObject.SendMessage("\nВведите имя нового сотрудника:", client);
                            client.messageType = typeof(string);
                            break;

                        case 2:
                            ServerObject.SendMessage("\nВведите фамилию нового сотрудника:", client);
                            client.messageType = typeof(string);
                            break;

                        case 3:
                            Employee.ToHireEmployee(client);
                            UserInterface.PrintInfoAboutNewEmployee(client);

                            UserInterface.AskQuitOrContinue(client);
                            break;
                    }
                    break;
            }
        }

     


        public static string GetTitleOfMenuItem (ClientObject client)
        {
            if (client.Answers.Count == 1)
            {
                return "Главное меню";
            }

            switch (client.Answers[0].IntAnswer)
            {
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
                default:
                    return "Ошибка. Такого пункта не существует ERR 101";
            }
        }
    }
}
