using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Controller
{
    /// <summary>
    /// connected
    /// </summary>
    public class connected : EventArgs
    {
        private bool _connect;
        public connected(bool _connect)
        {
            this._connect = _connect;
        }
        public bool connection
        {
            get { return _connect; }
        }
       
    }
   
    /// <summary>
    /// PLC Component
    /// </summary>
    public class Control
    {
        /// <summary>
        /// <para>Note : Declare the event using EventHandle for Coin Accept</para>
        /// </summary>
        public event EventHandler<connected> Connected;
        /// <summary>
        /// <para>Note : delegate method handle raise event</para>
        /// </summary>
        protected virtual void OnReveived(connected e)
        {
            Connected?.Invoke(this, e);
        }

        public OmronTCP Omron = new OmronTCP(System.Net.TransportType.Tcp);
        private AxisHelper accord = new AxisHelper();
        private AxisProperties props;
        /// <summary>
        /// constructor
        /// </summary>

        private void Omron_ConnectionChange(object sender, EventArgs e)
        {
            OnReveived(new connected(Omron.Connected));
        }
        /// <summary>
        /// setting ip
        /// </summary>
        /// <param name="ip">ip address</param>
        /// <param name="port">port</param>
        public void SetIP(string ip,int port)
        {
            props = new AxisProperties();
            props.IPAddress = IPAddress.Parse(ip);
            props.Port = port;
        }
        /// <summary>
        /// move servo motor to position
        /// </summary>
        /// <param name="_positionx">position x</param>
        /// <param name="_positiony">position y</param>
        /// <returns>state</returns>
        public bool PositionMove(Int16 _position)
        {
            bool state = false;
            try
            {
                Omron_Response_Code Response = Omron_Response_Code.Unknown;
                Omron.Write(0, _position, 804, Omron_Command_Header_Write.CIO_Area_Write);
                Omron.Write(0, _position, 904, Omron_Command_Header_Write.CIO_Area_Write);
                Thread.Sleep(300);
                Omron.WriteRelay(Omron_Command_Header_State_Write.CIO_Write, 4600, 00, true);
                Thread.Sleep(200);
                bool readbit = false;
                bool reply;
                do
                {
                    reply = Omron.ReadRelay(Omron_Command_Header_State_Read.CIO_Read, 4619, 00, ref readbit);
                    Thread.Sleep(200);
                    if (!readbit)
                    {
                        Thread.Sleep(500);
                        state = true;
                        break;
                    }
                    Thread.Sleep(200);
                    Array data = Omron.Read(Omron_Command_Header_Read.CIO_Area_Read, 4206, 1, ref Response, Omron_Data_Type.Integer);
                    int[] d = (int[])data;
                    if (d[0] != 0)
                    {
                        state = false;
                        break;
                    }
                } while (reply);

            }
            catch (Exception e)
            {
                Console.WriteLine("execption : " + e);
            }
            return state;
        }
        /// <summary>
        /// Initial servo[x,y], carrier, basket, gate
        /// </summary>
        public bool HomePosition()
        {
            bool state = false;
            try
            {
                Omron_Response_Code _x = Omron.Write(0, 0, 804, Omron_Command_Header_Write.CIO_Area_Write);
                Omron_Response_Code _y = Omron.Write(0, 0, 904, Omron_Command_Header_Write.CIO_Area_Write);
                Thread.Sleep(200);
                bool result = Omron.WriteRelay(Omron_Command_Header_State_Write.CIO_Write, 4680, 00, true);
                Thread.Sleep(500);
                bool readbit = false;
                bool reply;
                do
                {
                    reply = Omron.ReadRelay(Omron_Command_Header_State_Read.CIO_Read, 4699, 00, ref readbit);
                    Thread.Sleep(2000);
                    if (!readbit)
                    {
                        Thread.Sleep(9000);
                        state = true;
                        break;
                    }
                } while (reply);
            }
            catch(Exception e)
            {
                Console.WriteLine("execption : " + e);
            }
            return state;
        }
        /// <summary>
        /// Connect PLC
        /// get return properties _connected
        /// </summary>        
        public void Connect()
        {
            try
            {
                Omron.Open(props.IPAddress, props.Port);
                Omron.Connect();
            }
            catch(Exception e)
            {
                Console.WriteLine("connect execption : " + e);
                OnReveived(new connected(false));
            }
        }
        /// <summary>
        /// pick-up product --> basket --> open gate
        /// </summary>
        /// <returns>AxisState</returns>
        public bool CarrierPickUp()
        {
            bool state = false;
            bool readbit = false;
            bool reply;
            try
            {
                bool result = Omron.WriteRelay(Omron_Command_Header_State_Write.CIO_Write, 4620, 00, true);
                Omron_Response_Code Response = Omron_Response_Code.Unknown;
                do
                {
                    reply = Omron.ReadRelay(Omron_Command_Header_State_Read.CIO_Read, 4639, 00, ref readbit);
                    if (!readbit)
                    {
                        Thread.Sleep(500);
                        state = true;
                        break;
                    }
                    Thread.Sleep(200);
                    Array data = Omron.Read(Omron_Command_Header_Read.CIO_Area_Read, 4206, 1, ref Response, Omron_Data_Type.Integer);
                    int[] d = (int[])data;
                    if (d[0] != 0)
                    {
                        state = false;
                        break;
                    }
                } while (reply);
            }
            catch(Exception e)
            {
                Console.WriteLine("PickUpProduct execption : " + e);
            }
            return state;
        }
        /// <summary>
        /// basket --> open gate
        /// </summary>
        /// <returns>AxisState</returns>
        public bool BasketPickUp()
        {
            bool state = false;
            bool readbit = false;
            bool reply;
            try
            {
                bool result = Omron.WriteRelay(Omron_Command_Header_State_Write.CIO_Write, 4640, 00, true);
                do
                {
                    reply = Omron.ReadRelay(Omron_Command_Header_State_Read.CIO_Read, 4659, 00, ref readbit);
                    if (!readbit)
                    {
                        Thread.Sleep(500);
                        state = true;
                        break;
                    }
                } while (reply);
            } catch(Exception e)
            {
                Console.WriteLine("PickUpProduct exception : " + e);
            }
            return state;
        }
        /// <summary>
        /// close gate --> basket to home
        /// </summary>
        /// <returns>AxisState</returns>
        public bool CloseGate()
        {
            bool state = true;
            bool readbit = false;
            bool reply;
            try
            {
                Task.Run(() => {
                    bool result = Omron.WriteRelay(Omron_Command_Header_State_Write.CIO_Write, 4660, 00, true);
                    do
                    {
                        reply = Omron.ReadRelay(Omron_Command_Header_State_Read.CIO_Read, 4679, 00, ref readbit);
                        if (!readbit)
                        {
                            Thread.Sleep(500);
                            state = true;
                            break;
                        }
                    } while (result);
                    Console.WriteLine("task state : " + state);
                });                
            }
            catch(Exception e)
            {
                Console.WriteLine("CloseGate : " + e);
            }
            return state;
        }
        /// <summary>
        /// success process
        /// </summary>
        /// <param name="position">position product</param>
        /// <param name="IntervalOpen">interval gate</param>
        /// <returns></returns>
        public bool Process(Int16 position, int IntervalOpen) {
            bool state = false;
            try
            {
                bool move = PositionMove(position);
                Thread.Sleep(500);
                Console.WriteLine("move result : " + move);
                if (move)
                {
                    Thread.Sleep(500);
                    bool carrier = CarrierPickUp();
                    Thread.Sleep(500);
                    Console.WriteLine("move carrier : " + carrier);
                    if (carrier)
                    {
                        Thread.Sleep(500);
                        bool basket = BasketPickUp();
                        Thread.Sleep(500);
                        Console.WriteLine("move basket : " + basket);
                        if (basket)
                        {                            
                            Thread.Sleep(IntervalOpen);
                            bool close = CloseGate();
                            if (close)
                            {
                                Thread.Sleep(200);
                                Console.WriteLine("success.");
                                state = true;
                            }
                        }

                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("execption : "+e);
            }
            return state;
        }
    }
}
