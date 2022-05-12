using MarsRoverProject.Data.Entities;
using MarsRoverProject.Data.Enumerations;
using MarsRoverProject.Repository.Provider;
using System;
using System.Collections.Generic;

namespace MarsRoverProject.Repository.Infrastructure
{
    public class MoveStraight : ICommand
    {

        private int _plateauMaxX;
        private int _plateauMaxY;
        private int _roverNumber;
        private List<Coordinate> _otherRoverCoordinateList = new List<Coordinate>();

        public MoveStraight(int plateauMaxX, int plateauMaxY, int roverNumber, List<Coordinate> otherRoverCoordinateList)
        {
            this._plateauMaxX = plateauMaxX;
            this._plateauMaxY = plateauMaxY;
            this._roverNumber = roverNumber;
            this._otherRoverCoordinateList = otherRoverCoordinateList;
        }

        public Coordinate StartAction(Coordinate coordinate)
        {
            #region Rover Move
            if (coordinate.Direction.Value == DirectionEnum.North.Value)
            {
                if (coordinate.Y >= _plateauMaxY)
                {
                    coordinate = RoverOutsidePlateau();
                }
                else
                {
                    coordinate.Y += 1;
                }
            }
            else if (coordinate.Direction.Value == DirectionEnum.East.Value)
            {
                if (coordinate.X >= _plateauMaxX)
                {
                    coordinate = RoverOutsidePlateau();
                }
                else
                {
                    coordinate.X += 1;
                }
            }
            else if (coordinate.Direction.Value == DirectionEnum.South.Value)
            {
                if (coordinate.Y != 0)
                {
                    coordinate.Y -= 1;
                }
                else
                {
                    coordinate = RoverOutsidePlateau();
                }
            }
            else if (coordinate.Direction.Value == DirectionEnum.West.Value)
            {
                if (coordinate.X != 0)
                {
                    coordinate.X -= 1;
                }
                else
                {
                    coordinate = RoverOutsidePlateau();
                }
            }
            #endregion

            #region Rover crash control
            if (coordinate != null)//Rover is not outside the plateau. 
            {
                foreach (var otherRoverCoordinate in _otherRoverCoordinateList)//Rovers crash control list
                {
                    if (otherRoverCoordinate.X == coordinate.X && otherRoverCoordinate.Y == coordinate.Y)
                    {
                        coordinate = RoverCrash();
                    }
                }
            }
            #endregion

            return coordinate;
        }

        private Coordinate RoverOutsidePlateau()
        {
            Console.WriteLine((_roverNumber+1)+".Rover is outside the plateau.");
            return null;
        }

        private Coordinate RoverCrash()
        {
            Console.WriteLine("Two Rovers crashed.");
            return null;
        }
    }

}
