/*==============================================================================
 *
 * Model for get Ferry 
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/

namespace BusinessClass.Model
{
    /// <summary>
    /// A model class including Ferry name, company id, company name  and row version
    /// </summary>
    public class GetFerry
    {
        /// <summary>
        /// Row Version of current Ferry detail
        /// </summary>
        public byte[] RowVersion { get; set; }
        /// <summary>
        /// Ferry Name
        /// </summary>
        public string FerryName { get; set; }
        /// <summary>
        /// CompanyId
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// Company Name
        /// </summary>
        public string CompanyName { get; set; }
    }
}
