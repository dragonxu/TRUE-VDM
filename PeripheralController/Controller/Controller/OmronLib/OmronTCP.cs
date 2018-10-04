using System;
using System.Net;
using System.Text;

namespace Controller
{
    public class OmronTCP : BaseProtocal
    {
        public event EventHandler ConnectionChange;
        public event EventHandler OnRunTick;

        private byte[] Command_Header_Code = new byte[] { 
              0xFF          //0x00 : Test_Read 
            , 0xFF          //0x01 : Status_Read 
            , 0xFF          //0x02 : Error_Read 
            , 0x80          //0x03 : CIO_Area_Read -
            // 0XB1                : W_Area_Read
            , 0xB2          //0x04 : H_Area_Read -
            , 0xB3          //0x05 : A_Area_Read -
            , 0xFF          //0x06 : L_Area_Read 
            , 0xFF          //0x07 : TC_Status_Read
            , 0x82          //0x08 : DM_Area_Read -
            , 0xFF          //0x09 : FM_Index_Read
            , 0xFF          //0x0A : FM_Data_Read 
            , 0x81          //0x0B : TC_PV_Read -
            , 0xFF          //0x0C : SV_Read1 
            , 0xFF          //0x0D : SV_Read2 
            , 0xFF          //0x0E : SV_Read3
            , 0xFF          //0x0F : Status_Write
            , 0x80          //0x10 : CIO_Area_Write -
            , 0xB2          //0x11 : H_Area_Write -
            , 0xB3          //0x12 : A_Area_Write -
            , 0xFF          //0x13 : L_Area_Write
            , 0xFF          //0x14 : TC_Status_Write
            , 0x82          //0x15 : DM_Area_Write
            , 0xFF          //0x16 : FM_Area_Write
            , 0xFF          //0x17 : TC_PV_Write
            , 0xFF          //0x18 : SV_Write1
            , 0xFF          //0x19 : SV_Write2
            , 0xFF          //0x1A : SV_Write3
            , 0xFF          //0x1B :
            , 0xFF          //0x1C :
            , 0xFF          //0x1D :
            , 0x30          //0x1E : CIO_Bit Read / Write
            , 0x31          //0x1F : WR_Bit Read / Write
            , 0x32          //0x20 : HR_Bit Read / Write
            , 0x33          //0x21 : AR_Bit Read / Write
                
                // 70 = TK_Word
                // 80 = E0_Word
                // 128 = CIO_Word -
                // 129 = T_Word -
                // 130 = D_Word
                // 137 = T_Word
                // 144 = E0_Word
                // 152 = E0_Word
                // 160 = E0_Word
                // 176 = CIO_Word 
                // 177 = W_Word
                // 178 = H_word -
                // 179 = A_Word -
                // 0,1,2,6,7,9,10,32,184,185,186,187 = ??
        };
        private string _lastError = "";
        private TCPTransport _transport = null;//
        private byte[] cmdFS = new byte[] {
			(byte)'F', (byte)'I', (byte)'N', (byte)'S',		    // 'F' 'I' 'N' 'S'
			0x00, 0x00, 0x00, 0x00,		// Expected number of bytes for response
			0x00, 0x00, 0x00, 0x02,		// Command FS  Sending=2 / Receiving=3
			0x00, 0x00, 0x00, 0x00		// Error code
		};
        private byte[] respFS = new byte[] {
			0x00, 0x00, 0x00, 0x00,
			0x00, 0x00, 0x00, 0x00,
			0x00, 0x00, 0x00, 0x00,
			0x00, 0x00, 0x00, 0x00
		};
        private byte[] respFins = new byte[2048];
        private byte[] respFinsData = new byte[2048];
        private ushort finsResponseLen = 0;
        private byte[] cmdFins = new byte[] {
			//---- COMMAND HEADER -------------------------------------------------------
			0x80,				// 00 ICF Information control field 
			0x00,				// 01 RSC Reserved 
			0x03,				// 02 GTC Gateway count
			0x00,				// 03 DNA Destination network address (0=local network)
			0x00,				// 04 DA1 Destination node number
			0x00,				// 05 DA2 Destination unit address
			0x00,				// 06 SNA Source network address (0=local network)
			0x00,				// 07 SA1 Source node number
			0x00,				// 08 SA2 Source unit address
			0x00,				// 09 SID Service ID
			//---- COMMAND --------------------------------------------------------------
			0x00,				// 10 MC Main command
			0x00,				// 11 SC Subcommand
			//---- PARAMS ---------------------------------------------------------------
			0x00, 0x00,	0x00, 0x00,			// 12 reserved area for additional params
			0x00, 0x00,	0x00, 0x00,			// depending on fins command
			0x00, 0x00 };
        private ushort finsCommandLen = 0;
        private const int F_PARAM = 12;

        public char[] Response
        {
            get
            {
                char[] newchar = new char[(this.finsResponseLen - 14) * 2];
                for (int i = 0; i < this.finsResponseLen - 14; i++)
                {
                    newchar[2 * i + 1] = ((this.respFinsData[i] % 16) < 10)
                                            ? (char)((this.respFinsData[i] % 16) + '0')
                                            : (char)((this.respFinsData[i] % 16) + 'A' - 10);
                    newchar[2 * i] = ((this.respFinsData[i] / 16) < 10)
                                        ? (char)((this.respFinsData[i] / 16) + '0')
                                        : (char)((this.respFinsData[i] / 16) + 'A' - 10);
                }
                return newchar;
            }
        }
        private byte ICF
        {
            get { return this.cmdFins[0]; }
            set { this.cmdFins[0] = value; }
        }
        private byte RSC
        {
            get { return this.cmdFins[1]; }
            set { this.cmdFins[1] = value; }
        }
        private byte GTC
        {
            get
            {
                return this.cmdFins[2];
            }
            set
            {
                this.cmdFins[2] = value;
            }
        }
        private byte DNA
        {
            get { return this.cmdFins[3]; }
            set { this.cmdFins[3] = value; }
        }
        private byte DA1
        {
            get { return this.cmdFins[4]; }
            set { this.cmdFins[4] = value; }
        }
        private byte DA2
        {
            get { return this.cmdFins[5]; }
            set { this.cmdFins[5] = value; }
        }
        private byte SNA
        {
            get { return this.cmdFins[6]; }
            set { this.cmdFins[6] = value; }
        }
        private byte SA1
        {
            get { return this.cmdFins[7]; }
            set { this.cmdFins[7] = value; }
        }
        private byte SA2
        {
            get { return this.cmdFins[8]; }
            set { this.cmdFins[8] = value; }
        }
        private byte SID
        {
            get { return this.cmdFins[9]; }
            set { this.cmdFins[9] = value; }
        }
        private byte MC
        {
            get { return this.cmdFins[10]; }
            set { this.cmdFins[10] = value; }
        }
        private byte SC
        {
            get
            {
                return this.cmdFins[11];
            }
            set
            {
                this.cmdFins[11] = value;
            }
        }
        private ushort FS_LEN
        {
            get { return ByteTool.BytesToUshort(this.cmdFS[6], this.cmdFS[7]); }
            set
            {
                this.cmdFS[6] = (Byte)((value >> 8) & 0xFF);
                this.cmdFS[7] = (Byte)(value & 0xFF);
            }
        }
        private ushort FSR_LEN
        {
            get { return ByteTool.BytesToUshort(this.respFS[6], this.respFS[7]); }
        }
        public string FSR_ERR
        {
            get
            {
                return respFS[8].ToString()
                        + respFS[9].ToString()
                        + respFS[10].ToString()
                        + respFS[11].ToString();
            }
        }
        private byte FSR_MER
        {
            get { return respFins[12]; }
        }
        private byte FSR_SER
        {
            get { return respFins[13]; }
        }

        private bool iIsOpen;
        public bool Connected
        {
            get { return this.iConnected; }
            set
            {
                bool change = (this.iConnected != value);
                this.iConnected = value;
                if (change == true)
                {
                    if (this.Connected)
                    {
                        if (ConnectionChange != null)
                            this.ConnectionChange(this, new EventArgs());
                        this.BRun.RunWorkerAsync();
                        this.iRetryReadCount = 0;
                    }
                    else
                    {
                        if (ConnectionChange != null)
                            this.ConnectionChange(this, new EventArgs());
                        this.BConnector.RunWorkerAsync();
                    }
                }
            }
        }
        public string LastError
        {
            get { return this._lastError; }
        }
        public bool IsOpen
        {
            get
            {
                return this.iIsOpen;
            }
        }
        private TCPTransport Transport
        {
            get { return this._transport; }
        }

        public OmronTCP(TransportType TType)
        {
            switch (TType)
            {
                case TransportType.Tcp:
                    this._transport = new TCPTransport();
                    this.SID = 0x08;
                    this.BConnector.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BConnector_RunWorkerCompleted);
                    this.BRun.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BRun_RunWorkerCompleted);
                    break;
                default:
                    throw new Exception("Transport type not defined.");
            }
        }

        public void Open(IPAddress ip, int port)
        {
            this._transport.RemoteIP = ip;
            this._transport.RemotePort = port;
        }
        public void Connect()
        {
            if (this.IsOpen == false)
            {
                try
                {
                    this.iIsOpen = Network.PingIP(this.Transport.RemoteIP);//this._transport.Ping();
                    if (iIsOpen)
                    {
                        this.iConnectotSleep = 100;
                        this.BConnector.RunWorkerAsync();

                    }
                    else
                    {
                        this.Connected = false;
                        this.iConnectotSleep = 1000;
                        this.BConnector.RunWorkerAsync();
                    }
                }
                catch (Exception ex)
                {
                    this.Connected = false;
                    this._lastError = ex.Message;
                    this.iConnectotSleep = 1000;
                    this.BConnector.RunWorkerAsync();
                }
            }
            else
            {
                bool conncet = this._transport.Open();
                if (conncet == true)
                {
                    this.NodeAddressDataSend();
                    this.Connected = true;
                }
                else
                {
                    this.Connected = false;
                    this.iConnectotSleep = 1000;
                    this.BConnector.RunWorkerAsync();
                }
            }
        }
        public void Disconnect()
        {
            if (this._transport == null)
                return;
            this.Connected = false;
            this.iIsOpen = false;
            this._transport.Close();
        }

        private void BConnector_RunWorkerCompleted(object sender, EventArgs e)
        {
            this.Connect();
        }
        private void BRun_RunWorkerCompleted(object sender, EventArgs e)
        {
            if (this.Connected == false)
                return;
            if (this.OnRead == true)
            {
                this.BRun.RunWorkerAsync();
                return;
            }
            try
            {
                if (OnRunTick != null)
                    OnRunTick(this, new EventArgs());
                this.BRun.RunWorkerAsync();
            }
            catch
            {
                this.iRetryReadCount++;
                if (this.iRetryReadCount >= this.iRetryRead)
                    this.Connected = false;
                else
                    this.BRun.RunWorkerAsync();
            }
        }

        public Array Read(Omron_Command_Header_Read memory_area, int start_address, int count, ref Omron_Response_Code p_response, Omron_Data_Type return_type)
        {
            return (Read(memory_area, start_address, 0, count, ref p_response, return_type));
        }
        private Array Read(Omron_Command_Header_Read memory_area, int start_address, byte bit_position, int count, ref Omron_Response_Code p_response, Omron_Data_Type return_type)
        {
            bool res = ReadMemoryArea(Command_Header_Code[(byte)memory_area], start_address, bit_position, count);
            if (res == true)
            {
                p_response = Omron_Response_Code.Command_Completed_Normally;
                this.iRetryReadCount = 0;
                if (return_type == Omron_Data_Type.Integer)
                {
                    int[] hexint = new int[count];
                    for (int i = 0; i < count; i++)
                    {
                        string hexinstr = "";
                        for (int j = 0; j < 4; j++)
                        {
                            hexinstr += Response[i * 4 + j];
                        }
                        hexint[i] = (int)new Hex(hexinstr).ToUInt;
                    }
                    return hexint;
                }
                else if (return_type == Omron_Data_Type.Char)
                {
                    return this.Response;
                }
                else if (return_type == Omron_Data_Type.Hex)
                {
                    Hex[] hexarr = new Hex[count];
                    for (int i = 0; i < count; i++)
                    {
                        string hexinstr = "";
                        for (int j = 0; j < 4; j++)
                        {
                            hexinstr += Response[i * 4 + j];
                        }
                        hexarr[i] = new Hex(hexinstr);
                    }
                    return hexarr;
                }
                else
                {
                    throw new Exception("This Omron data type not supply");
                }
            }
            else
            {
                p_response = Omron_Response_Code.Unknown;
                this.iRetryReadCount++;
                if (this.iRetryReadCount > iRetryRead)
                    this.Connected = false;
                return null;
            }
        }
        public bool ReadRelay(Omron_Command_Header_State_Read memory_area, int start_address, byte BitAddress, ref bool RelayState)
        {
            bool res = ReadMemoryArea(Command_Header_Code[(byte)memory_area], start_address, BitAddress, 1);
            if (res)
                RelayState = byte.Parse(this.Response[1].ToString()) > 0;
            return res;
        }
        private bool ReadMemoryArea(byte area, int address, byte bit_position, int count)
        {
            try
            {
                if (area == 0xFF)
                    throw new Exception("this area command not supply");
                // command & subcomand
                this.MC = 0x01;
                this.SC = 0x01;
                // memory area
                this.cmdFins[F_PARAM] = area;
                // address
                this.cmdFins[F_PARAM + 1] = (Byte)((address >> 8) & 0xFF);
                this.cmdFins[F_PARAM + 2] = (Byte)(address & 0xFF);
                // no bit position
                this.cmdFins[F_PARAM + 3] = bit_position;
                // count items
                this.cmdFins[F_PARAM + 4] = (Byte)((count >> 8) & 0xFF);
                this.cmdFins[F_PARAM + 5] = (Byte)(count & 0xFF);
                // set command lenght (12 + additional params)
                this.finsCommandLen = 18;
                // send the message
                return FrameSendRead(null);
            }
            catch (Exception ex)
            {
                this._lastError = ex.Message;
                return false;
            }
        }

        public Omron_Response_Code Write(int unit_add, short value, int start_add, Omron_Command_Header_Write memory_area)
        {
            short[] newValue = new short[] { value };
            return Write(unit_add, newValue, start_add, 1, memory_area);
        }
        public Omron_Response_Code Write(int unit_add, short[] value, int start_add, int Length, Omron_Command_Header_Write memory_area)
        {
            if (value == null)
                return Omron_Response_Code.Frame_Length_Err;
            if (value.Length == 0)
                return Omron_Response_Code.Frame_Length_Err;

            byte[] arr_value = new byte[value.Length * 2];
            for (int i = 0; i < value.Length; i++)
            {
                arr_value[i * 2] = (byte)(value[i] / 255);
                arr_value[i * 2 + 1] = (byte)(value[i] % 256);
            }
            this.WriteMemoryArea(Command_Header_Code[(byte)memory_area], start_add, 0, Length, ref arr_value);
            return Omron_Response_Code.Unknown;
        }
        public bool WriteRelay(Omron_Command_Header_State_Write memory_area, int address, byte bit_position, bool data)
        {
            byte[] bitdata = new byte[1] { (data == true) ? (byte)1 : (byte)0 };
            return WriteMemoryArea(Command_Header_Code[(byte)memory_area], address, bit_position, 1, ref bitdata);
        }
        public bool WriteRelay(Omron_Command_Header_State_Write memory_area, int address, byte bit_position, int count, byte[] data)
        {
            return WriteMemoryArea(Command_Header_Code[(byte)memory_area], address, bit_position, count, ref data);
        }
        private bool WriteMemoryArea(byte area, int address, byte bit_position, int count, ref byte[] data)
        {
            try
            {
                if (area == 0xFF)
                    throw new Exception("this area command not supply");
                // command & subcomand
                this.MC = 0x01;
                this.SC = 0x02;
                // memory area
                this.cmdFins[F_PARAM] = area;
                // address
                this.cmdFins[F_PARAM + 1] = (Byte)((address >> 8) & 0xFF);
                this.cmdFins[F_PARAM + 2] = (Byte)(address & 0xFF);
                // bit position
                this.cmdFins[F_PARAM + 3] = bit_position;
                // count items
                this.cmdFins[F_PARAM + 4] = (Byte)((count >> 8) & 0xFF);
                this.cmdFins[F_PARAM + 5] = (Byte)(count & 0xFF);
                // set command lenght (12 + additional params)
                this.finsCommandLen = 18;
                // send the message
                return FrameSendWrite(data);
            }
            catch (Exception ex)
            {
                this._lastError = ex.Message;
                return false;
            }
        }

        public bool ConnectionDataRead(Byte area)
        {
            try
            {
                // command & subcomand
                this.MC = 0x05;
                this.SC = 0x01;
                // memory area
                this.cmdFins[F_PARAM] = (Byte)area;
                // set command lenght (12 + additional params)
                this.finsCommandLen = 13;
                return FrameSendRead(null);
            }
            catch (Exception ex)
            {
                this._lastError = ex.Message;
                return false;
            }
        }
        private bool NodeAddressDataSend()
        {
            byte[] cmdNADS = new byte[] 
			{
			0x46, 0x49, 0x4E, 0x53, // 'F' 'I' 'N' 'S'
			0x00, 0x00, 0x00, 0x0C,	// 12 Bytes expected
			0x00, 0x00, 0x00, 0x00,	// NADS Command (0 Client to server, 1 server to client)
			0x00, 0x00, 0x00, 0x00,	// Error code (Not used)
			0x00, 0x00, 0x00, 0x00	// Client node address, 0 = auto assigned
			};
            // send NADS command
            this.Transport.Write(cmdNADS, cmdNADS.Length);
            // wait for a plc response
            byte[] respNADS = new byte[24];
            this.Transport.Read(respNADS, respNADS.Length);
            // checks response error
            if (respNADS[15] != 0)
            {
                this._lastError = "NASD command error: " + respNADS[15];
                this.Disconnect();
                return false;
            }
            // checking header error
            if (respNADS[8] != 0 || respNADS[9] != 0 || respNADS[10] != 0 || respNADS[11] != 1)
            {
                this._lastError = "Error sending NADS command. "
                                    + respNADS[8].ToString() + " "
                                    + respNADS[9].ToString() + " "
                                    + respNADS[10].ToString() + " "
                                    + respNADS[11].ToString();
                Disconnect();
                return false;
            }
            // save the client & server node in the FINS command for all next conversations
            this.DA1 = respNADS[23];
            this.SA1 = respNADS[19];
            return true;
        }
        private bool FrameSendWrite(Byte[] data)
        {
            // clear FS response buffer
            for (int x = 0; x < this.respFS.Length; respFS[x++] = 0) ;
            // data lenght plus 8 bytes (4 bytes for command & 4 bytes for error)
            int fsLen = this.finsCommandLen + 8;
            if (data != null)
                fsLen += data.Length;
            // set length [6]+[7]
            this.FS_LEN = (ushort)fsLen;
            // send frame header
            this.Transport.Write(cmdFS, cmdFS.Length);
            // send FINS command
            this.Transport.Write(this.cmdFins, this.finsCommandLen);
            // send additional data
            if (data != null)
                this.Transport.Write(data, data.Length);
            // frame response
            this.Transport.Read(this.respFS, this.respFS.Length);
            // check frame error [8]+[9]+[10]+[11]
            if (this.FSR_ERR != "0002")
            {
                this._lastError = "FRAME SEND error: " + this.FSR_ERR;
                return false;
            }
            // checks response error
            if (this.respFS[15] != 0)
            {
                this._lastError = "Error receving FS command: " + this.respFS[15];
                return false;
            }
            // calculate the expedted response lenght
            //
            // 16 bits word ([6] + [7])
            // substract the additional 8 bytes
            this.finsResponseLen = this.FSR_LEN;
            this.finsResponseLen -= 8;
            // fins command response
            this.Transport.Read(respFins, 14);
            if (finsResponseLen > 14)
            {
                // fins command response data
                this.Transport.Read(respFinsData, finsResponseLen - 14);
            }
            return true;
        }

        private bool FrameSendRead(Byte[] data)
        {
            // clear FS response buffer
            for (int x = 0; x < this.respFS.Length; respFS[x++] = 0) ;
            // add data 
            if (data != null)
                for (int i = 0; i < data.Length; i++)
                {
                    this.cmdFins[18 + i] = data[i];
                    this.finsCommandLen++;
                }
            // data lenght plus 8 bytes (4 bytes for command & 4 bytes for error)
            int fsLen = this.finsCommandLen + 8;
            //if (data != null)
            //    fsLen += data.Length;
            // set length [6]+[7]
            this.FS_LEN = (ushort)fsLen;
            // send frame header
            this.Transport.Write(cmdFS, cmdFS.Length);
            // send FINS command
            this.Transport.Write(this.cmdFins, this.finsCommandLen);
            // send additional data
            //if (data != null)
            //    this.Transport.Send(ref data, data.Length);
            // frame response
            this.Transport.Read(this.respFS, this.respFS.Length);
            // check frame error [8]+[9]+[10]+[11]
            if (this.FSR_ERR != "0002")
            {
                this._lastError = "FRAME SEND error: " + this.FSR_ERR;
                return false;
            }
            // checks response error
            if (this.respFS[15] != 0)
            {
                this._lastError = "Error receving FS command: " + this.respFS[15];
                return false;
            }
            // calculate the expedted response lenght
            //
            // 16 bits word ([6] + [7])
            // substract the additional 8 bytes
            this.finsResponseLen = this.FSR_LEN;
            this.finsResponseLen -= 8;
            // fins command response
            this.Transport.Read(respFins, 14);
            if (finsResponseLen > 14)
            {
                // fins command response data
                this.Transport.Read(respFinsData, finsResponseLen - 14);
            }

            // check response code
            //
            //if (this.FSR_MER != 0 || this.FSR_SER != 0)
            //{
            //    this._lastError += string.Format("Response Code error: (Code: {0}  Subcode: {1})",
            //                                        this.FSR_MER, this.FSR_SER);
            //    return false;
            //}
            return true;
        }
        public string LastDialog(string Caption)
        {
            StringBuilder dialog = new StringBuilder(Caption + Environment.NewLine);
            dialog.Append(Environment.NewLine);
            dialog.Append("FS HEADER" + Environment.NewLine);
            dialog.Append(BitConverter.ToString(this.cmdFS) + Environment.NewLine);
            dialog.Append("FINS COMMAND" + Environment.NewLine);
            dialog.Append(BitConverter.ToString(this.cmdFins, 0, finsCommandLen) + Environment.NewLine);
            dialog.Append("FS RESPONSE" + Environment.NewLine);
            dialog.Append(BitConverter.ToString(this.respFS) + Environment.NewLine);
            dialog.Append("FINS RESPONSE" + Environment.NewLine);
            dialog.Append(BitConverter.ToString(this.respFins, 0, 14) + Environment.NewLine);
            dialog.Append("FINS DATA" + Environment.NewLine);
            dialog.Append(BitConverter.ToString(this.respFinsData, 0, finsResponseLen - 14) + Environment.NewLine);
            dialog.Append("Last error: " + this._lastError + Environment.NewLine);
            dialog.Append(Environment.NewLine);

            return dialog.ToString();
        }
        public bool ClearPassword(byte[] password)
        {
            // command & subcomand
            this.MC = 0x03;
            this.SC = 0x05;
            this.cmdFins[F_PARAM] = 0xFF;
            this.cmdFins[F_PARAM + 1] = 0xFF;
            this.cmdFins[F_PARAM + 2] = 0x00;

            this.cmdFins[F_PARAM + 3] = password.Length > 0 ? password[0] : (byte)0x00;
            this.cmdFins[F_PARAM + 4] = password.Length > 1 ? password[1] : (byte)0x00;
            this.cmdFins[F_PARAM + 5] = password.Length > 2 ? password[2] : (byte)0x00;
            this.cmdFins[F_PARAM + 6] = password.Length > 3 ? password[3] : (byte)0x00;
            this.cmdFins[F_PARAM + 7] = password.Length > 4 ? password[4] : (byte)0x00;
            this.cmdFins[F_PARAM + 8] = password.Length > 5 ? password[5] : (byte)0x00;
            this.cmdFins[F_PARAM + 9] = password.Length > 6 ? password[6] : (byte)0x00;
            this.cmdFins[F_PARAM + 10] = password.Length > 7 ? password[7] : (byte)0x00;

            this.finsCommandLen = 23;
            return FrameSendRead(null);
        }

        public class NumbericData
        {
            private byte iByteValue = 0;
            private ushort iShortValue = 0;
            private uint iIntValue = 0;
            private ulong iLongValue = 0;
            private char[] iCharValue;
            private string iStringValue = "";
            private string iHEXValue = "";
            private string iBinaryValue = "";

            public NumbericData()
            {
            }
            public byte ByteValue
            {
                get { return iByteValue; }
                set
                {
                    this.iByteValue = (byte)value;
                    this.iShortValue = (ushort)value;
                    this.iIntValue = (uint)value;
                    this.iLongValue = (ulong)value;
                    this.iCharValue = new char[] { (char)value };
                    this.iHEXValue = String.Format("{0:x2}", value).ToUpper();
                    this.iBinaryValue = Convert.ToString(value, 2);
                    this.iStringValue = "";
                    for (int i = 0; i < iCharValue.Length; i++)
                        if (char.IsLetterOrDigit(this.CharValue[i]))
                            this.iStringValue += this.iCharValue[i].ToString().Trim();
                }
            }
            public ushort ShortValue
            {
                get { return this.iShortValue; }
                set
                {
                    this.iByteValue = (byte)value;
                    this.iShortValue = (ushort)value;
                    this.iIntValue = (uint)value;
                    this.iLongValue = (ulong)value;
                    byte[] h = new Hex((uint)value, 2).ToBytes;
                    char[] c = new char[2];
                    for (int i = 0; i < 2; i++)
                        c[i] = (char)h[i];
                    this.iCharValue = c;
                    this.iHEXValue = String.Format("{0:x2}", value).ToUpper();
                    this.iBinaryValue = Convert.ToString(value, 2);
                    this.iStringValue = "";
                    for (int i = 0; i < iCharValue.Length; i++)
                        if (char.IsLetterOrDigit(this.CharValue[i]))
                            this.iStringValue += this.iCharValue[i].ToString().Trim();
                }
            }
            public uint IntValue
            {
                get { return this.iIntValue; }
                set
                {
                    this.iByteValue = (byte)value;
                    this.iShortValue = (ushort)value;
                    this.iIntValue = (uint)value;
                    this.iLongValue = (ulong)value;
                    byte[] h = new Hex((uint)value, 4).ToBytes;
                    char[] c = new char[4];
                    for (int i = 0; i < 4; i++)
                        c[i] = (char)h[i];
                    this.iCharValue = c;
                    this.iHEXValue = String.Format("{0:x2}", value).ToUpper();
                    this.iBinaryValue = Convert.ToString(value, 2);
                    this.iStringValue = "";
                    for (int i = 0; i < iCharValue.Length; i++)
                        if (char.IsLetterOrDigit(this.CharValue[i]))
                            this.iStringValue += this.iCharValue[i].ToString().Trim();
                }
            }
            public ulong LongValue
            {
                get { return this.iLongValue; }
                set
                {
                    this.iByteValue = (byte)value;
                    this.iShortValue = (ushort)value;
                    this.iIntValue = (uint)value;
                    this.iLongValue = (ulong)value;
                    byte[] h = new Hex((uint)value, 8).ToBytes;
                    char[] c = new char[8];
                    for (int i = 0; i < 8; i++)
                        c[i] = (char)h[i];
                    this.iCharValue = c;
                    this.iHEXValue = String.Format("{0:x2}", value).ToUpper();
                    this.iBinaryValue = new Bits(new Hex(iHEXValue).ToBytes).ToString();
                    this.iStringValue = "";
                    for (int i = 0; i < iCharValue.Length; i++)
                        if (char.IsLetterOrDigit(this.CharValue[i]))
                            this.iStringValue += this.iCharValue[i].ToString().Trim();
                }
            }
            public char[] CharValue
            {
                get { return this.iCharValue; }
                set
                {
                    this.iCharValue = value;
                    for (int i = 0; i < value.Length; i++)
                    {
                        iLongValue <<= 8;
                        iLongValue += (byte)iCharValue[i];
                    }
                    this.iByteValue = (byte)iLongValue;
                    this.iShortValue = (ushort)iLongValue;
                    this.iIntValue = (uint)iLongValue;
                    this.iHEXValue = String.Format("{0:x2}", iLongValue).ToUpper();
                    this.iBinaryValue = new Bits(new Hex(iHEXValue).ToBytes).ToString();
                    this.iStringValue = "";
                    for (int i = 0; i < iCharValue.Length; i++)
                        if (char.IsLetterOrDigit(this.CharValue[i]))
                            this.iStringValue += this.iCharValue[i].ToString().Trim();
                }
            }
            public string StringValue
            {
                get { return this.iStringValue; }
                set
                {

                    this.iCharValue = value.ToCharArray();
                    iLongValue = 0;
                    for (int i = 0; i < value.Length; i++)
                    {
                        iLongValue <<= 8;
                        iLongValue += (byte)iCharValue[i];
                    }
                    this.iByteValue = (byte)iLongValue;
                    this.iShortValue = (ushort)iLongValue;
                    this.iIntValue = (uint)iLongValue;
                    this.iHEXValue = String.Format("{0:x2}", iLongValue).ToUpper();
                    this.iBinaryValue = new Bits(new Hex(iHEXValue).ToBytes).ToString();
                    this.iStringValue = value;
                }
            }
            public string HEXValue
            {
                get { return this.iHEXValue; }
                set
                {
                    this.iByteValue = (byte)System.Convert.ToUInt32(value, 16);
                    this.iShortValue = (ushort)System.Convert.ToUInt32(value, 16);
                    this.iIntValue = (uint)System.Convert.ToUInt32(value, 16);
                    this.iLongValue = (ulong)System.Convert.ToUInt32(value, 16);
                    this.iHEXValue = value;
                    this.iBinaryValue = Convert.ToString((uint)iLongValue, 2);
                    for (int i = iBinaryValue.Length; i < iHEXValue.Length * 4; i++)
                        this.iBinaryValue = "0" + iBinaryValue;
                    byte[] h = new Hex((uint)iLongValue, 8).ToBytes;
                    char[] c = new char[8];
                    for (int i = 0; i < 8; i++)
                        c[i] = (char)h[i];
                    this.iCharValue = c;
                    this.iStringValue = "";
                    for (int i = 0; i < iCharValue.Length; i++)
                        if (char.IsLetterOrDigit(this.CharValue[i]))
                            this.iStringValue += this.iCharValue[i].ToString().Trim();
                }
            }
            //public string BinaryValue
            //{
            //    get { return this.iBinaryValue; }
            //    set
            //    {
            //        this.iByteValue = (byte)System.Convert.ToUInt32(value, 2);
            //        this.iShortValue = (ushort)System.Convert.ToUInt32(value, 2);
            //        this.iIntValue = (uint)System.Convert.ToUInt32(value, 2);
            //        this.iLongValue = (ulong)System.Convert.ToUInt32(value, 2);
            //        this.iBinaryValue = new Bits(value).ToString();
            //        this.iHEXValue = Convert.ToString((uint)iLongValue, 16);
            //        byte[] h = new Hex((uint)iLongValue, 8).ToBytes();
            //        char[] c = new char[8];
            //        for (int i = 0; i < 8; i++)
            //            c[i] = (char)h[i];
            //        this.iCharValue = c;
            //        this.iStringValue = "";
            //        for (int i = 0; i < iCharValue.Length; i++)
            //            if (char.IsLetterOrDigit(this.CharValue[i]))
            //                this.iStringValue += this.iCharValue[i].ToString().Trim();
            //    }
            //}

            public Bits Bit
            {
                get { return new Bits(this.iIntValue, this.iBinaryValue.Length); }
            }
        }
        public class Hex
        {
            byte[] bytes;
            public int Count
            {
                get
                {
                    if (bytes == null)
                        return 0;
                    else
                        return bytes.Length;
                }
            }

            public Hex()
            {
                bytes = null;
            }
            public Hex(Hex hexData)
            {
                bytes = hexData.ToBytes;
            }
            public Hex(string data)
            {
                bytes = this.ParseByteString(data);
            }
            public Hex(byte[] byteArray)
            {
                bytes = this.CopyBytes(byteArray);
            }
            public Hex(uint data, int numBytes)
            {
                string format = String.Format("X{0}", numBytes * 2);
                string hexString = data.ToString(format);
                bytes = this.ParseByteString(hexString);
            }
            public Hex(int data, int numBytes)
            {
                string format = String.Format("X{0}", numBytes * 2);
                if (data < 0)
                    data += (int)Math.Pow(2, numBytes * 8);
                string hexString = data.ToString(format);
                bytes = this.ParseByteString(hexString);
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                if (bytes != null && bytes.Length > 0)
                {
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        sb.Append(bytes[i].ToString("X2"));
                    }
                }
                return sb.ToString();
            }
            public uint ToUInt
            {
                get
                {
                    return new Bits(this.ToBytes).ToUInt();
                }
            }
            public int ToInt
            {
                get
                {
                    return new Bits(this.ToBytes).ToInt();
                }
            }
            public byte[] ToBytes
            {
                get
                {
                    byte[] newBytes = null;
                    if (bytes != null && bytes.Length > 0)
                    {
                        newBytes = new byte[bytes.Length];
                        for (int i = 0; i < bytes.Length; i++)
                            newBytes[i] = bytes[i];
                    }
                    return bytes;
                }
            }
            private byte[] ParseByteString(string data)
            {
                byte[] byteArray = null;
                int byteCount = 0;
                if (data != null && data.Length > 0)
                {
                    if (data.Length % 2 != 0)  //make sure we have an even number of characters
                        data = "0" + data;
                    byteCount = data.Length / 2;
                    byteArray = new byte[byteCount];
                    string sTemp;
                    for (int i = 0; i < byteCount; i++)
                    {
                        sTemp = data.Substring(i * 2, 2);
                        byteArray[i] = Convert.ToByte(sTemp, 16);
                    }
                }
                return byteArray;
            }
            private byte[] CopyBytes(byte[] byteArray)
            {
                byte[] newByteArray = null;
                int numberOfBytes = byteArray.Length;
                if (numberOfBytes > 0)
                {
                    newByteArray = new byte[numberOfBytes];
                    for (int i = 0; i < newByteArray.Length; i++)
                    {
                        newByteArray[i] = byteArray[i];
                    }
                }
                return newByteArray;
            }
        }
        public class Bits
        {
            bool[] _bitArray;
            public int Count
            {
                get
                {
                    if (_bitArray == null)
                        return 0;
                    else
                        return _bitArray.Length;
                }
            }
            private Bits()
            {
            }
            public Bits(byte[] byteArray)
            {
                if (byteArray.Length > 0)
                    createArray(byteArray);
                else
                    throw new ApplicationException("Can't convert to bits a zero length array of bytes");
            }
            public Bits(uint data, int numBits)
            {
                if (numBits == 0)
                    throw new ApplicationException("Can't initialize bits when number of bits is 0");
                _bitArray = new bool[numBits];

                int bitIndex = 0;
                uint mask = Convert.ToUInt32(Math.Pow(2, numBits - 1));
                for (int maskIndex = numBits - 1; maskIndex >= 0; maskIndex--)
                {
                    if ((data & mask) > 0)
                        _bitArray[bitIndex] = true;
                    else
                        _bitArray[bitIndex] = false;
                    bitIndex++;
                    mask >>= 1;
                }
            }
            public Bits(int data, int numBits)
            {
                if (numBits == 0)
                    throw new ApplicationException("Can't initialize bits when number of bits is 0");

                if (data < 0)
                    data += Convert.ToInt32(System.Math.Pow(2, numBits));

                _bitArray = new bool[numBits];

                int bitIndex = 0;
                int mask = Convert.ToInt32(Math.Pow(2, numBits - 1));
                for (int maskIndex = numBits - 1; maskIndex >= 0; maskIndex--)
                {
                    if ((data & mask) > 0)
                        _bitArray[bitIndex] = true;
                    else
                        _bitArray[bitIndex] = false;
                    bitIndex++;
                    mask >>= 1;
                }
            }
            public Bits(Hex hexData)
            {
                createArray(hexData.ToBytes);
            }
            public Bits(string data)
            {
                if (ValidBitString(data))
                {
                    _bitArray = new bool[data.Length];
                    for (int i = 0; i < data.Length; i++)
                    {
                        if (data[i] == '1')
                            _bitArray[i] = true;
                        else
                            _bitArray[i] = false;
                    }
                }
                else
                    throw new ApplicationException("Invalid binary number: " + data);
            }
            public Bits(bool[] boolBits)
            {
                if (boolBits.Length > 0)
                {
                    _bitArray = new bool[boolBits.Length];
                    for (int i = 0; i < _bitArray.Length; i++)
                        _bitArray[i] = boolBits[i];
                }
                else
                    throw new ApplicationException("Can't initialize Bits because the input arry of boolBits can't be zero length");
            }

            public uint ToUInt()
            {
                uint result = 0;
                if (_bitArray != null)
                {
                    for (int i = 0; i < _bitArray.Length; i++)
                    {
                        //bit at position 0 is the MSBit so left shift bits from _bitArray
                        result <<= 1;
                        if (_bitArray[i] == true)
                            result |= 1;
                    }
                }
                return result;
            }
            public int ToInt()
            {
                int result = Convert.ToInt32(this.ToUInt());
                int numBits = _bitArray.Length;
                int maxPositiveValue = Convert.ToInt32(System.Math.Pow(2, numBits - 1) - 1);
                if (result > maxPositiveValue)
                    result -= Convert.ToInt32(System.Math.Pow(2, numBits));

                return result;
            }
            public byte[] ToBytes()
            {
                byte[] bytes = null;
                if (_bitArray != null)
                {
                    byte currByte = 0;
                    int numBytes = (int)(_bitArray.Length - 1) / 8 + 1;
                    bytes = new byte[numBytes];
                    int bitCnt = 0;
                    int byteIndex = 0;
                    for (int bitIndex = _bitArray.Length - 1; bitIndex >= 0; bitIndex--)
                    {
                        currByte >>= 1;
                        if (_bitArray[bitIndex] == true)
                            currByte |= 0x80;
                        //store byte every 8 bits
                        bitCnt++;
                        if (bitCnt % 8 == 0)
                        {
                            bytes[byteIndex] = currByte;
                            currByte = 0;
                            byteIndex++;
                        }
                    }
                    if (bitCnt % 8 != 0)
                        currByte >>= (8 - (bitCnt % 8));  //right shift remaining bits
                                                          //if last set of bits is less than 8 bits then it didn't get stored yet, so make sure to store it
                    if (byteIndex < numBytes)
                    {
                        bytes[byteIndex] = currByte;
                    }
                    System.Array.Reverse(bytes);
                }
                return bytes;
            }
            public Hex ToHex()
            {
                Hex hexData = new Hex(this.ToBytes());
                return hexData;
            }
            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                if (_bitArray.Length > 0)
                {
                    for (int i = 0; i < _bitArray.Length; i++)
                    {
                        if (_bitArray[i] == true)
                            sb.Append('1');
                        else
                            sb.Append('0');
                    }
                }
                return sb.ToString();
            }
            public bool[] ToBool
            {
                get
                {
                    bool[] array = null;
                    if (_bitArray.Length > 0)
                    {
                        array = new bool[_bitArray.Length];
                        for (int i = 0; i < _bitArray.Length; i++)
                            array[i] = _bitArray[i];
                    }
                    return array;
                }
            }
            public Bits GetBits(int bytePosition, int bitPosition, int numBits)
            {
                int byteIndex = (bytePosition + 1) * 8 - 1;
                int from = byteIndex - (bitPosition + numBits) + 1;
                int to = byteIndex - bitPosition;
                if (from < 0 || to > (_bitArray.Length - 1))
                {
                    throw new ApplicationException("Unable to extract bits because the combination of byte position, bit position, and number of bits is not valid");
                }

                bool[] extractedBits = new bool[numBits];
                int index = 0;
                for (int i = from; i <= to; i++)
                {
                    extractedBits[index] = _bitArray[i];
                    index++;
                }

                return new Bits(extractedBits);
            }
            public bool ValidBitString(string data)
            {
                bool validString = true;
                if (data != null)
                {
                    for (int i = 0; i < data.Length; i++)
                    {
                        if (data[i] != '0' && data[i] != '1')
                        {
                            validString = false;
                            break;
                        }
                    }
                }

                return validString;
            }
            private void createArray(byte[] byteArray)
            {
                byte currByte, mask;
                _bitArray = new bool[byteArray.Length * 8];

                int bitIndex = 0;
                for (int i = 0; i < byteArray.Length; i++)
                {
                    currByte = byteArray[i];
                    //the mask initially is: 1000 0000 then we can test the 7th bit
                    //then it becomes       : 0100 0000 so we can test the 6th bit
                    mask = 0x80;
                    for (int maskIndex = 7; maskIndex >= 0; maskIndex--)
                    {
                        if ((currByte & mask) > 0)
                            _bitArray[bitIndex] = true;
                        else
                            _bitArray[bitIndex] = false;
                        bitIndex++;
                        mask >>= 1;
                    }
                }
            }
        }

    }
}