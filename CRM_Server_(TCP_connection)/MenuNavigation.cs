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

            // TODO: Придумать как лаконично провести все эти проверки.

            if (string.Equals(message.ToLower(), "y"))
            {

                client.ClientAnswers.Clear();
                UserInterface.ClearClientConsole(client);
                UserInterface.PrintMainMenu(client);              
 
            }
            if (string.Equals(message.ToLower(), "n"))
            {

                FileReadAndWriteHandler.ToSaveDataBase();   // Сохраняем данные внесенные пользователем
                ServerObject.SendMessage("%", client);      // Требование отключится
                client.Close();

            }

            if (client.ClientAnswers.Count == 2 && (client.ClientAnswers[0].Equals("1") || client.ClientAnswers[0].Equals("2") || client.ClientAnswers[0].Equals("3") ))            // Первый выбор пользователя всегда цифра от 1 до 10
            {                               // Третий выбор пользователя - дата, если первый выбор 1, 2, 3
                if (DateTime.TryParse(message, out var result) && result >= DateTime.Parse("01.01.2010") && result <= DateTime.Now)
                {
                    client.AddClientAnswer(result.ToString("d"));
                    Console.WriteLine($"{client.userName} выбрал дату: {result.ToString("d")}");
                    ChooseMenuItem(client);
                }
                else
                {
                    Console.WriteLine($"{client.userName} сделал некорректный выбор при выборе даты: ({message})");
                    ServerObject.SendMessage($"Некорректный выбор. Введите дату с 01.01.2010 по {DateTime.Now.ToString("d")}", client);
                }
            }

            if (client.ClientAnswers.Count == 1 && (client.ClientAnswers[0].Equals("1") || client.ClientAnswers[0].Equals("2") || client.ClientAnswers[0].Equals("3") || client.ClientAnswers[0].Equals("4") || client.ClientAnswers[0].Equals("5") || client.ClientAnswers[0].Equals("7") || client.ClientAnswers[0].Equals("8")))            // Первый выбор пользователя всегда цифра от 1 до 10
            {                               // Второй выбор пользователя - порядковый номер сотрудника, если первый выбор 1, 2, 3, 4, 5, 7, 8  
                if (int.TryParse(message, out var result) && result >= 0 && result < Employee.GetNumberOfEmployees())
                {
                    client.AddClientAnswer(result.ToString());
                    Console.WriteLine($"{client.userName} выбрал сотрудника: {result} {Employee.GetEmployeeNameByIndex(result)}");
                    ChooseMenuItem(client);
                }
                else      // TODO: если список сотрудников пуст
                {
                    Console.WriteLine($"{client.userName} сделал некорректный выбор при выборе сотрудника: ({message})");
                    ServerObject.SendMessage($"Некорректный выбор. Введите цифру от 0 до {Employee.GetNumberOfEmployees()-1}", client);
                }
            }


            if (client.ClientAnswers.Count == 0)
            {                                   // Первый выбор пользователя всегда цифра от 1 до 10
                if (int.TryParse(message, out var result) && result >= 1 && result <= 10)
                {
                    client.AddClientAnswer(result.ToString());
                    Console.WriteLine($"{client.userName} выбрал: {result} в основном меню");
                    ChooseMenuItem(client);
                }
                else
                {
                    Console.WriteLine($"{client.userName} сделал некорректный выбор в основном меню: ({message})");
                    ServerObject.SendMessage("Некорректный выбор. Введите цифру от 1 до 10", client);
                }
            }





        }



        public static void ChooseMenuItem(ClientObject client)
        {

            switch (int.Parse(client.ClientAnswers[0]))
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
                            var result = Employee.AddFullWorkDay(Employee.GetEmployeeByIndex(int.Parse(client.ClientAnswers[1])), DateTime.Parse(client.ClientAnswers[2]));
                            ServerObject.SendMessage(result, client);

                            UserInterface.AskQuitOrContinue(client);
                            break;
                    }

                    break;

                case 2: // Необходимы: сотрудник, дата, количество часов                 //AddPartWorkDay
                    ServerObject.SendMessage("#Вы выбрали внести информацию про новый неполный рабочий день для сотрудника", client);
                    break;

                case 3: // Необходимы: сотрудник и дата                                 //GetInfoWorkHoursPerDate
                    ServerObject.SendMessage("#Вы выбрали получить информацию про количество отработанных сотрудником часов за дату", client);
                    break;


                case 4: // Необходимы: сотрудник и период                               //GetInfoWorkHoursPerPeriod
                    ServerObject.SendMessage("#Вы выбрали получить информацию про количество отработанных сотрудником часов за период", client);
                    break;


                case 5: // Необходимы: сотрудник                                        //GetInfoWorkHoursAllTime
                    ServerObject.SendMessage("#Вы выбрали получить информацию про количество отработанных сотрудником часов за все время", client);
                    break;

                case 6: // Необходимо: ничего                                           //GetInfoWorkHoursAllTime
                    ServerObject.SendMessage("#Вы выбрали получить информацию про всех сотрудников компании и общее количество отработанных часов", client);
                    break;

                case 7: // Необходимы: сначала сотрудник и дата, потом сколько часов    //ChangeWorkHoursAtDate
                    ServerObject.SendMessage("#Вы выбрали отредактировать информацию о количестве отработанных сотрудником часов в определенную дату", client);
                    break;

                case 8: // Необходимы: Сотрудник                                        //ToFireEmployee
                    ServerObject.SendMessage("#Вы выбрали уволить сотрудника", client);
                    break;

                case 9: // Необходимы: Имя, Фамилия                                     //ToHireEmployee
                    ServerObject.SendMessage("#Вы выбрали нанять сотрудника", client);
                    break;

                case 10: //"Сохранить информацию"                                       //ToSaveAndExit
                    ServerObject.SendMessage("#Сохранить информацию", client);
                    break;



            }


        }
    }

}
