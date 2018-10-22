namespace CashReceiver
{
    /// <summary>
    /// Device Status
    /// </summary>
    public enum CashState
    {
        /// <summary>
        /// Initial
        /// </summary>
        Unknown = -1,
        /// <summary>
        /// Device Ready
        /// </summary>
        Ready = 0,
        /// <summary>
        /// Device Unavailable
        /// </summary>
        Unavailable,
        /// <summary>
        /// Device Poweroff
        /// </summary>
        Poweroff,
        /// <summary>
        /// Device Disconnect
        /// </summary>
        Disconnected,
        /// <summary>
        /// Device Motor Failure
        /// </summary>
        MotorFailure,
        /// <summary>
        /// Device Check sum error
        /// </summary>
        CheckSumError,
        /// <summary>
        /// Device Bill jam
        /// </summary>
        BillJam,
        /// <summary>
        /// Device Bill Remove
        /// </summary>
        BillRemove,
        /// <summary>
        /// Device Stacker Open
        /// </summary>
        StackerOpen,
        /// <summary>
        /// Device sensor problem
        /// </summary>
        SensorProblem,
        /// <summary>
        /// Device Bill Fish
        /// </summary>
        BillFish
        /*Ready = "3E",
        Unavailable = "5E",
        Poweroff = "00",
        Disconnected =
        MotorFailure = "20",
        CheckSumError = "21",
        BillJam = "22",
        BillRemove = "23",
        StackerOpen = "24",
        SensorProblem = "25",
        BillFish = "27",
        Success = "10",
        BillReject = "29",
        Invalid = "2A",
        Reserved = "2E",
        Exclusion = "2F",*/
    }
}
