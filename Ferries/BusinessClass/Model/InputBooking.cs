/*==============================================================================
 *
 * model for booking input 
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/
using System;

namespace BusinessClass.Model
{
    public class InputBooking
    {
        /// <summary>
        /// user id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// booking reference 
        /// </summary>
        public string BookingReference { set; get; }
        /// <summary>
        /// number of cars
        /// </summary>
        public int Cars { get; set; }
        /// <summary>
        /// number of passengers 
        /// </summary>
        public int Passengers { get; set; }
        /// <summary>
        /// total cost of the journey 
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// company name of the ferry
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// ferry name 
        /// </summary>
        public string FerryName { get; set; }
        /// <summary>
        /// departure date time
        /// </summary>
        public DateTime DepartureDate { get; set; }
        /// <summary>
        /// departure location name
        /// </summary>
        public string DepartureLocation { get; set; }
        /// <summary>
        /// arrival date time
        /// </summary>
        public DateTime ArrivalDate { get; set; }
        /// <summary>
        /// arrival location name 
        /// </summary>
        public string ArrivalLocation { get; set; }
    }
}
