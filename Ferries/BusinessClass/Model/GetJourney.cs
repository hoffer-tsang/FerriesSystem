/*==============================================================================
 *
 * Model for get journey
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/
using System;

namespace BusinessClass.Model
{
    public class GetJourney
    {
        /// <summary>
        /// schedule id
        /// </summary>
        public int ScheduleId {get; set;}
        /// <summary>
        /// ferry name
        /// </summary>
        public string FerryName { get; set; }
        /// <summary>
        /// company name 
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// departure location name 
        /// </summary>
        public string DepartureLocation { get; set; }
        /// <summary>
        /// deparutre day
        /// </summary>
        public int DepartureDay { get; set; }
        /// <summary>
        /// departure time
        /// </summary>
        public TimeSpan DepartureTime { get; set; }
        /// <summary>
        /// deparure date time 
        /// </summary>
        public DateTime DepartureDateTime { get; set; }
        /// <summary>
        /// arrival location name 
        /// </summary>
        public string ArrivalLocation { get; set; }
        /// <summary>
        /// arrival day
        /// </summary>
        public int ArrivalDay { get; set; }
        /// <summary>
        /// arrival time 
        /// </summary>
        public TimeSpan ArrivalTime { get; set; }
        /// <summary>
        /// arrival date time
        /// </summary>
        public DateTime ArrivalDateTime {get; set; }
        /// <summary>
        /// cost per person 
        /// </summary>
        public decimal CostPerPerson { get; set; }
        /// <summary>
        /// cost per vehicle 
        /// </summary>
        public decimal CostPerVehicle { get; set; }
        /// <summary>
        /// number of stop 
        /// </summary>
        public int NumberOfStop { get; set; }
        /// <summary>
        /// total cost of the journey 
        /// </summary>
        public decimal TotalCost { get; set; }
        /// <summary>
        /// description of the joruney 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// days required of the journey 
        /// </summary>
        public int JourneyDay { get; set; }
    }
}
