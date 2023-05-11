/*==============================================================================
 *
 * IComparer model for middle stop day sorting 
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/
using System.Collections.Generic;

namespace BusinessClass.Model
{
    /// <summary>
    /// IComparer model that sort by arrival day
    /// </summary>
    public class JourneyMidStopComp : IComparer<GetJourneyMidStop>
    {
        private int _FirstDepartureDay;
        /// <summary>
        /// constructor that store the first departure day 
        /// </summary>
        /// <param name="firstDepartureDay"> departure day</param>
        public JourneyMidStopComp(int firstDepartureDay)
        {
            _FirstDepartureDay = firstDepartureDay;
        }
        /// <summary>
        /// compare the arrival day
        /// </summary>
        /// <param name="x"> get journey mid stop model x</param>
        /// <param name="y"> get journey mid stop model y</param>
        /// <returns> -1, 0, 1</returns>
        public int Compare(GetJourneyMidStop x, GetJourneyMidStop y)
        {
            if (x.ArrivalDay < _FirstDepartureDay)
            {
                x.ArrivalDay += 7;
            }
            if (y.ArrivalDay < _FirstDepartureDay)
            {
                y.ArrivalDay += 7;
            }
            if (x.ArrivalDay.CompareTo(y.ArrivalDay) != 0)
            {
                return x.ArrivalDay.CompareTo(y.ArrivalDay);
            }
            else
            {
                return 0;
            }
        }
    }
}
