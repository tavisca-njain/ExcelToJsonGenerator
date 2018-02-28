using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Tavisca.Sceptr.TestSuite.Excel.Configuration;

namespace ExcelToJsonGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var provider = new ProviderFactory().GetProvider();

            var airportsSheetPath = @"Airports.xlsx";
            var airportDataSet = provider.GetDataSet(airportsSheetPath);
            var airports = Parser.ParseInfoFromDataSet(airportDataSet, "Airports", "AirportCode", "AirportName");
            GenerateJsonFiles(airports, "airports.json");
            Console.WriteLine("Airports json file generated successfully");

            var airlinesSheetPath = @"Airlines.xlsx";
            var airlineDataSet = provider.GetDataSet(airlinesSheetPath);
            var airlines = Parser.ParseInfoFromDataSet(airlineDataSet, "Airlines", "AirlineCode", "FullName");
            GenerateJsonFiles(airlines, "airlines.json");
            Console.WriteLine("Airlines json file generated successfully");

            Console.ReadLine();
        }

        private static void GenerateJsonFiles(List<Entity> airports, string fileName)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.None,
                Converters = new List<JsonConverter> { new StringEnumConverter { CamelCaseText = true } },
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                PreserveReferencesHandling = PreserveReferencesHandling.None
            };

            var jsonFile = JsonConvert.SerializeObject(airports, Formatting.Indented, settings);

            File.WriteAllText(Path.Combine(@"", fileName), jsonFile);
        }
    }
}
