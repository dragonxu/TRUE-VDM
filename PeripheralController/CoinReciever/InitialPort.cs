using System;

namespace CoinReciever
{
    /// <summary>
    /// Inititalzed serial port
    /// </summary>
    public class InitialPort
    {
        private String comport;
        private int baudrate;
        private int databits;
        private bool dtrenable;
        /// <summary>
        /// setter and getter comport
        /// </summary>
        public String Comport
        {
            set { comport = value; }
            get { return comport; }
        }
        /// <summary>
        /// setter and getter BaudRate
        /// </summary>
        public int BaudRate
        {
            set { baudrate = value; }
            get { return baudrate; }
        }
        /// <summary>
        /// setter and getter DataBits
        /// </summary>
        public int DataBits
        {
            set { databits = value; }
            get { return databits; }
        }
        /// <summary>
        /// setter and getter DtrEnable
        /// </summary>
        public bool DtrEnable
        {
            set { dtrenable = value; }
            get { return dtrenable; }
        }
    }
}
