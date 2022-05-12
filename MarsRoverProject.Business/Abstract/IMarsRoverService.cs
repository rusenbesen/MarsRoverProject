using MarsRoverProject.Data.Entities;
using System.Collections.Generic;

namespace MarsRoverProject.Business.Abstract
{
    public interface IMarsRoverService
    {
        Coordinate DoExecuter(int roverNumber, string[] plateauMaxPoints, string roverPosition, string instructions, List<string> allRoverPosition);
    }
}
