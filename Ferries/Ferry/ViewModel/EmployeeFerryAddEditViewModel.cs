/*==============================================================================
 *
 * Add/Edit Employee Ferry View Model
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Ferry.ViewModel.SubViewModel;

namespace Ferry.ViewModel
{
    /// <summary>
    /// Contain Add/Edit Details for Ferry view for employee
    /// </summary>
    public class EmployeeFerryAddEditViewModel
    {
        /// <summary>
        /// Ferry name input field 
        /// </summary>
        [Required(ErrorMessage = "Ferry Name is Required")]
        [DisplayName("Ferry Name:")]
        [StringLength(256, ErrorMessage = "Ferry Name Cannot Exceeed 256 Character")]
        public string FerryName {  get; set; }
        /// <summary>
        /// Companyid input field
        /// </summary>
        [Required(ErrorMessage = "Company id is Required")]
        [DisplayName("Company Name:")]
        [Range(1, int.MaxValue, ErrorMessage = "Company id has to be positive")]
        public int CompanyId { get; set; }
        /// <summary>
        /// List of Company to select in dropdown 
        /// </summary>
        public List<CompanyDetail> CompanyList { get; set; }
        /// <summary>
        /// FerryId, negative if add view
        /// </summary>
        public int FerryId { get; set; }
        /// <summary>
        /// RowVerson of the current Ferry detail
        /// </summary>

        public byte[] RowVersion { get; set; }
        /// <summary>
        /// standard error code of the page 
        /// </summary>
        public int? StandardError { get; set; }
        /// <summary>
        /// concurrency error of the page 
        /// </summary>
        public int? ConcurrencyError { get; set; }
    }
}
