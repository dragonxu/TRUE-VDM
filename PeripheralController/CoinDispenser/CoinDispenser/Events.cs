using System;
namespace CoinDispenser
{
    /// <summary>
    /// Events Handlers Message
    /// </summary>
    public class Events : EventArgs
    {
        private readonly String _message;
        /// <summary>
        /// setter message
        /// </summary>
        /// <param name="message"></param>
        public Events(String message){
            _message = message;
        }
        /// <summary>
        /// getter message
        /// </summary>
        public String Message
        {
            get { return _message; }
        }
    }
}
