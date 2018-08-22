using System;

namespace CashReceiver
{
    /// <summary>
    /// event message object class
    /// </summary>
    public class CashEvent : EventArgs
    {
        private readonly String _message;
        private CashState _state;

        /// <summary>
        /// setter message
        /// </summary>
        /// <param name="message">data message</param>
        public CashEvent(String message){
            _message = message;
        }
        /// <summary>
        /// setter state
        /// </summary>
        /// <param name="state">data state</param>
        public CashEvent(CashState state)
        {
            _state = state;
        }
        /// <summary>
        /// getter message
        /// </summary>
        public String Message
        {
            get { return _message; }
        }
        /// <summary>
        /// getter state
        /// </summary>
        public CashState State
        {
            get { return _state; }
        }
    }
}
