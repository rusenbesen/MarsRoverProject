using MarsRoverProject.Business.Abstract;
using MarsRoverProject.Business.Concrete;
using MarsRoverProject.Data.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace MarsRoverProject
{
    class Program
    {
        static void Main(string[] args)
        {
            #region StartupService
            var services = new ServiceCollection();
            services.AddSingleton<IMarsRoverService, MarsRoverService>();
            #endregion

            #region Console Inputs and Preparing Parameters
            Console.Write("Plateau Max Coordinates Point(Example:5 5)=");
            var plateauMaxPoints = Console.ReadLine().Split(' ');
            Console.Write("Rover Count(For Example:2)=");
            var roverCount = Console.ReadLine();
            List<string> roverPositionList = new List<string>();
            List<string> roverPositionControlList = new List<string>(); //For possible Rovers crash control check list
            List<string> roverInstructionsList = new List<string>();
            string instructions = "";
            string roverPosition = "";
            for (int i = 0; i < Convert.ToInt32(roverCount); i++)
            {
                Console.Write("The " + (i + 1) + ".Rover's Position(Example:1 2 N)=");
                roverPosition = Console.ReadLine();
                roverPositionList.Add(roverPosition);
                roverPositionControlList.Add(roverPosition);
                Console.Write("The " + (i + 1) + ".Rover's Series Of Instructions(Example:LMLMLMLMM)=");
                instructions = Console.ReadLine();
                roverInstructionsList.Add(instructions);
            }
            #endregion

            #region ServiceProvider
            using (var _serviceProvider = services.BuildServiceProvider(true))
            {
                var _marsRoverServiceService = _serviceProvider.GetService<IMarsRoverService>();
                Coordinate coordinate = new Coordinate();
                var roverLastCoordinate = "";
                for (int i = 0; i < Convert.ToInt32(roverCount); i++)
                {
                    coordinate = _marsRoverServiceService.DoExecuter(i, plateauMaxPoints, roverPositionList[i], roverInstructionsList[i], roverPositionControlList);
                    if (coordinate != null)
                    {
                        roverLastCoordinate = coordinate.X + " " + coordinate.Y + " " + coordinate.Direction.Value;
                        roverPositionControlList.Remove(roverPositionList[i]);//Rover old position is delete the Rovers crash control check list
                        roverPositionControlList.Add(roverLastCoordinate);//Rover last position is add the Rovers crash control check list
                        Console.WriteLine((i + 1) + ".Rover's Last Position=" + roverLastCoordinate);
                    }
                    else
                    {
                        Console.WriteLine("Something Happened to the Rover");
                        break;
                    }
                }
            }
            #endregion
        }
    }
}
