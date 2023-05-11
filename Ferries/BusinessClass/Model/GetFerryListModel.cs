/*==============================================================================
 *
 * Model for get Ferry list SP
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/

namespace BusinessClass.Model
{
    /// <summary>
    /// A model class including id, Ferry name and company name 
    /// </summary>
    public class GetFerryListModel
    {
        /// <summary>
        /// Ferry Id
        /// </summary>
        public int FerryId { get; set; }
        /// <summary>
        /// Ferry Name
        /// </summary>
        public string FerryName { get; set; }
        /// <summary>
        /// Company Name
        /// </summary>
        public string CompanyName { get; set; }
    }
}
