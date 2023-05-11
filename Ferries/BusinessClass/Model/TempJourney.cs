/*==============================================================================
 *
 * Tempoary model for journey 
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
    /// temporary model for journey 
    /// </summary>
    public class TempJourney
    {
        /// <summary>
        /// location name 
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// departure day
        /// </summary>
        public int? DepartureDay { get; set; }
        /// <summary>
        /// departure time 
        /// </summary>
        public TimeSpan? DepartureTime { get; set; }
        /// <summary>
        /// arrival day
        /// </summary>
        public int? ArrivalDay { get; set; }
        /// <summary>
        /// arrival time 
        /// </summary>
        public TimeSpan? ArrivalTime { get; set; }
    }
}
