using ClientProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatServer
{
    public partial class Form1 : Form
    {

        List<Client> clients;
        public Form1()
        {
            InitializeComponent();
            clients = new List<Client>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartServer();
        }

        private async void StartServer()
        {
            //IPAddress ip = new IPAddress(new byte[] {192,168,1,5 });
            //IPAddress ip = IPAddress.Parse("192.168.1.16");
            IPAddress ip = IPAddress.Parse("127.0.0.1");

            //Listen to the trafic
            TcpListener listener = new TcpListener(ip , 49268);
            listener.Start(); //here just book the port number

            while(true) { 
                TcpClient client = await listener.AcceptTcpClientAsync(); //accept connection
                //Here the subscriber happend
                Client NewClient = new Client(client);
                NewClient.MessageReseved += NewClient_MessageReseved; 
                clients.Add(NewClient);
                btnSend.Enabled = true;
            }

            //NetworkStream stream = client.GetStream();
            //reader = new StreamReader(stream);
            //writer = new StreamWriter(stream);

            //writer.AutoFlush = true; //here will auto push the writed data on the stream


        }

        private void NewClient_MessageReseved(object sender, string msg)
        {
            textBoxSavedMessages.Text = $"{msg} {Environment.NewLine}";
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            foreach (Client client in clients)
            {
                client.SendMessage(textBox1.Text);
            }
            textBox1.Clear();
        }
    }
}
