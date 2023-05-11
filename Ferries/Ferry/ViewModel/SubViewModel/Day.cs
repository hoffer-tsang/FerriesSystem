/*==============================================================================
 *
 * Employee Day sub view Model
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/

namespace Ferry.ViewModel.SubViewModel
{
    /// <summary>
    /// sub view model for Day list display
    /// </summary>
    public class Day
    {
        /// <summary>
        /// Day number
        /// </summary>
        public int? DayNumber { get; set; }
        /// <summary>
        /// Day 
        /// </summary>
        public string DayName { get; set; }
    }
}
