/*==============================================================================
 *
 * Employee Location sub Model
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/

namespace Ferry.ViewModel.SubViewModel
{
    /// <summary>
    /// sub view model for Location list display
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Location Id
        /// </summary>
        public int? LocationId { get; set; }
        /// <summary>
        /// Location Name
        /// </summary>
        public string LocationName { get; set; }
    }
}
