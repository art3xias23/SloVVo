
using System.Collections.Generic;
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

            pdfDocument.Save(filename);
        }
    }
}
