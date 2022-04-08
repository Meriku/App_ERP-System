using System;
using System.Linq;
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
            // Сохранение и загрузка информации из базы данных (SQL)

         
            try
            {
                server = new ServerObject();                // Старт сервера
                listenThread = new Thread(new ThreadStart(server.Listen));
                listenThread.Start();                       // Старт потока
            }
            catch (Exception ex)
            {
                server.Disconnect();
                FileHandler.AddLog($"Сервер закрыт с ошибкой: {DateTime.Now} | {ex.Message}");
                Console.WriteLine(ex.Message);
            }                   
        }
    }
}
