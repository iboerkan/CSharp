using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HSAExtensions
{
    public partial class frmTcpIpPortOkuma : DevExpress.XtraEditors.XtraForm
    {
        SerialPort _serialPort;
        public frmTcpIpPortOkuma()
        {
            InitializeComponent();
        }

        private void frmElGoruntuAktar_Load(object sender, EventArgs e)
        {//921600
            this.Text = Ortam.FirmaAdi + " - " + this.Text;

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            memoEdit1.Enabled = true;
            Thread thread = new Thread(Deneme);
            thread.IsBackground = true;
            thread.Start();
        }
        private static TcpListener _TcpListener;
        private static bool _QuitProcessing = false;
        string mesaj = "";
        TcpClient client;
        private void Deneme()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            // IP address and port of the TCP server
            string serverIP = "10.0.10.141"; // Change this to the IP address of your server
            int serverPort = 3128; // Change this to the port number of your server
            client = new TcpClient(serverIP, serverPort);
            try
            {


                // Connect to the server

                Console.WriteLine("Connected to server.");

                // Get the network stream
                NetworkStream stream = client.GetStream();

                // Read data from the server
                byte[] buffer = new byte[1024];
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Received barcode: " + data);
                    memoEdit1.Text =DateTime.Now.ToString()+" : "+ data + Environment.NewLine+memoEdit1.Text;

                }

                // Close the connection
                client.Close();

            }
            catch (Exception ex)
            {
                client.Close();
                memoEdit1.Text += ex.Message + Environment.NewLine;
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            client.Close();
            memoEdit1.Text = "";
            memoEdit1.Enabled = false;
        }
    }
}