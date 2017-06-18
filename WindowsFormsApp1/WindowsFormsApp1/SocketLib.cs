using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class GameSetting
    {
        public static String serverIp = "127.0.0.1";
        public static int port = 2000;
    }

    public delegate String StrHandler(String str);

    public class GameSocket
    {
        public Socket socket;
        public NetworkStream stream;
        public StreamReader reader;
        public StreamWriter writer;
        public StrHandler inHandler;
        public EndPoint remoteEndPoint;
        public bool isDead = false;

        public GameSocket(Socket s)
        {
            socket = s;
            stream = new NetworkStream(s);
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            remoteEndPoint = socket.RemoteEndPoint;
        }

        public String receive()
        {
            return reader.ReadLine();
        }

        public GameSocket send(String line)
        {
            writer.WriteLine(line);
            writer.Flush();
            return this;
        }

        public static GameSocket connect(String ip)
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), GameSetting.port);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipep);
            return new GameSocket(socket);
        }

        public Thread newListener(StrHandler pHandler)
        {
            inHandler = pHandler;

            Thread listenThread = new Thread(new ThreadStart(listen));
            listenThread.Start();
            return listenThread;
        }

        public void listen()
        {
            try
            {
                while (true)
                {
                    String line = receive();
                    inHandler(line);
                }
            }
            catch (Exception ex)
            {
                isDead = true;
                Console.WriteLine(ex.Message);
            }
        }
    }
}
