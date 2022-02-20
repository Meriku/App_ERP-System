using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Client__TCP_connection_
{
    internal class CheckIfMessageIsCommand
    {

        public static bool IfCommandClearConsole(string message)
        {
            if (message.Length > 0)
            {
                if (message[0].Equals('#'))
                {
                    Console.Clear();                    
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }     
        }


        public static bool IfCommandDisconnect(string message)
        {       
            if (message.Length > 0)
            {
                if (message[0].Equals('%'))
                {
                    Console.WriteLine("\nВы отключены от сервера.");
                    Console.ReadLine();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }



}
