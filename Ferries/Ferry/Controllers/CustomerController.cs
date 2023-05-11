/*==============================================================================
 *
 * Customer Controller Class, all action for customer 
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using BusinessClass.Customer;
using BusinessClass.Employee;
using BusinessClass.Model;
using Ferry.ViewModel;
using Ferry.ViewModel.SubViewModel;
using Microsoft.AspNet.Identity;

namespace Ferry.Controllers
{
    /// <summary>
    /// Customer Controller class 
    /// </summary>
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private int _ItemPerPage = 5;
        private int _MaxPageNumber = 9;
        
        /// <summary>
        /// Get to Home page for customer 
        /// </summary>
        /// <returns> return view for home page </returns>
        [HttpGet]
        public ActionResult Home()
        {
            return View();
        }

        /// <summary>
        /// Get to Journey Page for customer 
        /// </summary>
        /// <returns> return view of journey page</returns>
        [HttpGet]
        public ActionResult Journey()
        {
            var view = JourneyConstructor();
            return View("Journey", view);
        }

        /// <summary>
        /// perform the search journey action in journey page 
        /// </summary>
        /// <param name="viewModel"> the view model of the journey page </param>
        /// <returns> journey page view with search filter </returns>
        [HttpPost]
        public ActionResult SearchJourney(CustomerJourneyViewModel viewModel)
        {
            if (!ModelState.IsValid || viewModel.ArriveBefore < viewModel.LeaveAfter)
            {
                var view = new CustomerJourneyViewModel();
                var scheduleBusinessClass = new EmployeeSchedule(_ItemPerPage, _MaxPageNumber);
                var journeyBusinessClass = new CustomerJourney(_ItemPerPage, _MaxPageNumber);
                view.LocationList = LocationDataToList(scheduleBusinessClass.GetLocationList(true));
                var journeyDetailList = (List<GetJourney>)Session["JourneyList"];
                var displayList = new List<GetJourney>();
                displayList = journeyDetailList.OrderBy(j => j.DepartureDateTime).Take(_ItemPerPage).ToList();
                view.AvailableJourney = JourneyDataToList(displayList);
                Session["JourneyList"] = journeyDetailList;
                var maxPageTotal = journeyBusinessClass.MaxPageNumber(journeyDetailList.Count);
                view.PageList = journeyBusinessClass.JourneyPageNumber(maxPageTotal, 1);
                ViewBag.CurrentPage = 1;
                ViewBag.MaxPageNumber = maxPageTotal;
                if (viewModel.ArriveBefore < viewModel.LeaveAfter)
                {
                    view.GeneralError = 2;
                }
                else
                {
                    view.GeneralError = 1;
                }
                return View("Journey", view);
            }
            else
            {
                ModelState.Clear();
                var view = new CustomerJourneyViewModel();
                if(viewModel.DepartureLocation == "Please Select")
                {
                    viewModel.DepartureLocation = null;
                }
                if (viewModel.ArrivalLocation == "Please Select")
                {
                    viewModel.ArrivalLocation = null;
                }
                var scheduleBusinessClass = new EmployeeSchedule(_ItemPerPage, _MaxPageNumber);
                var journeyBusinessClass = new CustomerJourney(_ItemPerPage, _MaxPageNumber);
                view.LocationList = LocationDataToList(scheduleBusinessClass.GetLocationList(true));
                var journeyDetailList = journeyBusinessClass.AvailableJourney(
                    viewModel.DepartureLocation, viewModel.ArrivalLocation, viewModel.LeaveAfter, 
                    viewModel.ArriveBefore, viewModel.Person, viewModel.Vehicle, viewModel.MaxPrice);
                journeyDetailList.Sort(new GetJournyListComp());
                var displayList = new List<GetJourney>();
                displayList = journeyDetailList.OrderBy(j => j.DepartureDateTime).Take(_ItemPerPage).ToList();
                view.AvailableJourney = JourneyDataToList(displayList);
                Session["JourneyList"] = journeyDetailList;
                var maxPageTotal = journeyBusinessClass.MaxPageNumber(journeyDetailList.Count);
                view.PageList = journeyBusinessClass.JourneyPageNumber(maxPageTotal, 1);
                ViewBag.CurrentPage = 1;
                ViewBag.MaxPageNumber = maxPageTotal;
                return View("Journey", view);
            }
        }

        /// <summary>
        /// Page Navigation after clicking page number 
        /// </summary>
        /// <param name="pageNumber"> page number navigate to </param>
        /// <returns> reutrn Journey page with view model </returns>
        [HttpGet]
        public ActionResult PageNavigation(int pageNumber)
        {
            var view = new CustomerJourneyViewModel();
            var scheduleBusinessClass = new EmployeeSchedule(_ItemPerPage, _MaxPageNumber);
            var journeyBusinessClass = new CustomerJourney(_ItemPerPage, _MaxPageNumber);
            view.LocationList = LocationDataToList(scheduleBusinessClass.GetLocationList(true));
            var journeyDetailList = (List<GetJourney>)Session["JourneyList"];
            var displayList = new List<GetJourney>();
            displayList = journeyDetailList.OrderBy(j => j.DepartureDateTime).Skip((pageNumber -1) * _ItemPerPage).Take(_ItemPerPage).ToList();
            view.AvailableJourney = JourneyDataToList(displayList);
            Session["JourneyList"] = journeyDetailList;
            var maxPageTotal = journeyBusinessClass.MaxPageNumber(journeyDetailList.Count);
            view.PageList = journeyBusinessClass.JourneyPageNumber(maxPageTotal, pageNumber);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.MaxPageNumber = maxPageTotal;
            return View("Journey", view);
        }

        /// <summary>
        /// Click Booking for the specific journey
        /// </summary>
        /// <param name="rowData"> the row of the list click</param>
        /// <param name="pageNumber"> the page number of the list lick </param>
        /// <returns> Booking wizard page 1 view </returns>
        [HttpGet]
        public ActionResult Booking(int rowData, int pageNumber)
        {
            var data = (List<GetJourney>) Session["JourneyList"];
            var journeyDetail = data[((pageNumber - 1) * 5) + rowData];
            var view = new CustomerBookingPageViewModel
            {
                FerryName = journeyDetail.FerryName,
                CompanyName = journeyDetail.CompanyName,
                DepartureLocation = journeyDetail.DepartureLocation,
                DepartureDateTime = journeyDetail.DepartureDateTime,
                ArrivalLocation = journeyDetail.ArrivalLocation,
                ArrivalDateTime = journeyDetail.ArrivalDateTime,
                CostPerPerson = journeyDetail.CostPerPerson,
                CostPerVehicle = journeyDetail.CostPerVehicle,
                Passengers = 1,
                Cars = 0,
                NumberOfStops = journeyDetail.NumberOfStop,
                TotalCost =  journeyDetail.CostPerPerson * journeyDetail.NumberOfStop
            };
            if (journeyDetail.Description != null)
            {
                view.Description = journeyDetail.Description;
            }
            Session["BookingOnGoingDetail"] = view;
            return View("BookingPage1", view);
        }

        /// <summary>
        /// Submit booking page 1 
        /// </summary>
        /// <param name="viewModel"> view model of booking page </param>
        /// <returns> booking page 2 view model </returns>
        [HttpPost]
        public ActionResult BookingPage1Submit(CustomerBookingPageViewModel viewModel)
        {
            Session.Clear();
            Session["BookingOnGoingDetail"] = viewModel;
            viewModel.TotalCost = ((viewModel.CostPerPerson * viewModel.Passengers) + (viewModel.CostPerVehicle * viewModel.Cars)) * viewModel.NumberOfStops;
            return View("BookingPage2", viewModel);
        }

        /// <summary>
        /// Get the booking page 1 details 
        /// </summary>
        /// <returns> booking page 1 view </returns>
        [HttpGet]
        public ActionResult GetBookingPage1()
        {
            var view = (CustomerBookingPageViewModel)Session["BookingOnGoingDetail"];
            return View("BookingPage1", view);
        }

        /// <summary>
        /// Get the booking page 2 details 
        /// </summary>
        /// <returns> booking page 2 view </returns>
        [HttpGet]
        public ActionResult GetBookingPage2()
        {
            var view = (CustomerBookingPageViewModel)Session["BookingOnGoingDetail"];
            return View("BookingPage2", view);
        }

        /// <summary>
        /// submit booking page 2 
        /// </summary>
        /// <param name="viewModel"> booking page view model </param>
        /// <returns> booking page 3 view </returns>
        [HttpPost]
        public ActionResult BookingPage2Submit(CustomerBookingPageViewModel viewModel)
        {
            Session.Clear();
            Session["BookingOnGoingDetail"] = viewModel;
            Session["Data"] = viewModel;
            return View("BookingPage3", viewModel);
        }

        /// <summary>
        /// Sumbit booking page 3 to finish booking 
        /// </summary>
        /// <returns> bookign page 4 with either error message or successful bookign reference </returns>
        [HttpGet]
        public ActionResult BookingPage3Submit()
        {
            CustomerBookingPageViewModel viewModel = (CustomerBookingPageViewModel)Session["Data"];
            if(viewModel == null)
            {
                var view2 = new CustomerBookingPage4ViewModel
                {
                    GeneralError = 1
                };
                return View("BookingPage4", view2);
            }
            var bookingBusinessClass = new Booking();
            var bookingModel = DataToInputBookingModel(viewModel);
            var bookingContactModel = DataToInputBookingContactModel(viewModel);
            var result = -1;
            var attempt = 0;
            while (result == -1 && attempt < 10)
            {
                bookingModel.BookingReference = BookingReference.GetBookingReference(5);
                result = bookingBusinessClass.AddBooking(bookingModel);
            }
            if(result == -1)
            {
                var view2 = new CustomerBookingPage4ViewModel
                {
                    GeneralError = 1
                };
                return View("BookingPage4", view2);
            }
            bookingContactModel.BookingId = result;
            result = bookingBusinessClass.AddBookingContact(bookingContactModel);
            if(result == 0)
            {
                var view2 = new CustomerBookingPage4ViewModel
                {
                    GeneralError = 1
                };
                return View("BookingPage4", view2);
            }
            else
            {
                var view = new CustomerBookingPage4ViewModel
                {
                    BookingReference = bookingModel.BookingReference
                };
                Session.Clear();
                return View("BookingPage4", view);
            }
        }

        /// <summary>
        /// Logout Navigation and back to logout page  
        /// </summary>
        [HttpGet]
        public void Logout()
        {
            var authenticationManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            Response.Redirect("~/Login.aspx");
        }

        /// <summary>
        /// The default journey page set up
        /// </summary>
        /// <returns> the view model of joruney page </returns>
        private CustomerJourneyViewModel JourneyConstructor()
        {
            var view = new CustomerJourneyViewModel();
            var scheduleBusinessClass = new EmployeeSchedule(_ItemPerPage, _MaxPageNumber);
            var journeyBusinessClass = new CustomerJourney(_ItemPerPage, _MaxPageNumber);
            view.LocationList = LocationDataToList(scheduleBusinessClass.GetLocationList(true));
            var journeyDetailList = journeyBusinessClass.AvailableJourney(
                    null, null, null, null, 1, 0, int.MaxValue);
            journeyDetailList.Sort(new GetJournyListComp());
            var displayList = new List<GetJourney>();
            displayList = journeyDetailList.OrderBy(j => j.DepartureDateTime).Take(_ItemPerPage).ToList();
            view.AvailableJourney = JourneyDataToList(displayList);
            Session["JourneyList"] = journeyDetailList;
            var maxPageTotal = journeyBusinessClass.MaxPageNumber(journeyDetailList.Count);
            view.PageList = journeyBusinessClass.JourneyPageNumber(maxPageTotal, 1);
            ViewBag.CurrentPage = 1;
            ViewBag.MaxPageNumber = maxPageTotal;
            return view;
        }

        /// <summary>
        /// convert booking page model data to input booking 
        /// </summary>
        /// <param name="data"> booking page view model data </param>
        /// <returns> input booking data </returns>
        private InputBooking DataToInputBookingModel(CustomerBookingPageViewModel data)
        {
            return new InputBooking
            {
                UserId = User.Identity.GetUserId(),
                Cars = data.Cars,
                Passengers = data.Passengers,
                Cost = data.TotalCost,
                CompanyName = data.CompanyName,
                FerryName = data.FerryName,
                DepartureDate = data.DepartureDateTime,
                DepartureLocation = data.DepartureLocation,
                ArrivalDate = data.ArrivalDateTime,
                ArrivalLocation = data.ArrivalLocation
            };
        }

        /// <summary>
        /// convert booking page view model data to inptu booking contact 
        /// </summary>
        /// <param name="data"> booking page view model data </param>
        /// <returns> input bookign contact data </returns>
        private InputBookingContact DataToInputBookingContactModel(CustomerBookingPageViewModel data)
        {
            var model = new InputBookingContact
            {
                Name = data.CustomerName,
                AddressLine1 = data.Address1,
                City = data.City,
                PostalCode = data.PostCode
            };
            if( data.Address2 != null)
            {
                model.AddressLine2 = data.Address2;
            }
            return model;
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
        /// convert betweeen business layer journey class to presentation layer joruney class
        /// </summary>
        /// <param name="data"> business layer journey data </param>
        /// <returns> presentation layer joruney data </returns>
        private List<JourneyList> JourneyDataToList(List<GetJourney> data)
        {
            var view = new List<JourneyList>();
            foreach (var d in data)
            {
                var journey = new JourneyList
                {
                    FerryName = d.FerryName,
                    DepartureLocation = d.DepartureLocation,
                    DepartureTime = d.DepartureDateTime,
                    ArrivalLocation = d.ArrivalLocation,
                    ArrivalTime = d.ArrivalDateTime,
                    TotalCost = d.TotalCost,
                    NumberOfStops = d.NumberOfStop,
                };
                view.Add(journey);
            }
            return view;
        }
    }
}
