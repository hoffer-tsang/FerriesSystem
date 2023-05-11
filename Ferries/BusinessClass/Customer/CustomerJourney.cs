/*==============================================================================
 *
 * Contain the business logic for customer booking a journey
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using BusinessClass.Model;

namespace BusinessClass.Customer
{
    /// <summary>
    /// include business logic to generate journey with fitler, generate page number list, and get max page number  
    /// </summary>
    public class CustomerJourney
    {
        private string _ConnectionString = ConfigurationManager
            .ConnectionStrings["DefaultConnection"].ConnectionString;
        private int _ItemPerPage;
        private int _MaxPageNumber;
        private List<DateTime> _MondayOfTheMonth;
        private List<DateTime> _TuesdayOfTheMonth;
        private List<DateTime> _WednesdayOfTheMonth;
        private List<DateTime> _ThursdayOfTheMonth;
        private List<DateTime> _FridayOfTheMonth;
        private List<DateTime> _SaturdayOfTheMonth;
        private List<DateTime> _SundayOfTheMonth;

        /// <summary>
        /// Get the MaxPageNumber of the current list fitler
        /// </summary>
        /// <param name="numberOfItem"> number of item to display </param>
        /// <returns> max page number </returns>
        public int MaxPageNumber(int numberOfItem)
        {
            int totalData = numberOfItem;
            var totalPageNumber = totalData / _ItemPerPage;
            if (totalPageNumber == 0)
            {
                return 1;
            }
            if (totalData % _ItemPerPage != 0)
            {
                totalPageNumber++;
            }
            return totalPageNumber;
        }

        /// <summary>
        /// Get a list of page number to display on the page
        /// </summary>
        /// <param name="maxPageTotal"> maximum page number </param>
        /// <param name="pageNumber"> current page number </param>
        /// <returns> a list of page number </returns>
        /// <exception cref="Exception"> an error occur </exception>
        public List<int> JourneyPageNumber(int maxPageTotal, int pageNumber)
        {
            var pageList = new List<int>();
            if (maxPageTotal == 1)
            {
                pageList.Add(1);
                return pageList;
            }
            int startPage;
            int endPage;
            if (pageNumber == 1)
            {
                startPage = 1;
                endPage = startPage + _MaxPageNumber - 1;
                if (endPage > maxPageTotal)
                {
                    endPage = maxPageTotal;
                }
            }
            else if (pageNumber <= _MaxPageNumber / 2)
            {
                startPage = 1;
                endPage = startPage + _MaxPageNumber - 1;
                if (endPage > maxPageTotal)
                {
                    endPage = maxPageTotal;
                }
            }
            else if (pageNumber > _MaxPageNumber / 2)
            {
                startPage = pageNumber - (_MaxPageNumber / 2);
                endPage = startPage + _MaxPageNumber - 1;
                if (endPage > maxPageTotal)
                {
                    endPage = maxPageTotal;
                    if (endPage - startPage < _MaxPageNumber - 1)
                    {
                        startPage = endPage - _MaxPageNumber + 1;
                        if (startPage < 1)
                        {
                            startPage = 1;
                        }
                    }
                }
            }
            else
            {
                throw new Exception("PageList generate error");
            }    
            for (int i = startPage; i < endPage + 1; i++)
            {
                pageList.Add(i);
            }
            return pageList;
        }

        /// <summary>
        /// Consturctor for customer Journey class to store item per page and max page number to display in page number list 
        /// </summary>
        /// <param name="itemPerPage"> item per page to display </param>
        /// <param name="maxPageNumber"> maximum page number to display in page number list </param>
        public CustomerJourney(int itemPerPage, int maxPageNumber)
        {
            _ItemPerPage = itemPerPage;
            _MaxPageNumber = maxPageNumber;
            _MondayOfTheMonth = new List<DateTime>();
            _TuesdayOfTheMonth = new List<DateTime>();
            _WednesdayOfTheMonth = new List<DateTime>();
            _ThursdayOfTheMonth = new List<DateTime>();
             _FridayOfTheMonth = new List<DateTime>();
            _SaturdayOfTheMonth = new List<DateTime>();
             _SundayOfTheMonth = new List<DateTime>();
        }

        /// <summary>
        /// Get the available journey to display 
        /// </summary>
        /// <param name="departureLocation"> departure location name </param>
        /// <param name="arrivalLocation"> arrival location name </param>
        /// <param name="leaveAfter"> day for journey leave after </param>
        /// <param name="arriveBefore"> day for arrive before </param>
        /// <param name="person"> number of person </param>
        /// <param name="vehicle"> number of vehicle </param>
        /// <param name="maxPrice"> maxprice to filter </param>
        /// <returns> a list of journey details to display </returns>
        public List<GetJourney> AvailableJourney(string departureLocation, string arrivalLocation, DateTime? leaveAfter, DateTime? arriveBefore, int person, int vehicle, int maxPrice)
        {
            var startEndList = GetJourneyList();
            var returnList = new List<GetJourney>();
            if (leaveAfter == null)
            {
                leaveAfter = DateTime.Today.AddDays(1);
            }
            if (arriveBefore == null)
            {
                if (leaveAfter == null)
                {
                    arriveBefore = DateTime.Today.AddMonths(1);
                }
                else
                {
                    arriveBefore = ((DateTime)leaveAfter).AddMonths(1);
                }
            }
            GenerateDate(leaveAfter, arriveBefore);
            if (departureLocation == null && arrivalLocation == null)
            {
                foreach (var journey in startEndList)
                {
                    FilterJourneyByDay(journey, returnList, leaveAfter, arriveBefore, person, vehicle, maxPrice);
                }
                return returnList;
            }
            else
            {
                var midStopList = GetMidStop();
                var tempReturnGetJourneyList = new List<GetJourney>();
                foreach (var schedule in startEndList)
                {
                    var journey = GetMatchJourney(schedule, midStopList, departureLocation, arrivalLocation);
                    if (journey != null)
                    {
                        tempReturnGetJourneyList.Add(journey);
                    }
                }
                foreach (var journey in tempReturnGetJourneyList)
                {
                    FilterJourneyByDay(journey, returnList, leaveAfter, arriveBefore, person, vehicle, maxPrice);
                }
                return returnList;
            }
        }
        /// <summary>
        /// Filter journey by date boundary 
        /// </summary>
        /// <param name="journey"> the get journey detail </param>
        /// <param name="returnList"> list to be return </param>
        /// <param name="leaveAfter"> day for journey leave after </param>
        /// <param name="arriveBefore"> day for arrive before </param>
        /// <param name="person"> number of person </param>
        /// <param name="vehicle"> number of vehicle </param>
        /// <param name="maxPrice"> maxprice to filter </param>
        private void FilterJourneyByDay(GetJourney journey, List<GetJourney> returnList, DateTime? leaveAfter, DateTime? arriveBefore, int person, int vehicle, int maxPrice)
        {
            switch (journey.DepartureDay)
            {
                case 0:
                    foreach (var date in _MondayOfTheMonth)
                    {
                        var tempJourney = GetJourneyDetail(date, leaveAfter, arriveBefore, journey, person, vehicle, maxPrice);
                        if (tempJourney != null)
                        {
                            returnList.Add(tempJourney);
                        }
                    }
                    break;
                case 1:
                    foreach (var date in _TuesdayOfTheMonth)
                    {
                        var tempJourney = GetJourneyDetail(date, leaveAfter, arriveBefore, journey, person, vehicle, maxPrice);
                        if (tempJourney != null)
                        {
                            returnList.Add(tempJourney);
                        }
                    }
                    break;
                case 2:
                    foreach (var date in _WednesdayOfTheMonth)
                    {
                        var tempJourney = GetJourneyDetail(date, leaveAfter, arriveBefore, journey, person, vehicle, maxPrice);
                        if (tempJourney != null)
                        {
                            returnList.Add(tempJourney);
                        }
                    }
                    break;
                case 3:
                    foreach (var date in _ThursdayOfTheMonth)
                    {
                        var tempJourney = GetJourneyDetail(date, leaveAfter, arriveBefore, journey, person, vehicle, maxPrice);
                        if (tempJourney != null)
                        {
                            returnList.Add(tempJourney);
                        }
                    }
                    break;
                case 4:
                    foreach (var date in _FridayOfTheMonth)
                    {
                        var tempJourney = GetJourneyDetail(date, leaveAfter, arriveBefore, journey, person, vehicle, maxPrice);
                        if (tempJourney != null)
                        {
                            returnList.Add(tempJourney);
                        }
                    }
                    break;
                case 5:
                    foreach (var date in _SaturdayOfTheMonth)
                    {
                        var tempJourney = GetJourneyDetail(date, leaveAfter, arriveBefore, journey, person, vehicle, maxPrice);
                        if (tempJourney != null)
                        {
                            returnList.Add(tempJourney);
                        }
                    }
                    break;
                case 6:
                    foreach (var date in _SundayOfTheMonth)
                    {
                        var tempJourney = GetJourneyDetail(date, leaveAfter, arriveBefore, journey, person, vehicle, maxPrice);
                        if (tempJourney != null)
                        {
                            returnList.Add(tempJourney);
                        }
                    }
                    break;
            }
        }
        /// <summary>
        /// Generate the date within the date limit for filter 
        /// </summary>
        /// <param name="startDate"> start date </param>
        /// <param name="endDate"> end date </param>
        private void GenerateDate(DateTime? startDate, DateTime? endDate)
        {
            for (DateTime result = (DateTime)startDate; result < endDate; result = result.AddDays(1))
            {
                switch (result.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        _MondayOfTheMonth.Add(result);
                        break;
                    case DayOfWeek.Tuesday:
                        _TuesdayOfTheMonth.Add(result);
                        break;
                    case DayOfWeek.Wednesday:
                        _WednesdayOfTheMonth.Add(result);
                        break;
                    case DayOfWeek.Thursday:
                        _ThursdayOfTheMonth.Add(result);
                        break;
                    case DayOfWeek.Friday:
                        _FridayOfTheMonth.Add(result);
                        break;
                    case DayOfWeek.Saturday:
                        _SaturdayOfTheMonth.Add(result);
                        break;
                    case DayOfWeek.Sunday:
                        _SundayOfTheMonth.Add(result);
                        break;
                }
            }
        }
        
        /// <summary>
        /// Get the match journey of a particular scheudle 
        /// </summary>
        /// <param name="schedule"> the schedule detail </param>
        /// <param name="midStopList"> the list of schedule mid stop</param>
        /// <param name="departureLocation"> the departure location </param>
        /// <param name="arrivalLocation"> the arrival location </param>
        /// <returns> the journey to display </returns>
        private GetJourney GetMatchJourney(GetJourney schedule, List<GetJourneyMidStop> midStopList, string departureLocation, string arrivalLocation)
        {
            var tempList = GetTempJourneyList(schedule, midStopList);
            if (tempList != null)
            {
                int departureStop = 0;
                int arrivalStop = tempList.Count() - 1;
                bool departureStopFound = true;
                bool arrivalStopFound = true;
                if (departureLocation != null)
                {
                    departureStop = int.MaxValue;
                    departureStopFound = false;
                }
                if (arrivalLocation != null)
                {
                    arrivalStopFound = false;
                }
                for (int i = 0; i < tempList.Count; i++)
                {
                    if (tempList[i].Location == departureLocation)
                    {
                        departureStop = Math.Min(departureStop, i);
                        departureStopFound = true;
                    }
                    if (tempList[i].Location == arrivalLocation && i > departureStop)
                    {
                        if (departureStopFound && i != 0)
                        {
                            arrivalStop = Math.Min(arrivalStop, i);
                            arrivalStopFound = true;
                        }
                    }
                }
                if (departureStopFound && arrivalStopFound)
                {
                    var tempJourney = PopulateJourneyDetail(schedule, tempList, departureStop, arrivalStop);
                    return tempJourney;
                }
            }
            return null;
        }

        /// <summary>
        /// Populate detail to get Journey class for output 
        /// </summary>
        /// <param name="schedule"> Schedule detail </param>
        /// <param name="tempList"> temp list that contains the middle stop </param>
        /// <param name="departureStop"> first stop index </param>
        /// <param name="arrivalStop"> last Stop index</param>
        /// <returns> get journey details </returns>
        private GetJourney PopulateJourneyDetail(GetJourney schedule, List<TempJourney> tempList, int departureStop, int arrivalStop)
        {
            var tempJourney = new GetJourney
            {
                ScheduleId = schedule.ScheduleId,
                FerryName = schedule.FerryName,
                CompanyName = schedule.CompanyName,
                DepartureLocation = tempList[departureStop].Location,
                DepartureDay = (int)tempList[departureStop].DepartureDay,
                DepartureTime = (TimeSpan)tempList[departureStop].DepartureTime,
                ArrivalLocation = tempList[arrivalStop].Location,
                ArrivalDay = (int)tempList[arrivalStop].ArrivalDay,
                ArrivalTime = (TimeSpan)tempList[arrivalStop].ArrivalTime,
                CostPerPerson = schedule.CostPerPerson,
                CostPerVehicle = schedule.CostPerVehicle,
                NumberOfStop = arrivalStop - departureStop,

            };
            if (schedule.Description != null)
            {
                tempJourney.Description = schedule.Description;
            }
            if (tempJourney.ArrivalDay < tempJourney.DepartureDay)
            {
                tempJourney.JourneyDay = tempJourney.ArrivalDay + 7 - tempJourney.DepartureDay;
            }
            else
            {
                tempJourney.JourneyDay = tempJourney.ArrivalDay - tempJourney.DepartureDay;
            }
            return tempJourney;
        }

        /// <summary>
        /// the journey list that include first stop, mid stops and last stops of a journey 
        /// </summary>
        /// <param name="schedule"> schedule detail </param>
        /// <param name="midStopList"> list of middle stop </param>
        /// <returns> list of temp journey </returns>
        private List<TempJourney> GetTempJourneyList(GetJourney schedule, List<GetJourneyMidStop> midStopList)
        {
            var tempJourneyList = new List<TempJourney>();
            tempJourneyList.Add(new TempJourney
            {
                Location = schedule.DepartureLocation,
                DepartureDay = schedule.DepartureDay,
                DepartureTime = schedule.DepartureTime,
            });
            var scheuldId = schedule.ScheduleId;
            var tempStopList = new List<GetJourneyMidStop>();
            foreach (var stop in midStopList)
            {
                if (stop.ScheduleId == scheuldId)
                {
                    tempStopList.Add(new GetJourneyMidStop
                    {
                        DepartureTime = stop.DepartureTime,
                        DepartureDay = stop.DepartureDay,
                        ArrivalDay = stop.ArrivalDay,
                        ArrivalTime = stop.ArrivalTime,
                        Location = stop.Location
                    });
                }
            }
            tempStopList.Sort(new JourneyMidStopComp(schedule.DepartureDay));
            foreach (var tempStop in tempStopList)
            {
                tempJourneyList.Add(new TempJourney
                {
                    Location = tempStop.Location,
                    DepartureDay = tempStop.DepartureDay,
                    DepartureTime = tempStop.DepartureTime,
                    ArrivalTime = tempStop.ArrivalTime,
                    ArrivalDay = tempStop.ArrivalDay,
                });
            }
            tempJourneyList.Add(new TempJourney
            {
                Location = schedule.DepartureLocation,
                ArrivalDay = schedule.ArrivalDay,
                ArrivalTime = schedule.ArrivalTime,
            });
            return tempJourneyList;
        }
        private List<GetJourneyMidStop> GetMidStop()
        {
            using (var conn = new SqlConnection(this._ConnectionString))
            using (var cmd = new SqlCommand("dbo.usp_JourneyMidStop_Get", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    var midStopList = new List<GetJourneyMidStop>();
                    while (reader.Read())
                    {
                        midStopList.Add(GetStopListRecord(reader));
                    }
                    return midStopList;
                }
            }
        }

        /// <summary>
        /// Convert details to the get journey class with date filter and price filter
        /// </summary>
        /// <param name="date"> the date of the journey </param>
        /// <param name="leaveAfter"> the date to leave after </param>
        /// <param name="arriveBefore"> the date to arrive before </param>
        /// <param name="journey"> the journey detail </param>
        /// <param name="person"> the number of person in the journey </param>
        /// <param name="vehicle"> the number of vehicle in the journey </param>
        /// <param name="maxPrice"> the maximum price as filter </param>
        /// <returns> get the journey detail that matches, or null if no match </returns>
        private GetJourney GetJourneyDetail(DateTime date, DateTime? leaveAfter, DateTime? arriveBefore, GetJourney journey, int person, int vehicle, int maxPrice)
        {
            if (date >= leaveAfter && date <= arriveBefore)
            {
                var tempJourney = new GetJourney
                {
                    FerryName = journey.FerryName,
                    CompanyName = journey.CompanyName,
                    DepartureLocation = journey.DepartureLocation,
                    DepartureTime = journey.DepartureTime,
                    DepartureDay = journey.DepartureDay,
                    DepartureDateTime = date.Add(journey.DepartureTime),
                    ArrivalLocation = journey.ArrivalLocation,
                    ArrivalTime = journey.ArrivalTime,
                    ArrivalDay = journey.DepartureDay,
                    ArrivalDateTime = date.AddDays(journey.JourneyDay).Add(journey.ArrivalTime),
                    CostPerPerson = journey.CostPerPerson,
                    CostPerVehicle = journey.CostPerVehicle,
                    NumberOfStop = journey.NumberOfStop,
                    Description = journey.Description,
                    JourneyDay = journey.JourneyDay,
                    TotalCost = ((journey.CostPerPerson * person) + (journey.CostPerVehicle * vehicle)) * journey.NumberOfStop
                };
                if (tempJourney.ArrivalDateTime < arriveBefore && tempJourney.TotalCost <= maxPrice)
                {
                    return tempJourney;
                }
            }
            return null;
        }

        /// <summary>
        /// Get all schedule in the database 
        /// </summary>
        /// <returns></returns>
        private List<GetJourney> GetJourneyList()
        {
            using (var conn = new SqlConnection(this._ConnectionString))
            using (var cmd = new SqlCommand("dbo.usp_JourneyList_Get", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    var scheduleList = new List<GetJourney>();
                    while (reader.Read())
                    {
                        scheduleList.Add(GetSchedulesListRecord(reader));
                    }
                    return scheduleList;
                }
            }
        }

        /// <summary>
        /// convert sql record to class get journey mid stop
        /// </summary>
        /// <param name="record"> sql data </param>
        /// <returns> a class contain detail for mid stop in journey </returns>
        private GetJourneyMidStop GetStopListRecord(IDataRecord record)
        {
            var list = new GetJourneyMidStop();
            list.ScheduleId = (int)record["ScheduleId"];
            list.DepartureDay = (int)(byte)record["DepartureDay"];
            list.DepartureTime = (TimeSpan)record["DepartureTime"];
            list.ArrivalDay = (int)(byte)record["ArrivalDay"];
            list.ArrivalTime = (TimeSpan)record["ArrivalTime"];
            list.Location = (string)record["Name"];
            return list;
        }

        /// <summary>
        /// convert sql record to class get journey 
        /// </summary>
        /// <param name="record"> sql scheudle data </param>
        /// <returns> a class contain scheudle detail </returns>
        private GetJourney GetSchedulesListRecord(IDataRecord record)
        {
            var list = new GetJourney();
            list.ScheduleId = (int)record["ScheduleId"];
            list.FerryName = (string)record[1];
            list.CompanyName = (string)record[2];
            list.DepartureDay = (int)(byte)record["DepartureDay"];
            list.DepartureTime = (TimeSpan)record["DepartureTime"];
            list.DepartureLocation = (string)record[4];
            list.ArrivalDay = (int)(byte)record["ArrivalDay"];
            list.ArrivalTime = (TimeSpan)record["ArrivalTime"];
            list.ArrivalLocation = (string)record[8];
            list.CostPerPerson = (decimal)record["CostPerPerson"];
            list.CostPerVehicle = (decimal)record["CostPerVehicle"];
            if (list.ArrivalDay < list.DepartureDay)
            {
                list.JourneyDay = list.ArrivalDay + 7 - list.DepartureDay;
            }
            else
            {
                list.JourneyDay = list.ArrivalDay - list.DepartureDay;
            }
            if (record["Description"] != DBNull.Value)
            {
                list.Description = (string)record["Description"];
            }
            if (record["NumberOfStop"] == DBNull.Value)
            {
                list.NumberOfStop = 1;
            }
            else
            {
                list.NumberOfStop = (int)record["NumberOfStop"];
            }
            return list;
        }
    }
}
