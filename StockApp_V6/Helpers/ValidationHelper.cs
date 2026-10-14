
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services.Helpers
{
    public class ValidationHelper
    {
        internal static void ModelValidation(object obj)
        {
            //Model validations
            ValidationContext validationContext = new ValidationContext(obj);

            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool isvalid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            if (!isvalid)
            {
                string errors = string.Join(
                    Environment.NewLine,
                    validationResults.Select(x => x.ErrorMessage)
                );

                throw new ArgumentException(errors);
            }


        }
    }
}
