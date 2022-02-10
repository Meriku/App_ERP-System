using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRM_Server__TCP_connection_
{
    internal class Program
    {            
        static void Main(string[] args)
        {
            // Приложение должно иметь следующие функции:
            // Добавлять информацию про отработанные сотрудниками полные и неполные рабочие дни в определенные даты;
            // Добавлять/уменьшать количество отработанных сотрудником часов в определенную дату;
            // Считать и выводить по запросу отработанные часы за дату, период, все время.
            // Нанимать / увольнять сотрудников
            // Сохранение и загрузка информации из базы данных (набор информации в простом текстовом файле)

            Employee TempEmployee;
            DateTime TempDate;
            double TempHours;


            Console.WriteLine("Здравствуйте! Вас приветствует приложение ERM2000v1.0");
            
            UserInterface.PrintLoadingAsync();              // Интерфейс загрузки
            FileReadAndWriteHandler.ToLoadDataBase();       // Загрузка базы данных с файла
           

            while (true)
            {               
                Console.Clear();
                UserInterface.FirstGreeting();              // Выводим приветствие
           
                switch (UserInterface.GetUserDecision())    // Пользователь выбирает пункт меню   
                {
                case 1:
                        Console.Clear();                    // Необходимы: сотрудник, дата
                        Console.WriteLine("Вы выбрали внести информацию про новый полный рабочий день (8 часов) для сотрудника");   
                        TempEmployee = UserInterface.AskWhichEmployee();

                        if (TempEmployee != null)
                        {                           
                            TempDate = UserInterface.AskWhatDate();

                            TempEmployee.AddFullWorkDay(TempDate);                                                                                          
                            UserInterface.AskQuitOrContinue();
                        }
                                                                                      
                        break;
                case 2:
                        Console.Clear();            // Необходимы: сотрудник, количество часов, дата
                        Console.WriteLine("Вы выбрали внести информацию про новый неполный рабочий день для сотрудника");           
                        TempEmployee = UserInterface.AskWhichEmployee();

                        if (TempEmployee != null)
                        {
                            TempDate = UserInterface.AskWhatDate();
                            TempHours = UserInterface.AskWorkHoursCount();

                            TempEmployee.AddPartWorkDay(TempDate, TempHours);
                            UserInterface.AskQuitOrContinue();
                        }
                        break;
                case 3:
                        Console.Clear();            // Необходимы: сотрудник и дата
                        Console.WriteLine("Вы выбрали получить информацию про количество отработанных сотрудником часов за дату");  
                        TempEmployee = UserInterface.AskWhichEmployee();

                        if (TempEmployee != null)
                        {
                            TempDate = UserInterface.AskWhatDate();

                            TempEmployee.GetInfoWorkHoursAtDate(TempDate);
                            UserInterface.AskQuitOrContinue();
                        }
                        break;
                case 4:
                        Console.Clear();            // Необходимы: сотрудник и период
                        Console.WriteLine("Вы выбрали получить информацию про количество отработанных сотрудником часов за период"); 
                        TempEmployee = UserInterface.AskWhichEmployee();
                        if (TempEmployee != null)
                        {
                            DateTime FirstTempDate = UserInterface.AskWhatDateForPeriod(1);
                            DateTime SecondTempDate = UserInterface.AskWhatDateForPeriod(2);

                            TempEmployee.GetInfoWorkHoursPerPeriod(FirstTempDate, SecondTempDate);
                            UserInterface.AskQuitOrContinue();
                        }
                        break;
                case 5:
                        Console.Clear();            // Необходимы: сотрудник
                        Console.WriteLine("Вы выбрали получить информацию про количество отработанных сотрудником часов за все время"); 
                        TempEmployee = UserInterface.AskWhichEmployee();
                        if (TempEmployee != null)
                        {
                            TempEmployee.GetInfoWorkHoursAllTime();
                            UserInterface.AskQuitOrContinue();
                        }
                        break;
                case 6:
                        Console.Clear();            // Необходимо: ничего
                        Console.WriteLine("Вы выбрали получить информацию про всех сотрудников компании и общее количество отработанных часов");
                        Employee.PrintListOfEmployeesAndTheirWorkHours();
                        UserInterface.AskQuitOrContinue();
                        break;
                case 7:
                        Console.Clear();            // Необходимы: сначала сотрудник и дата, потом сколько часов
                        Console.WriteLine("Вы выбрали отредактировать информацию о количестве отработанных сотрудником часов в определенную дату"); 
                        TempEmployee = UserInterface.AskWhichEmployee();
                        if (TempEmployee != null)   
                        {
                            TempDate = UserInterface.AskWhatDate();
                            TempEmployee.GetInfoWorkHoursAtDate(TempDate);

                            TempHours = UserInterface.AskWorkHoursToEditInfo();
                            TempEmployee.AddPartWorkDay(TempDate, TempHours);
                            UserInterface.AskQuitOrContinue();
                        }
                        break;
                case 8:
                        Console.Clear();            // Необходимы: Сотрудник
                        Console.WriteLine("Вы выбрали уволить сотрудника");
                        TempEmployee = UserInterface.AskWhichEmployee();

                        if (TempEmployee != null)
                        {
                            TempEmployee.ToFireEmployee();
                        }
                        UserInterface.AskQuitOrContinue();
                        break;
                case 9:
                        Console.Clear();            // Необходимы: Имя, Фамилия
                        Console.WriteLine("Вы выбрали нанять сотрудника");                                                                  
                        string TempFirstName = UserInterface.AskFirstNameForNewEmployee();
                        string TempLastName = UserInterface.AskLastNameForNewEmployee();

                        new Employee(TempFirstName, TempLastName);
                        Console.WriteLine($"Вы успешно наняли сотрудника {TempFirstName} {TempLastName}");      
                        UserInterface.AskQuitOrContinue();
                        break;
                case 10:
                        Console.Clear();            // Необходимо: ничего
                        // Сохранить информацию
                        FileReadAndWriteHandler.ToSaveDataBase();
                        Environment.Exit(0);
                        break;
                }
            }

            Console.ReadLine();

        }        
    }
}
