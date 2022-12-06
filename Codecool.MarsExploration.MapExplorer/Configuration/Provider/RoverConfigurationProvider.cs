using Codecool.MarsExploration.MapExplorer.Configuration.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Configuration.Provider
{
    public class RoverConfigurationProvider : IRoverConfigurationProvider
    {
        public RoverConfiguration GetRoverConfiguration(
            string fileLocation,
            Coordinate startingCoordinate,
            IEnumerable<string> mineralList,
            int simulationSteps)
        {
            return new RoverConfiguration(
                fileLocation,
                startingCoordinate,
                mineralList,
                simulationSteps
                );
        }
    }
}
