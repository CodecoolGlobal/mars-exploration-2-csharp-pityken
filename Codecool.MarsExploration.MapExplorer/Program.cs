using Codecool.MarsExploration.MapExplorer.Configuration.Provider;
using Codecool.MarsExploration.MapExplorer.Configuration.Service;
using Codecool.MarsExploration.MapExplorer.Exploration;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapExplorer.MarsRover;
using Codecool.MarsExploration.MapExplorer.Simulation.Model;
using Codecool.MarsExploration.MapExplorer.Simulation.Service;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;
using System.Security.Cryptography.X509Certificates;

namespace Codecool.MarsExploration.MapExplorer;

internal class Program
{
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;

    public static void Main(string[] args)
    {
        var mapFile = $@"{WorkDir}\Resources\exploration-0.map";
        var fileMap = File.ReadAllLines(mapFile);
        var row = fileMap.Length;
        var col = fileMap[0].Length;
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
        IMapLoader mapLoader = new MapLoader.MapLoader();
        Map loadedMap = mapLoader.Load(mapFile);
        IMineralListProvider mineralListProvider = new MineralListProvider();
        IStartingCoordinateProvider startingCoordinateProvider = new StartingCoordinateProvider();
        IRoverConfigurationProvider roverConfigurationProvider = new RoverConfigurationProvider();
        ExplorationOutcome Outcome = new ExplorationOutcome();
        var landingSpot = startingCoordinateProvider.GetStartingCoordinate(row, col);


        var roverDeployer = new MarsRoverDeployer();

        var mineralGoals = mineralListProvider.GetMinerals(possibleMinerals, amountToGather);
        var roverConfiguration = roverConfigurationProvider.GetRoverConfiguration(mapFile, landingSpot, mineralGoals, simulationSteps);
        
        IRoverConfigurationValidator roverConfigrationValidator = new RoverConfigurationValidator(_obstacles);
        var roverConfigurationIsValid = roverConfigrationValidator.Validate(roverConfiguration);
        //Validate BEFORE Deploy
        var Rover = roverDeployer.Deploy(roverConfigurationIsValid, roverConfiguration, loadedMap);
        

        SimulationContext Context = new SimulationContext(1, 300, Rover, Rover.currentPosition, loadedMap, possibleMinerals, Outcome, roverConfiguration);

        IExplorationSimulator explorationSimulator = new ExplorationSimulator(Context);
    }
}
