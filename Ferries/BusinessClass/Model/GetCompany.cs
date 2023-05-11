/*==============================================================================
 *
 * Model for get company 
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/

namespace BusinessClass.Model
{
    /// <summary>
    /// A model class including company name and row version
    /// </summary>
    public class GetCompany
    {
        /// <summary>
        /// Row Version of current company detail
        /// </summary>
        public byte[] RowVersion { get; set; }
        /// <summary>
        /// Company Name
        /// </summary>
        public string CompanyName { get; set; }
    }
}
