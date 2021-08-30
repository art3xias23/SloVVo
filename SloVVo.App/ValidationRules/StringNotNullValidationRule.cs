using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SloVVo.App.ValidationRules
{
    public class StringNotNullValidationRule : ValidationRule
    {
        public StringNotNullValidationRule()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace(value?.ToString()))
            {
                return new ValidationResult(false,"Полето не може да бъде празно.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
