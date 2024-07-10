using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientProject
{
    internal class Client
    {
        public event EventHandler<string> MessageReseved;

        TcpClient client;
        StreamReader streamReader;
        StreamWriter streamWriter;

        public Client(TcpClient client)
        {
            this.client = client;
            NetworkStream stream = this.client.GetStream();
            this.streamWriter = new StreamWriter(stream);
            this.streamReader = new StreamReader(stream);
            this.streamWriter.AutoFlush = true;

            ReadMessages();
        }

        public void SendMessage (string message)
        {
            streamWriter.WriteLine(message);
        }

        private async void ReadMessages()
        {
            while (true)
            {
                string msg = await streamReader.ReadLineAsync();
                
                if(MessageReseved != null)
                {
                    MessageReseved(this, msg);
                } 

            }
        }
    }
}
