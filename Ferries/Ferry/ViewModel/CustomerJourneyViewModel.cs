/*==============================================================================
 *
 * Customer Journey Page View Model
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ferry.ViewModel.SubViewModel;
using Ferry.ViewModel.Validation;

namespace Ferry.ViewModel
{
    /// <summary>
    /// Customer Journey page view model that contain all detail of the page 
    /// </summary>
    public class CustomerJourneyViewModel
    {
        /// <summary>
        /// User Id 
        /// </summary>
        public int? UserId { get; set; }
        /// <summary>
        /// Deparutre Location
        /// </summary>
        [Display(Name = "Departure Location:")]
        public string DepartureLocation { get; set; }
        /// <summary>
        /// Arrival Location 
        /// </summary>
        [Display(Name = "Arrival Location:")]
        public string ArrivalLocation { get; set; }
        /// <summary>
        /// Leave After Date
        /// </summary>
        [Display(Name = "Leave After:")]
        [DataType(DataType.Date)]
        [DateValidation(ErrorMessage = "Date must be in the future")]
        public DateTime? LeaveAfter { get; set; }
        /// <summary>
        /// Arrival Before Date
        /// </summary>
        [Display(Name = "Arrival Before:")]
        [DataType(DataType.Date)]
        [DateValidation(ErrorMessage = "Date must be in the future")]
        public DateTime? ArriveBefore { get; set; }
        /// <summary>
        /// Number of Person 
        /// </summary>
        [Display(Name = "Person:")]
        [Range(1, 10, ErrorMessage = "Person enter has to be between 1 to 10")]
        public int Person { get; set; }
        /// <summary>
        /// Number of Vehicle 
        /// </summary>
        [Display(Name = "Vehicle:")]
        [Range(0, 3, ErrorMessage = "Vehicle enter has to be between 0 to 3")]
        public int Vehicle { get; set; }
        /// <summary>
        /// List of Max Price to display 
        /// </summary>
        public List<MaxPriceList> PriceMaxList { get; set; }
        /// <summary>
        /// Max Price Value 
        /// </summary>
        [Display(Name = "Max Price:")]
        public int MaxPrice { get; set; }
        /// <summary>
        /// Available Journey to display 
        /// </summary>
        public List<JourneyList> AvailableJourney { get; set; }
        /// <summary>
        /// page list to display 
        /// </summary>
        public List<int> PageList { get; set; }
        /// <summary>
        /// location list to dropdown 
        /// </summary>
        public List<Location> LocationList { get; set; }
        /// <summary>
        /// general error code 
        /// </summary>
        public int GeneralError { get; set; }
        /// <summary>
        /// Constructor of CustomerJourneyViewModel, initiate list and populate option for price max list 
        /// </summary>
        public CustomerJourneyViewModel()
        {
            LocationList = new List<Location>();
            AvailableJourney = new List<JourneyList>();
            PageList = new List<int>();
            PriceMaxList = new List<MaxPriceList>
            {
                new MaxPriceList { MaxPrice = int.MaxValue, MaxPriceString = "No Max" },
                new MaxPriceList { MaxPrice = 10, MaxPriceString = "Below £10" },
                new MaxPriceList { MaxPrice = 20, MaxPriceString = "Below £20" },
                new MaxPriceList { MaxPrice = 50, MaxPriceString = "Below £50" },
                new MaxPriceList { MaxPrice = 100, MaxPriceString = "Below £100" },
                new MaxPriceList { MaxPrice = 150, MaxPriceString = "Below £150" },
                new MaxPriceList { MaxPrice = 200, MaxPriceString = "Below £200" }
            };
        }
    }
}
