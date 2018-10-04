namespace Controller
{
    /// <summary>
    /// Servo Motor result
    /// </summary>
    public enum AxisState
    {
        /// <summary>
        /// Initial
        /// </summary>
        Unknown,
        /// <summary>
        /// Axis x Move position timeout
        /// </summary>
        TimeoutAxisx,
        /// <summary>
        /// Axis y Move position timeout
        /// </summary>
        TimeoutAxisy,
        /// <summary>
        /// Axis x move position error
        /// </summary>
        ErrorAxisx,
        /// <summary>
        /// Axis y move position error
        /// </summary>
        ErrorAxisy,
        /// <summary>
        /// Axis move position success
        /// </summary>
        Success,
        /// <summary>
        /// error
        /// </summary>
        Error
    }
}
