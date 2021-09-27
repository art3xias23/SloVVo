using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SloVVo.App.ValidationRules
{
    public class StringIsValidValidationRule : ValidationRule
    {
        public StringIsValidValidationRule()
        {
            
        }


        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (Regex.IsMatch(value?.ToString(), "^[A-za-z]|[А-яа-я]+$"))
            {
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, "Трябва да съдържа само букви.");
        }
    }
}
