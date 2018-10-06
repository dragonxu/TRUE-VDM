using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Controller
{
    public class TCPTransport  
    {
        private IPEndPoint _endPoint = null;
        private Socket _socket = null;
        private Int32 _timeout = 2000;
        private Ping _ping = null;
        private PingReply _pingReply = null;

        public IPAddress RemoteIP
        {
            get { return this._endPoint.Address; }
            set { this._endPoint.Address = value; }
        }
        public int RemotePort
        {
            get { return this._endPoint.Port; }
            set { this._endPoint.Port = value; }
        }

        public int ReadTimeout
        {
            get { return this._timeout; }
            set { this._timeout = value; }
        }
        public bool IsOpen
        {
            get
            { return (this._socket != null); }
        }
        public bool Connected
        {
            get
            {
                if (this._socket == null)
                    return false;
                else
                    return this._socket.Connected;
            }
        }

        public TCPTransport()
        {
            int back = 0;
            this._ping = new Ping();
            this._endPoint = new IPEndPoint(0, 0);
        }
        
        public bool Open()
        {
            this._socket = new Socket(_endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            this._socket.SendTimeout = this._timeout;
            this._socket.ReceiveTimeout = this._timeout;
            this._socket.Connect(this._endPoint);
            return this.Connected;
        }
        public void Close()
        {
            if (this._socket == null) return;
            if (this.Connected)
            {
                this._socket.Disconnect(false);
                this._socket.Close();
            }
            this._socket = null;
        }
        public void Write(string command)
        {
            this.Write(System.Text.Encoding.ASCII.GetBytes(command), command.Length);
        }
        public void Write(byte[] command)
        {
            this.Write(command, command.Length);
        }
        public void Write(byte[] command, int cmdLen)
        {
            if (!this.Connected)
            {
                throw new Exception("Socket is not connected.");
            }
            int bytesSent = this._socket.Send(command, cmdLen, SocketFlags.None);
            if (bytesSent != cmdLen)
            {
                string msg = string.Format("Sending error. (Expected bytes: {0}  Sent: {1})", cmdLen, bytesSent);
                throw new Exception(msg);
            }
        }
        public int Read(char[] response, int respLen)
        {
            return this.Read(response, 0, respLen);
        }
        public int Read(char[] response, int offset, int respLen)
        {
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(response);
            int retLen = this.Read(bytes, offset, respLen);
            response = System.Text.Encoding.Unicode.GetChars(bytes);
            return retLen;
        }
        public int Read(byte[] response, int respLen)
        {
            return this.Read(response, 0, respLen);
        }
        public int Read(byte[] response, int offset, int respLen)
        {
            if (!this.Connected)
            {
                throw new Exception("Socket is not connected.");
            }
            int bytesRecv = this._socket.Receive(response, offset, respLen, SocketFlags.None);
            if (bytesRecv != respLen)
            {
                string msg = string.Format("Receiving error. (Expected: {0}  Received: {1})", respLen, bytesRecv);
            }
            return bytesRecv;
        }
        //public bool Ping()
        //{
        //    if (this._endPoint.Address == null) return false;
        //    this._pingReply = this._ping.Send(this._endPoint.Address, this._timeout);
        //    return (this._pingReply.Status == IPStatus.Success) ? true : false;
        //}
    }
}