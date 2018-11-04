using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FTD2XX_NET; //load FTDI driver

/* 
 * PINOUT
 * 
 * 1: Front Left (1, 254)
 * 2: Front Right (2, 253)
 * 3: Back Left (4, 251)
 * 4: Back Right (8, 247)
 * 5: Surface (16, 239)
 * 6: Dive (32, 223)
 * 7: Light (64, 191)
 * 8: Reserved (128, 127)
 */


namespace ROV_Controller_v0._1
{
    public partial class Form1 : Form
    {
        //FTDI stuff
        public FTDI myFtdiDevice = new FTDI();
        FTDI.FT_STATUS ftStatus;
        byte[] sentBytes = new byte[2];
        uint receivedBytes;

        //light
        bool lightOff = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void initialiseBoard_Click(object sender, EventArgs e)
        {
            ftStatus = myFtdiDevice.OpenByIndex(0);
            ftStatus = myFtdiDevice.SetBaudRate(921600);
            ftStatus = myFtdiDevice.SetBitMode(255, 4);
            sentBytes[0] = 0;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W: //Forwards
                    sentBytes[0] = (byte)(sentBytes[0] | 1);
                    sentBytes[0] = (byte)(sentBytes[0] | 2);
                    myFtdiDevice.Write(sentBytes, 1, ref receivedBytes);
                    break;
                case Keys.A: //Left
                    sentBytes[0] = (byte)(sentBytes[0] | 2);
                    sentBytes[0] = (byte)(sentBytes[0] | 8);
                    myFtdiDevice.Write(sentBytes, 1, ref receivedBytes);
                    break;
                case Keys.S: //Backwards
                    sentBytes[0] = (byte)(sentBytes[0] | 4);
                    sentBytes[0] = (byte)(sentBytes[0] | 8);
                    myFtdiDevice.Write(sentBytes, 1, ref receivedBytes);
                    break;
                case Keys.D: //Right
                    sentBytes[0] = (byte)(sentBytes[0] | 1);
                    sentBytes[0] = (byte)(sentBytes[0] | 4);
                    myFtdiDevice.Write(sentBytes, 1, ref receivedBytes);
                    break;
                case Keys.Z: //Swivel Left
                    sentBytes[0] = (byte)(sentBytes[0] | 2);
                    sentBytes[0] = (byte)(sentBytes[0] | 4);
                    myFtdiDevice.Write(sentBytes, 1, ref receivedBytes);
                    break;
                case Keys.X: //Swivel Right
                    sentBytes[0] = (byte)(sentBytes[0] | 1);
                    sentBytes[0] = (byte)(sentBytes[0] | 8);
                    myFtdiDevice.Write(sentBytes, 1, ref receivedBytes);
                    break;
                case Keys.R: //Surface
                    sentBytes[0] = (byte)(sentBytes[0] | 16);
                    myFtdiDevice.Write(sentBytes, 1, ref receivedBytes);
                    break;
                case Keys.F: //Dive
                    sentBytes[0] = (byte)(sentBytes[0] | 32);
                    myFtdiDevice.Write(sentBytes, 1, ref receivedBytes);
                    break;
                case Keys.L: //Light
                    if (lightOff)
                    {
                        sentBytes[0] = (byte)(sentBytes[0] | 64);
                        myFtdiDevice.Write(sentBytes, 1, ref receivedBytes);
                        lightOff = false;
                    }
                    else
                    {
                        sentBytes[0] = (byte)(sentBytes[0] & 191);
                        myFtdiDevice.Write(sentBytes, 1, ref receivedBytes);
                        lightOff = true;
                    }
                    break;
                default:
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            sentBytes[0] = (byte)(sentBytes[0] & 254);
            sentBytes[0] = (byte)(sentBytes[0] & 253);
            sentBytes[0] = (byte)(sentBytes[0] & 251);
            sentBytes[0] = (byte)(sentBytes[0] & 247);
            sentBytes[0] = (byte)(sentBytes[0] & 239);
            sentBytes[0] = (byte)(sentBytes[0] & 223);
            myFtdiDevice.Write(sentBytes, 1, ref receivedBytes);
        }
    }
}
