using System;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        private static int _y;
        private static Object _lock = new Object();

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
                
                Stream stm;
                string str="start";
            
                Thread worker = new Thread(() =>
                {
                    while (true)
                    {
                        lock (str)
                        {
                            if (str.Equals("bye"))
                            {
                                break;
                            }
                        }
                        stm = tcpclnt.GetStream();
                        byte[] bb = new byte[100];
                        int k; 
                        
                        k = stm.Read(bb, 0, 100);
                        _y = Console.CursorTop;
                            
                        if (_y == Console.BufferHeight - 1)
                        { 
                            Console.WriteLine(); Console.SetCursorPosition(0, --_y);
                        }
                            
                        int x = Console.CursorLeft;
                        Console.MoveBufferArea(0, _y, Console.WindowWidth, 1, 0, _y + 1);
                        //move the current line to next line, if the console start a new line, will move only the line at the bottom.
                        Console.SetCursorPosition(0, _y);//back to previous line
                            
                        for (int i = 0; i < k; i++) 
                        { 
                            Console.Write(Convert.ToChar(bb[i]));
                        }
                            
                        Console.SetCursorPosition(x, _y + 1);//move the cursor to where it was
                    }
                });
                
                worker.Start();
                
                Console.WriteLine("Connected");
                while (true)
                {
                    Console.Write(">>>");
                    str = Console.ReadLine();
                    if (str.Length==0)
                        continue;
                    stm = tcpclnt.GetStream();
                    ASCIIEncoding asen = new ASCIIEncoding();
                    byte[] ba = asen.GetBytes(str);

                    Console.WriteLine("Transmitting.....");
                    stm.Write(ba, 0, ba.Length);

                    if (str.Equals("bye"))
                    {
                        break;
                    }
                    //byte[] bb = new byte[100];
                    //int k = stm.Read(bb, 0, 100);
                    //for (int i = 0; i < k; i++)
                    //    Console.Write(Convert.ToChar(bb[i]));
                }
                tcpclnt.Close();
            }
        
            catch (Exception e) {
                Console.WriteLine("Error..... \n" + e.StackTrace);
            }            
        }
    }
}