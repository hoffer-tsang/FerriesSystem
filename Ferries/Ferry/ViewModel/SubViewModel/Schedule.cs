/*==============================================================================
 *
 * Employee Schedule Sub View Model
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/

namespace Ferry.ViewModel.SubViewModel
{
    /// <summary>
    /// sub view model for schedule list display
    /// </summary>
    public class Schedule
    {
        /// <summary>
        /// Schedule Id
        /// </summary>
        public int ScheduleId { get; set; }
        /// <summary>
        /// Departure location id
        /// </summary>
        public int DepartureLocationId { get; set; }
        /// <summary>
        /// Departure location
        /// </summary>
        public string DepartureLocation { get; set; }
        /// <summary>
        /// departure day
        /// </summary>
        public string DepartureDay { get; set; }
        /// <summary>
        /// arrival location id
        /// </summary>
        public int ArrivalLocationId { get; set; }
        /// <summary>
        /// arrival location 
        /// </summary>
        public string ArrivalLocation { get; set; }
        /// <summary>
        /// arrival day 
        /// </summary>
        public string ArrivalDay { get; set; }
    }
}
