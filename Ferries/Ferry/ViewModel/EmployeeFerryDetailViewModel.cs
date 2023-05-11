/*==============================================================================
 *
 * Employee Ferry Detail View Model
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Ferry.ViewModel.SubViewModel;

namespace Ferry.ViewModel
{
    /// <summary>
    /// Contain Details for Ferry Detail view for employee
    /// </summary>
    public class EmployeeFerryDetailViewModel
    {
        /// <summary>
        /// ferry id 
        /// </summary>
        public int FerryId { get; set; }
        /// <summary>
        /// ferry Name 
        /// </summary>
        public string FerryName { get; set; }
        /// <summary>
        /// company Name 
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// departure day input 
        /// </summary>
        [DisplayName("Departure Day:")]
        [Range(0, 6, ErrorMessage = "Departure Day has to be between Monday to Sunday")]
        public int? DepartureDay { get; set; }
        /// <summary>
        /// deparutre location id
        /// </summary>
        [DisplayName("Departure Location:")]
        [Range(1, int.MaxValue, ErrorMessage = "Departure Location error")]
        public int? DepartureLocationId { get; set; }
        /// <summary>
        /// arrival day input 
        /// </summary>
        [DisplayName("Arrival Day:")]
        [Range(0, 6, ErrorMessage = "Arrival Day has to be between Monday to Sunday")]
        public int? ArrivalDay { get; set; }
        /// <summary>
        /// arrival Location id
        /// </summary>
        [DisplayName("Arrival Location:")]
        [Range(1, int.MaxValue, ErrorMessage = "Departure Location error")]
        public int? ArrivalLocationId { get; set; }
        /// <summary>
        /// list of day to display in dropdown 
        /// </summary>
        public List<Day> DayList { get; set; }
        /// <summary>
        /// list of location to display in dropdown 
        /// </summary>
        public List<Location> LocationList { get; set; }
        /// <summary>
        /// list of Schedule to display 
        /// </summary>
        public List<Schedule> ScheduleList { get; set; }
        /// <summary>
        /// list of page number to display
        /// </summary>
        public List<int> PageList { get; set; }
        /// <summary>
        /// General Error int 1 for yes
        /// </summary>
        public int? GeneralError { get; set; }
        /// <summary>
        /// hidden scheudle Id 
        /// </summary>
        public int? HiddenScheduleId { get; set; }
        /// <summary>
        /// Constructor of employeecompanyviewmodel
        /// </summary>
        public EmployeeFerryDetailViewModel()
        {
            ScheduleList = new List<Schedule>();
            PageList = new List<int>();
            LocationList = new List<Location>();
            DayList = new List<Day>
            {
                new Day { DayNumber = null, DayName ="Please Select..."},
                new Day { DayNumber = 0, DayName = "Monday" },
                new Day { DayNumber = 1, DayName = "Tuesday" },
                new Day { DayNumber = 2, DayName = "Wednesday" },
                new Day { DayNumber = 3, DayName = "Thursday" },
                new Day { DayNumber = 4, DayName = "Friday" },
                new Day { DayNumber = 5, DayName = "Saturday" },
                new Day { DayNumber = 6, DayName = "Sunday" }
            };
        }
    }
}
