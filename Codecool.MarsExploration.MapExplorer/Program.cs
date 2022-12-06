﻿using Codecool.MarsExploration.MapExplorer.Configuration.Provider;
using Codecool.MarsExploration.MapExplorer.Configuration.Service;

using Codecool.MarsExploration.MapExplorer.MarsRover;


namespace Codecool.MarsExploration.MapExplorer;

internal class Program
{
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;

    public static void Main(string[] args)
    {
        var mapFile = $@"{WorkDir}\Resources\exploration-0.map";
        int dimension = 6, simulationSteps = 10, amountToGather = 5;
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


        var roverDeployer = new MarsRoverDeployer();
        var landingSpot = startingCoordinateProvider.GetStartingCoordinate(x, y);

        var mineralGoals = mineralListProvider.GetMinerals(possibleMinerals, amountToGather);
        var roverConfiguration = roverConfigurationProvider.GetRoverConfiguration(mapFile, landingSpot, mineralGoals, simulationSteps);
        IRoverConfigurationValidator roverConfigrationValidator = new RoverConfigurationValidator(_obstacles);

        var roverConfigurationIsValid = roverConfigrationValidator.Validate(roverConfiguration);
        roverDeployer.Deploy(roverConfigurationIsValid, roverConfiguration, null);

    }
}
