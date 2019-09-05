using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Speech.Recognition;

namespace serialPortArduino
{
    public partial class Form1 : Form
    {
        SerialPort port;
        int cmdState = 0;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Click(object sender, EventArgs e)
        {
            if (cmdState == 0)
            {
                cmdState = 1;
                label1.Text = "Listening...";
                label2.Visible = false;
                string cmd = "";
                while (cmd != "sleep")
                {
                    SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();
                    Grammar g = new DictationGrammar();
                    recognizer.LoadGrammar(g);
                    recognizer.SetInputToDefaultAudioDevice();
                    string rec = "";
                    try
                    {
                        RecognitionResult result = recognizer.Recognize();
                        rec = result.Text.ToLower();
                    }
                    catch (NullReferenceException c)
                    {
                        rec = "000";
                    }

                    if (rec.IndexOf("on") != -1)
                    {
                        sendCmd("on");
                        label1.Text = "Turning lights on...";
                        cmd = "000";
                    }
                    else if (rec.IndexOf("off") != -1)
                    {
                        sendCmd("off");
                        label1.Text = "Turning lights off...";
                        cmd = "000";
                    }
                    else if (rec.IndexOf("close") != -1)
                    {
                        label1.Text = "Sleeping...";
                        label2.Visible = true;
                        cmd = "sleep";
                        cmdState = 0;
                    }
                    else
                    {
                        label1.Text = "What?...";
                    }
                }
            }
        }

        private void sendCmd(string c)
        {
            if (c == "on")
            {
                port = new SerialPort();
                port.PortName = "COM3";
                port.BaudRate = 9600;
                port.Open();
                port.Write("1");
                port.Close();
            }

            else if (c == "off")
            {
                port = new SerialPort();
                port.PortName = "COM3";
                port.BaudRate = 9600;
                port.Open();
                port.Write("0");
                port.Close();
            }
            else
            {
                c = "000";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
