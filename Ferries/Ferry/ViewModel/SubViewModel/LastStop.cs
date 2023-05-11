/*==============================================================================
 *
 * Last Stop sub View Model
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
    /// <summary>
    /// sub view model for schedule list display
    /// </summary>
    public class LastStop
    {
        /// <summary>
        /// location id
        /// </summary>
        public int LocationId { get; set; }
        /// <summary>
        /// location name
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// arrival day 
        /// </summary>
        [Range(0, 6, ErrorMessage = "Arrival Day has to be between Monday to Sunday")]
        public int ArrivalDay { get; set; }
        /// <summary>
        /// arrival day name
        /// </summary>
        public string ArrivalDayName { get; set; }
        /// <summary>
        /// arrival time 
        /// </summary>
        [Required(ErrorMessage = "Arrival Time is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
        [RegularExpression(@"((([0-1][0-9])|(2[0-3]))(:[0-5][0-9])(:[0-5][0-9])?)", ErrorMessage = "Time must be between 00:00 to 23:59")]
        public TimeSpan ArrivalTime { get; set; }
    }
}
