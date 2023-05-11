/*==============================================================================
 *
 * Model for get scheudle middle stop 
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
    /// A model class including a schedule middle stop detail 
    /// </summary>
    public class GetStop
    {
        /// <summary>
        /// scheduel stop id
        /// </summary>
        public int ScheduleStopId { get; set; }
        /// <summary>
        /// location id 
        /// </summary>
        public int LocationId { get; set; }
        /// <summary>
        /// location name 
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// departure day
        /// </summary>
        public int DepartureDay { get; set; }
        /// <summary>
        /// departure time 
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
