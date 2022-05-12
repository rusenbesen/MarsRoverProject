using MarsRoverProject.Business.Abstract;
using MarsRoverProject.Data.Entities;
using MarsRoverProject.Data.Enumerations;
using MarsRoverProject.Repository.Infrastructure;
using MarsRoverProject.Repository.Provider;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRoverProject.Business.Concrete
{
    public class MarsRoverService : IMarsRoverService
    {
        public Coordinate DoExecuter(int roverNumber, string[] plateauMaxPoints, string roverPosition, string instructions, List<string> allRoverPosition)
        {
            var roverPositionSplit = roverPosition.Split(' ');
            if (!Validate(roverNumber, plateauMaxPoints, roverPositionSplit, instructions))
            {
                return null; //Not Valid.
            }

            #region Make a possible Rover crash control list
            var otherRoverPositionList = allRoverPosition.Where(item => item != roverPosition).ToList(); //Current Rover Position is delete the Rovers crash control list. Because it can not crash on its own.
            var otherRoverCoordinateList = new List<Coordinate>();
            var coordinate = new Coordinate();
            foreach (var otherRoverPosition in otherRoverPositionList)
            {
                coordinate = new Coordinate();
                var otherRoverPositionSplit = otherRoverPosition.Split(' ');
                coordinate.X = Convert.ToInt32(otherRoverPositionSplit[0]);
                coordinate.Y = Convert.ToInt32(otherRoverPositionSplit[1]); //Rover Direction equalize is not necessary for Rovers crash control list.
                otherRoverCoordinateList.Add(coordinate);
            }
            #endregion

            #region Preparing Parameters
            int plateauMaxX = Convert.ToInt32(plateauMaxPoints[0]);
            int plateauMaxY = Convert.ToInt32(plateauMaxPoints[1]);
            coordinate = new Coordinate();
            coordinate.X = Convert.ToInt32(roverPositionSplit[0]);
            coordinate.Y = Convert.ToInt32(roverPositionSplit[1]);
            coordinate.Direction = new DirectionEnum().GetValue(roverPositionSplit[2]);
            ICommand command;
            #endregion

            #region Rover instructions actions
            foreach (var dir in instructions)
            {
                if (dir == MoveDirectionEnum.Left.Value)
                {
                    command = new MoveLeft();
                }
                else if (dir == MoveDirectionEnum.Right.Value)
                {
                    command = new MoveRight();
                }
                else if (dir == MoveDirectionEnum.MoveStraight.Value)
                {
                    command = new MoveStraight(plateauMaxX, plateauMaxY, roverNumber, otherRoverCoordinateList);
                }
                else
                {
                    return null;
                }

                var lastCoordinate = command.StartAction(coordinate);
                if (lastCoordinate == null)
                    return null;

                coordinate.Direction = lastCoordinate.Direction;
                coordinate.X = lastCoordinate.X;
                coordinate.Y = lastCoordinate.Y;
            }
            #endregion

            return coordinate;
        }

        private bool Validate(int roverNumber, string[] plateauMaxPoints, string[] roverPosition, string instructions)
        {
            #region Plateau Validate
            if (plateauMaxPoints.Length != 2)
            {
                Console.WriteLine("Plateau Coordinates Point Invalid.(Example:5 5)"); //throw new SystemException("");
                return false;
            }
            foreach (var plateauPoint in plateauMaxPoints)
            {
                if (!plateauPoint.All(char.IsNumber))
                {
                    Console.WriteLine("Plateau Coordinates Point Invalid.(Example:5 5)");
                    return false;
                }
            }
            #endregion

            #region Rover Position Validate
            if (roverPosition.Length != 3)
            {
                Console.WriteLine((roverNumber + 1) + ".Rover's Positions Input Invalid.(Example:1 2 N)");
                return false;
            }
            for (int i = 0; i < roverPosition.Length; i++)
            {
                if ((i == 0 || i == 1) && !roverPosition[i].All(char.IsNumber))
                {
                    Console.WriteLine((roverNumber + 1) + ".Rover's Position Coordinates Point Not Numeric.");
                    return false;
                }
                else if (i == 2 && new DirectionEnum().GetValue(roverPosition[2]) == null)
                {
                    Console.WriteLine((roverNumber + 1) + ".Rover's Position Direction Invalid.(Example:N,E,S,W)");
                    return false;
                }
            }
            #endregion

            #region Rover Instructions Validate
            foreach (var instruction in instructions)
            {
                if (new MoveDirectionEnum().GetValue(instruction) == null)
                {
                    Console.WriteLine((roverNumber + 1) + ".Rover's instructions command Invalid.(Example:R,L,M)");
                    return false;
                }
            }
            #endregion

            return true;
        }
    }
}
