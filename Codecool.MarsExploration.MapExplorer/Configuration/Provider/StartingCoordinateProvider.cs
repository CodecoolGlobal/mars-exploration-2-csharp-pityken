using Codecool.MarsExploration.MapExplorer.Configuration.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Configuration.Provider
{
    public class StartingCoordinateProvider : IStartingCoordinateProvider
    {
        private Random _random = new Random();
        public Coordinate GetStartingCoordinate(int minimumSize, int maximumSize)
        {
            return new Coordinate(_random.RandomWithinRanges(minimumSize, maximumSize), _random.RandomWithinRanges(minimumSize, maximumSize));
        }
    }
}
