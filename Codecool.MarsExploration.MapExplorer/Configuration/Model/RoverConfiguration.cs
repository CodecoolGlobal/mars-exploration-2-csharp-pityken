using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Configuration.Model
{
    public record RoverConfiguration(
        string fileLocation,
        Coordinate startingCoordinate,
        IEnumerable<string> mineralList,
        int simulationSteps);
}
