using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using SloVVo.Logic.PdfLogic;
using SloVVo.Logic.PdfModels;

namespace SloVVo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestPdf();

        }

       

      
        private static void TestPdf()
        {
            var pdfExport = new Pdf<Someclass>();
            var list = pdfExport.GetBogusDataData();

            var path = @"C:\temp\pdfTest\Test.pdf";
            pdfExport.ExportToPdf(list, path);
            pdfExport.OpenDocument(path);
        }
    }
}
