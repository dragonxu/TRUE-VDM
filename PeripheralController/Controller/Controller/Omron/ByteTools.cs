using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
    public static class ByteTool
    {
        public static byte[] UshortToBytes(ushort value)
        {
            byte[] resp = new byte[2];
            resp[1] = (byte)(value & 0xFF);
            resp[0] = (byte)((value >> 8) & 0xFF);
            return resp;
        }
        public static byte[] ShortToBytes(short value)
        {
            byte[] resp = new byte[2];
            resp[1] = (byte)(value & 0xFF);
            resp[0] = (byte)((value >> 8) & 0xFF);
            return resp;
        }
        public static ushort[] UintToUshort(uint value)
        {
            ushort[] resp = new ushort[2];
            resp[1] = (ushort)(value & 0xFFFF);
            resp[0] = (ushort)((value >> 16) & 0xFFFF);
            return resp;
        }
        public static short[] IntToShort(int value)
        {
            short[] resp = new short[2];
            resp[1] = (short)(value & 0xFFFF);
            resp[0] = (short)((value >> 16) & 0xFFFF);
            return resp;
        }
        public static ushort BytesToUshort(byte B1, byte B2)
        {
            ushort value = B1;
            value <<= 8;
            value += Convert.ToUInt16(B2);
            return value;
        }
        public static short BytesToShort(byte B1, byte B2)
        {
            short value = B1;
            value <<= 8;
            value += Convert.ToInt16(B2);
            return value;
        }
        public static bool IsBitSet(byte value, int position)
        {
            if (position < 0 || position > 7)
                throw new ArgumentOutOfRangeException("position", "position must be in the range 0 - 7");

            return (value & (1 << position)) != 0;
        }
        public static Byte SetBit(byte value, int position)
        {
            if (position < 0 || position > 7)
                throw new ArgumentOutOfRangeException("position", "position must be in the range 0 - 7");

            return (byte)(value | (1 << position));
        }
        public static byte UnsetBit(byte value, int position)
        {
            if (position < 0 || position > 7)
                throw new ArgumentOutOfRangeException("position", "position must be in the range 0 - 7");

            return (byte)(value & ~(1 << position));
        }
        public static byte ToggleBit(byte value, int position)
        {
            if (position < 0 || position > 7)
                throw new ArgumentOutOfRangeException("position", "position must be in the range 0 - 7");

            return (byte)(value ^ (1 << position));
        }
        public static string ToBinaryString(byte value)
        {
            return Convert.ToString(value, 2).PadLeft(8, '0');
        }
    }
    
}
