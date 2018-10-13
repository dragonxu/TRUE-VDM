namespace Controller
{
    public enum Omron_Command_Header_Read
    {
        Test_Read = 0x00,
        Status_Read = 0x01,
        Error_Read = 0x02,
        CIO_Area_Read = 0x03,
        H_Area_Read = 0x04,
        A_Area_Read = 0x05,
        L_Area_Read = 0x06,
        TC_Status_Read = 0x07,
        DM_Area_Read = 0x08,
        FM_Index_Read = 0x09,
        FM_Data_Read = 0x0A,
        TC_PV_Read = 0x0B,
        SV_Read1 = 0x0C,
        SV_Read2 = 0x0D,
        SV_Read3 = 0x0E,
    }
    public enum Omron_Response_Code { 
        Unknown = -1,
        Command_Completed_Normally = 0,
        Exe_Blocked_Run_Mode = 1,
        Exe_Blocked_Monitor_Mode = 2,
        Exe_Blocked_PROM_mounte = 3,
        Address_Overflow = 4,
        Exe_Blocked_Program_Mode = 0x0B,
        Exe_Blocked_Debug_Mode = 0x0C,
        Exe_Blocked_HotLink_is_Local = 0x0D,
        Parity_Err = 0x10,
        Frame_Err = 0x11,
        Overrun = 0x12,
        FCS_Err = 0x13,
        Command_Format_Err = 0x14,
        Incorrect_Data_Area = 0x15,
        Invalid_Instruction = 0x16,
        Frame_Length_Err = 0x18,
        Exe_Blocked_UnExe_Err = 0x19,
        IO_Table_Gen_Impossible = 0x20,
        PC_s_CPU = 0x21,
        Memory_not_Exist = 0x22,
        Memory_Write_Protected = 0x23,
        Abort_Parity_Err = 0xA0,
        Abort_Frame_Err = 0xA1,
        Abort_Overrun_Err = 0xA2,
        Abort_FCS_Err = 0xA3,
        Abort_Format_Err = 0xA4,
        Abort_Entry_Number_Err = 0xA5,
        Abort_Frame_Length_Err = 0xA6,
        Exe_Blocked_Less_16KByte = 0xB0,
        }
    public enum Omron_Data_Type : byte
    {
        Boolean = 0,
        Byte = 1,
        Char = 2,
        Double = 3,
        Integer = 4,
        Short = 5,
        Single = 6,
        UShort = 7,
        Hex = 8,
        Float = 9
    }
    public enum Omron_Command_Header_State_Read
    {
        CIO_Read = 0x1E,
        WR_Read = 0x1F,
        HR_Read = 0x20,
        AR_Read = 0x21
    }

    public enum Omron_Command_Header_Write
    {
        Status_Write = 0xF,
        CIO_Area_Write = 0x10,
        H_Area_Write = 0x11,
        A_Area_Write = 0x12,
        L_Area_Write = 0x13,
        TC_Status_Write = 0x14,
        DM_Area_Write = 0x15,
        FM_Area_Write = 0x16,
        TC_PV_Write = 0x17,
        SV_Write1 = 0x18,
        SV_Write2 = 0x19,
        SV_Write3 = 0x1A
    }
    public enum Omron_Command_Header_State_Write
    {
        CIO_Write = 0x1E,
        WR_Write = 0x1F,
        HR_Write = 0x20,
        AR_Write = 0x21
    }

}
