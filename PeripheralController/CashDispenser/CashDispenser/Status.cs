using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashDispenser
{
    /// <summary>
    /// cash dispenser device
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// 
        /// </summary>
        Unknown = -1,
        /// <summary>
        /// 
        /// </summary>
        Ready,
        /// <summary>
        /// 
        /// </summary>
        Disconnected,
        /// <summary>
        /// 
        /// </summary>
        Single_machine_payout,
        /// <summary>
        /// 
        /// </summary>
        Multiple_machine_payout,
        /// <summary>
        /// 
        /// </summary>
        Payout_successful,
        /// <summary>
        /// 
        /// </summary>
        Payout_fails,
        /// <summary>
        /// 
        /// </summary>
        Empty_note,
        /// <summary>
        /// 
        /// </summary>
        Stock_less,
        /// <summary>
        /// 
        /// </summary>
        Note_jam,
        /// <summary>
        /// 
        /// </summary>
        Over_length,
        /// <summary>
        /// 
        /// </summary>
        Note_Not_Exit,
        /// <summary>
        /// 
        /// </summary>
        Sensor_Error,
        /// <summary>
        /// 
        /// </summary>
        Double_note_error,
        /// <summary>
        /// 
        /// </summary>
        Motor_Error,
        /// <summary>
        /// 
        /// </summary>
        Dispensing_busy,
        /// <summary>
        /// 
        /// </summary>
        Sensor_adjusting,
        /// <summary>
        /// 
        /// </summary>
        Checksum_Error,
        /// <summary>
        /// 
        /// </summary>
        Low_power_Error
    }
}
