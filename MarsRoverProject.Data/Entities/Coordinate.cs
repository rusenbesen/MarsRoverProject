using MarsRoverProject.Data.Enumerations;

namespace MarsRoverProject.Data.Entities
{
    public class Coordinate
    {
        
        public int X { get; set; }
               
        public int Y { get; set; }

        public DirectionEnum Direction { get; set; }
    }
}
