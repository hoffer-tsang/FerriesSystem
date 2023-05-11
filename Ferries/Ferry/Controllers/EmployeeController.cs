/*==============================================================================
 *
 * Employee Controller Class, all action for employee
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using BusinessClass.Employee;
using BusinessClass.Model;
using Ferry.ViewModel;
using Ferry.ViewModel.SubViewModel;
using Newtonsoft.Json;

namespace Ferry.Controllers
{
    /// <summary>
    /// Employee Class Controller 
    /// </summary>
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        // GET: Employee
        private int _ItemPerPage = 5;
        private int _MaxPageNumber = 5;

        /// <summary>
        /// Employee Home Page Action
        /// </summary>
        /// <returns> Home Page of employee </returns>
        [HttpGet]
        public ActionResult Home()
        {
            return View();
        }

        /// <summary>
        /// Employee Company Page Action
        /// </summary>
        /// <returns> the default company page </returns>
        [HttpGet]
        public ActionResult Company()
        {
            var view = NewCompanyPageConstructor();
            return View(view);
        }

        /// <summary>
        /// Sort the Company list
        /// </summary>
        /// <param name="searchValue"> previous search company name</param>
        /// <param name="sortOrder"> new sortOrder of the list </param>
        /// <returns> Company Page with new sort order </returns>
        [HttpGet]
        public ActionResult SortCompany(string searchValue, int sortOrder)
        {
            if (sortOrder == 1)
            {
                sortOrder = 0;
            }
            else if (sortOrder == 0)
            {
                sortOrder = 1;
            }
            var view = new EmployeeCompanyViewModel();
            var getCompany = new EmployeeCompany(_ItemPerPage, _MaxPageNumber);
            var companyDataList = getCompany.GetCompanyList(searchValue, 1, sortOrder);
            ViewBag.SortOrder = sortOrder;
            ViewBag.Search = searchValue;
            ViewBag.CurrentPage = 1;
            var maxPageTotal = getCompany.GetNewMaxPageNumber(searchValue);
            ViewBag.MaxPageNumber = maxPageTotal;
            view.CompanyList = CompanyDataToList(companyDataList);
            view.PageList = getCompany.GetCompanyPagination(1, maxPageTotal);
            return View("Company", view);
        }

        /// <summary>
        /// Perform the search of the company 
        /// </summary>
        /// <param name="viewModel"> the view model of the company page </param>
        /// <returns> company page with new search filter list </returns>
        [HttpPost]
        public ActionResult SearchCompany(EmployeeCompanyViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var viewCompany = NewCompanyPageConstructor();
                return View("Company", viewCompany);
            }
            var view = new EmployeeCompanyViewModel();
            var getCompany = new EmployeeCompany(_ItemPerPage, _MaxPageNumber);
            var companyDataList = getCompany.GetCompanyList(viewModel.Search, 1, 1);
            ViewBag.SortOrder = 1;
            ViewBag.Search = viewModel.Search;
            ViewBag.CurrentPage = 1;
            var maxPageTotal = getCompany.GetNewMaxPageNumber(viewModel.Search);
            ViewBag.MaxPageNumber = maxPageTotal;
            view.CompanyList = CompanyDataToList(companyDataList);
            view.PageList = getCompany.GetCompanyPagination(1, maxPageTotal);
            return View("Company", view);
        }

        /// <summary>
        /// page navigation for company list 
        /// </summary>
        /// <param name="searchValue"> the previous search value </param>
        /// <param name="pageNumber"> the page number to navigate </param>
        /// <param name="sortOrder"> the previous sort order </param>
        /// <returns> company page list with new page </returns>
        [HttpGet]
        public ActionResult PageNavigation(string searchValue, int pageNumber, int sortOrder)
        {
            var view = new EmployeeCompanyViewModel();
            var getCompany = new EmployeeCompany(_ItemPerPage, _MaxPageNumber);
            var companyDataList = getCompany.GetCompanyList(searchValue, pageNumber, sortOrder);
            ViewBag.SortOrder = sortOrder;
            ViewBag.Search = searchValue;
            ViewBag.CurrentPage = pageNumber;
            var maxPageTotal = getCompany.GetNewMaxPageNumber(searchValue);
            ViewBag.MaxPageNumber = maxPageTotal;
            view.CompanyList = CompanyDataToList(companyDataList);
            view.PageList = getCompany.GetCompanyPagination(pageNumber, maxPageTotal);
            return View("Company", view);
        }

        /// <summary>
        /// Navigate to edit company with add company setting
        /// </summary>
        /// <returns> edit company page  </returns>
        [HttpGet]
        public ActionResult AddEditCompany()
        {
            var view = new EmployeeCompanyAddEditViewModel
            {
                CompanyId = -1
            };
            return View("AddEditCompany", view);
        }

        /// <summary>
        /// navigate to edit company page with edit comapny setting and details to populate on the page 
        /// </summary>
        /// <param name="id"> company id </param>
        /// <returns> edit company page  </returns>
        [HttpGet]
        public ActionResult EditCompany(int id)
        {
            var getCompany = new EmployeeCompany(_ItemPerPage, _MaxPageNumber);
            var view = new EmployeeCompanyAddEditViewModel
            {
                CompanyId = id
            };
            var companyDetail = getCompany.GetCompanyDetail(id);
            view.CompanyName = companyDetail.CompanyName;
            view.RowVersion = (byte[])companyDetail.RowVersion;
            return View("AddEditCompany", view);
        }

        /// <summary>
        /// update action to update the company 
        /// </summary>
        /// <param name="view"> the edit company view model </param>
        /// <returns> company page if success, back to edit page with error message if fail </returns>
        [HttpPost]
        public ActionResult UpdateCompany(EmployeeCompanyAddEditViewModel view)
        {
            if (!ModelState.IsValid)
            {
                return View("AddEditCompany", view);
            }
            var companyBusinessClass = new EmployeeCompany(_ItemPerPage, _MaxPageNumber);
            var id = view.CompanyId;
            if (id < 0)
            {
                var result = companyBusinessClass.AddCompany(view.CompanyName);
                if (result != 1)
                {
                    view.StandardError = 1;
                    return View("AddEditCompany", view);
                }
                else
                {
                    var viewCompany = NewCompanyPageConstructor();
                    return View("Company", viewCompany);
                }
            }
            else
            {
                var result = companyBusinessClass.EditCompany(view.CompanyId, view.CompanyName, view.RowVersion);
                if (result == 0)
                {
                    view.StandardError = 1;
                    return View("AddEditCompany", view);
                }
                else if (result == -1)
                {
                    view.ConcurrencyError = 1;
                    return View("AddEditCompany", view);
                }
                else
                {
                    var viewCompany = NewCompanyPageConstructor();
                    return View("Company", viewCompany);
                }
            }
        }

        /// <summary>
        /// delete a company 
        /// </summary>
        /// <param name="viewModel"> the view model of company page </param>
        /// <returns> company page and refresh company list display, if fail display error message </returns>
        [HttpPost]
        public ActionResult DeleteCompany(EmployeeCompanyViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var viewCompanyEmpty = NewCompanyPageConstructor();
                viewCompanyEmpty.GeneralError = 1;
                return View("Company", viewCompanyEmpty);
            }
            var viewCompany = new EmployeeCompanyViewModel();
            var companyBusinessClass = new EmployeeCompany(_ItemPerPage, _MaxPageNumber);
            var result = companyBusinessClass.DeleteCompany((int)viewModel.HiddenCompanyId);
            if (result == 0)
            {
                viewCompany.GeneralError = 1;
            }
            ViewBag.SortOrder = 1;
            ViewBag.CurrentPage = 1;
            var maxPageTotal = companyBusinessClass.GetNewMaxPageNumber(null);
            ViewBag.MaxPageNumber = maxPageTotal;
            viewCompany.CompanyList = CompanyDataToList(companyBusinessClass.GetCompanyList(null, 1, 1));
            viewCompany.PageList = companyBusinessClass.GetCompanyPagination(1, maxPageTotal);
            return View("Company", viewCompany);
        }

        /// <summary>
        /// Ferry Page Action
        /// </summary>
        /// <returns> the Ferry Page with defualt settings </returns>
        [HttpGet]
        public ActionResult Ferry()
        {
            var view = NewFerryPageConstructor();
            return View(view);
        }

        /// <summary>
        /// perform the search ferry
        /// </summary>
        /// <param name="viewModel"> view model of ferry page</param>
        /// <returns> the filter ferry page </returns>
        [HttpPost]
        public ActionResult SearchFerry(EmployeeFerryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var viewFerry = NewFerryPageConstructor();
                return View("Ferry", viewFerry);
            }
            var view = new EmployeeFerryViewModel();
            var ferryBusinessClass = new EmployeeFerry(_ItemPerPage, _MaxPageNumber);
            var ferryDataList = ferryBusinessClass.GetFerryList(viewModel.FerryNameSearch, viewModel.CompanyNameSearch, 1, 1, "FerryName");
            ViewBag.CurrentPage = 1;
            ViewBag.SortColumn = "FerryName";
            ViewBag.SortOrder = 1;
            ViewBag.SearchFerry = viewModel.FerryNameSearch;
            ViewBag.SearchCompany = viewModel.CompanyNameSearch;
            var maxPageTotal = ferryBusinessClass.GetNewMaxPageNumber(viewModel.FerryNameSearch, viewModel.CompanyNameSearch);
            ViewBag.MaxPageNumber = maxPageTotal;
            view.FerryList = FerryDataToList(ferryDataList);
            view.PageList = ferryBusinessClass.GetFerryPagination(1, maxPageTotal);
            return View("Ferry", view);
        }

        /// <summary>
        /// navigate to edit ferry page with add setting 
        /// </summary>
        /// <returns> Add ferry page </returns>
        [HttpGet]
        public ActionResult AddEditFerry()
        {
            var view = new EmployeeFerryAddEditViewModel
            {
                FerryId = -1
            };
            var companyBusiness = new EmployeeCompany(_ItemPerPage, _MaxPageNumber);
            var companyDataList = CompanyDataToList(companyBusiness.GetCompanyList(null, 1, 1, 9999));
            view.CompanyList = companyDataList;
            return View("AddEditFerry", view);
        }

        /// <summary>
        /// navigate to ferry details page that display scheudle 
        /// </summary>
        /// <param name="id"> ferry id </param>
        /// <returns> ferry detials page </returns>
        [HttpGet]
        public ActionResult FerryDetails(int id)
        {
            var view = NewFerryDetailPageConstructor(id);
            return View("FerryDetails", view);
        }

        /// <summary>
        /// edit the ferry to edit ferry page 
        /// </summary>
        /// <param name="id"> ferry id </param>
        /// <returns> edit ferry page </returns>
        [HttpGet]
        public ActionResult EditFerry(int id)
        {
            var view = new EmployeeFerryAddEditViewModel
            {
                FerryId = id
            };
            var companyBusiness = new EmployeeCompany(_ItemPerPage, _MaxPageNumber);
            var companyDataList = CompanyDataToList(companyBusiness.GetCompanyList(null, 1, 1, 9999));
            view.CompanyList = companyDataList;
            var ferryBusiness = new EmployeeFerry(_ItemPerPage, _MaxPageNumber);
            var ferryDetail = ferryBusiness.GetFerryDetail(id);
            view.FerryName = ferryDetail.FerryName;
            view.RowVersion = ferryDetail.RowVersion;
            foreach (var company in view.CompanyList)
            {
                if (company.CompanyId == ferryDetail.CompanyId)
                {
                    view.CompanyId = company.CompanyId;
                    break;
                }
            }
            return View("AddEditFerry", view);
        }

        /// <summary>
        /// add or update the ferry with details 
        /// </summary>
        /// <param name="viewModel"> add or update ferry view model </param>
        /// <returns> ferry page if success, if error occur display error message with add edit ferry </returns>
        [HttpPost]
        public ActionResult UpdateFerry(EmployeeFerryAddEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var companyBusiness2 = new EmployeeCompany(_ItemPerPage, _MaxPageNumber);
                var companyDataList2 = CompanyDataToList(companyBusiness2.GetCompanyList(null, 1, 1, 9999));
                viewModel.CompanyList = companyDataList2;
                return View("AddEditFerry", viewModel);
            }
            var companyBusiness = new EmployeeCompany(_ItemPerPage, _MaxPageNumber);
            var companyDataList = CompanyDataToList(companyBusiness.GetCompanyList(null, 1, 1, 9999));
            viewModel.CompanyList = companyDataList;
            var ferryBusiness = new EmployeeFerry(_ItemPerPage, _MaxPageNumber);
            var id = viewModel.FerryId;
            if (id < 0)
            {
                var result = ferryBusiness.AddFerry(viewModel.FerryName, viewModel.CompanyId);
                if (result != 1)
                {
                    viewModel.StandardError = 1;
                    return View("AddEditFerry", viewModel);
                }
                else
                {
                    var view = NewFerryPageConstructor();
                    return View("Ferry", view);
                }
            }
            else
            {
                var result = ferryBusiness.EditFerry(viewModel.FerryId, viewModel.FerryName, viewModel.CompanyId, viewModel.RowVersion);
                if (result == 0)
                {
                    viewModel.StandardError = 1;
                    return View("AddEditFerry", viewModel);
                }
                else if (result == -1)
                {
                    viewModel.ConcurrencyError = 1;
                    return View("AddEditFerry", viewModel);
                }
                else
                {
                    var view = NewFerryPageConstructor();
                    return View("Ferry", view);
                }
            }
        }

        /// <summary>
        /// delete the ferry 
        /// </summary>
        /// <param name="viewModel"> ferry page view model </param>
        /// <returns> ferry page, if fail return along with error message </returns>
        [HttpPost]
        public ActionResult DeleteFerry(EmployeeFerryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var viewFerry2 = NewFerryPageConstructor();
                viewFerry2.GeneralError = 1;
                return View("Ferry", viewFerry2);
            }
            var viewFerry = new EmployeeFerryViewModel();
            var ferryBusinessClass = new EmployeeFerry(_ItemPerPage, _MaxPageNumber);
            var result = ferryBusinessClass.DeleteFerry((int)viewModel.HiddenFerryId);
            if (result == 0)
            {
                viewFerry.GeneralError = 1;
            }
            var ferryDataList = ferryBusinessClass.GetFerryList(null, null, 1, 1, "FerryName");
            ViewBag.CurrentPage = 1;
            ViewBag.SortColumn = "FerryName";
            ViewBag.SortOrder = 1;
            ViewBag.SearchFerry = null;
            ViewBag.SearchCompany = null;
            var maxPageTotal = ferryBusinessClass.GetNewMaxPageNumber(null, null);
            ViewBag.MaxPageNumber = maxPageTotal;
            viewFerry.FerryList = FerryDataToList(ferryDataList);
            viewFerry.PageList = ferryBusinessClass.GetFerryPagination(1, maxPageTotal);
            return View("Ferry", viewFerry);
        }

        /// <summary>
        /// perform the sort of the ferry 
        /// </summary>
        /// <param name="sortColumn"> current sorting column </param>
        /// <param name="lastSortColumn"> previous sorting column </param>
        /// <param name="sortOrder"> the sort order to perform </param>
        /// <param name="searchFerry"> ferry filter </param>
        /// <param name="searchCompany"> company filter </param>
        /// <returns> ferry page with sort ferry list </returns>
        [HttpGet]
        public ActionResult SortFerry(string sortColumn, string lastSortColumn, int sortOrder, string searchFerry, string searchCompany)
        {
            if (sortColumn == lastSortColumn)
            {
                if (sortOrder == 1)
                {
                    sortOrder = 0;
                }
                else
                {
                    sortOrder = 1;
                }
            }
            else
            {
                sortOrder = 1;
            }
            var view = new EmployeeFerryViewModel();
            var ferryBusinessClass = new EmployeeFerry(_ItemPerPage, _MaxPageNumber);
            var ferryDataList = ferryBusinessClass.GetFerryList(searchFerry, searchCompany, 1, sortOrder, sortColumn);
            ViewBag.CurrentPage = 1;
            ViewBag.SortColumn = sortColumn;
            ViewBag.SortOrder = sortOrder;
            ViewBag.SearchFerry = searchFerry;
            ViewBag.SearchCompany = searchCompany;
            var maxPageTotal = ferryBusinessClass.GetNewMaxPageNumber(searchFerry, searchCompany);
            ViewBag.MaxPageNumber = maxPageTotal;
            view.FerryList = FerryDataToList(ferryDataList);
            view.PageList = ferryBusinessClass.GetFerryPagination(1, maxPageTotal);
            return View("Ferry", view);
        }

        /// <summary>
        /// show another page of the ferry list 
        /// </summary>
        /// <param name="lastSortColumn"> preivous sorting column </param>
        /// <param name="sortOrder"> previous sorting order </param>
        /// <param name="searchFerry"> ferry name fitler </param>
        /// <param name="searchCompany"> company name filter </param>
        /// <param name="pageNumber"> page number to navigate </param>
        /// <returns> ferry page with new page of ferry list </returns>
        [HttpGet]
        public ActionResult PageFerry(string lastSortColumn, int sortOrder, string searchFerry, string searchCompany, int pageNumber)
        {
            var view = new EmployeeFerryViewModel();
            var ferryBusinessClass = new EmployeeFerry(_ItemPerPage, _MaxPageNumber);
            var ferryDataList = ferryBusinessClass.GetFerryList(searchFerry, searchCompany, pageNumber, sortOrder, lastSortColumn);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.SortColumn = lastSortColumn;
            ViewBag.SortOrder = sortOrder;
            ViewBag.SearchFerry = searchFerry;
            ViewBag.SearchCompany = searchCompany;
            var maxPageTotal = ferryBusinessClass.GetNewMaxPageNumber(searchFerry, searchCompany);
            ViewBag.MaxPageNumber = maxPageTotal;
            view.FerryList = FerryDataToList(ferryDataList);
            view.PageList = ferryBusinessClass.GetFerryPagination(pageNumber, maxPageTotal);
            return View("Ferry", view);
        }

        /// <summary>
        /// perform the schedule search on ferry detials page 
        /// </summary>
        /// <param name="viewModel"> ferry details page </param>
        /// <returns> ferry details page, if model state invalid display error message </returns>
        [HttpPost]
        public ActionResult SearchSchedule(EmployeeFerryDetailViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var viewSchedule2 = NewFerryDetailPageConstructor(viewModel.FerryId);
                viewSchedule2.GeneralError = 1;
                return View("FerryDetails", viewSchedule2);
            }
            var view = new EmployeeFerryDetailViewModel();
            var ferryDetailBusinessClass = new EmployeeSchedule(_ItemPerPage, _MaxPageNumber);
            var ferryBusinessClass = new EmployeeFerry(_ItemPerPage, _MaxPageNumber);
            var ferryDetail = ferryBusinessClass.GetFerryDetail(viewModel.FerryId);
            view.FerryName = ferryDetail.FerryName;
            view.CompanyName = ferryDetail.CompanyName;
            view.LocationList = LocationDataToList(ferryDetailBusinessClass.GetLocationList(true));
            view.ScheduleList = ScheduleDataToList(ferryDetailBusinessClass.GetScheduleList(
                viewModel.FerryId, viewModel.DepartureDay, viewModel.DepartureLocationId, viewModel.ArrivalDay, viewModel.ArrivalLocationId, 1, 1, "DepartureLocation"));
            view.FerryId = viewModel.FerryId;
            ViewBag.CurrentPage = 1;
            ViewBag.SortColumn = "DepartureLocation";
            ViewBag.SortOrder = 1;
            ViewBag.SearchDepartureLocation = viewModel.DepartureLocationId;
            ViewBag.SearchDepartureDay = viewModel.DepartureDay;
            ViewBag.SearchArrivalLocation = viewModel.ArrivalLocationId;
            ViewBag.SearchArrivalDay = viewModel.ArrivalDay;
            var maxPageTotal = ferryDetailBusinessClass.GetNewMaxPageNumber(
                viewModel.FerryId, viewModel.DepartureDay, viewModel.DepartureLocationId, viewModel.ArrivalDay, viewModel.ArrivalLocationId);
            ViewBag.MaxPageNumber = maxPageTotal;
            view.PageList = ferryDetailBusinessClass.GetSchedulePagination(1, maxPageTotal);
            return View("FerryDetails", view);
        }

        /// <summary>
        /// perform the sort of the schedule list in ferry detail page 
        /// </summary>
        /// <param name="ferryId"> ferry id </param>
        /// <param name="sortColumn"> current column to sort </param>
        /// <param name="lastSortColumn"> previous sort column </param>
        /// <param name="sortOrder"> previous sort order </param>
        /// <param name="departureDay"> departure day </param>
        /// <param name="departureLocationId"> departure location id </param>
        /// <param name="arrivalDay"> arrival day </param>
        /// <param name="arrivalLocationId"> arrival location id </param>
        /// <returns> ferry details page with new sorting </returns>
        [HttpGet]
        public ActionResult SortSchedule(
            int ferryId, string sortColumn, string lastSortColumn, int sortOrder, int? departureDay, int? departureLocationId, int? arrivalDay, int? arrivalLocationId)
        {
            if (sortColumn == lastSortColumn)
            {
                if (sortOrder == 1)
                {
                    sortOrder = 0;
                }
                else
                {
                    sortOrder = 1;
                }
            }
            else
            {
                sortOrder = 1;
            }
            var view = new EmployeeFerryDetailViewModel();
            var ferryDetailBusinessClass = new EmployeeSchedule(_ItemPerPage, _MaxPageNumber);
            var ferryBusinessClass = new EmployeeFerry(_ItemPerPage, _MaxPageNumber);
            var ferryDetail = ferryBusinessClass.GetFerryDetail(ferryId);
            view.FerryName = ferryDetail.FerryName;
            view.CompanyName = ferryDetail.CompanyName;
            view.LocationList = LocationDataToList(ferryDetailBusinessClass.GetLocationList(true));
            view.ScheduleList = ScheduleDataToList(ferryDetailBusinessClass.GetScheduleList(
                ferryId, departureDay, departureLocationId, arrivalDay, arrivalLocationId, 1, sortOrder, sortColumn));
            view.FerryId = ferryId;
            ViewBag.CurrentPage = 1;
            ViewBag.SortColumn = sortColumn;
            ViewBag.SortOrder = sortOrder;
            ViewBag.SearchDepartureLocation = departureLocationId;
            ViewBag.SearchDepartureDay = departureDay;
            ViewBag.SearchArrivalLocation = arrivalLocationId;
            ViewBag.SearchArrivalDay = arrivalDay;
            var maxPageTotal = ferryDetailBusinessClass.GetNewMaxPageNumber(
                ferryId, departureDay, departureLocationId, arrivalDay, arrivalLocationId);
            ViewBag.MaxPageNumber = maxPageTotal;
            view.PageList = ferryDetailBusinessClass.GetSchedulePagination(1, maxPageTotal);
            return View("FerryDetails", view);
        }

        /// <summary>
        /// navigiate to new page in the schedule list 
        /// </summary>
        /// <param name="ferryId"> ferry id </param>
        /// <param name="lastSortColumn"> previous sorting column </param>
        /// <param name="sortOrder"> previous sorting order </param>
        /// <param name="departureDay"> departure day </param>
        /// <param name="departureLocationId"> deparutre location id </param>
        /// <param name="arrivalDay"> arrival day </param>
        /// <param name="arrivalLocationId"> arrival location id </param>
        /// <param name="pageNumber"> page number to navigiate </param>
        /// <returns> ferry details with new page </returns>
        [HttpGet]
        public ActionResult PageSchedule(int ferryId, string lastSortColumn, int sortOrder, int? departureDay, int? departureLocationId, int? arrivalDay, int? arrivalLocationId, int pageNumber)
        {
            var view = new EmployeeFerryDetailViewModel();
            var ferryDetailBusinessClass = new EmployeeSchedule(_ItemPerPage, _MaxPageNumber);
            var ferryBusinessClass = new EmployeeFerry(_ItemPerPage, _MaxPageNumber);
            var ferryDetail = ferryBusinessClass.GetFerryDetail(ferryId);
            view.FerryName = ferryDetail.FerryName;
            view.CompanyName = ferryDetail.CompanyName;
            view.LocationList = LocationDataToList(ferryDetailBusinessClass.GetLocationList(true));
            view.ScheduleList = ScheduleDataToList(ferryDetailBusinessClass.GetScheduleList(
                ferryId, departureDay, departureLocationId, arrivalDay, arrivalLocationId, pageNumber, sortOrder, lastSortColumn));
            view.FerryId = ferryId;
            ViewBag.CurrentPage = pageNumber;
            ViewBag.SortColumn = lastSortColumn;
            ViewBag.SortOrder = sortOrder;
            ViewBag.SearchDepartureLocation = departureLocationId;
            ViewBag.SearchDepartureDay = departureDay;
            ViewBag.SearchArrivalLocation = arrivalLocationId;
            ViewBag.SearchArrivalDay = arrivalDay;
            var maxPageTotal = ferryDetailBusinessClass.GetNewMaxPageNumber(
                ferryId, departureDay, departureLocationId, arrivalDay, arrivalLocationId);
            ViewBag.MaxPageNumber = maxPageTotal;
            view.PageList = ferryDetailBusinessClass.GetSchedulePagination(pageNumber, maxPageTotal);
            return View("FerryDetails", view);
        }

        /// <summary>
        /// to detail schedule page that display stops as well
        /// </summary>
        /// <param name="id"> schedule id </param>
        /// <param name="ferryId"> ferry id </param>
        /// <returns> scheudle details page view </returns>
        [HttpGet]
        public ActionResult DetailSchedule(int id, int ferryId)
        {
            var view = NewScheduleDetailViewModel(id, ferryId);
            return View("ScheduleDetails", view);
        }

        /// <summary>
        /// edit schedule details page 
        /// </summary>
        /// <param name="id"> schedule id </param>
        /// <param name="ferryId"> ferry id </param>
        /// <returns> edit schedule detail page view </returns>
        [HttpGet]
        public ActionResult EditScheduleDetail(int id, int ferryId)
        {
            var view = NewEditScheduleDetailViewModel(id, ferryId);
            return View("EditScheduleDetails", view);
        }

        /// <summary>
        /// add new schedule detail page 
        /// </summary>
        /// <param name="ferryId"> ferry id </param>
        /// <returns> add new schedule detail page </returns>
        [HttpGet]
        public ActionResult NewScheduleDetail(int ferryId)
        {
            var view = NewAddScheduleDetailViewModel(ferryId);
            return View("AddScheduleDetails", view);
        }

        /// <summary>
        /// delete a schedule and related schedule details 
        /// </summary>
        /// <param name="viewModel"> ferry detail page view model </param>
        /// <returns> ferry detials page, if error display with error message </returns>
        [HttpPost]
        public ActionResult DeleteSchedule(EmployeeFerryDetailViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var viewSchedule2 = NewFerryDetailPageConstructor(viewModel.FerryId);
                viewSchedule2.GeneralError = 1;
                return View("FerryDetails", viewSchedule2);
            }
            var view = new EmployeeFerryDetailViewModel();
            var ferryDetailBusinessClass = new EmployeeSchedule(_ItemPerPage, _MaxPageNumber);
            var ferryBusinessClass = new EmployeeFerry(_ItemPerPage, _MaxPageNumber);
            var result = ferryDetailBusinessClass.DeleteSchedule((int)viewModel.HiddenScheduleId);
            if (result == 0)
            {
                view.GeneralError = 1;
            }
            var ferryDetail = ferryBusinessClass.GetFerryDetail(viewModel.FerryId);
            view.FerryId = viewModel.FerryId;
            view.FerryName = ferryDetail.FerryName;
            view.CompanyName = ferryDetail.CompanyName;
            view.LocationList = LocationDataToList(ferryDetailBusinessClass.GetLocationList(true));
            view.ScheduleList = ScheduleDataToList(ferryDetailBusinessClass.GetScheduleList(viewModel.FerryId, null, null, null, null, 1, 1, "DepartureLocation"));   
            ViewBag.CurrentPage = 1;
            ViewBag.SortColumn = "DepartureLocation";
            ViewBag.SortOrder = 1;
            ViewBag.SearchDepartureLocation = null;
            ViewBag.SearchDepartureDay = null;
            ViewBag.SearchArrivalLocation = null;
            ViewBag.SearchArrivalDay = null;
            var maxPageTotal = ferryDetailBusinessClass.GetNewMaxPageNumber(viewModel.FerryId, null, null, null, null);
            ViewBag.MaxPageNumber = maxPageTotal;
            view.PageList = ferryDetailBusinessClass.GetSchedulePagination(1, maxPageTotal);
            return View("FerryDetails", view);
        }

        /// <summary>
        /// save the schedule details in add or edit schedule details page 
        /// </summary>
        /// <param name="viewModel"> schedule detail page view model </param>
        /// <returns> return to schedule details page if success, if not return to edit schedule details page with error message </returns>
        [HttpPost]
        public ActionResult SaveScheduleDetails(EmployeeScheduleDetailViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.GeneralError = 1;
                var ferryDetailBusinessClass = new EmployeeSchedule(_ItemPerPage, _MaxPageNumber);
                viewModel.LocationList = LocationDataToList(ferryDetailBusinessClass.GetLocationList(true));
                return View("EditScheduleDetails", viewModel);
            }
            else
            {
                var scheduleBusinessClass = new EmployeeSchedule(_ItemPerPage, _MaxPageNumber);
                var sortError = StopListValidation(viewModel.StopList, viewModel.FirstStop.DepartureDay, 
                    viewModel.FirstStop.DepartureTime, viewModel.LastStop.ArrivalDay, viewModel.LastStop.ArrivalTime);
                if (sortError == -1)
                {
                    viewModel.GeneralError = 1;
                    var ferryDetailBusinessClass = new EmployeeSchedule(_ItemPerPage, _MaxPageNumber);
                    viewModel.LocationList = LocationDataToList(ferryDetailBusinessClass.GetLocationList(true));
                    return View("EditScheduleDetails", viewModel);
                }
                var result = scheduleBusinessClass.EditSchedule(ScheduleViewModelToUpdateModel(viewModel));
                if (result == -1)
                {
                    viewModel.ConcurrencyError = 1;
                    var ferryDetailBusinessClass = new EmployeeSchedule(_ItemPerPage, _MaxPageNumber);
                    viewModel.LocationList = LocationDataToList(ferryDetailBusinessClass.GetLocationList(true));
                    return View("EditScheduleDetails", viewModel);
                }
                if (viewModel.ScheduleId == -1)
                {
                    viewModel.ScheduleId = result;
                }
                var view = NewScheduleDetailViewModel(viewModel.ScheduleId, viewModel.FerryId);
                return View("ScheduleDetails", view);
            }   
        }

        /// <summary>
        /// Logout and navigate to login page 
        /// </summary>
        [HttpGet]
        public void Logout()
        {
            var authenticationManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            Response.Redirect("~/Login.aspx");
        }

        /// <summary>
        /// convert business layer ferry model to presentation layer ferry detail 
        /// </summary>
        /// <param name="data"> business layer ferry model </param>
        /// <returns> presentation layer ferry model </returns>
        private List<FerryDetail> FerryDataToList(List<GetFerryListModel> data)
        {
            var view = new List<FerryDetail>();
            foreach (var d in data)
            {
                var detail = new FerryDetail
                {
                    FerryId = d.FerryId,
                    FerryName = d.FerryName,
                    CompanyName = d.CompanyName
                };
                view.Add(detail);
            }
            return view;
        }

        /// <summary>
        /// convert business layer company model to presentation layer company model
        /// </summary>
        /// <param name="data"> business layer company model </param>
        /// <returns> presentation layer company model </returns>
        private List<CompanyDetail> CompanyDataToList(List<GetCompanyListModel> data)
        {
            var view = new List<CompanyDetail>();
            foreach (var d in data)
            {
                var detail = new CompanyDetail
                {
                    CompanyId = d.CompanyId,
                    CompanyName = d.CompanyName
                };
                view.Add(detail);
            }
            return view;
        }

        /// <summary>
        /// convert between business layer location class to presentation layer loction class 
        /// </summary>
        /// <param name="data"> business layer location data list </param>
        /// <returns> presentation layer location data list </returns>
        private List<Location> LocationDataToList(List<GetLocation> data)
        {
            var view = new List<Location>();
            foreach (var d in data)
            {
                var location = new Location
                {
                    LocationId = d.LocationId,
                    LocationName = d.LocationName
                };
                view.Add(location);
            }
            return view;
        }

        /// <summary>
        /// convert business layer schedule model to presentation layer model
        /// </summary>
        /// <param name="data"> business schedule model </param>
        /// <returns> presentation layer model </returns>
        private List<Schedule> ScheduleDataToList(List<GetFerrySchedulesListModel> data)
        {
            var view = new List<Schedule>();
            foreach (var d in data)
            {
                var detail = new Schedule
                {
                    ScheduleId = d.ScheduleId,
                    DepartureLocation = d.DepartureLocation,
                    ArrivalLocation = d.ArrivalLocation
                };
                detail.DepartureDay = DayIntToString(d.DepartureDay);
                detail.ArrivalDay = DayIntToString(d.ArrivalDay);
                view.Add(detail);
            }
            return view;
        }

        /// <summary>
        /// convert business layer mid stop model to presentation layer mid stop model 
        /// </summary>
        /// <param name="data"> business layer middle stop model </param>
        /// <returns> presentation layer middle stop model </returns>
        private List<ScheduleDetail> StopDataToList(List<GetStop> data)
        {
            var view = new List<ScheduleDetail>();
            foreach (var d in data)
            {
                var detail = new ScheduleDetail
                {
                    ScheduleStopId = d.ScheduleStopId,
                    LocationId = d.LocationId,
                    Location = d.Location,
                    DepartureTime = d.DepartureTime,
                    DepartureDay = d.DepartureDay,
                    ArrivalTime = d.ArrivalTime,
                    ArrivalDay = d.ArrivalDay
                };
                detail.DepartureDayName = DayIntToString(d.DepartureDay);
                detail.ArrivalDayName = DayIntToString(d.ArrivalDay);
                view.Add(detail);
            }
            return view;
        }

        /// <summary>
        /// get the detail of first stop from schedule model 
        /// </summary>
        /// <param name="data">schedule model</param>
        /// <returns> first stop detail </returns>
        private FirstStop FirstStopDataToList(GetFerrySchedulesListModel data)
        {
            var firstStop = new FirstStop
            {
                LocationId = data.DepartureLocationId,
                Location = data.DepartureLocation,
                DepartureTime = data.DepartureTime,
                DepartureDay = data.DepartureDay
            };
            firstStop.DepartureDayName = DayIntToString(data.DepartureDay);
            return firstStop;
        }

        /// <summary>
        /// get the detail final stop from schedule model 
        /// </summary>
        /// <param name="data"> schedule model </param>
        /// <returns> last stop details </returns>
        private LastStop LastStopDataToList(GetFerrySchedulesListModel data)
        {
            
            var lastStop = new LastStop
            {
                LocationId = data.ArrivalLocationId,
                Location = data.ArrivalLocation,
                ArrivalTime = data.ArrivalTime,
                ArrivalDay = data.ArrivalDay
            };
            lastStop.ArrivalDayName = DayIntToString(data.ArrivalDay);
            return lastStop;
        }

        /// <summary>
        /// get middle stop list
        /// </summary>
        /// <param name="data">mid stop class data </param>
        /// <returns> a list of middle stop id </returns>
        private List<int> GetStopIdList (List<ScheduleDetail> data)
        {
            var list = new List<int>();
            foreach(var d in data)
            {
                list.Add(d.ScheduleStopId);
            }
            return list;
        }

        /// <summary>
        /// Convert presentation layer view model to business layer update scheudle model 
        /// </summary>
        /// <param name="viewModel"> presentation layer schedule details view model </param>
        /// <returns> business layer update scheudle model </returns>
        private UpdateSchedule ScheduleViewModelToUpdateModel(EmployeeScheduleDetailViewModel viewModel)
        {
            var updateSchedule = new UpdateSchedule
            {
                ScheduleId = viewModel.ScheduleId,
                FerryId = viewModel.FerryId,
                Description = viewModel.Description,
                CostPerPerson = viewModel.CostPerPerson,
                CostPerVehicle = viewModel.CostPerVehicle,
                RowVersion = viewModel.RowVersion,
                DepartureLocationId = viewModel.FirstStop.LocationId,
                ArrivalLocationId = viewModel.LastStop.LocationId,
                DepartureDay = viewModel.FirstStop.DepartureDay,
                DepartureTime = viewModel.FirstStop.DepartureTime,
                ArrivalDay = viewModel.LastStop.ArrivalDay,
                ArrivalTime = viewModel.LastStop.ArrivalTime            
            };
            if (viewModel.ScheduleId != -1)
            {
                updateSchedule.OriginalStopIdList = JsonConvert.DeserializeObject<List<int>>(viewModel.StopIdList);
            }
            foreach (var stop in viewModel.StopList)
            {
                updateSchedule.StopList.Add(ScheduleDetailsToStopSubClass(stop, viewModel.ScheduleId));
            }
            return updateSchedule;
        }

        /// <summary>
        /// convert from schedule detail to stop sub class
        /// </summary>
        /// <param name="data"> schedule detail </param>
        /// <param name="scheduleId"> schedule id </param>
        /// <returns> the stop sub class for business layer </returns>
        private StopSubClass ScheduleDetailsToStopSubClass(ScheduleDetail data, int scheduleId)
        {
            return new StopSubClass
            {
                ScheduleStopId = data.ScheduleStopId,
                ScheduleId = scheduleId,
                LocationId = data.LocationId,
                DepartureDay = data.DepartureDay,
                DepartureTime = data.DepartureTime,
                ArrivalDay = data.ArrivalDay,
                ArrivalTime = data.ArrivalTime
            };
        }

        /// <summary>
        /// convert the value to string 
        /// </summary>
        /// <param name="day"> day 0-6</param>
        /// <returns> Days name </returns>
        /// <exception cref="Exception"> day is not 0 - 6</exception>
        private string DayIntToString(int day)
        {
            switch (day)
            {
                case 0: 
                    return "Monday";
                case 1:
                    return "Tuesday";
                case 2:
                    return "Wednesday";
                case 3:
                    return "Thursday";
                case 4:
                    return "Friday";
                case 5:
                    return "Saturday";
                case 6:
                    return "Sunday";
                default:
                    throw new Exception("day not between 0 and 6");
            }
        }

        /// <summary>
        /// defualt company page construcotr 
        /// </summary>
        /// <returns> view model of company page </returns>
        private EmployeeCompanyViewModel NewCompanyPageConstructor()
        {
            var view = new EmployeeCompanyViewModel();
            var getCompany = new EmployeeCompany(_ItemPerPage, _MaxPageNumber);
            ViewBag.SortOrder = 1;
            ViewBag.CurrentPage = 1;
            var maxPageTotal = getCompany.GetNewMaxPageNumber(null);
            ViewBag.MaxPageNumber = maxPageTotal;
            view.CompanyList = CompanyDataToList(getCompany.GetCompanyList(null, 1, 1));
            view.PageList = getCompany.GetCompanyPagination(1, maxPageTotal);
            return view;
        }

        /// <summary>
        /// defualt ferry page constructor 
        /// </summary>
        /// <returns> view model of ferry page </returns>
        private EmployeeFerryViewModel NewFerryPageConstructor()
        {
            var view = new EmployeeFerryViewModel();
            var ferryBusinessClass = new EmployeeFerry(_ItemPerPage, _MaxPageNumber);
            var ferryDataList = ferryBusinessClass.GetFerryList(null, null, 1, 1, "FerryName");
            ViewBag.CurrentPage = 1;
            ViewBag.SortColumn = "FerryName";
            ViewBag.SortOrder = 1;
            ViewBag.SearchFerry = null;
            ViewBag.SearchCompany = null;
            var maxPageTotal = ferryBusinessClass.GetNewMaxPageNumber(null, null);
            ViewBag.MaxPageNumber = maxPageTotal;
            view.FerryList = FerryDataToList(ferryDataList);
            view.PageList = ferryBusinessClass.GetFerryPagination(1, maxPageTotal);
            return view;
        }

        /// <summary>
        /// default ferry detail page constructor 
        /// </summary>
        /// <param name="id"> ferry id </param>
        /// <returns></returns>
        private EmployeeFerryDetailViewModel NewFerryDetailPageConstructor(int id)
        {
            var view = new EmployeeFerryDetailViewModel();
            var ferryDetailBusinessClass = new EmployeeSchedule(_ItemPerPage, _MaxPageNumber);
            var ferryBusinessClass = new EmployeeFerry(_ItemPerPage, _MaxPageNumber);
            var ferryDetail = ferryBusinessClass.GetFerryDetail(id);
            view.FerryId = id;
            view.FerryName = ferryDetail.FerryName;
            view.CompanyName = ferryDetail.CompanyName;
            view.LocationList = LocationDataToList(ferryDetailBusinessClass.GetLocationList(true));
            view.ScheduleList = ScheduleDataToList(ferryDetailBusinessClass.GetScheduleList(id, null, null, null, null, 1, 1, "DepartureLocation"));
            ViewBag.CurrentPage = 1;
            ViewBag.SortColumn = "DepartureLocation";
            ViewBag.SortOrder = 1;
            ViewBag.SearchDepartureLocation = null;
            ViewBag.SearchDepartureDay = null;
            ViewBag.SearchArrivalLocation = null;
            ViewBag.SearchArrivalDay = null;
            var maxPageTotal = ferryDetailBusinessClass.GetNewMaxPageNumber(id, null, null, null, null);
            ViewBag.MaxPageNumber = maxPageTotal;
            view.PageList = ferryDetailBusinessClass.GetSchedulePagination(1, maxPageTotal);
            return view;
        }

        /// <summary>
        /// default edit schedle detail page 
        /// </summary>
        /// <param name="id"> scheudle id </param>
        /// <param name="ferryId"> ferry id </param>
        /// <returns> view model of edit schedule detail page </returns>
        private EmployeeScheduleDetailViewModel NewEditScheduleDetailViewModel(int id, int ferryId)
        {
            var view = new EmployeeScheduleDetailViewModel();
            var scheduleBusinessClass = new EmployeeSchedule(_ItemPerPage, _MaxPageNumber);
            view.FerryId = ferryId;
            view.ScheduleId = id;
            view.LocationList = LocationDataToList(scheduleBusinessClass.GetLocationList(false));
            view.StopList = StopDataToList(scheduleBusinessClass.GetScheduleDetail(id));
            view.StopListLength = view.StopList.Count;
            view.StopIdList = JsonConvert.SerializeObject(GetStopIdList(view.StopList));
            var firstLastDetail = scheduleBusinessClass.GetScheduleList(ferryId, id);
            view.FirstStop = FirstStopDataToList(firstLastDetail);
            view.LastStop = LastStopDataToList(firstLastDetail);
            if (view.StopList.Count > 1)
            {
                view.StopList.Sort(new StopListComp(firstLastDetail.DepartureDay));
            }
            view.RowVersion = firstLastDetail.RowVersion;
            view.CostPerPerson = firstLastDetail.CostPerPerson;
            view.CostPerVehicle = firstLastDetail.CostPerVehicle;
            view.Description = firstLastDetail.Description;
            return view;
        }

        /// <summary>
        /// default schedule detail page constructor 
        /// </summary>
        /// <param name="id"> schedule id </param>
        /// <param name="ferryId"> ferry id </param>
        /// <returns> view model of schedule detail page </returns>
        private EmployeeScheduleDetailViewModel NewScheduleDetailViewModel(int id, int ferryId)
        {
            var view = new EmployeeScheduleDetailViewModel();
            var scheduleBusinessClass = new EmployeeSchedule(_ItemPerPage, _MaxPageNumber);
            view.FerryId = ferryId;
            view.ScheduleId = id;
            view.LocationList = LocationDataToList(scheduleBusinessClass.GetLocationList(false));
            view.StopList = StopDataToList(scheduleBusinessClass.GetScheduleDetail(id));
            var firstLastDetail = scheduleBusinessClass.GetScheduleList(ferryId, id);
            view.FirstStop = FirstStopDataToList(firstLastDetail);
            view.LastStop = LastStopDataToList(firstLastDetail);
            if( view.StopList.Count > 1)
            {
                view.StopList.Sort(new StopListComp(firstLastDetail.DepartureDay));
            }
            view.CostPerPerson = firstLastDetail.CostPerPerson;
            view.CostPerVehicle = firstLastDetail.CostPerVehicle;
            view.Description = firstLastDetail.Description;
            return view;
        }

        /// <summary>
        /// new add schedule detail page constructor 
        /// </summary>
        /// <param name="ferryId"> ferry id </param>
        /// <returns> view model of add schedule detail page </returns>
        private EmployeeScheduleDetailViewModel NewAddScheduleDetailViewModel(int ferryId)
        {
            var view = new EmployeeScheduleDetailViewModel();
            var scheduleBusinessClass = new EmployeeSchedule(_ItemPerPage, _MaxPageNumber);
            view.FerryId = ferryId;
            view.ScheduleId = -1;
            view.LocationList = LocationDataToList(scheduleBusinessClass.GetLocationList(false));
            return view;
        }

        /// <summary>
        /// validation to check if mid stop is between first departure day and arrival day 
        /// </summary>
        /// <param name="stopList"></param>
        /// <param name="firstDepartureDay"></param>
        /// <param name="lastArrivalDay"></param>
        /// <returns> 0 if sucess, -1 if fail </returns>
        private int StopListValidation(List<ScheduleDetail> stopList, int firstDepartureDay, TimeSpan firstDepartureTime, int lastArrivalDay, TimeSpan lastDepartureTime)
        {
            stopList.Sort(new StopListComp(firstDepartureDay));
            if (lastArrivalDay < firstDepartureDay)
            {
                lastArrivalDay += 7;
            }
            var previousDepartureDay = firstDepartureDay;
            var previousDepartureTime = firstDepartureTime;
            foreach (var stop in stopList)
            {
                var arrivalDay = stop.ArrivalDay;
                var departureDay = stop.DepartureDay;
                if (arrivalDay < firstDepartureDay)
                {
                    arrivalDay += 7;
                }
                if (departureDay < firstDepartureDay)
                {
                    departureDay += 7;
                }
                if (arrivalDay > lastArrivalDay || departureDay > lastArrivalDay
                    || departureDay < arrivalDay || 
                    (stop.DepartureTime <= stop.ArrivalTime && departureDay == arrivalDay))
                {
                    return -1;
                }
                if (stop.ArrivalDay < previousDepartureDay || (stop.ArrivalDay == previousDepartureDay && stop.ArrivalTime <= previousDepartureTime ))
                {
                    return -1;
                }
                previousDepartureDay = stop.DepartureDay;
                previousDepartureTime = stop.DepartureTime;
            }
            if (lastArrivalDay < previousDepartureDay || (lastArrivalDay == previousDepartureDay && lastDepartureTime <= previousDepartureTime ))
            {
                return -1;
            }
            return 0;
        }
    }
}
