/*==============================================================================
 *
 * Model for get location
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/

namespace BusinessClass.Model
{
    /// <summary>
    /// A model class including location id and name
    /// </summary>
    public class GetLocation
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
