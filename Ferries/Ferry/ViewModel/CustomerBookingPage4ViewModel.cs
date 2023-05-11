/*==============================================================================
 *
 * Customer Booking Page 4 View Model 
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/
using System.ComponentModel.DataAnnotations;

namespace Ferry.ViewModel
{
    /// <summary>
    /// Customer booking page 4 view model 
    /// </summary>
    public class CustomerBookingPage4ViewModel
    {
        /// <summary>
        /// Booking reference of the booking 
        /// </summary>
        [Display (Name = "Booking Reference:")]
        public string BookingReference { get; set; }
        /// <summary>
        /// Gemeral Error 
        /// </summary>
        public int GeneralError {get; set;}
    }
}
