/*==============================================================================
 *
 * Get Journey List IComparer
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/
using System.Collections.Generic;
using BusinessClass.Model;

namespace Ferry.ViewModel.SubViewModel
{
    /// <summary>
    /// IComparer class for get journey list 
    /// </summary>
    public class GetJournyListComp : IComparer<GetJourney>
    {
        /// <summary>
        /// perform the comparison of departure date time first, and then arrival date time 
        /// </summary>
        /// <param name="x"> get journey x </param>
        /// <param name="y"> get journey y </param>
        /// <returns> -1, 0, 1</returns>
        public int Compare(GetJourney x, GetJourney y)
        {
            if (x.DepartureDateTime.CompareTo(y.DepartureDateTime) != 0)
            {
                return x.DepartureDateTime.CompareTo(y.DepartureDateTime);
            }
            else if (x.ArrivalDateTime.CompareTo(y.ArrivalDateTime) != 0)
            {
                return x.ArrivalDateTime.CompareTo(y.ArrivalDateTime);
            }
            else
            {
                return 0;
            }
        }
    }
}
