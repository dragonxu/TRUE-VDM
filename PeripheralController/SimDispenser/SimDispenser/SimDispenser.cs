using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace SimDispenser
{
    /// <summary>
    /// <para>MCU Devices Library</para>
    /// </summary>
    public class SimDispenser : SerialPortHelper
    {
        private Deveice _device = new Deveice();
        private DeviceInfo info = new DeviceInfo
        {
            /**/
            PinInput1 = "21",
            PinInput2 = "22",
            PinInput3 = "23",
            PinInput4 = "24",
            PinInput5 = "25",
            PinInput6 = "26",
            /**/
            PinOutput1A = "0101",
            PinOutput1B = "0102",
            PinOutput2A = "0201",
            PinOutput2B = "0202",
            PinOutput3A = "0301",
            PinOutput3B = "0302",
            PinOutput4A = "0401",
            PinOutput4B = "0402",
            PinOutput5A = "0501",
            PinOutput5B = "0502",
            PinOutput6A = "0601",
            PinOutput6B = "0602",
            /**/
            PinBreak1 = "0100",
            PinBreak2 = "0200",
            PinBreak3 = "0300",
            PinBreak4 = "0400",
            PinBreak5 = "0500",
            PinBreak6 = "0600",
            /**/
            PinLEDA = "4002",
            PinLEDB = "4001",
            PinLEDOff = "4000"
        };
        private event EventHandler<Events> _event;
        /// <summary>
        /// delegate method handle raise event
        /// </summary>
        /// <param name="e">message event</param>
        protected virtual void ReadSensor(Events e)
        {
            _event?.Invoke(this, e);
        }

        /**Initialzed**/
        private SerialPort _serialPort = new SerialPort();
        /// <summary>
        /// Initial Port
        /// </summary>
        private InitialPort init = new InitialPort();
        /// <summary>
        /// <para>Note : Set Port to MCU device</para> 
        /// </summary>
        public void SetPort(String PortName)
        {
            try
            {
                init.Comport = PortName;
                init.DataBits = 8;
                init.BaudRate = 9600;
                init.DtrEnable = true;

                _serialPort = Initial(init);
            }
            catch(Exception e)
            {
                Console.WriteLine("exception : " + e);
            }
        }
        /// <summary>
        /// GetPort Name
        /// </summary>
        /// <returns>PortName</returns>
        public string GetPort()
        {
            return _serialPort.PortName == null ? "" : _serialPort.PortName;
        }
        /// <summary>
        /// <para>return success ? true : false</para>
        /// <para name="target">parameter : 0102, 0202, 0302, 0402, 0502, 0602</para> 
        /// <para name="target">parameter : 0101, 0201, 0301, 0401, 0501, 0601</para> 
        /// </summary>
        public Boolean Dispenser1(int Timeout)
        {
            bool state = false;
            try
            {
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                }
                if (_serialPort.IsOpen)
                {
                    /*Motor Rotates Forward*/
                    _device.Rotates(_serialPort, info.PinOutput1A);
                    /*Wait sensor active*/
                    var tokenSource = new CancellationTokenSource();
                    var token = tokenSource.Token;

                    var processInformationTask = Task<string>.Factory.StartNew(() =>
                    {
                        while (_device.Received(_serialPort, info.PinInput1) != 1)
                        {
                            if (_device.Received(_serialPort, info.PinInput1) == 1)
                            {
                            // Clean up here, then...
                            Console.WriteLine("break..");
                                Thread.Sleep(500);
                            /*Motor Break*/
                                _device.Rotates(_serialPort, info.PinBreak1);
                                break;
                            }
                            if (token.IsCancellationRequested)
                            {
                            // Clean up here, then...
                            Console.WriteLine("break..");
                                Thread.Sleep(500);
                            /*Motor Break*/
                                _device.Rotates(_serialPort, info.PinBreak1);
                                break;
                            }
                        }
                    /*Motor Break*/
                        Thread.Sleep(500);
                        _device.Rotates(_serialPort, info.PinBreak1);
                    /*Motor Rotates Revers*/
                        Thread.Sleep(500);
                        _device.Rotates(_serialPort, info.PinOutput1B);
                        Thread.Sleep(2500);
                    /*Motor Break*/
                        _device.Rotates(_serialPort, info.PinBreak1);
                        state = true;
                        if (token.IsCancellationRequested)
                        {
                            tokenSource.Cancel();
                        }
                        return "Ran without problems'";

                    }, token);
                    if (!processInformationTask.Wait(Timeout, token))
                    {

                        Console.WriteLine("ProcessInformationTask timed out");
                        tokenSource.Cancel();

                    }
                    if (tokenSource.IsCancellationRequested)
                    {
                        /*Motor Break*/
                        _device.Rotates(_serialPort, info.PinBreak1);
                        Thread.Sleep(500);
                    }
                }
            }
            catch (Exception e)
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
                Console.WriteLine("exception : " + e);
            }
            _serialPort.Close();
            return state;
        }
        /// <summary>
        /// <para>return success ? true : false</para>
        /// <para name="target">parameter : 0102, 0202, 0302, 0402, 0502, 0602</para> 
        /// <para name="target">parameter : 0101, 0201, 0301, 0401, 0501, 0601</para> 
        /// </summary>
        public Boolean Dispenser2(int Timeout)
        {
            bool state = false;
            try
            {
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                }
                if (_serialPort.IsOpen)
                {
                    /*Motor Rotates Forward*/
                    _device.Rotates(_serialPort, info.PinOutput2A);
                    /*Wait sensor active*/
                    var tokenSource = new CancellationTokenSource();
                    var token = tokenSource.Token;

                    var processInformationTask = Task<string>.Factory.StartNew(() =>
                    {
                        while (_device.Received(_serialPort, info.PinInput2) != 1)
                        {
                            if (_device.Received(_serialPort, info.PinInput2) == 1)
                            {
                            // Clean up here, then...
                            Console.WriteLine("break..");
                                Thread.Sleep(500);
                            /*Motor Break*/
                                _device.Rotates(_serialPort, info.PinBreak2);
                                break;
                            }
                            if (token.IsCancellationRequested)
                            {
                            // Clean up here, then...
                            Console.WriteLine("break..");
                                Thread.Sleep(500);
                            /*Motor Break*/
                                _device.Rotates(_serialPort, info.PinBreak2);
                                break;
                            }
                        }
                    /*Motor Break*/
                        Thread.Sleep(500);
                        _device.Rotates(_serialPort, info.PinBreak2);
                    /*Motor Rotates Revers*/
                        Thread.Sleep(500);
                        _device.Rotates(_serialPort, info.PinOutput2B);
                        Thread.Sleep(2500);
                    /*Motor Break*/
                        _device.Rotates(_serialPort, info.PinBreak2);
                        state = true;
                        if (token.IsCancellationRequested)
                        {
                            tokenSource.Cancel();
                        }
                        return "Ran without problems'";

                    }, token);
                    if (!processInformationTask.Wait(Timeout, token))
                    {

                        Console.WriteLine("ProcessInformationTask timed out");
                        tokenSource.Cancel();

                    }
                    if (tokenSource.IsCancellationRequested)
                    {
                        /*Motor Break*/
                        _device.Rotates(_serialPort, info.PinBreak2);
                        Thread.Sleep(500);
                    }
                }
            }
            catch (Exception e)
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
                Console.WriteLine("exception : " + e);
            }
            _serialPort.Close();
            return state;
        }
        /// <summary>
        /// <para>return success ? true : false</para>
        /// <para name="target">parameter : 0102, 0202, 0302, 0402, 0502, 0602</para> 
        /// <para name="target">parameter : 0101, 0201, 0301, 0401, 0501, 0601</para> 
        /// </summary>
        public Boolean Dispenser3(int Timeout)
        {
            bool state = false;
            try
            {
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                }
                if (_serialPort.IsOpen)
                {
                    /*Motor Rotates Forward*/
                    _device.Rotates(_serialPort, info.PinOutput3A);
                    /*Wait sensor active*/
                    var tokenSource = new CancellationTokenSource();
                    var token = tokenSource.Token;

                    var processInformationTask = Task<string>.Factory.StartNew(() =>
                    {
                        while (_device.Received(_serialPort, info.PinInput3) != 1)
                        {
                            if (_device.Received(_serialPort, info.PinInput3) == 1)
                            {
                            // Clean up here, then...
                            Console.WriteLine("break..");
                                Thread.Sleep(500);
                            /*Motor Break*/
                                _device.Rotates(_serialPort, info.PinBreak3);
                                break;
                            }
                            if (token.IsCancellationRequested)
                            {
                            // Clean up here, then...
                            Console.WriteLine("break..");
                                Thread.Sleep(500);
                            /*Motor Break*/
                                _device.Rotates(_serialPort, info.PinBreak3);
                                break;
                            }
                        }
                    /*Motor Break*/
                        Thread.Sleep(500);
                        _device.Rotates(_serialPort, info.PinBreak3);
                    /*Motor Rotates Revers*/
                        Thread.Sleep(500);
                        _device.Rotates(_serialPort, info.PinOutput3B);
                        Thread.Sleep(2500);
                    /*Motor Break*/
                        _device.Rotates(_serialPort, info.PinBreak3);
                        state = true;
                        if (token.IsCancellationRequested)
                        {
                            tokenSource.Cancel();
                        }
                        return "Ran without problems'";

                    }, token);
                    if (!processInformationTask.Wait(Timeout, token))
                    {

                        Console.WriteLine("ProcessInformationTask timed out");
                        tokenSource.Cancel();

                    }
                    if (tokenSource.IsCancellationRequested)
                    {
                        /*Motor Break*/
                        _device.Rotates(_serialPort, info.PinBreak3);
                        Thread.Sleep(500);
                    }
                }
            }
            catch (Exception e)
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
                Console.WriteLine("exception : " + e);
            }
            _serialPort.Close();
            return state;
        }
        /// <summary>
        /// <para>return success ? true : false</para>
        /// <para name="target">parameter : 0102, 0202, 0302, 0402, 0502, 0602</para> 
        /// <para name="target">parameter : 0101, 0201, 0301, 0401, 0501, 0601</para> 
        /// </summary>
        public Boolean Dispenser4(int Timeout)
        {
            bool state = false;
            try
            {
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                }
                if (_serialPort.IsOpen)
                {
                    /*Motor Rotates Forward*/
                    _device.Rotates(_serialPort, info.PinOutput4A);
                    /*Wait sensor active*/
                    var tokenSource = new CancellationTokenSource();
                    var token = tokenSource.Token;

                    var processInformationTask = Task<string>.Factory.StartNew(() =>
                    {
                        while (_device.Received(_serialPort, info.PinInput4) != 1)
                        {
                            if (_device.Received(_serialPort, info.PinInput4) == 1)
                            {
                            // Clean up here, then...
                            Console.WriteLine("break..");
                                Thread.Sleep(500);
                            /*Motor Break*/
                                _device.Rotates(_serialPort, info.PinBreak4);
                                break;
                            }
                            if (token.IsCancellationRequested)
                            {
                            // Clean up here, then...
                            Console.WriteLine("break..");
                                Thread.Sleep(500);
                            /*Motor Break*/
                                _device.Rotates(_serialPort, info.PinBreak4);
                                break;
                            }
                        }
                    /*Motor Break*/
                        Thread.Sleep(500);
                        _device.Rotates(_serialPort, info.PinBreak4);
                    /*Motor Rotates Revers*/
                        Thread.Sleep(500);
                        _device.Rotates(_serialPort, info.PinOutput4B);
                        Thread.Sleep(2500);
                    /*Motor Break*/
                        _device.Rotates(_serialPort, info.PinBreak4);
                        state = true;
                        if (token.IsCancellationRequested)
                        {
                            tokenSource.Cancel();
                        }
                        return "Ran without problems'";

                    }, token);
                    if (!processInformationTask.Wait(Timeout, token))
                    {

                        Console.WriteLine("ProcessInformationTask timed out");
                        tokenSource.Cancel();

                    }
                    if (tokenSource.IsCancellationRequested)
                    {
                        /*Motor Break*/
                        _device.Rotates(_serialPort, info.PinBreak4);
                        Thread.Sleep(500);
                    }
                }
            }
            catch (Exception e)
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
                Console.WriteLine("exception : " + e);
            }
            _serialPort.Close();
            return state;
        }
        /// <summary>
        /// <para>return success ? true : false</para>
        /// <para name="target">parameter : 0102, 0202, 0302, 0402, 0502, 0602</para> 
        /// <para name="target">parameter : 0101, 0201, 0301, 0401, 0501, 0601</para> 
        /// </summary>
        public Boolean Dispenser5(int Timeout)
        {
            bool state = false;
            try
            {
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                }
                if (_serialPort.IsOpen)
                {
                    /*Motor Rotates Forward*/
                    _device.Rotates(_serialPort, info.PinOutput5A);
                    /*Wait sensor active*/
                    var tokenSource = new CancellationTokenSource();
                    var token = tokenSource.Token;

                    var processInformationTask = Task<string>.Factory.StartNew(() =>
                    {
                        while (_device.Received(_serialPort, info.PinInput5) != 1)
                        {
                            if (_device.Received(_serialPort, info.PinInput5) == 1)
                            {
                            // Clean up here, then...
                            Console.WriteLine("break..");
                                Thread.Sleep(500);
                            /*Motor Break*/
                                _device.Rotates(_serialPort, info.PinBreak5);
                                break;
                            }
                            if (token.IsCancellationRequested)
                            {
                            // Clean up here, then...
                            Console.WriteLine("break..");
                                Thread.Sleep(500);
                            /*Motor Break*/
                                _device.Rotates(_serialPort, info.PinBreak5);
                                break;
                            }
                        }
                    /*Motor Break*/
                        Thread.Sleep(500);
                        _device.Rotates(_serialPort, info.PinBreak5);
                    /*Motor Rotates Revers*/
                        Thread.Sleep(500);
                        _device.Rotates(_serialPort, info.PinOutput5B);
                        Thread.Sleep(2500);
                    /*Motor Break*/
                        _device.Rotates(_serialPort, info.PinBreak5);
                        state = true;
                        if (token.IsCancellationRequested)
                        {
                            tokenSource.Cancel();
                        }
                        return "Ran without problems'";

                    }, token);
                    if (!processInformationTask.Wait(Timeout, token))
                    {

                        Console.WriteLine("ProcessInformationTask timed out");
                        tokenSource.Cancel();

                    }
                    if (tokenSource.IsCancellationRequested)
                    {
                        /*Motor Break*/
                        _device.Rotates(_serialPort, info.PinBreak5);
                        Thread.Sleep(500);
                    }
                }
            }
            catch (Exception e)
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
                Console.WriteLine("exception : " + e);
            }
            _serialPort.Close();
            return state;
        }
        /// <summary>
        /// <para>return success ? true : false</para>
        /// <para name="target">rotates : 0601,0602 break : 0600</para> 
        /// </summary>
        public Boolean RotateBack()
        {
            bool state = false;
            try
            {
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                }
                if (_serialPort.IsOpen)
                {
                    _device.Rotates(_serialPort, info.PinOutput6A);
                    Thread.Sleep(100);
                    state = true;
                }
            }
            catch(Exception e)
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
                Console.WriteLine("exception : " + e);
            }
            _serialPort.Close();
            return state;
        }
        public Boolean RotateForward()
        {
            bool state = false;
            try
            {
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                }
                if (_serialPort.IsOpen)
                {
                    _device.Rotates(_serialPort, info.PinOutput6B);
                    Thread.Sleep(100);
                    state = true;
                }
            }
            catch (Exception e)
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
                Console.WriteLine("exception : " + e);
            }
            _serialPort.Close();
            return state;
        }
        public Boolean Break()
        {
            bool state = false;
            try
            {
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                }
                if (_serialPort.IsOpen)
                {
                    _device.Rotates(_serialPort, info.PinBreak6);
                    Thread.Sleep(500);
                    state = true;
                }
            }
            catch (Exception e)
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
                Console.WriteLine("exception : " + e);
            }
            _serialPort.Close();
            return state;
        }
        /*
<para>Note : command คือ led ที่ต้องการให้กระพริบ.</para>
<para>4000 : led1 OFF, led2 OFF</para>
<para>4001 : led1 OFF, led2 ON</para>
<para>4002 : led1 ON, led2 OFF</para>
<para>4003 : led1 ON, led2 ON</para>
<para>return success ? true : false</para> 
<para name="command">parameter : 4000, 4001, 4002, 4003</para>
*/
        /// <summary>
        /// Method Blink LED A
        /// </summary>
        /// <param name="Interval">Timer blink</param>
        /// <returns>Boolean</returns>
        public Boolean BlinkA(int Interval)
        {
            bool state = false;
            try
            {
                 _device.Blink(_serialPort, info.PinLEDA);
                Thread.Sleep(Interval);
                _device.Blink(_serialPort, info.PinLEDOff);
                state = true;
            }
            catch (Exception e)
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
                Console.WriteLine("exception : " + e);
            }
            return state;
        }
        /// <summary>
        /// Method Blink LED B
        /// </summary>
        /// <param name="Interval">Timer blink</param>
        /// <returns>Boolean</returns>
        public Boolean BlinkB(int Interval)
        {
            bool state = false;
            try
            {
                _device.Blink(_serialPort, info.PinLEDB);
                Thread.Sleep(Interval);
                _device.Blink(_serialPort, info.PinLEDOff);
                state = true;
            }
            catch (Exception e)
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
                Console.WriteLine("exception : " + e);
            }
            return state;
        }
    }
}
