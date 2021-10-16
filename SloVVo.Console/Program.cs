using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SloVVo.Logic.PdfLogic;

namespace SloVVo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var pdfExport = new Pdf<Someclass>();
            var list = pdfExport.GetBogusDataData(); 

            var path = @"C:\temp\pdfTest\Test.pdf";
            pdfExport.ExportToPdf(list, path);
            pdfExport.OpenDocument(path);
        }

      
    }
}
