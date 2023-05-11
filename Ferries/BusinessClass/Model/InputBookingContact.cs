/*==============================================================================
 *
 * Model for input booking contact 
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/

namespace BusinessClass.Model
{
    public class InputBookingContact
    {
        /// <summary>
        /// booking id
        /// </summary>
        public int BookingId { get; set; }
        /// <summary>
        /// name of customer
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// address line 1
        /// </summary>
        public string AddressLine1 { get; set; }
        /// <summary>
        /// address line 2
        /// </summary>
        public string AddressLine2 { get; set; }
        /// <summary>
        /// city 
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// post code 
        /// </summary>
        public string PostalCode { get; set; }
    }
}
