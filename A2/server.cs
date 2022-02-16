namespace faq
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.IO;
    using System.Threading;

    class Worker
    {
        TcpClient client;
        FAQ myFAQ;

        public Worker(TcpClient Client, FAQ MyFAQ)
        {
            client = Client;
            myFAQ = MyFAQ;

            new Thread(new ThreadStart(HandleRequest)).Start();
        }

        public void HandleRequest()
        {

            bool loop = true;

            // get streams
            StreamReader reader = new StreamReader(client.GetStream());
            StreamWriter writer = new StreamWriter(client.GetStream());

            // read and write


            while (loop)
            {
                writer.WriteLine("OPTIONS");
                writer.WriteLine("1 Create Question");
                writer.WriteLine("2 List Questions");
                writer.WriteLine("3 Read Responses to Question");
                writer.WriteLine("4 Add Comment to Question");
                writer.WriteLine("5 Exit");
                writer.Flush();

                {
                    string command = reader.ReadLine();

                    if (command == "1")
                    {
                        writer.WriteLine("Type Question");
                        writer.Flush();
                        string question = reader.ReadLine();
                        myFAQ.CreateQuestion(question);
                    }
                    if (command == "2")
                    {
                        writer.WriteLine("Questions");
                        writer.WriteLine(myFAQ.ListQuestions());
                        writer.Flush();
                    }
                    if (command == "3")
                    {
                        writer.WriteLine("Type Question");
                        writer.Flush();
                        string question = reader.ReadLine();
                        writer.WriteLine("Comments");
                        writer.WriteLine(myFAQ.GetComments(question));
                        writer.Flush();

                    }
                    if (command == "4")
                    {
                        writer.WriteLine("Type Question");
                        writer.Flush();
                        string question = reader.ReadLine();
                        writer.WriteLine("Type Comment");
                        writer.Flush();
                        string comment = reader.ReadLine();

                        myFAQ.AddComment(question, comment);
                    }
                    if (command == "5")
                    {
                        writer.WriteLine("EXIT");
                        loop = false;
                    }
                }




            }


            writer.Flush();
            client.Close();


        }




    }
    class Server
    {
        static void Main(string[] args)
        {
            FAQ myFAQ = new FAQ();

            Int32 port = 8080;
            IPAddress localAddr = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
             
            TcpListener server = new TcpListener(localAddr, port);
            //TcpListener server = new TcpListener(port);
            server.Start();

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("connection");
                new Worker(client, myFAQ);
            }
        }
    }
}