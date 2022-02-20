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

        public Employee(string firstname, string lastname)
        {
            FirstName = firstname;
            LastName = lastname;
            Employees.Add(this);
        }
        public Employee(ClientObject client)
        {
            FirstName = (string)client.ClientAnswers[1];
            LastName = (string)client.ClientAnswers[2];           
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

        public static string GetListOfEmployees()       // Возвращаем список сотрудников
        {
            int i = 0;
            string result = "";

            while (i < Employees.Count)
            {
                result += $"\n{i}. {Employees[i].FirstName} {Employees[i].LastName}";
                i++;
            }

            return result;
        }


        public static string GetEmployeeName(object empObj)    // Имя Фамилия сотрудника 
        {
            var emp = (Employee)empObj;
            return $"{emp.FirstName} {emp.LastName}";
        }

        public static int GetNumberOfEmployees()                // Возвращает количество сотрудников
        {
            return Employees.Count;
        }

        public static Employee GetEmployeeByIndex(int index)    // Возвращает сотрудника по порядковому номеру в списке сотрудников 
        {
            return Employees[index];
        }

        public static string AddFullWorkDay(ClientObject client)  // Добавляем новый полный рабочий день                   
        {
            var emp = (Employee)client.ClientAnswers[1];
            var workdate = (DateTime)client.ClientAnswers[2];

            string result = "";

            if (GetInfoWorkHoursAtDate(client) == 0)
            {
                emp.WorkDays.Add(new WorkDay(workdate, 8.0));
                result += $"\nТеперь количество часов отработанных сотрудником {emp.FirstName} {emp.LastName} за {workdate.ToString("d")} составляет 8 рабочих часов ";
            }
            else                                                // Если рабочий день уже существует 
            {
                result += $"\nУказано что {emp.FirstName} {emp.LastName} за {workdate.ToString("d")} отработал {GetInfoWorkHoursAtDate(client)} рабочих часов\nДля внесение изменений в существующие данные воспользуйтесь пунктом 7 'Отредактировать информацию'";
            }

            return result;
        }

        public static string AddPartWorkDay(ClientObject client)    
        {
            var emp = (Employee)client.ClientAnswers[1]; 
            var workdate = (DateTime)client.ClientAnswers[2];
            var hours = (double)client.ClientAnswers[3];


            string result = "";

            if (GetInfoWorkHoursAtDate(client) == 0)          // Добавляем новый неполный рабочий день
            {
                emp.WorkDays.Add(new WorkDay(workdate, hours));
                result += $"\nТеперь количество часов отработанных сотрудником {emp.FirstName} {emp.LastName} за {workdate.ToString("d")} составляет {hours} рабочих часов ";               
            }
            else                                                // Если рабочий день уже существует 
            {
                result += $"\nУказано что {emp.FirstName} {emp.LastName} за {workdate.ToString("d")} отработал {GetInfoWorkHoursAtDate(client)} рабочих часов\nДля внесение изменений в существующие данные воспользуйтесь пунктом 7 'Отредактировать информацию'";
            }

            return result;
        }


        public static double GetInfoWorkHoursAtDate(ClientObject client) // Возвращает количество рабочих часов сотрудника в определенную дату
        {
            var emp = (Employee)client.ClientAnswers[1];
            var workdate = (DateTime)client.ClientAnswers[2];

            double hours = 0;
            var i = 0;

            while (i < emp.WorkDays.Count)                      // Находим в списке рабочих дней необходимый
            {
                if (emp.WorkDays[i].WorkDate.Equals(workdate))
                {
                    hours = emp.WorkDays[i].WorkHoursAtDay();
                    break;
                }
                i++;
            }

            return hours;
        }

        public static string GetInfoWorkHoursPerPeriod(ClientObject client)    // Возвращает количество рабочих часов сотрудника за период
        {
            var emp = (Employee)client.ClientAnswers[1];
            var firstdate = (DateTime)client.ClientAnswers[2];
            var seconddate = (DateTime)client.ClientAnswers[3];

            double sum = 0;
            var i = 0;

            while (i < emp.WorkDays.Count)
            {
                if (emp.WorkDays[i].WorkDate >= firstdate && emp.WorkDays[i].WorkDate <= seconddate)
                {
                    sum += emp.WorkDays[i].WorkHoursAtDay();   // Получаем количество рабочих часов этого сотрудника в этот день и суммируем
                }
                i++;
            }
           
            return $"C {firstdate.ToString("d")} по {seconddate.ToString("d")} сотрудник {emp.FirstName} {emp.LastName} отработал {sum} часов";
        }

        public static string GetInfoWorkHoursAllTime(ClientObject client)            // Возвращает количество рабочих часов сотрудника за все время
        {
            var emp = (Employee)client.ClientAnswers[1];

            double sum = 0;
            var i = 0;

            while (i < emp.WorkDays.Count)
            {
                sum += emp.WorkDays[i].WorkHoursAtDay();   // Получаем количество рабочих часов этого сотрудника в этот день и суммируем
                i++;
            }

            return $"\nВсего сотрудник {emp.FirstName} {emp.LastName} отработал {sum} часов";
        }


        public static string GetListOfEmployeesAndWorkHours()       // Выводим список сотрудников и отработанных часов
        {
            string result = "\nСписок сотрудников и рабочих часов:\n\n";

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

                result += $"{i}. {Employees[i].FirstName} {Employees[i].LastName} всего отработал: \t{sum} часов\n";
                i++;
            }

            return result;
        }


        public static string EditInfoAboutWorkDay(ClientObject client)
        {
            Employee emp = (Employee)client.ClientAnswers[1];
            DateTime workdate = (DateTime)client.ClientAnswers[2];
            double hours = (double)client.ClientAnswers[3];

            int i = 0;

            if (GetInfoWorkHoursAtDate(client) > 0)              // Если такой рабочий день уже есть
            {
                while (i < emp.WorkDays.Count)
                {
                    if (DateTime.Equals(emp.WorkDays[i].WorkDate, workdate))
                    {
                        emp.WorkDays[i].EditWorkHoursAtDay(hours);
                    }

                    i++;
                }
            }
            else
            {
                emp.WorkDays.Add(new WorkDay(workdate, hours));         // Если такого рабочего дня еще нет 
            }


            return $"Информация отредактирована. Зафиксировано что {emp.FirstName} {emp.LastName} за {workdate.ToString("d")} отработал {GetInfoWorkHoursAtDate(client)} рабочих часов";

        }


        public void AddCustomWorkDayForSaveLoad(DateTime workdate, double hours)     // Для загрузки программы с файла
        {
            WorkDays.Add(new WorkDay(workdate, hours));
        }
         
        public static string ToFireEmployee(ClientObject client)                // Уволить сотрудника (удалить из списка)
        {
            var emp = (Employee)client.ClientAnswers[1];
            var IsSure = (string)client.ClientAnswers[2];

            if (IsSure.Equals("y"))
            {
                Employees.Remove(emp);
                Console.WriteLine($"Пользователь {client.userName} уволил {emp.FirstName} {emp.LastName}");
                return $"Сотрудник {emp.FirstName} {emp.LastName} уволен";
            }
            else if (IsSure.Equals("n"))
            {
                Console.WriteLine($"Пользователь {client.userName} отменил процедуру увольнения {emp.FirstName} {emp.LastName}");
                return $"Сотрудник {emp.FirstName} {emp.LastName} не уволен. Процедура отменена.";
            }    
            else
            {
                return "Введите Y что бы уволить сотрудника, или N что бы отменить процедуру.";
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
