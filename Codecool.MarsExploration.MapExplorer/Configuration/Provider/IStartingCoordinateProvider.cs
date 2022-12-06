using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Configuration.Provider
{
    public interface IStartingCoordinateProvider
    {
        Coordinate GetStartingCoordinate(int min, int max);
    }
}
