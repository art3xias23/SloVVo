using System;
using System.Globalization;
using System.Windows.Controls;

namespace SloVVo.App.ValidationRules
{
    public class NumberIsNot0ValidationRule : ValidationRule
    {
        public NumberIsNot0ValidationRule()
        {
            
        } 
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (Int32.Parse(value?.ToString()) == 0)
            {
                return new ValidationResult(false, "Числото трябва да е различно от 0");
            }
            return ValidationResult.ValidResult;
        }
    }
}
