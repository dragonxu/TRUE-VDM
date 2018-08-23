using System;
using System.IO.Ports;
using System.Threading;

namespace CashDispenser
{
    /// <summary>
    /// Cash Dispenser
    /// </summary>
    public class CashDispenser : SerialPortHelper
    {
        private SerialPort _serialPort = new SerialPort();
        private InitialPort initPort;
        private Status _invoke = Status.Unknown;
        /// <summary>
        /// Connect to Device
        /// </summary>
        /// <returns>Boolean</returns>
        public Boolean Dispense(int qty)
        {
            byte[] data = { };
            bool result = true; 
            try
            {
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                }
                if (_serialPort.IsOpen)
                {
                    do {
                        data = ConvertHexToByte("011000100100");
                        _serialPort.Write(data, 0, data.Length);
                        
                        Thread.Sleep(2800);
                        do {
                            CallbackState();
                            Thread.Sleep(1000);
                        } while (_invoke == Status.Unknown);
                        Console.WriteLine("State : " + _invoke);
                        if (_invoke != Status.Ready
                            && _invoke != Status.Payout_successful
                            && _invoke != Status.Unknown
                            && _invoke != Status.Dispensing_busy)
                        {
                            result = false;
                            break;
                        }
                        qty--;
                                            
                        Console.WriteLine("Remain : "+ qty);
                        
                    } while (qty != 0);
                }
                else
                {
                    _invoke = Status.Disconnected;
                }
                
                Console.WriteLine("Balance Note : " + qty);
            }
            catch (Exception exception)
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
                _invoke = Status.Disconnected;
                Console.WriteLine("exception : " + exception);
            }
            if (_serialPort.IsOpen)
            {
                _serialPort.DiscardOutBuffer();
                _serialPort.DiscardInBuffer();
                _serialPort.Close();
            }
            return result;
        }

        /// <summary>
        /// send command reset device
        /// </summary>
        /// <returns>Boolean</returns>
        public Boolean Reset()
        {
            byte[] data = { };
            bool reset = false;
            try
            {
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                }
                if (_serialPort.IsOpen)
                {
                    data = ConvertHexToByte("011000120000");
                    _serialPort.Write(data, 0, data.Length);
                    Thread.Sleep(100);
                    CallbackState();
                    Thread.Sleep(100);
                }
                else
                {
                    _invoke = Status.Disconnected;
                }
                if (_invoke.Equals(Status.Ready)){
                    reset = true;
                }
            }
            catch (Exception exception)
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
                Console.WriteLine("exception : " + exception);
            }
            if (_serialPort.IsOpen)
            {
                _serialPort.DiscardOutBuffer();
                _serialPort.DiscardInBuffer();
                _serialPort.Close();
            }
                return reset;
        }
        /// <summary>
        /// status device
        /// </summary>
        /// <returns>status</returns>
        public Status CurrentStatus()
        {
            byte[] data = { };
            try
            {
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                }
                if (_serialPort.IsOpen)
                {
                    CallbackState();
                    Console.WriteLine("current status : " + _invoke);
                }
                else
                {
                    _invoke = Status.Disconnected;
                }
            }
            catch(Exception exception)
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
                _invoke = Status.Disconnected;
                Console.WriteLine("exception : " + exception);
            }
            if (_serialPort.IsOpen)
            {
                _serialPort.DiscardOutBuffer();
                _serialPort.DiscardInBuffer();
                _serialPort.Close();
            }
            return _invoke;
        }
        private void CallbackState()
        {
            byte[] data = { };
            try
            {
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                }
                if (_serialPort.IsOpen)
                {
                    do
                    {
                        data = ConvertHexToByte("011000110000");
                        _serialPort.Write(data, 0, data.Length);
                        Thread.Sleep(100);
                        CallState(_serialPort);
                    } while (_invoke == Status.Unknown);
                }
            }
            catch (Exception exception)
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
                _invoke = Status.Disconnected;
                Console.WriteLine("exception : " + exception);
            }
        }
            /// <summary>
            /// setter port name
            /// </summary>
            /// <param name="port">Port Name String</param>
            public void SetPort(string port)
        {
            initPort = new InitialPort();
            initPort.Comport = port;
            initPort.BaudRate = 9600;
            initPort.DataBits = 8;
            initPort.DtrEnable = true;

            _serialPort = Initial(initPort);
        }
        /// <summary>
        /// getter port name
        /// </summary>
        /// <returns>PortName String</returns>
        public string GetPort()
        {
            return _serialPort.PortName == null ? "" : _serialPort.PortName;
        }

        private delegate void getStatus(string data);
        private void StateInfo(string data)
        {
            string value = data.Equals("") ? data.ToUpper() : data.ToUpper().Substring(0, 8);
            switch (value)
            {
                case "01010000":
                    _invoke = Status.Ready;
                    break;
                case "01100010":
                    _invoke = Status.Single_machine_payout;
                    break;
                case "01100113":
                    _invoke = Status.Multiple_machine_payout;
                    break;
                case "010100AA":
                    _invoke = Status.Payout_successful;
                    break;
                case "010100BB":
                    _invoke = Status.Payout_fails;
                    break;
                case "01010001":
                    _invoke = Status.Empty_note;
                    break;
                case "01010002":
                    _invoke = Status.Stock_less;
                    break;
                case "01010003":
                    _invoke = Status.Note_jam;
                    break;
                case "01010004":
                    _invoke = Status.Over_length;
                    break;
                case "01010005":
                    _invoke = Status.Note_Not_Exit;
                    break;
                case "01010006":
                    _invoke = Status.Sensor_Error;
                    break;
                case "01010007":
                    _invoke = Status.Double_note_error;
                    break;
                case "01010008":
                    _invoke = Status.Motor_Error;
                    break;
                case "01010009":
                    _invoke = Status.Dispensing_busy;
                    break;
                case "0101000A":
                    _invoke = Status.Sensor_adjusting;
                    break;
                case "0101000B":
                    _invoke = Status.Checksum_Error;
                    break;
                case "0101000C":
                    _invoke = Status.Low_power_Error;
                    break;
                default:
                    _invoke = Status.Unknown;
                    break;
            }
        }
        private void CallState(SerialPort serialPort)
        {
            byte[] rxBytes = { };
            if (serialPort.IsOpen)
            {
                //serialPort.ReadTimeout = 1000;
                int count = 0;
                count = serialPort.BytesToRead;

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
        }
    }
}
