using System.Collections.Generic;
using System.Drawing;

namespace SloVVo.Logic.PdfLogic
{
    public interface IPdf<T> where T :class   {
        void ExportToPdf(IEnumerable<T> items, string filePath);
    }
}