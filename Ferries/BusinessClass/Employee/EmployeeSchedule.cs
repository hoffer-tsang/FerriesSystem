/*==============================================================================
 *
 * Business class for Employee Ferry Detail View
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
using BusinessClass.Model;

namespace BusinessClass.Employee
{
    /// <summary>
    /// Contain all business logic required for employee schedule
    /// </summary>
    public class EmployeeSchedule
    {
        private string _ConnectionString = ConfigurationManager
            .ConnectionStrings["DefaultConnection"].ConnectionString;
        private int _ItemPerPage;
        private int _MaxPageNumber;

        /// <summary>
        /// Constructor of Employee schedule class
        /// </summary>
        /// <param name="itemPerPage"> item per page allow </param>
        /// <param name="maxPageNummber"> maximum page number allow </param>
        public EmployeeSchedule(int itemPerPage, int maxPageNummber)
        {
            _ItemPerPage = itemPerPage;
            _MaxPageNumber = maxPageNummber;
        }

        /// <summary>
        /// get the location list in database
        /// </summary>
        /// <param name="needNull"> need null option or not </param>
        /// <returns> a list of location name </returns>
        public List<GetLocation> GetLocationList(bool needNull)
        {
            using (var conn = new SqlConnection(this._ConnectionString))
            using (var cmd = new SqlCommand("dbo.usp_LocationList_Get", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    var locationList = new List<GetLocation>();
                    if (needNull)
                    {
                        var location = new GetLocation { LocationId = null, LocationName = "Please Select" };
                        locationList.Add(location);
                    }
                    while (reader.Read())
                    {
                        locationList.Add(GetLocationRecord(reader));
                    }
                    return locationList;
                }
            }
        }

        /// <summary>
        /// get the schedule list with search filter 
        /// </summary>
        /// <param name="ferryId"> ferry id </param>
        /// <param name="departureDay"> departure day </param>
        /// <param name="departureLocationId"> departure location id </param>
        /// <param name="arrivalDay"> arrival day </param>
        /// <param name="arrivalLocationId"> arrival location id </param>
        /// <param name="pageNumber"> page number </param>
        /// <param name="isAsec"> is ascending display </param>
        /// <param name="sortColumn"> column to sort </param>
        /// <returns> return a list of schedule to display </returns>
        public List<GetFerrySchedulesListModel> GetScheduleList(
            int ferryId, int? departureDay, int? departureLocationId, int? arrivalDay, 
            int? arrivalLocationId, int pageNumber, int isAsec, string sortColumn)
        {
            using (var conn = new SqlConnection(this._ConnectionString))
            using (var cmd = new SqlCommand("dbo.usp_FerriesSchedulesList_Get", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("FerryId", System.Data.SqlDbType.Int).Value = ferryId;
                if (departureDay != null)
                {
                    cmd.Parameters.Add("StartDay", System.Data.SqlDbType.TinyInt).Value = departureDay;
                }
                if (departureLocationId != null)
                {
                    cmd.Parameters.Add("StartLocationId", System.Data.SqlDbType.Int).Value = departureLocationId;
                }
                if (arrivalDay != null)
                {
                    cmd.Parameters.Add("EndDay", System.Data.SqlDbType.TinyInt).Value = arrivalDay;
                }
                if (arrivalLocationId != null)
                {
                    cmd.Parameters.Add("EndLocationId", System.Data.SqlDbType.Int).Value = arrivalLocationId;
                }
                if (!String.IsNullOrWhiteSpace(sortColumn))
                {
                    if (sortColumn == "DepartureDay")
                    {
                        cmd.Parameters.Add("ColumnSorting", System.Data.SqlDbType.NVarChar, 256).Value = "StartDay";
                    }
                    else if (sortColumn == "DepartureLocation")
                    {
                        cmd.Parameters.Add("ColumnSorting", System.Data.SqlDbType.NVarChar, 256).Value = "StartLocation";
                    }
                    else if (sortColumn == "ArrivalDay")
                    {
                        cmd.Parameters.Add("ColumnSorting", System.Data.SqlDbType.NVarChar, 256).Value = "EndDay";
                    }
                    else if (sortColumn == "ArrivalLocation")
                    {
                        cmd.Parameters.Add("ColumnSorting", System.Data.SqlDbType.NVarChar, 256).Value = "EndLocation";
                    }
                }
                cmd.Parameters.Add("PageNumber", System.Data.SqlDbType.Int).Value = pageNumber;
                cmd.Parameters.Add("ItemPerPage", System.Data.SqlDbType.Int).Value = _ItemPerPage;
                cmd.Parameters.Add("IsAsec", System.Data.SqlDbType.Bit).Value = isAsec;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    var scheduleList = new List<GetFerrySchedulesListModel>();
                    while (reader.Read())
                    {
                        scheduleList.Add(GetSchedulesListRecord(reader));
                    }
                    return scheduleList;
                }
            }
        }

        /// <summary>
        /// get the pagination list of schedule 
        /// </summary>
        /// <param name="pageNumber"> current page number </param>
        /// <param name="maxPageTotal"> total maximum page </param>
        /// <returns> list of int of page number </returns>
        /// <exception cref="Exception"> error occured in page number generation </exception>
        public List<int> GetSchedulePagination(int pageNumber, int maxPageTotal)
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
        /// get new max page number with search filter 
        /// </summary>
        /// <param name="ferryId"> ferry id </param>
        /// <param name="departureDay"> departure day </param>
        /// <param name="departureLocationId"> departure location id </param>
        /// <param name="arrivalDay"> arrival day </param>
        /// <param name="arrivalLocationId"> arrival location id </param>
        /// <returns> max page number </returns>
        public int GetNewMaxPageNumber(int ferryId, int? departureDay, int? departureLocationId, int? arrivalDay, int? arrivalLocationId)
        {
            using (var conn = new SqlConnection(this._ConnectionString))
            using (var cmd = new SqlCommand("dbo.usp_FerriesSchedulesList_Get", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("FerryId", System.Data.SqlDbType.Int).Value = ferryId;
                if (departureDay != null)
                {
                    cmd.Parameters.Add("StartDay", System.Data.SqlDbType.TinyInt).Value = departureDay;
                }
                if (departureLocationId != null)
                {
                    cmd.Parameters.Add("StartLocationId", System.Data.SqlDbType.Int).Value = departureLocationId;
                }
                if (arrivalDay != null)
                {
                    cmd.Parameters.Add("EndDay", System.Data.SqlDbType.TinyInt).Value = arrivalDay;
                }
                if (arrivalLocationId != null)
                {
                    cmd.Parameters.Add("EndLocationId", System.Data.SqlDbType.Int).Value = arrivalLocationId;
                }
                cmd.Parameters.Add("PageNumber", System.Data.SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("ItemPerPage", System.Data.SqlDbType.Int).Value = 9999;
                cmd.Parameters.Add("IsAsec", System.Data.SqlDbType.Bit).Value = 1;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    int totalData = 0;
                    while (reader.Read())
                    {
                        totalData++;
                    }
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
            }
        }

        /// <summary>
        /// get the schedule list detail
        /// </summary>
        /// <param name="ferryId"> ferry id </param>
        /// <param name="scheduleId"> scheudle it </param>
        /// <returns> the model with schedule details </returns>
        public GetFerrySchedulesListModel GetScheduleList(int ferryId, int scheduleId)
        {
            using (var conn = new SqlConnection(this._ConnectionString))
            using (var cmd = new SqlCommand("dbo.usp_FerriesSchedulesList_Get", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("FerryId", System.Data.SqlDbType.Int).Value = ferryId;
                cmd.Parameters.Add("ScheduleId", System.Data.SqlDbType.Int).Value = scheduleId;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    var scheduleList = new GetFerrySchedulesListModel();
                    while (reader.Read())
                    {
                        return GetSchedulesListRecord(reader);
                    }
                    return null;
                }
            }
        }

        /// <summary>
        /// Get the scheudle middle stop detail 
        /// </summary>
        /// <param name="scheduleId"> scheudle id </param>
        /// <returns> a list of middle stop of scheudle </returns>
        public List<GetStop> GetScheduleDetail(int scheduleId)
        {
            using (var conn = new SqlConnection(this._ConnectionString))
            using (var cmd = new SqlCommand("dbo.usp_ScheduleDetails_Get", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("ScheduleId", System.Data.SqlDbType.Int).Value = scheduleId;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    var stopList = new List<GetStop>();
                    while (reader.Read())
                    {
                        var stop = new GetStop
                        {
                            ScheduleStopId = (int)reader["ScheduleStopId"],
                            LocationId = (int)reader["LocationId"],
                            Location = (string)reader["Name"],
                            DepartureDay = (int)(byte)reader["DepartureDay"],
                            DepartureTime = (TimeSpan)reader["DepartureTime"],
                            ArrivalDay = (int)(byte)reader["ArrivalDay"],
                            ArrivalTime = (TimeSpan)reader["ArrivalTime"]
                        };
                        stopList.Add(stop);
                    }
                    return stopList;
                }
            }
        }

        /// <summary>
        /// edit the schedule and middle stop 
        /// </summary>
        /// <param name="data"> schedule data to be updated or add </param>
        /// <returns> return schedule id or -1 if concurrency error occur in edit schedule </returns>
        public int EditSchedule(UpdateSchedule data)
        {
            if (data.ScheduleId == -1)
            {
                using (var conn = new SqlConnection(this._ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    AddScheduleDetail(cmd, data);
                }
                using (var conn = new SqlConnection(this._ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    SqlTransaction transaction;
                    transaction = conn.BeginTransaction();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Transaction = transaction;
                    try
                    {
                        DeleteScheduleDetail(cmd, data);
                        AddEditScheduleDetail(cmd, data);
                        transaction.Commit();
                        return data.ScheduleId;
                    }
                    catch (Exception)
                    {
                        try
                        {
                            transaction.Rollback();
                            DeleteSchedule(data.ScheduleId);
                        }
                        catch (Exception)
                        {
                            return data.ScheduleId;
                        }
                        return data.ScheduleId;
                    }
                }
            }
            else
            {
                using (var conn = new SqlConnection(this._ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    SqlTransaction transaction;
                    transaction = conn.BeginTransaction();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Transaction = transaction;
                    try
                    {
                        var result = EditScheduleDetail(cmd, data);
                        if (result == -1)
                        {
                            return result;
                        }
                        DeleteScheduleDetail(cmd, data);
                        AddEditScheduleDetail(cmd, data);
                        transaction.Commit();
                        return data.ScheduleId;
                    }
                    catch (Exception)
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception)
                        {
                            return data.ScheduleId;
                        }
                        return data.ScheduleId;
                    }
                }
            }
        }

        /// <summary>
        /// delete the schedule in database
        /// </summary>
        /// <param name="scheduleId"> schedule id </param>
        /// <returns> 1 if success, 0 if fail </returns>
        public int DeleteSchedule(int scheduleId)
        {
            using (var conn = new SqlConnection(this._ConnectionString))
            using (var cmd = new SqlCommand("dbo.usp_Schedule_Delete", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("ScheduleId", System.Data.SqlDbType.Int).Value = scheduleId;
                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    return 0;
                }
                return 1;
            }
        }

        /// <summary>
        /// convert sql data to location record
        /// </summary>
        /// <param name="record"> sql data </param>
        /// <returns> getlocation wtih locaton id and name </returns>
        private GetLocation GetLocationRecord(IDataRecord record)
        {
            return new GetLocation
            {
                LocationId = (int)record["LocationId"],
                LocationName = (string)record["Name"]
            };
        }

        /// <summary>
        /// convert sql data to scheudle model 
        /// </summary>
        /// <param name="record"> sql data </param>
        /// <returns> GetFerrySchedulesListModel with record details </returns>
        private GetFerrySchedulesListModel GetSchedulesListRecord(IDataRecord record)
        {
            var list = new GetFerrySchedulesListModel();
            list.ScheduleId = (int)record["ScheduleId"];
            list.RowVersion = (byte[])record["RowVersion"];
            list.DepartureDay = (int)(byte)record["DepartureDay"];
            list.DepartureTime = (TimeSpan)record["DepartureTime"];
            list.DepartureLocationId = (int)record["DepartureLocationId"];
            list.DepartureLocation = (string)record[3];
            list.ArrivalDay = (int)(byte)record["ArrivalDay"];
            list.ArrivalTime = (TimeSpan)record["ArrivalTime"];
            list.ArrivalLocationId = (int)record["ArrivalLocationId"];
            list.ArrivalLocation = (string)record[7];
            list.CostPerPerson = (decimal)record["CostPerPerson"];
            list.CostPerVehicle = (decimal)record["CostPerVehicle"];
            if (record["Description"] != DBNull.Value)
            {
                list.Description = (string)record["Description"];
            }
            if(record["NumberOfStop"] == DBNull.Value)
            {
                list.NumberOfStop = 1;
            }
            else
            {
                list.NumberOfStop = (int)record["NumberOfStop"];
            }
            return list;
        }

        /// <summary>
        /// add the schedule detail to data base 
        /// </summary>
        /// <param name="cmd"> the sql command to add </param>
        /// <param name="data"> the data to be add </param>
        private void AddScheduleDetail(SqlCommand cmd, UpdateSchedule data)
        {
            cmd.CommandText = "dbo.usp_Schedule_Insert";
            cmd.Parameters.Add("FerryId", System.Data.SqlDbType.Int).Value = data.FerryId;
            cmd.Parameters.Add("CostPerPerson", System.Data.SqlDbType.Decimal).Value = data.CostPerPerson;
            cmd.Parameters.Add("CostPerVehicle", System.Data.SqlDbType.Decimal).Value = data.CostPerVehicle;
            cmd.Parameters.Add("DepartureLocationId", System.Data.SqlDbType.Int).Value = data.DepartureLocationId;
            cmd.Parameters.Add("ArrivalLocationId", System.Data.SqlDbType.Int).Value = data.ArrivalLocationId;
            cmd.Parameters.Add("DepartureDay", System.Data.SqlDbType.TinyInt).Value = (byte)data.DepartureDay;
            cmd.Parameters.Add("DepartureTime", System.Data.SqlDbType.Time).Value = data.DepartureTime;
            cmd.Parameters.Add("ArrivalDay", System.Data.SqlDbType.TinyInt).Value = (byte)data.ArrivalDay;
            cmd.Parameters.Add("ArrivalTime", System.Data.SqlDbType.Time).Value = data.ArrivalTime;
            if (!String.IsNullOrWhiteSpace(data.Description))
            {
                cmd.Parameters.Add("Description", System.Data.SqlDbType.NVarChar, 256).Value = data.Description;
            }
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    data.ScheduleId = (int)reader["ScheduleId"];
                }
            }
        }

        /// <summary>
        /// edit the schedule detail in data base 
        /// </summary>
        /// <param name="cmd"> the sql command to edit </param>
        /// <param name="data"> the data to be update </param>
        /// <returns> -1 if concurrency error, 0 if success </returns>
        private int EditScheduleDetail(SqlCommand cmd, UpdateSchedule data)
        {
            cmd.CommandText = "dbo.usp_Schedule_Update";
            cmd.Parameters.Add("FerryId", System.Data.SqlDbType.Int).Value = data.FerryId;
            cmd.Parameters.Add("ScheduleId", System.Data.SqlDbType.Int).Value = data.ScheduleId;
            cmd.Parameters.Add("CostPerPerson", System.Data.SqlDbType.Decimal).Value = data.CostPerPerson;
            cmd.Parameters.Add("CostPerVehicle", System.Data.SqlDbType.Decimal).Value = data.CostPerVehicle;
            cmd.Parameters.Add("RowVersion", System.Data.SqlDbType.Timestamp).Value = data.RowVersion;
            cmd.Parameters.Add("DepartureLocationId", System.Data.SqlDbType.Int).Value = data.DepartureLocationId;
            cmd.Parameters.Add("ArrivalLocationId", System.Data.SqlDbType.Int).Value = data.ArrivalLocationId;
            cmd.Parameters.Add("DepartureDay", System.Data.SqlDbType.TinyInt).Value = (byte)data.DepartureDay;
            cmd.Parameters.Add("DepartureTime", System.Data.SqlDbType.Time).Value = data.DepartureTime;
            cmd.Parameters.Add("ArrivalDay", System.Data.SqlDbType.TinyInt).Value = (byte)data.ArrivalDay;
            cmd.Parameters.Add("ArrivalTime", System.Data.SqlDbType.Time).Value = data.ArrivalTime;
            if (!String.IsNullOrWhiteSpace(data.Description))
            {
                cmd.Parameters.Add("Description", System.Data.SqlDbType.NVarChar, 256).Value = data.Description;
            }
            if (cmd.ExecuteNonQuery() == 0)
            {
                return -1;
            }
            cmd.Parameters.RemoveAt("FerryId");
            cmd.Parameters.RemoveAt("ScheduleId");
            cmd.Parameters.RemoveAt("CostPerPerson");
            cmd.Parameters.RemoveAt("CostPerVehicle");
            cmd.Parameters.RemoveAt("RowVersion");
            cmd.Parameters.RemoveAt("DepartureLocationId");
            cmd.Parameters.RemoveAt("ArrivalLocationId");
            cmd.Parameters.RemoveAt("DepartureDay");
            cmd.Parameters.RemoveAt("DepartureTime");
            cmd.Parameters.RemoveAt("ArrivalDay");
            cmd.Parameters.RemoveAt("ArrivalTime");
            if (!String.IsNullOrWhiteSpace(data.Description))
            {
                cmd.Parameters.RemoveAt("Description");
            }
            return 0;
        }

        /// <summary>
        /// delete the schedule and stop
        /// </summary>
        /// <param name="cmd"> sql command </param>
        /// <param name="data"> data with id to delete </param>
        private void DeleteScheduleDetail(SqlCommand cmd, UpdateSchedule data)
        {
            var remainStopId = new List<int>();
            foreach (var d in data.StopList)
            {
                remainStopId.Add(d.ScheduleStopId);
            }
            foreach (var id in data.OriginalStopIdList)
            {
                if (!remainStopId.Contains(id))
                {
                    cmd.CommandText = "dbo.usp_ScheduleStop_Delete";
                    cmd.Parameters.Add("ScheduleStopId", System.Data.SqlDbType.Int).Value = id;
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.RemoveAt("ScheduleStopId");
                }
            }
        }

        /// <summary>
        /// add or edit the schedule stop 
        /// </summary>
        /// <param name="cmd"> sql command</param>
        /// <param name="data"> data to be add or update </param>
        private void AddEditScheduleDetail(SqlCommand cmd, UpdateSchedule data)
        {
            foreach (var stop in data.StopList)
            {
                if (stop.ScheduleStopId == -1)
                {
                    cmd.CommandText = "dbo.usp_ScheduleStop_Insert";
                    cmd.Parameters.Add("ScheduleId", System.Data.SqlDbType.Int).Value = data.ScheduleId;
                    cmd.Parameters.Add("LocationId", System.Data.SqlDbType.Int).Value = stop.LocationId;
                    cmd.Parameters.Add("DepartureDay", System.Data.SqlDbType.TinyInt).Value = (byte)stop.DepartureDay;
                    cmd.Parameters.Add("DepartureTime", System.Data.SqlDbType.Time).Value = stop.DepartureTime;
                    cmd.Parameters.Add("ArrivalDay", System.Data.SqlDbType.TinyInt).Value = (byte)stop.ArrivalDay;
                    cmd.Parameters.Add("ArrivalTime", System.Data.SqlDbType.Time).Value = stop.ArrivalTime;
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.RemoveAt("ScheduleId");
                    cmd.Parameters.RemoveAt("LocationId");
                    cmd.Parameters.RemoveAt("DepartureDay");
                    cmd.Parameters.RemoveAt("DepartureTime");
                    cmd.Parameters.RemoveAt("ArrivalDay");
                    cmd.Parameters.RemoveAt("ArrivalTime");
                }
                else
                {
                    cmd.CommandText = "dbo.usp_ScheduleStop_Update";
                    cmd.Parameters.Add("ScheduleStopId", System.Data.SqlDbType.Int).Value = stop.ScheduleStopId;
                    cmd.Parameters.Add("ScheduleId", System.Data.SqlDbType.Int).Value = data.ScheduleId;
                    cmd.Parameters.Add("LocationId", System.Data.SqlDbType.Int).Value = stop.LocationId;
                    cmd.Parameters.Add("DepartureDay", System.Data.SqlDbType.TinyInt).Value = (byte)stop.DepartureDay;
                    cmd.Parameters.Add("DepartureTime", System.Data.SqlDbType.Time).Value = stop.DepartureTime;
                    cmd.Parameters.Add("ArrivalDay", System.Data.SqlDbType.TinyInt).Value = (byte)stop.ArrivalDay;
                    cmd.Parameters.Add("ArrivalTime", System.Data.SqlDbType.Time).Value = stop.ArrivalTime;
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.RemoveAt("ScheduleStopId");
                    cmd.Parameters.RemoveAt("ScheduleId");
                    cmd.Parameters.RemoveAt("LocationId");
                    cmd.Parameters.RemoveAt("DepartureDay");
                    cmd.Parameters.RemoveAt("DepartureTime");
                    cmd.Parameters.RemoveAt("ArrivalDay");
                    cmd.Parameters.RemoveAt("ArrivalTime");
                }
            }
        }
    }
}
