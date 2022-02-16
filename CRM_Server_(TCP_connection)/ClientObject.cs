using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Server__TCP_connection_
{
    public class ClientObject
    {
        

        public string Id { get; private set; }
        public NetworkStream Stream { get; private set; }
        public string userName;                                     // Имя пользователя 
        public List<string> ClientAnswers = new List<string>();     // Список ответов пользователя

        public TcpClient client;
        public ServerObject server;                                 // Объект сервера

        public ClientObject(TcpClient tcpClient, ServerObject serverObject)
        {
            Id = Guid.NewGuid().ToString();
            client = tcpClient;
            server = serverObject;
            serverObject.AddConnection(this);
        }

        public void AddClientAnswer(string answer)
        {
            ClientAnswers.Add(answer);
        }



        public void Process()
        {
            try
            {
                Stream = client.GetStream();
               
                string message = GetMessage(); // Получаем имя пользователя
                userName = message;

                message = $"#Здравствуйте! {userName}, вы успешно подключились к системе";  // Посылаем сообщение о успешном подключении к серверу 
                ServerObject.SendMessage(message, this);

                UserInterface.PrintMainMenu(this);                                          // Отправляем первое приветствие (меню)

                Console.WriteLine($"{userName} подключился к системе в {DateTime.Now.ToString("HH:mm:ss")}");

                
                while (true) // В бесконечном цикле получаем сообщения от клиента
                {
                    try
                    {
                        message = GetMessage();

                        MenuNavigation.AddClientAnswer(message, this);

                    }
                    catch
                    {
                        Console.WriteLine($"Пользователь {userName} вышел из системы");
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally // В случае выхода из цикла закрываем ресурсы
            {               
                server.RemoveConnection(this.Id);
                Close();
            }
        }

        
        public string GetMessage() // Чтение входящего сообщения и преобразование в строку
        {
            byte[] data = new byte[512];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
            }
            while (Stream.DataAvailable);

            return builder.ToString();
        }

        
        public void Close() // Закрытие подключения
        {
            if (Stream != null)
                Stream.Close();
            if (client != null)
                client.Close();
        }
    }
}
