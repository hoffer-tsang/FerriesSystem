/*==============================================================================
 *
 * Business class for Employee Company View
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
    /// Contain all business logic required for employee company
    /// </summary>
    public class EmployeeCompany
    {
        private string _ConnectionString = ConfigurationManager
            .ConnectionStrings["DefaultConnection"].ConnectionString;
        private int _ItemPerPage;
        private int _MaxPageNumber;

        /// <summary>
        /// Constructor of Employee Company class
        /// </summary>
        /// <param name="itemPerPage"> item per page allow </param>
        /// <param name="maxPageNummber"> maximum page number allow </param>
        public EmployeeCompany(int itemPerPage, int maxPageNummber)
        {
            _ItemPerPage = itemPerPage;
            _MaxPageNumber = maxPageNummber;
        }

        /// <summary>
        /// Get the list of company that match search result
        /// </summary>
        /// <param name="search"> company name </param>
        /// <param name="pageNumber"> page number to search </param>
        /// <param name="isAsec"> asecending order or not </param>
        /// <returns> a list of company to be display </returns>
        public List<GetCompanyListModel> GetCompanyList(string search, int pageNumber, int isAsec)
        {
            using(var conn = new SqlConnection(this._ConnectionString))
            using(var cmd = new SqlCommand("dbo.usp_CompaniesList_Get", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if(!String.IsNullOrWhiteSpace(search))
                {
                    cmd.Parameters.Add("CompanyName", System.Data.SqlDbType.NVarChar,256).Value = search;
                }
                cmd.Parameters.Add("PageNumber", System.Data.SqlDbType.Int).Value = pageNumber;
                cmd.Parameters.Add("ItemPerPage", System.Data.SqlDbType.Int).Value = _ItemPerPage;
                cmd.Parameters.Add("IsAsec", System.Data.SqlDbType.Bit).Value = isAsec;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    var companyList = new List<GetCompanyListModel>();
                    while (reader.Read())
                    {
                        companyList.Add(GetCompanyListRecord(reader));
                    }
                    return companyList;
                }
            }
        }

        /// <summary>
        /// Get list of company with potential option of extra item per page 
        /// </summary>
        /// <param name="search"> company name </param>
        /// <param name="pageNumber"> page number to search </param>
        /// <param name="isAsec"> asecending order or not </param>
        /// <param name="itemPerPage"> item to be display in the list </param>
        /// <returns> a list of company to be display </returns>
        public List<GetCompanyListModel> GetCompanyList(string search, int pageNumber, int isAsec, int itemPerPage)
        {
            using (var conn = new SqlConnection(this._ConnectionString))
            using (var cmd = new SqlCommand("dbo.usp_CompaniesList_Get", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (!String.IsNullOrWhiteSpace(search))
                {
                    cmd.Parameters.Add("CompanyName", System.Data.SqlDbType.NVarChar, 256).Value = search;
                }
                cmd.Parameters.Add("PageNumber", System.Data.SqlDbType.Int).Value = pageNumber;
                cmd.Parameters.Add("ItemPerPage", System.Data.SqlDbType.Int).Value = itemPerPage;
                cmd.Parameters.Add("IsAsec", System.Data.SqlDbType.Bit).Value = isAsec;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    var companyList = new List<GetCompanyListModel>();
                    while (reader.Read())
                    {
                        companyList.Add(GetCompanyListRecord(reader));
                    }
                    return companyList;
                }
            }
        }

        /// <summary>
        /// get the pagination list to display  
        /// </summary>
        /// <param name="pageNumber"> current page number </param>
        /// <param name="maxPageTotal"> maximum page avaialable </param>
        /// <returns> a list of page number </returns>
        /// <exception cref="Exception"> error occur when generating page list </exception>
        public List<int> GetCompanyPagination(int pageNumber, int maxPageTotal)
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
        /// get the new max page number with a speicif search filter 
        /// </summary>
        /// <param name="search"> search parameters </param>
        /// <returns> int of max page number </returns>
        public int GetNewMaxPageNumber(string search)
        {
            using (var conn = new SqlConnection(this._ConnectionString))
            using (var cmd = new SqlCommand("dbo.usp_CompaniesList_Get", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (!String.IsNullOrWhiteSpace(search))
                {
                    cmd.Parameters.Add("CompanyName", System.Data.SqlDbType.NVarChar, 256).Value = search;
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
        /// Get the company detail with the company id 
        /// </summary>
        /// <param name="companyId"> id of the company </param>
        /// <returns> the company details in get company class, null if no compnay data is found  </returns>
        public GetCompany GetCompanyDetail(int companyId)
        {
            using (var conn = new SqlConnection(this._ConnectionString))
            using (var cmd = new SqlCommand("dbo.usp_Company_Get", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("CompanyId", System.Data.SqlDbType.Int).Value = companyId;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var companyDetail = new GetCompany
                        {
                            CompanyName = (string)reader["Name"],
                            RowVersion = (byte[])reader["RowVersion"]
                        };
                        return companyDetail;
                    }
                    return null;
                }
            }
        }

        /// <summary>
        /// add the company to the database 
        /// </summary>
        /// <param name="companyName"> name of the company </param>
        /// <returns> return 0 if fail, return 1 if sucess</returns>
        public int AddCompany(string companyName)
        {
            using (var conn = new SqlConnection(this._ConnectionString))
            using (var cmd = new SqlCommand("dbo.usp_Company_Insert", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("Name", System.Data.SqlDbType.NVarChar,256).Value = companyName;
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
        /// Update the company details in database 
        /// </summary>
        /// <param name="companyId"> company id to update </param>
        /// <param name="companyName"> company name </param>
        /// <param name="rowVersion"> rowVersion to update </param>
        /// <returns> return -1 if rowVersion error no update, return 0 if fail, return 1 if success </returns>
        public int EditCompany(int companyId, string companyName, byte[] rowVersion)
        {
            using (var conn = new SqlConnection(this._ConnectionString))
            using (var cmd = new SqlCommand("dbo.usp_Company_Update", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("Name", System.Data.SqlDbType.NVarChar, 256).Value = companyName;
                cmd.Parameters.Add("CompanyId", System.Data.SqlDbType.Int).Value = companyId;
                cmd.Parameters.Add("RowVersion", System.Data.SqlDbType.Timestamp).Value = rowVersion;
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
        /// delete the company in sql 
        /// </summary>
        /// <param name="companyId"> id of the company </param>
        /// <returns> return 0 if fail, return 1 if success </returns>
        public int DeleteCompany(int companyId) 
        {
            using (var conn = new SqlConnection(this._ConnectionString))
            using (var cmd = new SqlCommand("dbo.usp_Company_Delete", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
        /// get sql data to get company list model 
        /// </summary>
        /// <param name="record"> sql data </param>
        /// <returns> company list model </returns>
        private GetCompanyListModel GetCompanyListRecord(IDataRecord record)
        {
            return new GetCompanyListModel
            {
                CompanyId = (int)record["CompanyId"],
                CompanyName = (string)record["Name"]
            };
        }
    }
}
