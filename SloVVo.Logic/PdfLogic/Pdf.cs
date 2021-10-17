
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Bogus;
using Bogus.DataSets;
using Bogus.Extensions;
using SloVVo.Data.Models;
using SloVVo.Logic.ObservableModels;
using FontStyle = System.Drawing.FontStyle;
using FontStyles = Aspose.Pdf.Text.FontStyles;
using HorizontalAlignment = Aspose.Pdf.HorizontalAlignment;
using Table = Aspose.Pdf.Table;

namespace SloVVo.Logic.PdfLogic
{
    public class Pdf<T> : IPdf<T> where T : class
    {
        public void ExportToPdf(IEnumerable<T> items, string filePath)
        {
            Aspose.Pdf.Document doc = new Aspose.Pdf.Document();
            doc.Pages.Insert(1);

            Aspose.Pdf.Table table = new Aspose.Pdf.Table();
            table.Border = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Green));
            table.DefaultCellBorder = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Green));
            var firstItem = items.FirstOrDefault();
            if (firstItem != null)
            {
                //Create Page
                var pdfPage = doc.Pages.First();

                //Set page size
                pdfPage.SetPageSize(1000, 842);
                var secondaryParagraphText = string.Empty;

                var mainParagraphText1 = @"Библиотека";
                var mainParagraphText2 = @"на";
                var mainParagraphText3 = @"Народно читалище 'Пробуда - 1896'";
                secondaryParagraphText = SetSecondaryTypeText(firstItem);

                var mainParagraph1 = SetHeaderParagraph(mainParagraphText1);
                pdfPage.Paragraphs.Add(mainParagraph1);

                var mainParagraph2 = SetHeaderParagraph(mainParagraphText2);
                pdfPage.Paragraphs.Add(mainParagraph2);

                var mainParagraph3 = SetHeaderParagraph(mainParagraphText3);
                pdfPage.Paragraphs.Add(mainParagraph3);

                var secondaryParagraph = SetTitleParagraph(secondaryParagraphText);
                pdfPage.Paragraphs.Add(secondaryParagraph);

                //Get List of Columns starting with their name
                var listOfColumnsStartingWithName = GetListOfColumnsStartingWithName(items, out var properties);

                //Add values to the columns
                AddValuesToColumns(items, properties, listOfColumnsStartingWithName);

                PopulateTableInPdf(listOfColumnsStartingWithName, table);

                doc.Pages[1].Paragraphs.Add(table);
                doc.Save(filePath);
            }
        }

        private static void PopulateTableInPdf(List<List<string>> listOfColumnsStartingWithName, Table table)
        {
            for (int rowNumber = 0; rowNumber < listOfColumnsStartingWithName.First().Count; rowNumber++)
            {
                var row = table.Rows.Add();
                for (int columnNumber = 0; columnNumber < listOfColumnsStartingWithName.Count - 1; columnNumber++)
                {
                    if (rowNumber == 0)
                    {
                        row.DefaultCellTextState.FontStyle = FontStyles.Bold;
                        row.DefaultCellTextState.FontSize = 12;
                    }

                    row.DefaultCellTextState.FontSize = 12;
                    row.Cells.Add(listOfColumnsStartingWithName[columnNumber][rowNumber]?.ToString() ?? string.Empty);
                }
            }
        }

        private string SetSecondaryTypeText(T firstItem)
        {
            if (firstItem.GetType() == typeof(Someclass))
            {
                return "Списък с пробни данни";
            }
            else if (firstItem.GetType() == typeof(ObservableBook))
            {
                return "Списък с книги";
            }
            else if (firstItem.GetType() == typeof(User))
            {
                return "Списък с потребители";
            }
            return String.Empty;
        }

        private static TextFragment SetHeaderParagraph(string mainParagraphText1)
        {
            var mainParagraph1 = new TextFragment(mainParagraphText1);
            mainParagraph1.TextState.Font = FontRepository.FindFont("Georgia");
            mainParagraph1.TextState.FontSize = 20;
            mainParagraph1.TextState.FontStyle = FontStyles.Bold;
            mainParagraph1.HorizontalAlignment = HorizontalAlignment.Center;
            mainParagraph1.Margin = new MarginInfo(0, 10, 0, 0);
            return mainParagraph1;
        }

        private static TextFragment SetTitleParagraph(string mainParagraphText1)
        {
            var mainParagraph1 = new TextFragment(mainParagraphText1);
            mainParagraph1.TextState.Font = FontRepository.FindFont("Calibri");
            mainParagraph1.TextState.FontSize = 15;
            mainParagraph1.TextState.FontStyle = FontStyles.Italic;
            mainParagraph1.HorizontalAlignment = HorizontalAlignment.Center;
            mainParagraph1.Margin = new MarginInfo(0, 20, 0, 20);
            return mainParagraph1;
        }

        private static void AddValuesToColumns(IEnumerable<T> items, PropertyInfo[] properties, List<List<string>> listOfColumnsStartingWithName)
        {
            foreach (var item in items)
            {
                var count = 0;
                foreach (var prop in properties)
                {
                    var propTypeInfo = item?.GetType();
                    var property = propTypeInfo.GetProperty(prop.Name);
                    var propValue = GetPropertyValue(property, item);
                    listOfColumnsStartingWithName[count].Add(propValue);
                    count++;
                }
            }
        }

        private static string GetPropertyValue(PropertyInfo property, T item)
        {
            return property?.GetValue(item)?.ToString();
        }

        private static List<List<string>> GetListOfColumnsStartingWithName(IEnumerable<T> items, out PropertyInfo[] properties)
        {
            properties = typeof(T).GetProperties();
            var propCount = properties.Count();
            var columnCount = items.Count();
            var listOfColumns = new List<List<string>>();
            foreach (var property in properties)
            {
                var column = new List<string>();
                column.Add(property.Name);

                listOfColumns.Add(column);
                //row.Cells.Add(property.Name);
            }

            return listOfColumns;
        }

        public void OpenDocument(string path)
        {
            var fileName = path;
            using (var pdfDocument = new Aspose.Pdf.Document(fileName))
            {
                Console.WriteLine($"Pages {pdfDocument.Pages.Count}");
            }
        }

        public List<Someclass> GetBogusDataData()
        {
            Randomizer.Seed = new Random(10);
            var testObjects = new Faker<Someclass>()
                .StrictMode(true)
                .RuleFor(x => x.Id, a => a.System.Random.Int(0, 1000))
                .RuleFor(x => x.Firstname, a => a.Name.FirstName(Name.Gender.Male))
                .RuleFor(x => x.Surname, a => a.Name.LastName())
                .RuleFor(x => x.Age, a => a.System.Random.Int(0, 99))
                .RuleFor(x => x.DoB, a => a.Date.Between(DateTime.Now.AddYears(-40), DateTime.Now).AddYears(-20))
                .RuleFor(x => x.Town, a => a.Address.City())
                .FinishWith((f, u) => { Console.WriteLine("SomeClass Created! Id={0}", u.Id); });

            return testObjects.GenerateBetween(100, 100);

        }

        public void CreateDirectoryIfNotExist(string directoryPath)
        {
            System.IO.Directory.CreateDirectory(directoryPath);
        }

        public int GetCountOfDirectoryItems(string directoryPath)
        {
            return System.IO.Directory.GetFiles(directoryPath).Count();
        }
    }

    public class Someclass
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public DateTime DoB { get; set; }
        public string Town { get; set; }
    }


}
