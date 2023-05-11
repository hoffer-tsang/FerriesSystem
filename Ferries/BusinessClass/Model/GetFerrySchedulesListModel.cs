/*==============================================================================
 *
 * Model for get Scheudle list SP 
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
    /// Schedule List that contan all schedule detail 
    /// </summary>
    public class GetFerrySchedulesListModel
    {
        /// <summary>
        /// Schedule id
        /// </summary>
        public int ScheduleId { get; set; }
        /// <summary>
        /// schedule row version
        /// </summary>
        public byte[] RowVersion { get; set; }
        /// <summary>
        /// departure location id
        /// </summary>
        public int DepartureLocationId { get; set; }
        /// <summary>
        /// departure location name
        /// </summary>
        public string DepartureLocation { get; set; }
        /// <summary>
        /// departure day
        /// </summary>
        public int DepartureDay { get; set; }
        /// <summary>
        /// departure day name
        /// </summary>
        public string DepartureDayName { get; set; }
        /// <summary>
        /// departure time
        /// </summary>
        public TimeSpan DepartureTime { get; set; }
        /// <summary>
        /// arrival location id
        /// </summary>
        public int ArrivalLocationId { get; set; }
        /// <summary>
        /// arrival location name
        /// </summary>
        public string ArrivalLocation { get; set; }
        /// <summary>
        /// arrival day
        /// </summary>
        public int ArrivalDay { get; set; }
        /// <summary>
        /// arrival day name 
        /// </summary>
        public string ArrivalDayname { get; set; }
        /// <summary>
        /// arrival time 
        /// </summary>
        public TimeSpan ArrivalTime { get; set; }
        /// <summary>
        /// cost per person per stop
        /// </summary>
        public decimal CostPerPerson { get; set; }
        /// <summary>
        /// cost per vehicle per stop
        /// </summary>
        public decimal CostPerVehicle { get; set; }
        /// <summary>
        /// description of the schedule 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// total number of stop 
        /// </summary>
        public int NumberOfStop { get; set; }
    }
}
