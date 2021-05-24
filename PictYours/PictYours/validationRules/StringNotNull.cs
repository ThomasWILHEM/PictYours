using System;
using System.Globalization;
using System.Windows.Controls;

namespace PictYours.validationRules
{
    public class StringNotNull : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty(value?.ToString()))
            {
                return new ValidationResult(false, "Le champ doit être remplit");
            }
            return new ValidationResult(true, null);
        }
    }
}
