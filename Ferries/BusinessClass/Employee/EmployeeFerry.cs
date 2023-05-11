/*==============================================================================
 *
 * Business class for Employee Ferry View
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
    /// Contain all business logic required for employee ferry page 
    /// </summary>
    public class EmployeeFerry
    {
        private string _ConnectionString = ConfigurationManager
            .ConnectionStrings["DefaultConnection"].ConnectionString;
        private int _ItemPerPage;
        private int _MaxPageNumber;

        /// <summary>
        /// Constructor of Employee ferry class
        /// </summary>
        /// <param name="itemPerPage"> item per page allow </param>
        /// <param name="maxPageNummber"> maximum page number allow </param>
        public EmployeeFerry(int itemPerPage, int maxPageNummber)
        {
            _ItemPerPage = itemPerPage;
            _MaxPageNumber = maxPageNummber;
        }
        /// <summary>
        /// Get the list of ferry that match search result
        /// </summary>
        /// <param name="ferryName"> ferryName to be search </param>
        /// <param name="companyName"> company Name to search</param>
        /// <param name="pageNumber"> page number to display </param>
        /// <param name="isAsec"> ascending order or not </param>
        /// <param name="sortColumn"> the column to sort by </param>
        /// <returns> return a list of gerry model </returns>
        public List<GetFerryListModel> GetFerryList(string ferryName, string companyName, int pageNumber, int isAsec, string sortColumn)
        {
            using (var conn = new SqlConnection(this._ConnectionString))
            using (var cmd = new SqlCommand("dbo.usp_FerriesList_Get", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (!String.IsNullOrWhiteSpace(ferryName))
                {
                    cmd.Parameters.Add("FerryName", System.Data.SqlDbType.NVarChar, 256).Value = ferryName;
                }
                if (!String.IsNullOrWhiteSpace(companyName))
                {
                    cmd.Parameters.Add("CompanyName", System.Data.SqlDbType.NVarChar, 256).Value = companyName;
                }
                if (!String.IsNullOrWhiteSpace(sortColumn))
                {
                    if(sortColumn == "CompanyName")
                    {
                        cmd.Parameters.Add("ColumnSorting", System.Data.SqlDbType.NVarChar, 256).Value = "Company.Name";
                    }
                    else
                    {
                        cmd.Parameters.Add("ColumnSorting", System.Data.SqlDbType.NVarChar, 256).Value = "Ferry.Name";
                    }
                }
                cmd.Parameters.Add("PageNumber", System.Data.SqlDbType.Int).Value = pageNumber;
                cmd.Parameters.Add("ItemPerPage", System.Data.SqlDbType.Int).Value = _ItemPerPage;
                cmd.Parameters.Add("IsAsec", System.Data.SqlDbType.Bit).Value = isAsec;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    var ferryList = new List<GetFerryListModel>();
                    while (reader.Read())
                    {
                        ferryList.Add(GetFerryListRecord(reader));
                    }
                    return ferryList;
                }
            }
        }

        /// <summary>
        /// get the pagination for ferry 
        /// </summary>
        /// <param name="pageNumber"> current page number </param>
        /// <param name="maxPageTotal"> total max page allow </param>
        /// <returns> a list of page number allow </returns>
        /// <exception cref="Exception"> error occur on generating page number list </exception>
        public List<int> GetFerryPagination(int pageNumber, int maxPageTotal)
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
        /// get max page number on current filter 
        /// </summary>
        /// <param name="ferryName"> ferryname to search </param>
        /// <param name="companyName"> company name to search </param>
        /// <returns> max page number available </returns>
        public int GetNewMaxPageNumber(string ferryName, string companyName)
        {
            using (var conn = new SqlConnection(this._ConnectionString))
            using (var cmd = new SqlCommand("dbo.usp_FerriesList_Get", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (!String.IsNullOrWhiteSpace(ferryName))
                {
                    cmd.Parameters.Add("FerryName", System.Data.SqlDbType.NVarChar, 256).Value = ferryName;
                }
                if (!String.IsNullOrWhiteSpace(companyName))
                {
                    cmd.Parameters.Add("CompanyName", System.Data.SqlDbType.NVarChar, 256).Value = companyName;
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
                    if(totalData % _ItemPerPage != 0)
                    {
                        totalPageNumber++;
                    }
                    return totalPageNumber;
                }
            }
        }

        /// <summary>
        /// get the ferry detail with ferry id
        /// </summary>
        /// <param name="ferryId"> ferry id </param>
        /// <returns> return the ferry detail, if not found return null</returns>
        public GetFerry GetFerryDetail(int ferryId)
        {
            using (var conn = new SqlConnection(this._ConnectionString))
            using (var cmd = new SqlCommand("dbo.usp_Ferry_Get", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("FerryId", System.Data.SqlDbType.Int).Value = ferryId;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var ferryDetail = new GetFerry
                        {
                            FerryName = (string)reader[1],
                            CompanyId = (int)reader["CompanyId"],
                            CompanyName = (string)reader[4],
                            RowVersion = (byte[])reader["RowVersion"]
                        };
                        return ferryDetail;
                    }
                    return null;
                }
            }
        }

        /// <summary>
        /// add the ferry in the database  
        /// </summary>
        /// <param name="ferryName"> ferry name</param>
        /// <param name="companyId"> company id </param>
        /// <returns> return 1 if success, 0 if fail </returns>
        public int AddFerry(string ferryName, int companyId)
        {
            using (var conn = new SqlConnection(this._ConnectionString))
            using (var cmd = new SqlCommand("dbo.usp_Ferry_Insert", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("Name", System.Data.SqlDbType.NVarChar, 256).Value = ferryName;
                cmd.Parameters.Add("CompanyId", System.Data.SqlDbType.Int).Value = companyId;
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
        /// edit the ferry in database
        /// </summary>
        /// <param name="ferryId"> ferry id</param>
        /// <param name="ferryName"> ferry name</param>
        /// <param name="companyId"> company id </param>
        /// <param name="rowVersion"> row version of current detail </param>
        /// <returns> return 1 if success, 0 if fail, -1 if concurrency error as nothing update </returns>
        public int EditFerry(int ferryId, string ferryName, int companyId, byte[] rowVersion)
        {
            using (var conn = new SqlConnection(this._ConnectionString))
            using (var cmd = new SqlCommand("dbo.usp_Ferry_Update", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("FerryId", System.Data.SqlDbType.Int).Value = ferryId;
                cmd.Parameters.Add("FerryName", System.Data.SqlDbType.NVarChar, 256).Value = ferryName;
                cmd.Parameters.Add("CompanyId", System.Data.SqlDbType.Int).Value = companyId;
                cmd.Parameters.Add("FerryRowVersion", System.Data.SqlDbType.Timestamp).Value = rowVersion;
                conn.Open();
                try
                {
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        return -1;
                    }
                    return 1;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// delete the ferry in database
        /// </summary>
        /// <param name="ferryId"> ferry id </param>
        /// <returns> return 1 if success and 0 if fail </returns>
        public int DeleteFerry(int ferryId)
        {
            using (var conn = new SqlConnection(this._ConnectionString))
            using (var cmd = new SqlCommand("dbo.usp_Ferry_Delete", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("FerryId", System.Data.SqlDbType.Int).Value = ferryId;
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
        /// convert sql data to get ferry list model
        /// </summary>
        /// <param name="record"> sql data </param>
        /// <returns> ferry list detail </returns>
        private GetFerryListModel GetFerryListRecord(IDataRecord record)
        {
            return new GetFerryListModel
            {
                FerryId = (int)record["FerryId"],
                FerryName = (string)record["Name"],
                CompanyName = (string)record[4]
            };
        }
    }
}
