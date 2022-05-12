using MarsRoverProject.Data.Entities;

namespace MarsRoverProject.Repository.Provider
{
    public interface ICommand
    {
        Coordinate StartAction(Coordinate coordinate);
    }
}
