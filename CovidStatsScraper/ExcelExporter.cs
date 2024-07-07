using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML.Excel;

namespace CovidStatsScraper
{
    /// Handles the export of COVID-19 statistics to an Excel file.
    public class ExcelExporter
    {
        /// Exports the given COVID-19 statistics to an Excel file.
        /// returns rhe file path of the created Excel file.
        public string ExportToExcel(List<CountryStats> stats)
        {
            var world = stats.FirstOrDefault(s => s.Country == "World");
            var continents = stats.Where(s => s.Country.EndsWith(":")).ToList();
            var countries = stats.Where(s => !s.Country.EndsWith(":") && s.Country != "World").ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("COVID-19 Stats");

                AddHeaders(worksheet, 1);

                int row = 2;

                if (world != null)
                {
                    AddDataRow(worksheet, row++, world);
                    row++;
                }

                foreach (var continent in continents)
                {
                    AddDataRow(worksheet, row++, continent, true);
                }
                row++;

                foreach (var country in countries)
                {
                    AddDataRow(worksheet, row++, country);
                }

                worksheet.Columns().AdjustToContents();

                var rootDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var filePath = Path.Combine(rootDirectory, "COVID19_Stats.xlsx");
                workbook.SaveAs(filePath);

                return filePath;
            }
        }

        /// Adds header row to the worksheet.
        private void AddHeaders(IXLWorksheet worksheet, int row)
        {
            var headers = new[]
            {
                "Country/Continent", "Total Cases", "New Cases", "Total Deaths", "New Deaths",
                "Total Recovered", "New Recovered", "Active Cases", "Serious, Critical",
                "Tot Cases/1M pop", "Deaths/1M pop", "Total Tests", "Tests/1M pop", "Population"
            };

            for (int i = 0; i < headers.Length; i++)
            {
                worksheet.Cell(row, i + 1).Value = headers[i];
            }
        }

        /// Adds a data row to the worksheet.
        private void AddDataRow(IXLWorksheet worksheet, int row, CountryStats stats, bool isContinent = false)
        {
            string name = isContinent ? stats.Country.TrimEnd(':') : stats.Country;
            var values = new object[]
            {
                name,
                stats.TotalCases,
                stats.NewCases,
                stats.TotalDeaths,
                stats.NewDeaths,
                stats.TotalRecovered,
                stats.NewRecovered,
                stats.ActiveCases,
                stats.SeriousCritical,
                stats.TotalCasesPerOneMillion,
                stats.DeathsPerOneMillion,
                stats.TotalTests,
                stats.TestsPerOneMillion,
                stats.Population
            };

            for (int i = 0; i < values.Length; i++)
            {
                // Explicitly convert to XLCellValue to resolve the CS0266 error
                worksheet.Cell(row, i + 1).Value = XLCellValue.FromObject(values[i]);
            }

            if (isContinent)
            {
                worksheet.Range(row, 1, row, values.Length).Style.Font.Bold = true;
            }
        }
    }
}