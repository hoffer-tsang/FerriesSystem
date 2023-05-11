/*==============================================================================
 *
 * Journey List sub View Model
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/
using System;
using System.ComponentModel.DataAnnotations;

namespace Ferry.ViewModel.SubViewModel
{
    public class JourneyList
    {
        /// <summary>
        /// Ferry name
        /// </summary>
        public string FerryName { get; set; }
        /// <summary>
        /// Departure Location 
        /// </summary>
        public string DepartureLocation { get; set; }
        /// <summary>
        /// Deparutre Date Time
        /// </summary>
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy HH:mm}")]
        public DateTime DepartureTime { get; set; }
        /// <summary>
        /// Arrival Location 
        /// </summary>
        public string ArrivalLocation { get; set; }
        /// <summary>
        /// Arrival Time 
        /// </summary>
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy HH:mm}")]
        public DateTime ArrivalTime { get; set; }
        /// <summary>
        /// Total Cost of the Journey 
        /// </summary>
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal TotalCost { get; set; }
        /// <summary>
        /// Number of stops of the journey 
        /// </summary>
        public int NumberOfStops { get; set; }
    }
}
