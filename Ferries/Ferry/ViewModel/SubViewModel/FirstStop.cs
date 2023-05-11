﻿/*==============================================================================
 *
 * Employee First Stop sub View Model
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
    /// sub view model for first stop 
    /// </summary>
    public class FirstStop
    {
        /// <summary>
        /// Departure location id
        /// </summary>
        public int LocationId { get; set; }
        /// <summary>
        /// Departure location
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// departure day
        /// </summary>
        [Range(0, 6, ErrorMessage = "Departure Day has to be between Monday to Sunday")]
        public int DepartureDay { get; set; }
        /// <summary>
        /// departure day name
        /// </summary>
        public string DepartureDayName { get; set; }
        /// <summary>
        /// departure time 
        /// </summary>
        [Required(ErrorMessage = "Departure Time is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
        [RegularExpression(@"((([0-1][0-9])|(2[0-3]))(:[0-5][0-9])(:[0-5][0-9])?)", ErrorMessage = "Time must be between 00:00 to 23:59")]
        public TimeSpan DepartureTime { get; set; }
    }
}
