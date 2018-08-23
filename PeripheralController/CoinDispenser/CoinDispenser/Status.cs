using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinDispenser
{
    /// <summary>
    /// 
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// Initial
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
        Enable_BA_if_hopper_problems_recovered,
        /// <summary>
        /// 
        /// </summary>
        Inhibit_BA_if_hopper_problems_occurred,
        /// <summary>
        /// 
        /// </summary>
        Motor_Problems,
        /// <summary>
        /// 
        /// </summary>
        Insufficient_Coin,
        /// <summary>
        /// 
        /// </summary>
        Dedects_coin_dispensing_activity_after_suspending_the_dispene_signal,
        /// <summary>
        /// 
        /// </summary>
        Reserved,
        /// <summary>
        /// 
        /// </summary>
        Prism_Sensor_Failure,
        /// <summary>
        /// 
        /// </summary>
        Shaft_Sensor_Failure,
        /// <summary>
        /// 
        /// </summary>
        Unavailable,
        /// <summary>
        /// Enable
        /// </summary>
        Enable = 12
    }
}
