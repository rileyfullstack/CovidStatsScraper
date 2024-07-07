using System;
using System.Threading.Tasks;

namespace CovidStatsScraper
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var scraper = new CovidStatsScraper();
                var stats = await scraper.ScrapeWorldometers();

                var excelExporter = new ExcelExporter();
                var filePath = excelExporter.ExportToExcel(stats);

                Console.WriteLine($"Excel file has been saved to: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}