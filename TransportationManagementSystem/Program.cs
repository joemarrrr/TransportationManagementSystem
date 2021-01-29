using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using TransportationManagementSystem.Models;
using TransportationManagementSystem.Services.Concretes;
using TransportationManagementSystem.Services.Contracts;

namespace TransportationManagementSystem
{
    class Program
    {
        private static readonly string _resourceUrl = ConfigurationManager.AppSettings["ResourceUrl"];
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static ITransportation _iTranspo;
        private static int noOfPassengers = 0;

        static async Task Main(string[] args)
        {
            try
            {
                _iTranspo = new Transportation();
                string input = "";
                string starShipUrl = "";

                while (true)
                {
                    Console.Write("Enter number of passengers: ");
                    input = Console.ReadLine();

                    if (input.All(char.IsDigit)) // check if input is numeric
                    {
                        noOfPassengers = Convert.ToInt32(input);
                        break;
                    }
                    else
                        Console.WriteLine("Invalid input, please try again.");
                }
                
                starShipUrl = _iTranspo.GetResources(_resourceUrl).Starships; //get list of resources and select startships only

                await GetAvailableStarship(starShipUrl); // wait until all startships are printed
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to process, please contact your administrator.");
                _logger.Error(ex.ToString()); //write down the whole exceptions
            }
            finally
            {
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
        }
        private async static Task GetAvailableStarship(string starShipUrl)
        {
            List<Task> tasks = new List<Task>();
            while (starShipUrl != null)
            {
                var starShipDetails = _iTranspo.GetStarships(starShipUrl); //get list of startships
                foreach (var detail in starShipDetails.Results.Where(a => a.Passengers.All(char.IsDigit) &&
                    int.Parse(a.Passengers) >= noOfPassengers && a.Pilots.Length > 0)) //check if the spaceship can carry the passengers and if there's a pilot
                {
                    tasks.Add(GetPilots(detail)); // execute GetPilots asynchronously
                }
                starShipUrl = starShipDetails.Next;
            }
            await Task.WhenAll(tasks); // wait until all tasks are done 
        }
        private static async Task GetPilots(StarhipDetails detail)
        {
            await Task.Run(() =>
            {
                string output = detail.Name + " - ";
                foreach (string pilot in detail.Pilots)
                {
                    var pilotDetails = _iTranspo.GetPilots(pilot); // get the names of the pilot
                    WriteStarshipAndPilot(output + pilotDetails.Name);
                }
            });
        }
        private static void WriteStarshipAndPilot(string startshipAndPilot)
        {
            Console.WriteLine(startshipAndPilot);
        }
    }
}
