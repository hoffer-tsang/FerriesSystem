/*==============================================================================
 *
 * another sub class for stop with ids
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/
using System;

namespace BusinessClass.Model
{
    /// <summary>
    /// Schedule middle stop details 
    /// </summary>
    public class StopSubClass
    {
        /// <summary>
        /// schedule stop id
        /// </summary>
        public int ScheduleStopId { get; set; }
        /// <summary>
        /// schedule id
        /// </summary>
        public int ScheduleId { get; set; }
        /// <summary>
        /// location id 
        /// </summary>
        public int LocationId { get; set; }
        /// <summary>
        /// departure day
        /// </summary>
        public int DepartureDay { get; set; }
        /// <summary>
        /// departuretime 
        /// </summary>
        public TimeSpan DepartureTime { get; set; }
        /// <summary>
        /// arrival day 
        /// </summary>
        public int ArrivalDay { get; set; }
        /// <summary>
        /// arrival time 
        /// </summary>
        public TimeSpan ArrivalTime { get; set; }
    }
}
