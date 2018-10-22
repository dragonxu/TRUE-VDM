﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace SimDispenser
{
    /// <summary>
    /// <para>Note : Initialzed Serial Port</para>
    /// </summary>
    public class SerialPortHelper
    {
        /// <summary>
        /// Initial serial port
        /// </summary>
        /// <param name="init">port config class</param>
        /// <returns></returns>
        protected SerialPort Initial(InitialPort init)
        {
            SerialPort _serialport = new SerialPort();
            _serialport.PortName = init.Comport;
            _serialport.BaudRate = init.BaudRate;
            _serialport.DataBits = init.DataBits;
            _serialport.StopBits = StopBits.One;
            _serialport.DtrEnable = init.DtrEnable;
            _serialport.Parity = Parity.Even;
            _serialport.RtsEnable = true;

            return _serialport;
        }
        /// <summary>
        /// convert String binary format to String hex format.
        /// </summary>
        /// <param name="binary">string binary format.</param>
        /// <returns>string hex format</returns>
        protected String ConvertBinaryToHex(String binary)
        {
            StringBuilder result = new StringBuilder(binary.Length / 8 + 1);
            int divide = binary.Length % 8;
            if (divide != 0)
            {
                binary = binary.PadLeft(((binary.Length / 8) + 1) * 8, '0');
            }

            for (int i = 0; i < binary.Length; i += 8)
            {
                string eightBits = binary.Substring(i, 8);
                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            return result.ToString();
        }
        /// <summary>
        /// convert string hex to byte.
        /// </summary>
        /// <param name="hex">string hex format.</param>
        /// <returns>Byte[]</returns>
        protected Byte[] ConvertHexToByte(String hex)
        {
            hex = hex.Replace(" ", "");
            byte[] buffer = new byte[hex.Length / 2];

            for (int index = 0; index < hex.Length; index += 2)
            {
                buffer[index / 2] = (byte)Convert.ToByte(hex.Substring(index, 2), 16);
            }
            return buffer;
        }
        /// <summary>
        /// convert byte data to String hex format.
        /// </summary>
        /// <param name="bytes">data byte</param>
        /// <returns>string hex format.</returns>
        protected String ConvertByteToString(Byte[] bytes)
        {
            StringBuilder stringbuild = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                stringbuild.AppendFormat("{0:x2}", b);
            }
            return stringbuild.ToString();
        }

    }
}
