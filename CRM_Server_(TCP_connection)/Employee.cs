using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Server__TCP_connection_
{
    public class Employee
    {

        private string FirstName;
        private string LastName;
        private List<WorkDay> WorkDays = new List<WorkDay>();           // Количество проработанных дней и количество проработанных часов в эти дни

        public static List<Employee> Employees = new List<Employee>();  // Список сотрудников

        public Employee (string firstname, string lastname)
        {
            FirstName = firstname; 
            LastName = lastname;
            Employees.Add(this);
        }


        public static bool IfEvenOneEmployeeInListExist() // Проверка не пустой ли список сотрудников
        {
            if (Employees.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }  
        }

        public static void PrintListOfEmployees()       // Выводим список сотрудников
        {
            Console.WriteLine("\nСписок сотрудников:");
            int i = 0;
            while (i < Employees.Count)
            {
                Console.WriteLine($"{i}. {Employees[i].FirstName} {Employees[i].LastName}");
                i++;
            }
        }

        public static void PrintListOfEmployeesAndTheirWorkHours()       // Выводим список сотрудников и отработанных часов
        {
            Console.WriteLine("\nСписок сотрудников и рабочих часов:\n");
            int i = 0;
            while (i < Employees.Count)
            {
                double sum = 0;
                var j = 0;

                while (j < Employees[i].WorkDays.Count)
                {
                    sum += Employees[i].WorkDays[j].WorkHoursAtDay();   // Получаем количество рабочих часов этого сотрудника в этот день и суммируем
                    j++;
                }

                Console.WriteLine($"{i}. {Employees[i].FirstName} {Employees[i].LastName} всего отработал: \t{sum} часов");
                i++;
            }
        }

        public static string GetEmployeeFNameLNameByIndex(int index)    // Имя Фамилия сотрудника по порядковому номеру в списке сотрудников 
        {
            return $"{Employees[index].FirstName} {Employees[index].LastName}";
        }

        public static int GetNumberOfEmployees()                // Возвращает количество сотрудников
        {
            return Employees.Count;
        }

        public static Employee GetEmployeeByIndex(int index)    // Возвращает сотрудника по порядковому номеру в списке сотрудников 
        {
            return Employees[index];
        }
  
        public void AddFullWorkDay(DateTime workdate)                   // TODO: проверка существует ли уже этот рабочий день у сотрудника
        {
            WorkDays.Add(new WorkDay(workdate, 8.0));
            Console.WriteLine($"Теперь количество часов отработанных сотрудником {FirstName} {LastName} за {workdate.ToString("d")} составляет 8 рабочих часов ");
        }

        public void AddPartWorkDay(DateTime workdate, double hours)     // TODO: проверка существует ли уже этот рабочий день у сотрудника
        {
            WorkDays.Add(new WorkDay(workdate, hours));
            Console.WriteLine($"Теперь количество часов отработанных сотрудником {FirstName} {LastName} за {workdate.ToString("d")} составляет {hours} рабочих часов ");
        }
        public void AddCustomWorkDayForSaveLoad(DateTime workdate, double hours)     // Для загрузки программы с файла
        {
            WorkDays.Add(new WorkDay(workdate, hours));
        }


        public double GetInfoWorkHoursAtDate(DateTime dateofwork) // Возвращает количество рабочих часов сотрудника в определенную дату
        {
            double hours = 0;
            var i = 0;

            while (hours == 0)
            {                                                   // TODO: Выход из цикла, для выбора другого сотрудника 
          
                while (i < WorkDays.Count)                      // Находим в списке рабочих дней необходимый
                {
                    if (WorkDays[i].WorkDate.Equals(dateofwork))    
                    {
                        hours = WorkDays[i].WorkHoursAtDay();
                        Console.WriteLine($"Сотрудник {FirstName} {LastName} отработал {dateofwork.ToString("d")} {hours} часов");
                        break;
                    }               
                    i++;
                }

                if (hours == 0)
                {
                    Console.WriteLine($"Данный сотрудник не был на работе {dateofwork.ToString("d")}");
                    dateofwork = UserInterface.AskWhatDate();
                }
            }
            return hours;
        }

        
        public double GetInfoWorkHoursPerPeriod(DateTime firstdate, DateTime seconddate)    // Возвращает количество рабочих часов сотрудника за период
        {
            double sum = 0;
            var i = 0;

            while (i < WorkDays.Count)
            {
                if (WorkDays[i].WorkDate >= firstdate && WorkDays[i].WorkDate <= seconddate)
                {
                    sum += WorkDays[i].WorkHoursAtDay();   // Получаем количество рабочих часов этого сотрудника в этот день и суммируем
                }               
                i++;
            }

            Console.WriteLine($"C {firstdate.ToString("d")} по {seconddate.ToString("d")} сотрудник {FirstName} {LastName} отработал {sum} часов");
            return sum;
        }
        public double GetInfoWorkHoursAllTime()            // Возвращает количество рабочих часов сотрудника за все время
        {            
            double sum = 0;
            var i = 0;

            while (i < WorkDays.Count)
            {
                sum += WorkDays[i].WorkHoursAtDay();   // Получаем количество рабочих часов этого сотрудника в этот день и суммируем
                i++;
            }

            Console.WriteLine($"Всего сотрудник {FirstName} {LastName} отработал {sum} часов");
            return sum;           
        }

        public void ToFireEmployee()                // Уволить сотрудника (удалить из списка)
        {
            Console.Clear();
            Console.WriteLine($"Вы действительно хотите уволить сотрудника {FirstName} {LastName}? Y or N ");
            while (true)
            {
                var answer = Console.ReadLine().ToLower();
                if (string.Equals(answer, "y"))
                {
                    Console.WriteLine($"Сотрудник {FirstName} {LastName} уволен");
                    Employees.Remove(this);
                    break;
                }
                else if (string.Equals(answer, "n"))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Введите Y для продолжение работы или N для выхода из приложения");
                }
            }
        }


        public string GetEmployeeDatesAndHours ()       // Возвращает строку для записи в файл
        {
            string result = $"#{FirstName} {LastName}\n";
            foreach (WorkDay workday in WorkDays)
            {
                result += $"{workday.WorkDate.ToString("d")}#{workday.WorkHoursAtDay()}\n";
            }          
            return result;
        }
    }
}
