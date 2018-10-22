using System;

namespace CoinReciever
{
    /// <summary>
    /// responde status device class
    /// </summary>
    public enum CoinState
    {
        /// <summary>
        /// Initial status
        /// </summary>
        Unknown = -1,
        /// <summary>
        /// status device Unavailable.
        /// </summary>
        Unavailable = 0,
        /// <summary>
        /// status device Ready.
        /// </summary>
        Ready,
        /// <summary>
        /// status device Disconnect.
        /// </summary>
        Disconnected,
        /// <summary>
        /// status device sensor 1 problem.
        /// </summary>
        Sensor_1_problem,
        /// <summary>
        /// status device sensor 2 problem.
        /// </summary>
        Sensor_2_problem,
        /// <summary>
        /// status device sensor 3 problem.
        /// </summary>
        Sensor_3_problem
    }
}
