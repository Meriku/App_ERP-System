using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRM_Server__TCP_connection_
{
    internal class FileReadAndWriteHandler
    {
        public static bool isReading = true;

        public static void ToLoadDataBase()
        {
            isReading = true;

            string result;
            string FirstName;
            string LastName;
            DateTime date;
            double hours;

            Employee TempEmployee = null;

            using (var DataBase = new StreamReader("DataBase.txt", Encoding.UTF8)) //Загружаем информацию с базы данных
            {             
                while (!DataBase.EndOfStream)
                {
                    result = DataBase.ReadLine();

                    if (result[0].Equals('#'))
                    {
                        FirstName = result.Substring(1, result.IndexOf(" ")-1);    
                        LastName = result.Substring(result.IndexOf(" ")+1);

                        TempEmployee = new Employee(FirstName, LastName);
                    }
                    else
                    {
                        date = DateTime.Parse(result.Substring(0, result.IndexOf("#")));
                        hours = double.Parse(result.Substring(result.IndexOf("#") + 1));

                        if (TempEmployee != null)
                        {
                            TempEmployee.AddCustomWorkDayForSaveLoad(date, hours);
                        }

                    }
                }             
                Thread.Sleep(5000);
                isReading = false;
            }           
        }
   
        
        public static void ToSaveDataBase()
        {
            string result = "";

            foreach (Employee emp in Employee.Employees)
            {
                result += emp.GetEmployeeDatesAndHours();
            }

            //      "#Имя Фамилия"
            //      "10.02.2020#6"
            //      "11.02.2020#8"
            //      "#Имя Фамилия"

            using (var DataBase = new StreamWriter("DataBase.txt", append: false, Encoding.UTF8))
            {           
                DataBase.Write(result);
            }
        }
    }
}
