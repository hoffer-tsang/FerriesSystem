/*==============================================================================
 *
 * Model for get company list SP
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/

namespace BusinessClass.Model
{
    /// <summary>
    /// A model class including id, company name
    /// </summary>
    public class GetCompanyListModel
    {
        /// <summary>
        /// Company Id
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// Company Name
        /// </summary>
        public string CompanyName { get; set; }
    }
}
