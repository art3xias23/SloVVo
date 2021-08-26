using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace SloVVo.App.ValidationRules
{
    public class NumberValidationRule : ValidationRule
    {
        public NumberValidationRule()
        {
            
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int result = 0;
            try
            {
                result = Int32.Parse((string)value);
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Въведената стойност не е цифра.");
            } 

            return ValidationResult.ValidResult;
        }
    }
}
