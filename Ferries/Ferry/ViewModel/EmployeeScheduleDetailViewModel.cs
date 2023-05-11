/*==============================================================================
 *
 * Employee Schedule Detail View Model
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

namespace Ferry.ViewModel
{
    /// <summary>
    /// Contain Details for Schedule Details view for employee
    /// </summary>
    public class EmployeeScheduleDetailViewModel
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
        /// row version 
        /// </summary>
        public byte[] RowVersion { get; set; }
        /// <summary>
        /// ferry name of the scheudle 
        /// </summary>
        public string FerryName { get; set; }
        /// <summary>
        /// cost per person to enter 
        /// </summary>

        [Required(ErrorMessage = "Cost Per Person is Required")]
        [RegularExpression(@"([\d]{1,})(\.?)([\d]{0,2}$)", ErrorMessage = "Numeric and 2dp only")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Cost Per Person:")]
        public decimal CostPerPerson { get; set; }
        /// <summary>
        /// cost per vehicle to enter 
        /// </summary>

        [Required(ErrorMessage = "Cost Per Vehicle is Required")]
        [RegularExpression(@"([\d]{1,})(\.?)([\d]{0,2}$)", ErrorMessage = "Numeric and 2dp only")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Cost Per Vehicle:")]
        public decimal CostPerVehicle { get; set; }
        /// <summary>
        /// description of the schedule 
        /// </summary>

        [Display(Name = "Description (Optional): ")]
        [StringLength(256, ErrorMessage = "Description cannot exceeed 256 characters")]
        public string Description { get; set; }
        /// <summary>
        /// list of day to display in dropdown 
        /// </summary>
        public List<Day> DayList { get; set; }
        /// <summary>
        /// list of location to display in dropdown 
        /// </summary>
        public List<Location> LocationList { get; set; }
        /// <summary>
        /// list of middle stop to display 
        /// </summary>

        public List<ScheduleDetail> StopList { get; set; }
        /// <summary>
        /// hiddne stop list for clone 
        /// </summary>
        public List<ScheduleDetail> HiddenStopList { get; set; }
        /// <summary>
        /// length of the stop list 
        /// </summary>
        public int StopListLength { get; set; }
        /// <summary>
        /// first stop of the schedule 
        /// </summary>
        public FirstStop FirstStop { get; set; }
        /// <summary>
        /// last stop of the scheudle 
        /// </summary>
        public LastStop LastStop { get; set; }
        /// <summary>
        /// list of stop id list to display in json seralize format 
        /// </summary>
        public string StopIdList { get; set; }
        /// <summary>
        /// General Error int 1 for yes
        /// </summary>
        public int? GeneralError { get; set; }
        /// <summary>
        /// concurrency error int 1 for yes 
        /// </summary>
        public int? ConcurrencyError { get; set; }
        /// <summary>
        /// Constructor of EmployeeScheduleDetailViewModel
        /// </summary>
        public EmployeeScheduleDetailViewModel()
        {
            StopList = new List<ScheduleDetail>();
            LocationList = new List<Location>();
            DayList = new List<Day>
            {
                new Day { DayNumber = 0, DayName = "Monday" },
                new Day { DayNumber = 1, DayName = "Tuesday" },
                new Day { DayNumber = 2, DayName = "Wednesday" },
                new Day { DayNumber = 3, DayName = "Thursday" },
                new Day { DayNumber = 4, DayName = "Friday" },
                new Day { DayNumber = 5, DayName = "Saturday" },
                new Day { DayNumber = 6, DayName = "Sunday" }
            };
            HiddenStopList = new List<ScheduleDetail>();
            var detail = new ScheduleDetail
            {
                LocationId = 1,
                ArrivalDay = 0,
                ArrivalTime = new TimeSpan(),
                DepartureDay = 0,
                DepartureTime = new TimeSpan(),
                ScheduleStopId = -999
            };
            HiddenStopList.Add(detail);
        }
    }
}
