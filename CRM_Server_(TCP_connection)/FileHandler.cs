using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRM_Server__TCP_connection_
{
    internal class FileHandler
    {      
        public static void AddLog(string log)     
        {
            using (var logs = new StreamWriter("logs.txt", append: true, Encoding.UTF8))
            {
                logs.WriteLine(log);
                Console.WriteLine(log);
            }
        }

        public static void AddLogClientChoice(ClientObject client, bool successful = true, string plus = "")
        {
            using (var logs = new StreamWriter("logs.txt", append: true, Encoding.UTF8))
            {
                if (successful)
                {
                    logs.WriteLine($"{client.userName} сделал в пункте: '{MenuNavigation.GetTitleOfMenuItem(client)}' выбор: ({client.Answers.Last()}).");
                    Console.WriteLine($"{client.userName} сделал в пункте: '{MenuNavigation.GetTitleOfMenuItem(client)}' выбор: ({client.Answers.Last()}).");
                }
                else
                {
                    logs.WriteLine($"{client.userName} сделал некорректный выбор в пункте: '{MenuNavigation.GetTitleOfMenuItem(client)}'");
                    Console.WriteLine($"{client.userName} сделал некорректный выбор в пункте: '{MenuNavigation.GetTitleOfMenuItem(client)}'");
                    ServerObject.SendMessage($"Введите корректное значение. {client.messageType.String()} {plus}", client);
                }
                
            }
        }

        public static void AddLogSwitchToMainMenu(ClientObject client)
        {
            using (var logs = new StreamWriter("logs.txt", append: true, Encoding.UTF8))
            {
                logs.WriteLine($"{client.userName} вернулся в главное меню");
                Console.WriteLine($"{client.userName} вернулся в главное меню");
            }
        }

    }
}
