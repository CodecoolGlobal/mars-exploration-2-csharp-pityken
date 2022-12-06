using Codecool.MarsExploration.MapExplorer.Configuration.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Configuration.Provider
{
    public class StartingCoordinateProvider : IStartingCoordinateProvider
    {
        private Random _random = new Random();
        public Coordinate GetStartingCoordinate(int maximumSize)
        {
            return new Coordinate(_random.Next(0, maximumSize), _random.Next(0, maximumSize));
        }
    }
}
