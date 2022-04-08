using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRM_Server__TCP_connection_
{
    public class ServerObject
    {
        private static TcpListener tcpListener;                         // сервер для прослушивания
        private List<ClientObject> clients = new List<ClientObject>();  // все подключения



        internal void AddConnection(ClientObject clientObject)    // добавляем подключение
        {
            clients.Add(clientObject);
        }

        internal void RemoveConnection(string id)                             // убираем подключение
        {
            ClientObject client = clients.FirstOrDefault(c => c.Id == id);  // получаем по id закрытое подключение   

            if (client != null)                                             // и удаляем его из списка подключений
            {
                clients.Remove(client);
            }
        }

        internal void Listen()                                    // прослушивание входящих подключений
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, 8888);
                tcpListener.Start();
                Console.WriteLine("Сервер запущен. Ожидание подключений...");

                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();

                    ClientObject clientObject = new ClientObject(tcpClient, this);
                    Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Disconnect();
            }
        }


        public static void SendMessage(string message, ClientObject client)   // передача данных     
        {
            byte[] data = Encoding.UTF8.GetBytes(message);

            client.Stream.Write(data, 0, data.Length);
        }

        internal void Disconnect()                                            // отключение всех клиентов
        {
            tcpListener.Stop();                                             //остановка сервера

            FileHandler.AddLog($"Сервер остановлен: {DateTime.Now}");

            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].Close();                                         //отключение клиента
            }
            Environment.Exit(0);                                            //завершение процесса
        }
    }
}
