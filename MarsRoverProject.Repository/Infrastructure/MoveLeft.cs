using MarsRoverProject.Data.Entities;
using MarsRoverProject.Data.Enumerations;
using MarsRoverProject.Repository.Provider;

namespace MarsRoverProject.Repository.Infrastructure
{
    public class MoveLeft : ICommand
    {
        public Coordinate StartAction(Coordinate coordinate)
        {
            if (coordinate.Direction.Value == DirectionEnum.North.Value)
            {
                coordinate.Direction = DirectionEnum.West;
            }
            else if (coordinate.Direction.Value == DirectionEnum.East.Value)
            {
                coordinate.Direction = DirectionEnum.North;
            }
            else if (coordinate.Direction.Value == DirectionEnum.South.Value)
            {
                coordinate.Direction= DirectionEnum.East;
            }
            else if (coordinate.Direction.Value == DirectionEnum.West.Value)
            {
                coordinate.Direction= DirectionEnum.South;
            }
            return coordinate;
        }
    }
}
