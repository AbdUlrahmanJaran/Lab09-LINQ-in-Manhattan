using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;


namespace Lab09_LINQ
{
    public class Program
    {
        static void Main(string[] args)
        {
            string path = @"../../../data.json";
            string read = File.ReadAllText(path);

            var jsonFile = JsonConvert.DeserializeObject<AllFeatures>(read);

            List<Feature> features = jsonFile.Features;

            var neighborhoods = from neighbor
                                in features
                                select neighbor.Properties.Neighborhood;

            var neighborhoodFilter = from neighbor
                                           in neighborhoods
                                           where (!neighbor.Equals(""))
                                           select neighbor;

            var Distinct = neighborhoods.Select(neighbor => neighbor).Distinct().Where(neighbor => !(neighbor.Equals("")));

            string userInput;
            int userInputInt = 1;

            Console.WriteLine($"Neighborhoods (All): {neighborhoods.Count()}");
            Console.WriteLine($"Neighborhoods (Filtered): {neighborhoodFilter.Count()}");
            Console.WriteLine($"Neighborhoods (Distinct): {Distinct.Count()}");
            Console.WriteLine();

            while (userInputInt != 0)
            {
                Console.WriteLine("Select");
                Console.WriteLine("1. Display all neighborhoods");
                Console.WriteLine("2. Display filtered neighborhoods");
                Console.WriteLine("3. Display distinct neighborhoods");
                Console.WriteLine("0. Exit");
                userInput = Console.ReadLine();

                try
                {
                    userInputInt = Convert.ToInt32(userInput);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                switch (userInputInt)
                {
                    case 1:
                        foreach (var neighborhood in neighborhoods)
                        {
                            Console.WriteLine(neighborhood);
                        }
                        break;

                    case 2:
                        foreach (var neighborhood in neighborhoodFilter)
                        {
                            Console.WriteLine(neighborhood);
                        }
                        break;

                    case 3:
                        foreach (var neighborhood in Distinct)
                        {
                            Console.WriteLine(neighborhood);
                        }
                        break;

                    case 0:
                        break;

                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                }
                Console.WriteLine();
            }
        }
    }

    public class AllFeatures
    {
        [JsonProperty("features")]
        public List<Feature> Features { get; set; }
    }

    public class Properties
    {
        [JsonProperty("neighborhood")]
        public string Neighborhood { get; set; }
    }

    public class Feature
    {
        [JsonProperty("properties")]
        public Properties Properties { get; set; }
    }
}
