/*==============================================================================
 *
 * Customer Booking Page 1-3 View Model
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/
using System;
using System.ComponentModel.DataAnnotations;

namespace Ferry.ViewModel
{
    /// <summary>
    /// Customer Booking Page 1-3 view model that contain all detail need to save booking 
    /// </summary>
    public class CustomerBookingPageViewModel
    {
        /// <summary>
        /// Ferry name of the joruney 
        /// </summary>
        [Display(Name = "Ferry Name:")]
        public string FerryName { get; set; }
        /// <summary>
        /// company name of the ferry 
        /// </summary>
        [Display(Name = "Company Name:")]
        public string CompanyName { get; set; }
        /// <summary>
        /// description of the joruney 
        /// </summary>
        [Display(Name = "Description:")]
        public string Description { get; set; }
        /// <summary>
        /// deparutre location
        /// </summary>
        [Display(Name = "Departure Location:")]
        public string DepartureLocation { get; set; }
        /// <summary>
        /// departure date time 
        /// </summary>
        [Display(Name = "Departure Time:")]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy HH:mm}")]
        public DateTime DepartureDateTime { get; set; }
        /// <summary>
        /// arrival location 
        /// </summary>
        [Display(Name = "Arrival Location:")]
        public string ArrivalLocation { get; set; }
        /// <summary>
        /// arrival date time 
        /// </summary>
        [Display(Name = "Arrival Time:")]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy HH:mm}")]
        public DateTime ArrivalDateTime { get; set; }
        /// <summary>
        /// number of passengers 
        /// </summary>
        [Display(Name = "Number of Passengers:")]
        [Range(1, 10, ErrorMessage = "Passengers enter has to be between 1 to 10")]
        public int Passengers { get; set; }
        /// <summary>
        /// number of vehicles 
        /// </summary>
        [Display(Name = "Number of Vehicles:")]
        [Range(0, 3, ErrorMessage = "Vehicles enter has to be between 0 to 3")]
        public int Cars { get; set; }
        /// <summary>
        /// number of stops of the joruney 
        /// </summary>
        public int NumberOfStops { get; set; }
        /// <summary>
        /// cost per person per stop 
        /// </summary>
        public decimal CostPerPerson { get; set; }
        /// <summary>
        /// cost per vehicle per stop 
        /// </summary>
        public decimal CostPerVehicle { get; set; }
        /// <summary>
        /// total cost of the joruney 
        /// </summary>
        [Display(Name = "Total Cost:")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal TotalCost { get; set; }
        /// <summary>
        /// Customer Name 
        /// </summary>
        [Required(ErrorMessage = "Name is Required")]
        [Display(Name = "Name:")]
        [StringLength(100, ErrorMessage = " Name Cannot Exceeed 100 Character")]
        public string CustomerName { get; set; }
        /// <summary>
        /// Address Line 1
        /// </summary>
        [Required(ErrorMessage = "Address Line 1 is Required")]
        [Display(Name = "Address Line1:")]
        [StringLength(256, ErrorMessage = "Address Line1 Cannot Exceeed 256 Character")]
        public string Address1 { get; set; }
        /// <summary>
        /// Address Line 2
        /// </summary>
        [Display(Name = "Address Line2 (Optional):")]
        [StringLength(256, ErrorMessage = "Address Line2 Cannot Exceeed 256 Character")]
        public string Address2 { get; set; }
        /// <summary>
        /// City 
        /// </summary>
        [Required(ErrorMessage = "City is Required")]
        [Display(Name = "City:")]
        [StringLength(256, ErrorMessage = "City Cannot Exceeed 256 Character")]
        public string City { get; set; }
        /// <summary>
        /// Postal Code
        /// </summary>
        [Required(ErrorMessage = "Postal Code is Required")]
        [Display(Name = "Postal Code:")]
        [StringLength(20, ErrorMessage = "Postal Code Cannot Exceeed 20 Character")]
        public string PostCode { get; set; }
    }
}
