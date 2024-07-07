using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidStatsScraper
{
    public class CountryStats
    {
        public string Country { get; set; }
        public long TotalCases { get; set; }
        public long NewCases { get; set; }
        public long TotalDeaths { get; set; }
        public long NewDeaths { get; set; }
        public long TotalRecovered { get; set; }
        public long NewRecovered { get; set; }
        public long ActiveCases { get; set; }
        public long SeriousCritical { get; set; }
        public double TotalCasesPerOneMillion { get; set; }
        public double DeathsPerOneMillion { get; set; }
        public long TotalTests { get; set; }
        public double TestsPerOneMillion { get; set; }
        public long Population { get; set; }

        public CountryStats(string country, long totalCases, long newCases, long totalDeaths, long newDeaths,
                            long totalRecovered, long newRecovered, long activeCases, long seriousCritical,
                            double totalCasesPerOneMillion, double deathsPerOneMillion, long totalTests,
                            double testsPerOneMillion, long population)
        {
            Country = country;
            TotalCases = totalCases;
            NewCases = newCases;
            TotalDeaths = totalDeaths;
            NewDeaths = newDeaths;
            TotalRecovered = totalRecovered;
            NewRecovered = newRecovered;
            ActiveCases = activeCases;
            SeriousCritical = seriousCritical;
            TotalCasesPerOneMillion = totalCasesPerOneMillion;
            DeathsPerOneMillion = deathsPerOneMillion;
            TotalTests = totalTests;
            TestsPerOneMillion = testsPerOneMillion;
            Population = population;
        }
    }
}