using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinReciever
{
    /// <summary>
    /// Handlers event response.
    /// </summary>
    public class CoinEvent : EventArgs
    {
        private readonly String _message;
        private CoinState _state;
        /// <summary>
        /// setting status
        /// </summary>
        /// <param name="state">status</param>
        public CoinEvent(CoinState state)
        {
            _state = state;
        }
        /// <summary>
        /// setting message
        /// </summary>
        /// <param name="message"></param>
        public CoinEvent(String message){
            _message = message;
        }
        /// <summary>
        /// getting message
        /// </summary>
        public String Message
        {
            get { return _message; }
        }
        /// <summary>
        /// getting status
        /// </summary>
        public CoinState State
        {
            get { return _state; }
        }
    }
}
