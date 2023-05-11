/*==============================================================================
 *
 * Employee Ferry View Model
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ferry.ViewModel.SubViewModel;

namespace Ferry.ViewModel
{
    /// <summary>
    /// Contain Details for Ferry view for employee
    /// </summary>
    public class EmployeeFerryViewModel
    {
        /// <summary>
        /// FerryName Search field for Ferry page
        /// </summary>
        [StringLength(256, ErrorMessage = "Ferry Name Cannot Exceeed 256 Character")]
        public string FerryNameSearch { get; set; }
        /// <summary>
        /// CompanyName Search field for Ferry page
        /// </summary>
        [StringLength(256, ErrorMessage = "Company Name Cannot Exceeed 256 Character")]
        public string CompanyNameSearch { get; set; }
        /// <summary>
        /// list of Ferry to display in table
        /// </summary>
        public List<FerryDetail> FerryList { get; set; }
        /// <summary>
        /// list of page number to display
        /// </summary>
        public List<int> PageList { get; set; }
        /// <summary>
        /// General Error int 1 for yes
        /// </summary>
        public int? GeneralError { get; set; }

        /// <summary>
        /// Constructor of employeeFerryViewModel
        /// </summary>
        public EmployeeFerryViewModel()
        {
            FerryList = new List<FerryDetail>();
            PageList = new List<int>();
        }
        /// <summary>
        /// hidden ferry id field 
        /// </summary>
        public int? HiddenFerryId { get; set; }
    }
}
