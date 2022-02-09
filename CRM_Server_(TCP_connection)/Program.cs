using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        
            AddEmployeesFirstTime();            // Создаем сотрудников // TODO: загрузка сотрудников с файла, и запись в файл

            Employee TempEmployee;
            DateTime TempDate;
            double TempHours;
                    
            while (true)
            {
                Console.Clear();
                UserInterface.FirstGreeting();      // Выводим приветствие
           
                switch (UserInterface.GetUserDecision())
                {
                case 1:
                        Console.Clear();            // Необходимы: сотрудник, дата
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
                case 7:
                        Console.Clear();            // Необходимы: Сотрудник
                        Console.WriteLine("Вы выбрали уволить сотрудника");
                        TempEmployee = UserInterface.AskWhichEmployee();

                        if (TempEmployee != null)
                        {
                            TempEmployee.ToFireEmployee();
                        }                   
                        break;
                case 8:
                        Console.Clear();            // Необходимы: Имя, Фамилия
                        Console.WriteLine("Вы выбрали нанять сотрудника");                                                                  
                        string TempFistName = UserInterface.AskFirstNameForNewEmployee();
                        string TempLastName = UserInterface.AskLastNameForNewEmployee();

                        new Employee(TempFistName, TempLastName);
                        Console.WriteLine($"Вы успешно наняли сотрудника {TempFistName} {TempLastName}");
                        Console.ReadLine();
                        break;

                }
            }

            Console.ReadLine();

        }
      
        static void AddEmployeesFirstTime() 
        {
            new Employee("Иван", "Иванов");
            new Employee("Петр", "Петров");
            new Employee("Олег", "Олегов");
            new Employee("Степан", "Степанов");
            new Employee("Николай", "Николаев");
            new Employee("Анатолий", "Мельник");
            new Employee("Сергей", "Сергеев");
        }

    



    }
}
