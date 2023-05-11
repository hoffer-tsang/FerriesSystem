/*==============================================================================
 *
 * Get a random booking reference
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/
using System;
using System.Linq;

namespace BusinessClass.Customer
{
    /// <summary>
    /// Contain logic to generate a booking reference 
    /// </summary>
    public static class BookingReference
    {
        private static readonly Random s_Global = new Random();
        [ThreadStatic] private static Random s_Local;
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        /// <summary>
        /// Generate a booking reference that is thread safe
        /// </summary>
        /// <param name="length"> length of boooking reference to be generated </param>
        /// <returns> booking reference </returns>
        public static string GetBookingReference(int length)
        {
            if (s_Local == null)
            {
                int seed;
                lock (s_Global)
                {
                    seed = s_Global.Next();
                }
                s_Local = new Random(seed);
            }
            return new string(Enumerable.Repeat(Chars, length)
            .Select(s => s[s_Local.Next(s.Length)]).ToArray());
        }
    }
}
