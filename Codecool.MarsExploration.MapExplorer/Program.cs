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
        MarsRover.MarsRover Rover;

        if (!roverConfigurationIsValid)
        {
            Console.WriteLine("Configuration was invalid");
            return;
        }

        Rover = roverDeployer.Deploy(roverConfigurationIsValid, roverConfiguration, loadedMap);
        var currPos = Rover.currentPosition;
        SimulationContext Context = new SimulationContext(1, 50, Rover, Rover.currentPosition, loadedMap, possibleMinerals, Outcome, roverConfiguration);
        IExplorationSimulator explorationSimulator = new ExplorationSimulator(Context);
        int waterCount = 0;
        int mineralCount = 0;
        int limit = Context.StepLimit;
        var nearestResource = explorationSimulator.FindNearestResourceCoordinate();
        do
        {

            var resourceList = roverConfiguration.mineralList.ToList();
            while (currPos != nearestResource)
            {
                currPos = explorationSimulator.Move(Rover.currentPosition);
            }

            foreach (var symbol in resourceList)
            {
                if (symbol == "*") waterCount++;
                mineralCount++;
            }

            var encounteredSymbol = Context.map.Representation[currPos.X, currPos.Y];
            explorationSimulator.PickUpResource(currPos);
            if (encounteredSymbol == "*") waterCount--;
            if (encounteredSymbol == "%") mineralCount--;
            limit--;
            nearestResource = explorationSimulator.FindNearestResourceCoordinate();
        } while ((mineralCount != 0 && waterCount != 0) || limit != 0);
    }
}

//1 tud 0 nem tud
/*https://github.com/valantonini/AStar */
