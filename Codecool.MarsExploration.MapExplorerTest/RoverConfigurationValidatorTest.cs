using Codecool.MarsExploration.MapExplorer.Configuration.Provider;
using Codecool.MarsExploration.MapExplorer.Configuration.Service;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorerTest
{
    public class Tests
    {
        private IRoverConfigurationValidator _configurationValidator;
        private IRoverConfigurationProvider _configurationProvider;
        private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;
        private IStartingCoordinateProvider _startingCoordinateProvider;
        private int _row, _col;
        private string _mapFile;

        [SetUp]
        public void Setup()
        {
            _mapFile = $@"{WorkDir}\Resources\exploration-0.map";
            var fileMap = File.ReadAllLines(_mapFile);
            _row = fileMap.Length;
            _col = fileMap[0].Length;
            var _obstacles = new List<string>() {
            "#",
            "&",
            "%",
            "*"
            };
            _configurationValidator = new RoverConfigurationValidator(_obstacles);
            _configurationProvider = new RoverConfigurationProvider();
            _startingCoordinateProvider = new StartingCoordinateProvider();
        }

        [Test]
        public void RoverConfigurationIsValid()
        {
            var configuration = _configurationProvider.GetRoverConfiguration(_mapFile, _startingCoordinateProvider.GetStartingCoordinate(_row, _col), new List<string> { "*", "%" }, 10);
            var result = _configurationValidator.Validate(configuration);
            Assert.That(result, Is.True);
        }
        [Test]
        public void CoordinateHasInvalidNeighbours()
        {
            _mapFile = $@"{WorkDir}\Resources\exploration-1.map";
            var configuration = _configurationProvider.GetRoverConfiguration(_mapFile, new Coordinate(20, 1), new List<string> { "*", "%" }, 10);
            var result = _configurationValidator.Validate(configuration);
            Assert.That(result, Is.False);
        }

        [TestCase(0, 0)]
        [TestCase(0, 10)]
        [TestCase(10, 0)]
        [TestCase(31, 31)]
        public void ValidEdgeCaseCoordinates(int x, int y)
        {
            var mapFile = $@"{WorkDir}\Resources\exploration-1.map";
            var configuration = _configurationProvider.GetRoverConfiguration(mapFile, new Coordinate(x, y), new List<string> { "*", "%" }, 10);
            var result = _configurationValidator.Validate(configuration);
            Assert.That(result, Is.True);
        }

        [Test]
        public void RoverConfigurationFileMissing()
        {
            var configuration = _configurationProvider.GetRoverConfiguration("", _startingCoordinateProvider.GetStartingCoordinate(_row, _col), new List<string> { "*", "%" }, 10);
            var result = _configurationValidator.Validate(configuration);
            Assert.That(result, Is.False);
        }
        [Test]
        public void RoverConfigurationMineralsAreNotDefined()
        {
            var configuration = _configurationProvider.GetRoverConfiguration(_mapFile, _startingCoordinateProvider.GetStartingCoordinate(_row, _col), new List<string> { }, 10);
            var result = _configurationValidator.Validate(configuration);
            Assert.That(result, Is.False);
        }
        [Test]
        public void RoverConfigurationTimeOutIsNotDefined()
        {
            var configuration = _configurationProvider.GetRoverConfiguration(_mapFile, _startingCoordinateProvider.GetStartingCoordinate(_row, _col), new List<string> { "*", "%" }, 0);
            var result = _configurationValidator.Validate(configuration);
            Assert.That(result, Is.False);
        }
    }
}