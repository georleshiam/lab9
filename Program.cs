using System;
using System.IO;
using System.Text.Json;

namespace TestingConsoleOutputWindows
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World! - DJ");
            string filePath = "C:\\Users\\DJ\\Documents\\GitHub\\TestingConsoleOutputWindows\\TestingConsoleOutputWindows\\data.json";
            string file = File.ReadAllText(filePath);

            FeatureCollection featureCollection = JsonSerializer.Deserialize<FeatureCollection>(file);
            Console.WriteLine("Deserialized the json data");

            Location[] locations = featureCollection.features;
            Console.WriteLine(locations);
            //Console.WriteLine("\nPart 1 - All Neighborhoods Grouped\n");
            ////Output all 147 locations
            //Part1WithLINQ(locations);
            //Console.WriteLine("\nPart 2 - All Neighboorhoods with Names\n");
            ////Output neighborhoods with names - expecting 143
            //Part2(locations);
            //Console.WriteLine("\nPart 3 - All Neighboorhood Names - No Duplicates\n");
            //Remove the duplicates (Final Total: 39 neighborhoods)
            //Part3(locations);

            //Rewrite the queries from above and consolidate all into one single query.
            //Console.WriteLine("\nPart 4 - ComboQuery \n");
            //Part4(locations);
            //Rewrite at least one of these questions only using the opposing method (example: Use LINQ Query statements instead of LINQ method calls and vice versa.)
            Part5(locations);
        }

        public static void Part5(Location[] items)
        {
           
            var neighborHoodQuery1 = from item in items
                                     group item by item.properties.neighborhood into grouped
                                     select new { Key = grouped.Key, Value = grouped.Count() };

            var neighborHoodQueryOne = items
                .Where(item => !string.IsNullOrEmpty(item.properties.neighborhood))
                .GroupBy(item => item.properties.neighborhood)
                .Select(grouped => new { Key = grouped.Key, Value = grouped.Count() });


            
            var neighborHoodQuery2 = from item in items
                                     where item.properties.neighborhood != ""
                                     select item;
            //Console.WriteLine("SYNTAX QUERY - SELECT ITEM\n");
            foreach (var neighborhood in neighborHoodQuery2)
            {
                //  Console.WriteLine("X: {0}, Y: {1} ", neighborhood.geometry.coordinates[0], neighborhood.geometry.coordinates[1]);
            }
            
            var neighborhoodQueryTwo = items
                .Where(item => !string.IsNullOrEmpty(item.properties.neighborhood));

            foreach (var neighborhood in neighborhoodQueryTwo)
            {
                // Console.WriteLine("X: {0}, Y: {1} ", neighborhood.geometry.coordinates[0], neighborhood.geometry.coordinates[1]);
            }


           
            var neighborhoodQuery4 = items
                                    .Where(item => !string.IsNullOrEmpty(item.properties.neighborhood))
                                    .Select(item => item.properties.neighborhood)
                                    .Distinct();

            
            var newQuery = (from item in items
                            where (item.properties.neighborhood != "" && item.properties.neighborhood != null)
                            && ((item.geometry.coordinates[0] == -73.986212)
                            && (item.geometry.coordinates[1] == 40.715775))
                            select item.properties.neighborhood);
            foreach (
        }


        public static void Part4(Location[] items)
        {
            var neighborhoodQuery = items
                .Where(item => !string.IsNullOrEmpty(item.properties.neighborhood))
                .Select(item => item.properties.neighborhood)
                .Distinct();

            foreach (var neighborhood in neighborhoodQuery)
            {
                Console.WriteLine(neighborhood);
            }
        }
        public static void Part3(Location[] items)
        {
            //var neighborHoodQuery = from item in items
            //    where !string.IsNullOrEmpty(item.properties.neighborhood)
            //      group item by item.properties.neighborhood into grouped
            //      select item;

            var distinctQuery = (from item in items
                                 where !string.IsNullOrEmpty(item.properties.neighborhood)
                                 select item.properties.neighborhood).Distinct();
            var distinctMethod = items
                                .Where(item => !string.IsNullOrEmpty(item.properties.neighborhood))
                                .Select(item => item.properties.neighborhood)
                                .Distinct();

            foreach (string n in distinctMethod)
            {
                Console.WriteLine(n);
            }

        }
        public static void Part2(Location[] items)
        {
            //var query = from item in items
            //            where items are what we need
            //            select item;

            var neighborHoodQuery = from item in items
                                    where item.properties.neighborhood != ""
                                    //group item by item.properties.neighborhood into grouped
                                    select item;

            foreach (var location in neighborHoodQuery)
            {
                Console.WriteLine(location.properties.neighborhood);
                //Console.WriteLine($"{location.Key}: {location.Value}");
            }
        }

       
        public static void Part1(Location[] items)
        {
            Dictionary<string, int> locationAppearances = new Dictionary<string, int>();
            for (int i = 0; i < items.Length; i++)
            {
                Location currentLocation = items[i];
                string neighborhood = currentLocation.properties.neighborhood;
                bool neighborhoodAlreadyInDictionary = locationAppearances.ContainsKey(neighborhood);
                if (neighborhoodAlreadyInDictionary == false)
                {
                    locationAppearances.Add(neighborhood, 1);
                }
                else
                {
                    int currentValue = locationAppearances.GetValueOrDefault(neighborhood);
                    int newValue = currentValue + 1;
                    locationAppearances[neighborhood] = newValue;

                }
            }

            foreach (var location in locationAppearances)
            {
                Console.WriteLine($"{location.Key}: {location.Value}");
            }

        }

        public static void Part1WithLINQ(Location[] items)
        {
            var neighborHoodQuery = from item in items
                                    group item by item.properties.neighborhood into grouped
                                    select new { Key = grouped.Key, Value = grouped.Count() };

            foreach (var location in neighborHoodQuery)
            {
                Console.WriteLine($"{location.Key}: {location.Value}");
            }
        }


    }
}