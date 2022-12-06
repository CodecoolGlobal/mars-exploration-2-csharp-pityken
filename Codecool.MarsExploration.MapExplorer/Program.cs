using Codecool.MarsExploration.MapExplorer.Configuration.Provider;
using Codecool.MarsExploration.MapExplorer.Configuration.Service;

namespace Codecool.MarsExploration.MapExplorer;

internal class Program
{
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;

    public static void Main(string[] args)
    {
        var mapFile = $@"{WorkDir}\Resources\exploration-0.map";
        var row = File.ReadAllLines(mapFile).Length;
        var col = File.ReadAllLines(mapFile)[0].Length;
        int simulationSteps = 10, amountToGather = 5;
        var possibleMinerals = new List<string>() {
            "*",
            "%",
        };
        var _obstacles = new List<string>() {
            "#",
            "&",
            "%",
            "*"
        };

        IMineralListProvider mineralListProvider = new MineralListProvider();
        IStartingCoordinateProvider startingCoordinateProvider = new StartingCoordinateProvider();
        IRoverConfigurationProvider roverConfigurationProvider = new RoverConfigurationProvider();

        var landingSpot = startingCoordinateProvider.GetStartingCoordinate(row, col);
        var mineralGoals = mineralListProvider.GetMinerals(possibleMinerals, amountToGather);
        var roverConfiguration = roverConfigurationProvider.GetRoverConfiguration(mapFile, landingSpot, mineralGoals, simulationSteps);

        IRoverConfigurationValidator roverConfigrationValidator = new RoverConfigurationValidator(_obstacles);

        var roverConfigurationIsValid = roverConfigrationValidator.Validate(roverConfiguration);

    }
}
