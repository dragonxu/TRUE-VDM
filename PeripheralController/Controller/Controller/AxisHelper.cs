using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Controller
{
    /// <summary>
    /// AxisHelper class
    /// </summary>
    public class AxisHelper
    {
        /// <summary>
        /// AxisAccording
        /// </summary>
        /// <param name="Omron">Omron lib</param>
        /// <param name="axis">axis type [x,y]</param>
        /// <returns></returns>
        public AxisState AxisAccording(OmronTCP Omron)
        {
            AxisState state = AxisState.Unknown;
            try { 

            }catch(Exception e)
            {
                Console.WriteLine("AxisAccording execption : " + e);
            }
            return state;
        }
        private AxisState AnalyzeAxis(OmronTCP.Hex[] reply, int[] compareBit)
        {
            AxisState results = AxisState.Success;
            try
            {
                /*overload x bit 1(0x02), y bit 2(0x04)*/
                /*timeout x bit9(0x200), y bit10(0x400)*/
                
            }
            catch(Exception e)
            {
                Console.WriteLine("execption : " + e);
            }
            return results;
        }
        /// <summary>
        /// initial device
        /// </summary>
        /// <param name="Omron">Omron component</param>
        /// <returns>Axis State</returns>
        public async Task<AxisState> HomeAccroding(OmronTCP Omron)
        {
            AxisState state = AxisState.Success;
            try
            {
                int[] _positionIni;
                do
                {
                    Omron_Response_Code Response = Omron_Response_Code.Unknown;
                    Array _homeRead = Omron.Read(Omron_Command_Header_Read.CIO_Area_Read, 4213, 1, ref Response, Omron_Data_Type.Integer);
                    _positionIni = (int[])_homeRead;
                    Thread.Sleep(200);
                    Console.WriteLine("position : " + _positionIni[0]);

                    Array reply = Omron.Read(Omron_Command_Header_Read.CIO_Area_Read, 4206, 3, ref Response, Omron_Data_Type.Hex);
                    OmronTCP.Hex[] results = (OmronTCP.Hex[])reply;
                    state = Identify(results);
                    Console.WriteLine("current state : " + state);
                    if (!state.Equals(AxisState.Success))
                    {
                        state = AxisState.Error;
                        Console.WriteLine("Execption : " + state);
                        break;
                    }
                } while (_positionIni[0] != 0);
            }
            catch(Exception e)
            {
                Console.WriteLine("Execption : " + e);
            }
            return state;
        }
        /// <summary>
        /// Position According
        /// </summary>
        /// <param name="Omron">Omron Lib</param>
        /// <returns>AxisState</returns>
        public AxisState PostionAccording(OmronTCP Omron)
        {
            AxisState state = AxisState.Success;
            try
            {
                int[] _Axis;
                do
                {
                    //4201 error, 4206 error timeout
                    /*206 return 1 == error ,0 == ok, timeout ===*/
                    /*213 return 1 == inprocess, 0 == home position*/
                    Omron_Response_Code Response = Omron_Response_Code.Unknown;
                    Array _read = Omron.Read(Omron_Command_Header_Read.CIO_Area_Read, 4201, 2, ref Response, Omron_Data_Type.Integer);
                    _Axis = (int[])_read;
                    Thread.Sleep(200);

                    Console.WriteLine("position[word 1] : " + _Axis[0]);
                    Console.WriteLine("position[word 2] : " + _Axis[1]);

                    Array reply = Omron.Read(Omron_Command_Header_Read.CIO_Area_Read, 4206, 3, ref Response, Omron_Data_Type.Hex);
                    OmronTCP.Hex[] hexaDecimal = (OmronTCP.Hex[])reply;

                    state = Identify(hexaDecimal);
                    if (!state.Equals(AxisState.Success))
                    {
                        Console.WriteLine("Execption : " + state);
                        break;
                    }
                } while (_Axis[0] != 0 && _Axis[1] != 0); // 2 word
            }
            catch(Exception e)
            {
                Console.WriteLine("Execption : " + e);
            }
            return state;
        }

        private AxisState Identify(OmronTCP.Hex[] reply)
        {
            AxisState state = AxisState.Success;
            for(int index=0; index < reply.Length; index++)
            {
                if (reply[index].ToInt != 0)
                {
                    /*alram x bit 1(0x02), y bit 2(0x04)*/
                    /*timeout x bit9(0x200), y bit10(0x400)*/
                    Console.WriteLine("error 0 ");
                    state = AxisState.Error;
                    if ((reply[index].ToInt & 0x200) != 0)
                    {
                        Console.WriteLine("Timeout x");
                        state = AxisState.TimeoutAxisx;
                    }
                    if ((reply[index].ToInt & 0x400) != 0)
                    {
                        Console.WriteLine("Timeout y");
                        state = AxisState.TimeoutAxisy;
                    }
                    if ((reply[index].ToInt & 0x02) != 0)
                    {
                        Console.WriteLine("alram servo x");
                        state = AxisState.ErrorAxisx;
                    }
                    if ((reply[index].ToInt & 0x04) != 0)
                    {
                        Console.WriteLine("alram servo y");
                        state = AxisState.ErrorAxisy;
                    }
                }
            }
            return state;
        }
    }
}
