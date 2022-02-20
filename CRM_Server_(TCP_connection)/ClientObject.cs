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
        public List<object> ClientAnswers = new List<object>();     // Список ответов пользователя
        public object[] ExpectedAnswer = new object[2];             // Ожидаемый ответ пользователя (тип переменной)        

        public TcpClient client;
        public ServerObject server;                                 // Объект сервера

        public ClientObject(TcpClient tcpClient, ServerObject serverObject)
        {
            Id = Guid.NewGuid().ToString();
            client = tcpClient;
            server = serverObject;
            serverObject.AddConnection(this);
            ExpectedAnswer[0] = typeof(int);                        // Ожидаемое полученное от пользователя первое значение - целое число
            ExpectedAnswer[1] = 10;                                 // Максимальное значение которого - 10
        }

        public void AddAnswer(int answer)
        {
            ClientAnswers.Add(answer);
        }
        public void AddAnswer(string answer)
        {
            ClientAnswers.Add(answer);
        }
        public void AddAnswer(DateTime answer)
        {
            ClientAnswers.Add(answer);
        }
        public void AddAnswer(Employee answer)
        {
            ClientAnswers.Add(answer);
        }
        public void AddAnswer(double answer)
        {         
            ClientAnswers.Add(Math.Round(answer, 2));
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

                Console.WriteLine($"{userName} подключился к системе в {DateTime.Now:HH:mm:ss}");


                while (true) // В бесконечном цикле получаем сообщения от клиента
                {
                    try
                    {
                        message = GetMessage();

                        MenuNavigation.AddClientAnswer(message, this);

                    }
                    catch
                    {
                        Console.WriteLine($"Пользователь {userName} вышел из системы в {DateTime.Now:HH:mm:ss}");
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
