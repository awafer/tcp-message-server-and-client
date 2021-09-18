using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //
            /*   Server Program    */
            string serverip = "192.168.1.100";
            int serverport = 8001;
            try {
                IPAddress ipAd = IPAddress.Parse(serverip);
                // use local m/c IP address, and 
                // use the same in the client

                /* Initializes the Listener */
                TcpListener listenerlist=new TcpListener(ipAd,serverport);

                /* Start Listening at the specified port */
                
                Console.WriteLine("The server is running at port "+serverport+"...");
                Console.WriteLine("The local End point is " +
                                  listenerlist.LocalEndpoint );
                Console.WriteLine("Waiting for a connection...");
                Socket skt;
                byte[] b;
                int k;
                
                listenerlist.Start();
                skt =listenerlist.AcceptSocket();
                while (true)
                {
                    b= new byte[100];
                    k = skt.Receive(b);
                    Console.WriteLine("Connection accepted from " + skt.RemoteEndPoint);
                    Console.WriteLine("Recieved...");
                    
                    for (int i = 0; i < k; i++)

                        Console.Write(Convert.ToChar(b[i]));

                    ASCIIEncoding asen = new ASCIIEncoding();

                    skt.Send(asen.GetBytes("The string was recieved by the server.\n"));

                    Console.WriteLine("\nSent Acknowledgement");
                    
                    /* clean up */
                    skt.Close();
                    listenerlist.Stop();
                }
            }
            catch (Exception e) {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }
    }
}