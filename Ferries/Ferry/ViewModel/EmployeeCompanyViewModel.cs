/*==============================================================================
 *
 * Employee Company View Model
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
    /// Contain Details for company view for employee
    /// </summary>
    public class EmployeeCompanyViewModel
    {
        /// <summary>
        /// Search field for company page
        /// </summary>
        [StringLength(256, ErrorMessage = "Company Name Cannot Exceeed 256 Character")]
        public string Search { get; set; }
        /// <summary>
        /// list of company to display in table
        /// </summary>
        public List<CompanyDetail> CompanyList { get; set; }
        /// <summary>
        /// list of page number to display
        /// </summary>
        public List<int> PageList { get; set; }
        /// <summary>
        /// General Error int 1 for yes
        /// </summary>
        public int? GeneralError { get; set; }
        /// <summary>
        /// hidden Company id 
        /// </summary>
        public int? HiddenCompanyId { get; set; }
        /// <summary>
        /// Constructor of employeecompanyviewmodel
        /// </summary>
        public EmployeeCompanyViewModel()
        {
            CompanyList = new List<CompanyDetail>();
            PageList = new List<int>();
        }
    }
}
