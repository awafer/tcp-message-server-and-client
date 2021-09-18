﻿using System;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string serverip = "10.0.0.100";
            int serverport = 8001;
            try
            {
                TcpClient tcpclnt;
                Console.WriteLine("Connecting.....");
            
                tcpclnt = new TcpClient();
                tcpclnt.Connect(serverip,serverport);
                // use the ipaddress as in the server program
            
                Console.WriteLine("Connected");
                while (true)
                {
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
                }
                tcpclnt.Close();
            }
        
            catch (Exception e) {
                Console.WriteLine("Error..... \n" + e.StackTrace);
            }            
        }
    }
}