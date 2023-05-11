/*==============================================================================
 *
 * Employee Ferry Sub View Model
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/

namespace Ferry.ViewModel.SubViewModel
{
    /// <summary>
    /// sub view model for Ferry list display
    /// </summary>
    public class FerryDetail
    {
        /// <summary>
        /// Company Name
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// Ferry Name
        /// </summary>
        public string FerryName { get; set; }
        /// <summary>
        /// Ferry Id
        /// </summary>
        public int FerryId { get; set; }
    }
}
