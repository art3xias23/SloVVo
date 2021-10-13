using System.Collections.Generic;

namespace SloVVo.Logic.PdfLogic
{
    public interface IPdf<T> where T :class   {
        void ExportToPdf(IEnumerable<T> items);
    }
}