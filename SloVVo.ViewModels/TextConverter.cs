using System;

namespace SloVVo.ViewModels
{
    public class TextConverter
    {
        private readonly Func<string, string> _conversion;

        public TextConverter(Func<string, string> conversion)
        {
            _conversion = conversion;
        }

        public string ConvertText(string inputText)
        {
            return _conversion(inputText);
        }
    }

}
