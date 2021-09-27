using System.Globalization;
using System.Windows.Controls;

namespace SloVVo.App.ValidationRules
{
    public class IsComboBoxEmptyValidationRule: ValidationRule
    {
        public IsComboBoxEmptyValidationRule()
        {
            
        }


        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace(value?.ToString()))
            {
                return new ValidationResult(false, "Не може да бъде празно.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
