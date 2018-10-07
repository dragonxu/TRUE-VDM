using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    /// <summary>
    /// AxisProperties class
    /// </summary>
    public class AxisProperties
    {
        private IPAddress iPAddress;
        private int port;

        /// <summary>
        /// ip address connect PLC
        /// </summary>
        public IPAddress IPAddress
        {
             set { iPAddress = value; }
             get { return iPAddress; }
        }
        /// <summary>
        /// port connect PLC
        /// </summary>
        public int Port
        {
            set { port = value; }
            get { return port; }
        }
    }
}
