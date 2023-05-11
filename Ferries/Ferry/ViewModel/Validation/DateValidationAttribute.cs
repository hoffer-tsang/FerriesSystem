/*==============================================================================
 *
 * Date Vallidation attribute class 
 *
 * Copyright © Dorset Software Services Ltd, 2023
 *
 * TSD Section: P900 Ferries
 *
 *============================================================================*/
using System;
using System.ComponentModel.DataAnnotations;

namespace Ferry.ViewModel.Validation
{
    /// <summary>
    /// date validation for date 
    /// </summary>
    public class DateValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// an override boolean for valid date
        /// </summary>
        /// <param name="value"> datetime </param>
        /// <returns> return null if fail, true if date is larger then now </returns>
        public override bool IsValid(object value)
        {
            DateTime todayDate = Convert.ToDateTime(value);
            return value == null || todayDate > DateTime.Now;
        }
    }
}
