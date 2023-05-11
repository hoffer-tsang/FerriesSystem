/*==============================================================================
 *
 * Stop List IComparer
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/
using System.Collections.Generic;

namespace Ferry.ViewModel.SubViewModel
{
    /// <summary>
    /// Stop list comparer class that sort by arrival day after departure day 
    /// </summary>
    public class StopListComp : IComparer<ScheduleDetail>
    {
        private int _FirstDepartureDay;
        /// <summary>
        /// Stop list comp constructor stores the first departure day 
        /// </summary>
        /// <param name="firstDepartureDay"></param>
        public StopListComp(int firstDepartureDay)
        {
            _FirstDepartureDay = firstDepartureDay;
        }
        /// <summary>
        /// Comparer
        /// </summary>
        /// <param name="x"> schedule detail x </param>
        /// <param name="y"> schedule detail y</param>
        /// <returns>-1, 0, 1</returns>
        public int Compare(ScheduleDetail x, ScheduleDetail y)
        {
            if(x.ArrivalDay < _FirstDepartureDay)
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
