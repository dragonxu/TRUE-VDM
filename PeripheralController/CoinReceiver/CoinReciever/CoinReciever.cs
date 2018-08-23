using System;
using System.IO.Ports;
using System.Threading;
namespace CoinReciever{
    
    /// <summary>
    /// <para>Note : Coin Reciever Class</para>
    /// </summary>
    public class CoinReciever : SerialPortHelper
    {
        //return state
        private CoinState _invoke = CoinState.Unknown;
        //Acknowledge event
        private delegate string _acknowledge(String e);
        private event _acknowledge _accept;
        private string _message = "";
        /// <summary>
        /// Receive return data from device
        /// </summary>
        /// <param name="e"></param>
        private string OnAccept(string e)
        {
            _message = _accept?.Invoke(e);
            return _message;
        }

        /// <summary>
        /// <para>Note : Declare the event using EventHandle for Coin Accept</para>
        /// </summary>
        public event EventHandler<CoinEvent> Recieved;
        /// <summary>
        /// <para>Note : delegate method handle raise event</para>
        /// </summary>
        protected virtual void OnReveived(CoinEvent e)
        {
            Recieved?.Invoke(this, e);
        }

        private SerialPort _serialPort = new SerialPort();
        private InitialPort initPort;
       
        /// <summary>
        /// setting comport
        /// </summary>
        /// <param name="port">comport name</param>
        public void SetPort(String port)
        {
           initPort = new InitialPort();
           initPort.Comport = port;
           initPort.BaudRate = 9600;
           initPort.DataBits = 8;
           initPort.DtrEnable = true;

           _serialPort = Initial(initPort);
        }
        /// <summary>
        /// getter portname
        /// </summary>
        /// <returns></returns>
        public String GetPort()
        {
            return _serialPort.PortName == null ? "" : _serialPort.PortName;
        }
     
        /// <summary>
        /// <para>Note : send command to devices.</para>
        /// <para>return DevicesInfo</para>
        /// <para>Note : DevicesInfo is java Object</para>
        /// <para>Status Device : 90051103A9</para>
        /// <para>Firmware Version : 900503039B</para>
        /// </summary>
        public CoinState CurrentStatus()
        {
            byte[] data = { };
            try
            {
                _serialPort.DataReceived -= _serialPortDataReceived;
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                }

                if (_serialPort.IsOpen)
                {
                    CallbackState();
                }
            }
            catch (Exception exception)
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
                _invoke = CoinState.Unavailable;
                Console.WriteLine("exception : " + exception);
            }
            Console.WriteLine("current state : " + _invoke);
            _serialPort.DataReceived += _serialPortDataReceived;
            return _invoke;
        }

        /// <summary>
        /// <para>Note : Enabled to devices.</para>
        /// <para>return status</para>
        /// <para>Command Device : 9005010399</para>
        /// </summary>
        public CoinState Open(){
            Byte[] data = { };
            try
            {
                _serialPort.DataReceived -= _serialPortDataReceived;
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                }
                if (_serialPort.IsOpen)
                {
                    //Enable Device
                    data = ConvertHexToByte("9005010399");
                    _serialPort.Write(data, 0, data.Length);

                    Thread.Sleep(100);
                    CallbackState();
                    _accept += CoinInfo;
                    if (_invoke.Equals(CoinState.Ready))
                    {
                        _serialPort.DataReceived += _serialPortDataReceived;
                    }
                }
            }
            catch (Exception exception)
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
                _invoke = CoinState.Unavailable;
                Console.WriteLine("exception : " + exception);
            }
            return _invoke;
        }

        /// <summary>
        /// <para>Note : Disabled to devices.</para>
        /// <para>return success ? true : false</para>
        /// <para>Command Device : 900502039A</para>
        /// </summary>
        public CoinState Close(){
            Byte[] data = { };
            try
            {
                _accept -= CoinInfo;
                _serialPort.DataReceived -= _serialPortDataReceived;
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                }
                if (_serialPort.IsOpen)
                {
                    //Disabled Device
                    data = ConvertHexToByte("900502039A");
                    _serialPort.Write(data, 0, data.Length);

                    Thread.Sleep(100);
                    CallbackState();
                }
                _serialPort.DiscardOutBuffer();
                _serialPort.DiscardInBuffer();
                _serialPort.Close();
            }
            catch (Exception exception)
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
                _invoke = CoinState.Unavailable;
                Console.WriteLine("exception : " + exception);
            }
            return _invoke;
        }

        /// <para>coinIn1A : 9006120103AC</para>
        /// <para>coinIn1B : 9006120603B1</para>
        /// <para>coinIn2A : 9006120203AD</para>
        /// <para>coinIn2B : 9006120503B0</para>
        /// <para>coinIn5 : 9006120303AE</para>
        /// <para>coinIn10 : 9006120403AF</para>
        /// <para>enabled disabled : 90055003E8</para>
        private void _serialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (_serialPort.IsOpen)
            {
               int count = 0;
                SerialPort sp = (SerialPort)sender;
                do
                {
                  count = sp.BytesToRead;
                } while (count < 6) ;
                
                int totBytesRead = 0;
                byte[] data = new byte[count];
                while (totBytesRead < count)
                {
                    int bytesRead = sp.Read(data, 0, count - totBytesRead);
                    totBytesRead += bytesRead;
                }

                string result = OnAccept(ConvertByteToString(data));
                OnReveived(new CoinEvent(result));
            }
        }

        private string CoinInfo(String data)
        {
            string coin = "";
            if (data.ToUpper().Contains("9006120103AC"))
            {
                coin = "1";
            }
            else if (data.ToUpper().Contains("9006120603B1"))
            {
                coin = "1";
            }
            else if(data.ToUpper().Contains("9006120503B0"))
            {
                coin = "2";
            }
            else if(data.ToUpper().Contains("9006120203AD"))
            {
                coin = "2";
            }
            else if(data.ToUpper().Contains("9006120303AE"))
            {
                coin = "5";
            }
            else if(data.ToUpper().Contains("9006120403AF"))
            {
                coin = "10";
            }
            /* switch (data.ToUpper())
             {
                 case "9006120103AC":
                     coin = "1";
                     break;
                 case "9006120603B1":
                     coin = "1";
                     break;
                 case "9006120203AD":
                     coin = "2";
                     break;
                 case "9006120503B0":
                     coin = "2";
                     break;
                 case "9006120303AE":
                     coin = "5";
                     break;
                 case "9006120403AF":
                     coin = "10";
                     break;
                 case "90055003E8":
                     coin = "";
                     break;
                 default:
                     coin = "";
                     break;
             }*/
            return coin;
        }
        private void CallbackState()
        {
            byte[] data = { };
            bool state = false;
            try
            {
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                }

                if (_serialPort.IsOpen)
                {
                    do {
                        data = ConvertHexToByte("90051103A9");
                        _serialPort.Write(data, 0, data.Length);
                        Thread.Sleep(100);
                        state = CallState(_serialPort);
                    } while (state);
                }
            }
            catch (Exception exception)
            {
                _serialPort.Close();
                Console.WriteLine("exception : " + exception);
            }
        }

        private delegate void getStatus(string data);
        private void StateInfo(string data)
        {
            if (data.ToUpper().Contains("90051103A9"))
            {
                _invoke = CoinState.Ready;
            }
            else if (data.ToUpper().Contains("90051403AC"))
            {
                _invoke = CoinState.Unavailable;
            }
            else if(data.ToUpper().Contains("9006160103B0"))
            {
                _invoke = CoinState.Sensor_1_problem;
            }
            else if(data.ToUpper().Contains("9006160203B1"))
            {
                _invoke = CoinState.Sensor_2_problem;
            }
            else if(data.ToUpper().Contains("9006160303B2"))
            {
                _invoke = CoinState.Sensor_3_problem;
            }
            /*switch (data.ToUpper())
            {
                case "90051103A9":
                    _invoke = CoinState.Ready.ToString();
                    break;
                case "90051403AC":
                    _invoke = CoinState.Unavailable.ToString();
                    break;
                case "9006160103B0":
                    _invoke = CoinState.Sensor_1_problem.ToString();
                    break;
                case "9006160203B1":
                    _invoke = CoinState.Sensor_2_problem.ToString();
                    break;
                case "9006160303B2":
                    _invoke = CoinState.Sensor_3_problem.ToString();
                    break;
                default:
                    _invoke = "";
                    break;
            }*/
        }
        private bool CallState(SerialPort serialPort)
        {
            byte[] rxBytes = { };
            bool status = false;
            if (serialPort.IsOpen)
            {
                int count = 0;
                do
                {
                    count = serialPort.BytesToRead;
                } while (count == 0);
                //serialPort.ReadTimeout = 1000;
                if (count > 10) { status = true; }
                    int totBytesRead = 0;
                    rxBytes = new byte[count];
                    while (totBytesRead < count)
                    {
                        int bytesRead = serialPort.Read(rxBytes, 0, count - totBytesRead);
                        totBytesRead += bytesRead;
                    }
                getStatus get = new getStatus(StateInfo);
                get(ConvertByteToString(rxBytes));               
            }
            return status;
        }
    }
}
