using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CRM_Server__TCP_connection_
{
    internal class Program
    {
        static ServerObject server; // Сервер
        static Thread listenThread; // Поток для прослушивания

        static void Main(string[] args)
        {

            // Приложение должно иметь следующие функции:
            // Запуск серверного приложения с возможностью работы с ним с нескольких клиентских приложений одновременно 
            // Запись и сохранение логов (истории изменений) с привязкой к пользователю 
            // ? Доступ к серверу с помощью пары (логин, пароль) 
            // Добавлять информацию про отработанные сотрудниками полные и неполные рабочие дни в определенные даты;
            // Добавлять/уменьшать количество отработанных сотрудником часов в определенную дату;
            // Считать и выводить по запросу отработанные часы за дату, период, все время.
            // Нанимать / увольнять сотрудников
            // Сохранение и загрузка информации из базы данных (набор информации в простом текстовом файле)


            FileReadAndWriteHandler.ToLoadDataBase();       // Загрузка базы данных с файла

            try
            {
                server = new ServerObject();                // Старт сервера
                listenThread = new Thread(new ThreadStart(server.Listen));
                listenThread.Start();                       // Старт потока
            }
            catch (Exception ex)
            {
                server.Disconnect();
                Console.WriteLine(ex.Message);
            }                   

            #region old
            //Employee TempEmployee;
            //DateTime TempDate;
            //double TempHours;

            //UserInterface.PrintLoadingAsync();              // Интерфейс загрузки           
            //while (true)
            //{               
            //    Console.Clear();
            //    UserInterface.FirstGreeting();              // Выводим приветствие

            //    switch (UserInterface.GetUserDecision())    // Пользователь выбирает пункт меню   
            //    {
            //    case 1:
            //            Console.Clear();                    // Необходимы: сотрудник, дата
            //            Console.WriteLine("Вы выбрали внести информацию про новый полный рабочий день (8 часов) для сотрудника");   
            //            TempEmployee = UserInterface.AskWhichEmployee();

            //            if (TempEmployee != null)
            //            {                           
            //                TempDate = UserInterface.AskWhatDate();

            //                TempEmployee.AddFullWorkDay(TempDate);                                                                                          
            //                UserInterface.AskQuitOrContinue();
            //            }

            //            break;
            //    case 2:
            //            Console.Clear();            // Необходимы: сотрудник, количество часов, дата
            //            Console.WriteLine("Вы выбрали внести информацию про новый неполный рабочий день для сотрудника");           
            //            TempEmployee = UserInterface.AskWhichEmployee();

            //            if (TempEmployee != null)
            //            {
            //                TempDate = UserInterface.AskWhatDate();
            //                TempHours = UserInterface.AskWorkHoursCount();

            //                TempEmployee.AddPartWorkDay(TempDate, TempHours);
            //                UserInterface.AskQuitOrContinue();
            //            }
            //            break;
            //    case 3:
            //            Console.Clear();            // Необходимы: сотрудник и дата
            //            Console.WriteLine("Вы выбрали получить информацию про количество отработанных сотрудником часов за дату");  
            //            TempEmployee = UserInterface.AskWhichEmployee();

            //            if (TempEmployee != null)
            //            {
            //                TempDate = UserInterface.AskWhatDate();
            //                TempHours = TempEmployee.GetInfoWorkHoursAtDate(TempDate);

            //                UserInterface.PrintInfoWorkHoursAtDate(TempDate, TempHours);
            //                UserInterface.AskQuitOrContinue();
            //            }
            //            break;
            //    case 4:
            //            Console.Clear();            // Необходимы: сотрудник и период
            //            Console.WriteLine("Вы выбрали получить информацию про количество отработанных сотрудником часов за период"); 
            //            TempEmployee = UserInterface.AskWhichEmployee();
            //            if (TempEmployee != null)
            //            {
            //                DateTime FirstTempDate = UserInterface.AskWhatDateForPeriod(1);
            //                DateTime SecondTempDate = UserInterface.AskWhatDateForPeriod(2);

            //                TempEmployee.GetInfoWorkHoursPerPeriod(FirstTempDate, SecondTempDate);
            //                UserInterface.AskQuitOrContinue();
            //            }
            //            break;
            //    case 5:
            //            Console.Clear();            // Необходимы: сотрудник
            //            Console.WriteLine("Вы выбрали получить информацию про количество отработанных сотрудником часов за все время"); 
            //            TempEmployee = UserInterface.AskWhichEmployee();
            //            if (TempEmployee != null)
            //            {
            //                TempEmployee.GetInfoWorkHoursAllTime();
            //                UserInterface.AskQuitOrContinue();
            //            }
            //            break;
            //    case 6:
            //            Console.Clear();            // Необходимо: ничего
            //            Console.WriteLine("Вы выбрали получить информацию про всех сотрудников компании и общее количество отработанных часов");
            //            Employee.PrintListOfEmployeesAndTheirWorkHours();
            //            UserInterface.AskQuitOrContinue();
            //            break;
            //    case 7:
            //            Console.Clear();            // Необходимы: сначала сотрудник и дата, потом сколько часов
            //            Console.WriteLine("Вы выбрали отредактировать информацию о количестве отработанных сотрудником часов в определенную дату"); 
            //            TempEmployee = UserInterface.AskWhichEmployee();
            //            if (TempEmployee != null)   
            //            {
            //                TempDate = UserInterface.AskWhatDate();                          
            //                UserInterface.PrintInfoWorkHoursAtDate(TempDate, TempEmployee.GetInfoWorkHoursAtDate(TempDate));

            //                TempHours = UserInterface.AskWorkHoursToEditInfo();

            //                TempEmployee.EditInfoAboutWorkDay(TempDate, TempHours);
            //                UserInterface.AskQuitOrContinue();
            //            }
            //            break;
            //    case 8:
            //            Console.Clear();            // Необходимы: Сотрудник
            //            Console.WriteLine("Вы выбрали уволить сотрудника");
            //            TempEmployee = UserInterface.AskWhichEmployee();

            //            if (TempEmployee != null)
            //            {
            //                TempEmployee.ToFireEmployee();
            //            }
            //            UserInterface.AskQuitOrContinue();
            //            break;
            //    case 9:
            //            Console.Clear();            // Необходимы: Имя, Фамилия
            //            Console.WriteLine("Вы выбрали нанять сотрудника");                                                                  
            //            string TempFirstName = UserInterface.AskFirstNameForNewEmployee();
            //            string TempLastName = UserInterface.AskLastNameForNewEmployee();

            //            new Employee(TempFirstName, TempLastName);
            //            Console.WriteLine($"Вы успешно наняли сотрудника {TempFirstName} {TempLastName}");      
            //            UserInterface.AskQuitOrContinue();
            //            break;
            //    case 10:
            //            // Сохранить информацию
            //            FileReadAndWriteHandler.ToSaveDataBase();
            //            Environment.Exit(0);
            //            break;
            //    }
            //}
            #endregion

        }
    }
}
