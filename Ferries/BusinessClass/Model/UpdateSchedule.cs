/*==============================================================================
 *
 * model that contain update detials of a schedule 
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/
using System;
using System.Collections.Generic;

namespace BusinessClass.Model
{
    /// <summary>
    /// update schedule model details 
    /// </summary>
    public class UpdateSchedule
    {
        /// <summary>
        /// schedule id 
        /// </summary>
        public int ScheduleId { get; set; }
        /// <summary>
        /// ferry id 
        /// </summary>
        public int FerryId { get; set; }
        /// <summary>
        /// description of the schedule 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// cost per persoon of the schedule 
        /// </summary>
        public decimal CostPerPerson { get; set; }
        /// <summary>
        /// cost per vehicle of the schedule 
        /// </summary>
        public decimal CostPerVehicle { get; set; }
        /// <summary>
        /// row versio of the schedule 
        /// </summary>
        public byte[] RowVersion { get; set; }
        /// <summary>
        /// departure location id
        /// </summary>
        public int DepartureLocationId { get; set; }
        /// <summary>
        /// arrival location id 
        /// </summary>
        public int ArrivalLocationId { get; set; }
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
        /// <summary>
        /// class of middle stop list 
        /// </summary>
        public List<StopSubClass> StopList { get; set; }
        /// <summary>
        /// originla stop id list before update 
        /// </summary>
        public List<int> OriginalStopIdList { get; set; }
        /// <summary>
        /// update schedule class constructor to construct list 
        /// </summary>
        public UpdateSchedule()
        {
            OriginalStopIdList = new List<int>();
            StopList = new List<StopSubClass>();
        }
    }
}
