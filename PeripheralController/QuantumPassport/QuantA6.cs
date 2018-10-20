using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace QuantumPassport
{
    public unsafe class QuantA6
    {
        public const int WM_SCANEND = 0x400 + 100;
        public const int WM_TRANSFEREND = 0x0400 + 101;
        public const int WM_SAVEEND = 0x0400 + 102;
        public const int WM_SCANTIMEOUT = 0x0400 + 103;
        public const int WM_SCANEND_NO = 0x0400 + 120;
        public const int WM_SAVEEND_NO = 0x0400 + 121;
        public const int WM_SHADING_END = 0x0400 + 122;

        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte QuantA6_ReadMRZ(int nDpi, StringBuilder strMrz);

        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte QuantA6_DeviceOpen(IntPtr APPHwnd);
        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool QuantA6_DeviceClose();
        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte QuantA6_Scan(string pImageFileName, byte nType);
        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte QuantA6_GetUsbSpeed();
        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte QuantA6_GetFirmwareVersion();
        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte QuantA6_GetFpgaVersion();
        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte QuantA6_GetDpi();
        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool QuantA6_SetDpi(byte nDpi);
        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte QuantA6_GetImageSetting();
        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool QuantA6_SetImageSetting(byte nSetting);
        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool QuantA6_GetAdcSetting(byte* pGain, byte* pOffset);
        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool QuantA6_SetAdcSetting(byte nGain, byte nOffset);
        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte QuantA6_GetThreshold();
        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool QuantA6_SetThreshold(byte nThreshold);
        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte QuantA6_GetBits();
        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool QuantA6_SetBits(byte nBits);
        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte QuantA6_GetDropColor();
        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool QuantA6_SetDropColor(byte nColor);
        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte QuantA6_GetSensorValue(bool* bSensor3, bool* bSensor2, bool* bSensor1);
        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool QuantA6_FirmwareUpdate(string pFileName);
        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool QuantA6_SetLEDLength(ushort wRLEDLen, ushort wGLEDLen, ushort wBLEDLen, ushort wXLEDLen);
        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool QuantA6_GetLEDLength(ushort* pRLEDLen, ushort* pGLEDLen, ushort* pBLEDLen, ushort* pXLEDLen, ushort* pULEDLen);
        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte QuantA6_ImageCrop_File(string FilePath, string SaveFilePath, int nx, int ny, int nWidth, int nHeight);
        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte QuantA6_GetCISPos();
        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool QuantA6_SetMoveCIS();
        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void QuantA6_GetGamma(double* nGammaR1, double* nGammaG1, double* nGammaB1, double* nGammaX1, double* nGammaR2, double* nGammaG2, double* nGammaB2, double* nGammaX2);
        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool QuantA6_SetGamma(double nGammaR1, double nGammaG1, double nGammaB1, double nGammaX1, double nGammaR2, double nGammaG2, double nGammaB2, double nGammaX2, int nShadingDpi);
        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void QuantA6_DefaultSetting();
        [DllImport("QuantA6.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ushort QuantA6_ExecuteShading(double d_SubGamma, int nDpiMode);

        //Define QuantumSan2 Guid for auto connect.
        public static Guid GUID_HSIDCPY = new Guid(0x6c19caee, 0x3201, 0x4c90, 0x84, 0x3c, 0x60, 0xf7, 0xa9, 0x60, 0xa6, 0x49);
    }
}
