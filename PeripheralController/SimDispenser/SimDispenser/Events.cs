using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimDispenser
{
    /// <summary>
    /// <para>Note : handlers message event</para>
    /// </summary>
    public class Events : EventArgs
    {
        private readonly String _message;
        /// <summary>
        /// <para>Note : message setter</para>
        /// </summary>
        public Events(String message)
        {
            _message = message;
        }
        /// <summary>
        /// <para>Note : message getter</para>
        /// </summary>
        public String Message
        {
            get { return _message; }
        }
    }
}
