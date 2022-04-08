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
        public static void ToAddLogs(string log)     
        {
            using (var logs = new StreamWriter("logs.txt", append: true, Encoding.UTF8))
            {
                logs.WriteLine(log);
                Console.WriteLine(log);
            }
        }
    }
}
