using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Controller
{
    public interface IProtocal
    {
        event EventHandler ConnectionChange;
        int RetryRead { set; }
        bool IsConnected { get; }
        bool OnRead { get; }
        bool OnWrite { get; }
        bool IsOpen { get; }
        void Connect();
        void Disconnect();
    }

    public class BaseProtocal
    {
        protected bool iOnRead = false;
        protected bool iOnWrite = false;
        protected bool iConnected = false;
        protected int iRetryRead = 3;
        protected int iRetryReadCount = 0;
        protected int iConnectotSleep = 1000;
        protected int iRunSleep = 150;
        protected bool SerialIn = false;

        public int RunInterval
        {
            set { this.iRunSleep = value; }
        } 

        protected BackgroundWorker BConnector;
        protected BackgroundWorker BRun;
        protected BackgroundWorker BHome;

        public BaseProtocal()
        {
            this.BConnector = new BackgroundWorker();
            this.BConnector.DoWork += new DoWorkEventHandler(BConnector_DoWork);

            this.BRun = new BackgroundWorker();
            this.BRun.DoWork += new DoWorkEventHandler(BRun_DoWork);
        }

        public void BConnector_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(iConnectotSleep);
        }
        public void BRun_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(iRunSleep);
        }

        public int RetryRead
        {
            get { return this.iRetryRead; }
            set { this.iRetryRead = value; }
        }
        public bool OnRead
        {
            get { return this.iOnRead; }
        }
        public bool OnWrite
        {
            get { return this.iOnWrite; }
        }
    }
}
