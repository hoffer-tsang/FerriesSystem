/*==============================================================================
 *
 * Add/Edit Employee Company View Model
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ferry.ViewModel
{
    /// <summary>
    /// Contain Add/Edit Details for company view for employee
    /// </summary>
    public class EmployeeCompanyAddEditViewModel
    {
        /// <summary>
        /// Companyname input field
        /// </summary>
        [Required(ErrorMessage = "Company Name is Required")]
        [DisplayName("Company Name:")]
        [StringLength(256, ErrorMessage = "Company Name Cannot Exceeed 256 Character")]
        public string CompanyName { get; set; }
        /// <summary>
        /// CompanyId, negative if add view
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// RowVerson of the current company detail
        /// </summary>
        public byte[] RowVersion { get; set; }
        /// <summary>
        /// Standard error code
        /// </summary>
        public int? StandardError { get; set; }
        /// <summary>
        /// concurrency error code 
        /// </summary>
        public int? ConcurrencyError { get; set; }
    }
}
