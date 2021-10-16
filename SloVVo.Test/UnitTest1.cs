using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using SloVVo.Logic.PdfLogic;

namespace SloVVo.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var something = new Something()
            {
                Item1 = 1,
                Item2 = 2,
                Item3 = 3,
                Item4 = "aaaaaaaaaaaaaaa",
                Item5 = "bbbbbbbbbbbb",
                Item6 = DateTime.Now
            };

            var something2 = something;
            var something3 = something2;

            var list = new List<Something>() {something, something2, something3};

            var pdfExport = new Pdf<Something>();

            var path = @"C:\temp\pdfTest\Test.pdf";
            pdfExport.ExportToPdf(list, path);
            pdfExport.OpenDocument(path);
        }

        private class Something
        {
            public int Item1;
            public int Item2;
            public int Item3;
            public string Item4;
            public string Item5;
            public DateTime Item6;
        }
    }
}
