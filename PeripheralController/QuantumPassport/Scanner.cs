using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using System.Runtime.InteropServices;
using System.Diagnostics;

namespace QuantumPassport
{
    unsafe public partial class Scanner : Form
    {
        public Scanner()
        {
            InitializeComponent();
        }

        //Register QuantA6Passport Guid for auto connect.
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr RegisterDeviceNotification(IntPtr recipient, IntPtr notificationFilter, int flags);
        [DllImport("user32.dll")]
        public static extern bool UnregisterDeviceNotification(IntPtr handle);
        [StructLayout(LayoutKind.Sequential)]

        public struct DevBroadcastDeviceinterface
        {
            internal int Size;
            internal int DeviceType;
            internal int Reserved;
            internal Guid ClassGuid;
            internal short Name;
        }


        //Define auto connect message.
        public const int DBT_DEVICEARRIVAL = 0x8000;
        public const int DBT_DEVICEREMOVECOMPLETE = 0x8004;
        public const int WM_DEVICECHANGE = 0x0219;
        //Define constant for auto connect.
        public const int DBT_DEVTYP_DEVICEINTERFACE = 5;

        //Global variables.
        public string FilePath = "C:\\inetpub\\wwwroot\\Passport";
        public static IntPtr g_notificationHandle;

        //AutoScan
        public bool g_bAutoScanned = false;

        // Raise Event
        public event EventHandler GetResult;

        //public void Setting_Default()
        //{

        //    byte nGain = 0x04;
        //    byte nOffset = 255;

        //    if (!g_bOpen)
        //    {
        //        txt_info.Text = "The handle is not open.";
        //        return;
        //    }

        //    ushort nRLED = 1200;
        //    ushort nGLED = 1300;
        //    ushort nBLED = 900;
        //    ushort nXLED = 1300;

        //    QuantA6.QuantA6_SetDpi(3);
        //    QuantA6.QuantA6_SetBits(32);
        //    QuantA6.QuantA6_SetThreshold(128);
        //    QuantA6.QuantA6_SetAdcSetting(nGain, nOffset);
        //    QuantA6.QuantA6_SetLEDLength(nRLED, nGLED, nBLED, nXLED);
        //    QuantA6.QuantA6_DefaultSetting();
        //}


        protected void FormScan_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        public void StartScan() {
            //Register QuantumScan Guid for auto connect.
            DevBroadcastDeviceinterface dbi = new DevBroadcastDeviceinterface
            {
                DeviceType = DBT_DEVTYP_DEVICEINTERFACE,
                Reserved = 0,
                ClassGuid = QuantA6.GUID_HSIDCPY,
                Name = 0
            };

            dbi.Size = Marshal.SizeOf(dbi);
            IntPtr buffer = Marshal.AllocHGlobal(dbi.Size);
            Marshal.StructureToPtr(dbi, buffer, true);

            g_notificationHandle = RegisterDeviceNotification(this.Handle, (IntPtr)(&dbi), 0);

            //Device open and auto scan.
            OpenScanner();
        }

        public string ReadMRZ()
        {

            StringBuilder strName = new StringBuilder(200);

            //Read MRZ
            const int nDpi = 3;
            QuantA6.QuantA6_ReadMRZ(nDpi, strName);

            //Show MRZ
            return strName.ToString();
        }

        public void ShowMRZ()
        {
            String MRZ = ReadMRZ();
            if (MRZ != "")
            {
                MessageBox.Show(MRZ);
                //var nameArraySplit = MRZ.Substring(5).Split(new[] { "<" }, StringSplitOptions.RemoveEmptyEntries);
                //MRZ = nameArraySplit.Length >= 2 ? nameArraySplit[2] : nameArraySplit[0].Replace("<", " ");


                //String CardType;
                //CardType = MRZ.Substring(0, 1);
                //String Nationality;
                //Nationality = MRZ.Substring(2, 3);

                //String _namePart;
                //_namePart = MRZ.Substring(5, 39);
                //String[] _nameSplit;
                //_nameSplit = _namePart.Split(new[] { "<" }, StringSplitOptions.RemoveEmptyEntries);


                //String Name;
                //Name = _nameSplit[0];
                //String SurName;
                //SurName = "";
                //String MiddleName;
                //MiddleName = "";

                //if (_nameSplit.Length == 3) {
                //    MiddleName = _nameSplit[1];
                //    SurName = _nameSplit[2];
                //} else {
                //    SurName = _nameSplit[1];
                //}
                //String sencondLine = MRZ.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries)[1];
                ////String 

            }
        }

        protected override void WndProc(ref Message wMessage)
        {
            switch (wMessage.Msg)
            {
                case WM_DEVICECHANGE:
                    {
                        switch (wMessage.WParam.ToInt32())
                        {
                            case DBT_DEVICEARRIVAL: // HSIT-ID600S is connected.
                                unsafe
                                {
                                    DevBroadcastDeviceinterface* pdbcc = (DevBroadcastDeviceinterface*)(wMessage.LParam);
                                    if (pdbcc->ClassGuid == QuantA6.GUID_HSIDCPY)
                                    {
                                        OpenScanner();
                                    }
                                }

                                break;
                            case DBT_DEVICEREMOVECOMPLETE: // HSIT-ID600S is removed.
                                unsafe
                                {
                                    DevBroadcastDeviceinterface* pdbcc = (DevBroadcastDeviceinterface*)(wMessage.LParam);

                                    pdbcc = (DevBroadcastDeviceinterface*)(wMessage.LParam);
                                    if (pdbcc->ClassGuid == QuantA6.GUID_HSIDCPY)
                                    {
                                        CloseScanner();
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;

                case QuantA6.WM_SAVEEND: //Scan and Save complete.
                    {
                        EventHandler handler = GetResult;
                        handler(this, EventArgs.Empty);
                    }
                    break;

                case QuantA6.WM_SCANTIMEOUT: //Scan timeout.

                    break;
                default:
                    break;
            }
            base.WndProc(ref wMessage);
        }

        public void OpenScanner() {
            QuantA6.QuantA6_DeviceClose();
            QuantA6.QuantA6_DeviceOpen(this.Handle);
            //txt_device.Text = "QuantumScan2";
            byte nGain, nOffset;
            //Get ADC
            bool bReturn = QuantA6.QuantA6_GetAdcSetting(&nGain, &nOffset);
            //Get Image Info
            QuantA6.QuantA6_SetDpi(3);
            int nCisPos = QuantA6.QuantA6_GetCISPos();
            if (nCisPos == 1)
            {
                QuantA6.QuantA6_SetMoveCIS();
                Thread.Sleep(1000);
            }

            /////////// Auto Scan /////////////////
            timer2.Interval = 500; //500msec
            timer2.Start();
        }


        public void CloseScanner() {
            timer2.Stop();
            QuantA6.QuantA6_DeviceClose();
        }

        public void ManualScan() {
            Scan();
        }

        private void Scan() {
            string str = FilePath + "\\Scanned"; //+ g_nScanCount.ToString("D3");
            int nResult = QuantA6.QuantA6_Scan(str, 0x00);
        }
        
        private void timer2_Tick(object sender, EventArgs e)
        {
            //if (g_bOpen)  // Device valid check...
            //{
                bool bOn = false;
                bool bSensorLeft, bSensorMid, bSensorRight;

                if (QuantA6.QuantA6_GetSensorValue(&bSensorLeft, &bSensorMid, &bSensorRight) != 0)
                {
                    if (bSensorLeft && bSensorMid && !bSensorRight)
                        bOn = true;
                    else if (bSensorLeft && bSensorMid && bSensorRight)
                        bOn = true;
                    else if (!bSensorLeft && !bSensorMid && !bSensorRight)
                        g_bAutoScanned = false;
                    else
                        bOn = false;
                }
                else
                    bOn = false;


                if (bOn && !g_bAutoScanned)
                {
                    Thread.Sleep(1000);
                    Scan();
                    g_bAutoScanned = true;
                }
        }

       
    }
}
