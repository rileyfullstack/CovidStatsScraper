using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace CovidStatsScraper
{
    public class CovidStatsScraper
    {
        /// <summary>
        /// Scrapes COVID-19 statistics from the Worldometers website.
        /// </summary>
        /// <returns>A list of CountryStats objects containing the scraped data.</returns>
        public async Task<List<CountryStats>> ScrapeWorldometers()
        {
            // URL to scrape
            var url = "https://www.worldometers.info/coronavirus/";
            var httpClient = new HttpClient();
            // Get the full html
            var html = await httpClient.GetStringAsync(url);
            // Create an HtmlDocument object to parse the HTML, then load it in with the html we got.
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            // Select the table with the covid statistics
            var statsTable = htmlDocument.DocumentNode.SelectSingleNode("//table[@id='main_table_countries_today']");
            // List to store the extracted data
            var stats = new List<CountryStats>();
            // Parse the table and extract country stats
            // Select all rows in the table body
            var rows = statsTable.SelectNodes(".//tbody/tr");
            foreach (var row in rows)
            {
                // Extract data from each cell in the row
                var cells = row.SelectNodes("td");
                if (cells != null && cells.Count >= 15)
                {
                    string countryName = cells[1].InnerText.Trim();

                    // Check if it's a continent row
                    if (string.IsNullOrWhiteSpace(countryName) && cells[0].InnerText.Contains("Total"))
                    {
                        countryName = cells[0].InnerText.Replace("Total:", "").Trim() + ":";
                    }

                    // Skip rows without a proper name or with placeholder data
                    if (string.IsNullOrWhiteSpace(countryName) || countryName == "Total:" ||
                        countryName.Contains("2020") || countryName.Contains("2021") ||
                        countryName.Contains("2022") || countryName.Contains("2023"))
                        continue;

                    var countryStats = new CountryStats(
                        countryName,
                        ParseLong(cells[2].InnerText),
                        ParseLong(cells[3].InnerText),
                        ParseLong(cells[4].InnerText),
                        ParseLong(cells[5].InnerText),
                        ParseLong(cells[6].InnerText),
                        ParseLong(cells[7].InnerText),
                        ParseLong(cells[8].InnerText),
                        ParseLong(cells[9].InnerText),
                        ParseDouble(cells[10].InnerText),
                        ParseDouble(cells[11].InnerText),
                        ParseLong(cells[12].InnerText),
                        ParseDouble(cells[13].InnerText),
                        ParseLong(cells[14].InnerText)
                    );
                    stats.Add(countryStats);
                }
            }
            return stats;
        }

        // Helper methods to parse string values to appropriate types
        private long ParseLong(string value)
        {
            return long.TryParse(value.Replace(",", ""), out long result) ? result : 0;
        }

        private double ParseDouble(string value)
        {
            return double.TryParse(value.Replace(",", ""), out double result) ? result : 0;
        }
    }
}