/*==============================================================================
 *
 * Employee Company sub View Model
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/

namespace Ferry.ViewModel.SubViewModel
{
    /// <summary>
    /// sub view model for company list display
    /// </summary>
    public class CompanyDetail
    {
        /// <summary>
        /// Company Name
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// Company Id
        /// </summary>
        public int CompanyId { get; set; }
    }
}
