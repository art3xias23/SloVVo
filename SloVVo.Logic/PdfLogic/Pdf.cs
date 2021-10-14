
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace SloVVo.Logic.PdfLogic
{
    public class Pdf<T> : IPdf<T> where T : class
    {
        public void ExportToPdf(IEnumerable<T> items)
        {
            var pdfDocument = new PdfDocument();
            var pdfPage = new PdfPage();
            pdfDocument.AddPage(pdfPage);
            XGraphics gfx = XGraphics.FromPdfPage(pdfPage);
            XFont font = new XFont("Verdana", 20, XFontStyle.Bold);
            gfx.DrawString("Hello, World!", font, XBrushes.Black,
            new XRect(0, 0, pdfPage.Width, pdfPage.Height),
                XStringFormat.Center);
            var filename = @"C:\temp\testPdf.pdf";

            Table table = new Table();

            table.Borders.Width = 0.75;
            var properties = items.First().GetType().GetProperties();

            var i = table.AddColumn()


        }
    }
}
