using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace Controller
{
    public class Network 
    {
        public class ComputerPr
        {
            protected bool Connection = true;
            public string ComputerName = "Computer Name";
            public string IPAddress = "192.168.1.1";
            public int Port = 11000;
            public List<string> ShareFolder;
            public string Note = "";
            public bool Running = true;
            public ComputerPr()
            {
            }
        }

        public static string MyComputerName
        {
            get
            {
                return Dns.GetHostName();
            }
        }
        public static IPAddress MyAddress
        {
            get
            {
                return Dns.Resolve(Dns.GetHostName()).AddressList[0];
            }
        }

        public static bool SendMessage(string message, IPAddress ipaddress, int port)
        {
            try
            {
                System.Net.Sockets.UdpClient client = new System.Net.Sockets.UdpClient();
                System.Net.IPEndPoint ep = new System.Net.IPEndPoint(ipaddress, port);
                client.Connect(ep);
                byte[] bytCommand = Encoding.ASCII.GetBytes(message);
                client.Send(bytCommand, bytCommand.Length);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static bool PingIP(string ipAddress)
        {
            IPAddress ip = new IPAddress(new byte[] { 0, 0, 0, 0 });
            if (IPAddress.TryParse(ipAddress, out ip) == false)
                return false;
            else
                return PingIP(ip);
        }
        public static bool PingIP(IPAddress ipAddress)
        {
            Ping ping = new Ping();
            PingReply reply;
            try
            { reply = ping.Send(ipAddress, 3000); }
            catch
            { return false; }
            return (reply.Status == IPStatus.Success);
        }
        public static bool PingComName(string ComputerName)
        {
            Ping ping = new Ping();
            PingReply reply = ping.Send(ComputerName, 3000);
            return (reply.Status == IPStatus.Success);
        }
        public static IPAddress[] GetIPByName(string ComputerName)
        {
            try
            {
                IPHostEntry hostInfo = Dns.GetHostEntry(ComputerName);
                return hostInfo.AddressList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string GetIP(string Host)
        {
            try
            {
                IPHostEntry IPE = Dns.GetHostEntry(Host);
                foreach (IPAddress IP in IPE.AddressList)
                {
                    if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        return IP.ToString();
                }
            }
            catch {; }
            return "";
        }
        public static void SendUDP(IPAddress ipaddress, int port, string command)
        {
            UdpClient client = new UdpClient();
            IPEndPoint ep = new IPEndPoint(ipaddress, port);
            client.Connect(ep);
            byte[] bytCommand = Encoding.ASCII.GetBytes(command);
            client.Send(bytCommand, bytCommand.Length);
        }
        public static bool IsIP4Address(string Host)
        {
            if (Host.Split('.').Length != 4 || Host.Length > 23 || Host.ToLower().IndexOf(".com") > 1)
                return false;
            if (Host.Length > 15) return false;
            IPAddress IP;
            return IPAddress.TryParse(Host, out IP);
        }
    }
}
