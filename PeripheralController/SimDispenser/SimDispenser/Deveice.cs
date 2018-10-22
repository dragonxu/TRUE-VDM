using System;
using System.IO.Ports;
using System.Threading;
namespace SimDispenser
{
    /// <summary>
    /// Method control Periperal
    /// </summary>
    public class Deveice : SerialPortHelper
    {
        /// <summary>
        /// <para>return success ? true : false</para>
        /// <para>0102 : rotate motor {1st}</para>
        /// <para>0202 : rotate motor {2nd}</para>
        /// <para>0302 : rotate motor {3rd}</para>
        /// <para>0402 : rotate motor {4th}</para>
        /// <para>0502 : rotate motor {5th}</para>
        /// <para>0602 : rotate motor {6th}</para>
        /// <para name="target">parameter : 0102, 0202, 0302, 0402, 0502, 0602</para> 
        /// </summary>
        /// <summary>
        /// <para>Note : target มอเตอร์ที่ต้องการให้หมุน</para>
        /// <para>return success ? true : false</para>
        /// <para>0101 : rotate motor {1st}</para>
        /// <para>0201 : rotate motor {2nd}</para>
        /// <para>0301 : rotate motor {3rd}</para>
        /// <para>0401 : rotate motor {4th}</para>
        /// <para>0501 : rotate motor {5th}</para>
        /// <para>0601 : rotate motor {6th}</para>
        /// <para name="target">parameter : 0101, 0201, 0301, 0401, 0501, 0601</para> 
        /// </summary>
        /// <summary>
        /// <para>Note : target คือ มอเตอรที่ต้องการให้หยุด</para>
        /// <para>return success ? true : false</para>
        /// <para>0100 : stop motor {1st}</para>
        /// <para>0200 : stop motor {2nd}</para>
        /// <para>0300 : stop motor {3rd}</para>
        /// <para>0400 : stop motor {4th}</para>
        /// <para>0500 : stop motor {5th}</para>
        /// <para>0600 : stop motor {6th}</para>
        /// <para name="target">parameter : 0100, 0200, 0300, 0400, 0500, 0600</para> 
        /// </summary>
        public Boolean Rotates(SerialPort _serial, string command)
        {
            byte[] data = { };
            bool state = false;
            try
            {
                if (!_serial.IsOpen){ _serial.Open(); }
                if (_serial.IsOpen)
                {
                    data = ConvertHexToByte(command);
                    _serial.Write(data, 0, data.Length);
                    Thread.Sleep(100);
                    state = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("exception : " + e);
                if (_serial.IsOpen)
                {
                    _serial.Close();
                }
            }
            return state;
        }

        /// <summary>
        /// <para>Note : return -> ค่าที่ได้ค่า sensor เป็น 0 : 1, command -> sensor ที่ต้องการอ่านค่า</para>
        /// <para>21 : read proximity sensor {1st} return 1:on, 0:off</para>
        /// <para>22 : read proximity sensor {2nd} return 1:on, 0:off</para>
        /// <para>23 : read proximity sensor {3rd} return 1:on, 0:off</para>
        /// <para>24 : read proximity sensor {4th} return 1:on, 0:off</para>
        /// <para>25 : read proximity sensor {5th} return 1:on, 0:off</para>
        /// <para>26 : read proximity sensor {6th} return 1:on, 0:off</para>
        /// <para>FF : read proximity sensor all</para>
        /// <para>F0 : Firmware version</para>
        /// <para name="command">parameter : 21, 22, 23, 24, 25, 26, FF, F0</para>
        /// <para>return String </para>
        /// </summary>
        public int Received(SerialPort _serial, string pinIn)
        {
            int result = 1;
            try
            {                
                byte[] data = { };
                if (!_serial.IsOpen)
                {
                    _serial.Open();
                }
                if (_serial.IsOpen)
                {
                    data = ConvertHexToByte(pinIn);
                    _serial.Write(data, 0, data.Length);

                    result = _serial.ReadChar();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("exception : " + e);
            }
            return result;
        }
        /// <summary>
        /// <para>Note : command คือ led ที่ต้องการให้กระพริบ.</para>
        /// <para>4000 : led1 OFF, led2 OFF</para>
        /// <para>4001 : led1 OFF, led2 ON</para>
        /// <para>4002 : led1 ON, led2 OFF</para>
        /// <para>4003 : led1 ON, led2 ON</para>
        /// <para>return success ? true : false</para> 
        /// <para name="command">parameter : 4000, 4001, 4002, 4003</para>
        /// </summary>
        public Boolean Blink(SerialPort _serial, string command)
        {
            byte[] data = { };
            bool state = false;
            try
            {
                if (!_serial.IsOpen) { _serial.Open(); }
                if (_serial.IsOpen)
                {
                    data = ConvertHexToByte(command);
                    _serial.Write(data, 0, data.Length);
                    Thread.Sleep(100);
                    state = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("exception : " + e);
                if (_serial.IsOpen)
                {
                    _serial.Close();
                }
            }
            return state;
        }
    }
}
