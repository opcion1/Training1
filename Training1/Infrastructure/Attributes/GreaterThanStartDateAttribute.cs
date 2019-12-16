using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Training1.Infrastructure
{
    public class GreaterThanStartDateAttribute : ValidationAttribute, IClientModelValidator
    {
        public string StartDatePropertyName { get; set; } = "StartDate";
        public string EndDatePropertyName { get; set; } = "EndDate";

        public new string ErrorMessage { get; set; } = "End Date must be greater than Start Date";


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo startDateProperty = validationContext.ObjectType.GetProperty(StartDatePropertyName);
            PropertyInfo endDateProperty = validationContext.ObjectType.GetProperty(EndDatePropertyName);

            if (
                DateTime.TryParse(startDateProperty.GetValue(validationContext.ObjectInstance, null)?.ToString(), out DateTime startDate) &&
                DateTime.TryParse(endDateProperty.GetValue(validationContext.ObjectInstance, null)?.ToString(), out DateTime endDate)
                )
            {
                if (endDate > startDate)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(ErrorMessage);
        }


        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-greaterthanstartdate", ErrorMessage);
        }
        private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }

            attributes.Add(key, value);
            return true;

        }
    }
}
