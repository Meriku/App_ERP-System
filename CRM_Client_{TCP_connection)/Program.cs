using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRM_Client__TCP_connection_
{
    internal class Program
    {
        static string userName;
        private const string host = "127.0.0.1";
        private const int port = 8888;
        static TcpClient client;
        static NetworkStream stream;


        static void Main(string[] args)
        {
            Console.WriteLine("Здравствуйте! Вас приветствует приложение ERM2000v1.0");
            Console.WriteLine("Введите свое имя: ");
            userName = Console.ReadLine();
            client = new TcpClient();

            try
            {
                client.Connect(host, port);     // Подключаем пользователя 
                stream = client.GetStream();    // Получаем поток

                string message = userName;
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
                
                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage)); // Новый поток для получения данных
                receiveThread.Start();                                              // Запускаем поток
                SendMessage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Disconnect();
            }
        }
        

        static void SendMessage() // Отправка сообщений
        {
            while (true)
            {
                
                string message = Console.ReadLine();
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
        }
        

        static void ReceiveMessage() // Получение сообщений
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[512]; 
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string message = builder.ToString();

                    if (CheckIfMessageIsCommand.IfCommandClearConsole(message))
                    {
                        message = message.Substring(1);
                    }
                    if (CheckIfMessageIsCommand.IfCommandDisconnect(message))
                    {
                        Disconnect();
                    }


                    Console.Write(message); // Вывод сообщения
                    Console.WriteLine();
                }
                catch
                {
                    Console.WriteLine("Подключение прервано!"); // Соединение было прервано
                    Console.ReadLine();
                    Disconnect();
                }
            }
        }

        static void Disconnect()
        {
            if (stream != null)
                stream.Close(); // Отключение потока
            if (client != null)
                client.Close(); // Отключение клиента
            Environment.Exit(0);// Завершение процесса
        }
    }
}
