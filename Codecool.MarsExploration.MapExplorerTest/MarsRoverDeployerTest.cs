using Codecool.MarsExploration.MapExplorer.Configuration.Provider;
using Codecool.MarsExploration.MapExplorer.Configuration.Service;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapExplorer.MarsRover;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codecool.MarsExploration.MapExplorerTest
{
    public class MarsRoverDeployerTest
    {
        private IRoverConfigurationProvider _configProvider;
        private IRoverConfigurationValidator _validator;
        private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;
        private MarsRoverDeployer _deployer = new MarsRoverDeployer();
        private IMapLoader _mapLoader;
        private int _row, _col;
        private string _mapFile;
        private IStartingCoordinateProvider _startingCoordinateProvider;
        private static int counter = 0;

        [SetUp]
        public void SetUp()
        {
            var _obstacles = new List<string>() {
            "#",
            "&",
            "%",
            "*"
            };
            _mapLoader = new MapLoader();
            _validator = new RoverConfigurationValidator(_obstacles);
            _mapFile = $@"{WorkDir}\Resources\exploration-0.map";
            var fileMap = File.ReadAllLines(_mapFile);
            _row = fileMap.Length;
            _col = fileMap[0].Length;
            _configProvider = new RoverConfigurationProvider();
            _startingCoordinateProvider = new StartingCoordinateProvider();

            var roverConfig = _configProvider.GetRoverConfiguration(
                    _mapFile, _startingCoordinateProvider.GetStartingCoordinate(_row, _col), new List<string> { "*", "%" }, 10);
            var canDeploy = _validator.Validate(roverConfig);
            var map = _mapLoader.Load(_mapFile);

            _deployer.Deploy(canDeploy, roverConfig, map);
            foreach(var symbol in map.Representation)
            {
                Console.WriteLine(symbol);
                if(symbol == "r") counter++;
            }
        }

        [Test]
        public void RoverCanDeploy()
        {
            var roverConfig = _configProvider.GetRoverConfiguration(
                    _mapFile, _startingCoordinateProvider.GetStartingCoordinate(_row, _col), new List<string> { "*", "%" }, 10);
            var canDeploy = _validator.Validate(roverConfig);
            Assert.True(canDeploy);
        }

        [Test]
        public void OneRoverIsOnTheMap()
        {
            Assert.That(counter, Is.EqualTo(1));
        }
    }
}
