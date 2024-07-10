using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientProject
{
    public partial class Form1 : Form
    {
        StreamReader reader ;
        StreamWriter writer ;
        TcpClient client ;
        public Form1()
        {
            InitializeComponent();
        }

        private async void Connect()
        {
            //To work with the client
            client = new TcpClient();
            //connect to the server socket
            client.Connect("127.0.0.1", 49268);

            NetworkStream stream = client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            writer.AutoFlush = true; //here will make auto flush , without need to make call flush function

            //NetworkStream stream = client.GetStream();
            //reader = new StreamReader(stream);
            //writer = new StreamWriter(stream);
            //writer.AutoFlush = true; //here will make auto flush , without need to make call flush function


            //writer.WriteLine("Fuck My Life..."); //But the message on the stream only
            ////writer.Flush(); //Here this send the message in the stream , and then clear the buffer
            buttonSend.Enabled = true;

            while(true)
            {
                string line = await reader.ReadLineAsync();
                textBoxSavedMessages.Text += $"{Environment.NewLine} {line}"; 
            }
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
           

            writer.WriteLine(textBox1.Text); //But the message on the stream only
            //writer.Flush(); //Here this send the message in the stream , and then clear the buffer
            
            textBox1.Clear();
        
        }
    }
}
