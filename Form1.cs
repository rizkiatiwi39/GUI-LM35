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

namespace GUI_LM35_Ins_Elka
{
    public partial class Form1 : Form
    {
        string serialDataIn;
        sbyte indexOfA, indexOfB, indexOfC;
        string dataSensor1, dataSensor2, dataSensor3;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button_connect.Enabled = true;
            button_disconnect.Enabled = false;
            //verticalProgressBar_statusCom.Value = 0;
            comboBox_baudRate.Text = "9600";
        }

        private void comboBox_comPort_DropDown(object sender, EventArgs e)
        {
            string[] portLists = SerialPort.GetPortNames();
            comboBox_comPort.Items.Clear();
            comboBox_comPort.Items.AddRange(portLists);

        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = comboBox_comPort.Text;
                serialPort1.BaudRate = Convert.ToInt32(comboBox_baudRate.Text);
                serialPort1.Open();

                pictureBox1.BackColor = Color.Green;

                button_connect.Enabled = false;
                button_disconnect.Enabled = true;
                
                //verticalProgressBar_statusCom.Value = 100;

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox_sensor1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_disconnect_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Close();

                button_connect.Enabled = true;
                button_disconnect.Enabled = false;
                //verticalProgressBar_statusCom.Value = 0;

                pictureBox1.BackColor = Color.Red;
                textBox_sensor1.Text = "0";
                textBox_sensor2.Text = "0";
                textBox_sensor3.Text = "0";


            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                serialPort1.Close();

            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            serialDataIn = serialPort1.ReadLine();
            this.BeginInvoke(new EventHandler(ProcessData));
        }

        private void ProcessData(object sender, EventArgs e)
        {
            try
            {
                indexOfA = (sbyte)Convert.ToByte( serialDataIn.IndexOf("A"));
                indexOfB = (sbyte)Convert.ToByte( serialDataIn.IndexOf("B"));
                indexOfC = (sbyte)Convert.ToByte( serialDataIn.IndexOf("C"));

                dataSensor1 = serialDataIn.Substring(0, indexOfA);
                dataSensor2 = serialDataIn.Substring(indexOfA + 1, (indexOfB - indexOfA) - 1);
                dataSensor3 = serialDataIn.Substring(indexOfB + 1, (indexOfC - indexOfB) - 1);

                textBox_sensor1.Text = dataSensor1;
                textBox_sensor2.Text = dataSensor2;
                textBox_sensor3.Text = dataSensor3;

                //verticalProgressBar2.Value = Convert.ToInt32(dataSensor1);
                //verticalProgressBar3.Value = Convert.ToInt32(dataSensor2);
                //verticalProgressBar4.Value = Convert.ToInt32(dataSensor3);

                //progressBar1.Value = Convert.ToInt32(dataSensor1);
                //progressBar2.Value = Convert.ToInt32(dataSensor2);
                //progressBar3.Value = Convert.ToInt32(dataSensor3);

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
    }
}
