using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string serverip = "192.168.1.100";
            int serverport = 8001;
            try
            {
                TcpClient tcpclnt;
                Console.WriteLine("Connecting.....");
            
                
                // use the ipaddress as in the server program
            
                Console.WriteLine("Connected");
                while (true)
                {
                    tcpclnt = new TcpClient();
                    tcpclnt.Connect(serverip,serverport);
                    Console.Write("Enter the string to be transmitted : ");
                    string str = Console.ReadLine();
                    Stream stm = tcpclnt.GetStream();
                    ASCIIEncoding asen = new ASCIIEncoding();
                    byte[] ba = asen.GetBytes(str);
                    Console.WriteLine("Transmitting.....");
                    stm.Write(ba, 0, ba.Length);
                    byte[] bb = new byte[100];
                    int k = stm.Read(bb, 0, 100);
                    for (int i = 0; i < k; i++)
                        Console.Write(Convert.ToChar(bb[i]));
                    tcpclnt.Close();
                }
                
            }
        
            catch (Exception e) {
                Console.WriteLine("Error..... \n" + e.StackTrace);
            }            
        }
    }
}