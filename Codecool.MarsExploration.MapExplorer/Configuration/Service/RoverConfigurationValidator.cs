using Codecool.MarsExploration.MapExplorer.Configuration.Model;

namespace Codecool.MarsExploration.MapExplorer.Configuration.Service
{
    internal class RoverConfigurationValidator : IRoverConfigurationValidator
    {
        private readonly IEnumerable<string> _obstacles;
        public RoverConfigurationValidator(IEnumerable<string> obstacles)
        {
            _obstacles = obstacles;
        }

        public bool Validate(RoverConfiguration roverConfig)
        {
            if (roverConfig.fileLocation == "" || roverConfig.mineralList.Count() == 0 || roverConfig.simulationSteps == 0)
            {
                return false;
            }
            var map = File.ReadAllLines(roverConfig.fileLocation);
            var startingCoordinate = roverConfig.startingCoordinate;
            if (_obstacles.Any(x => x != map[startingCoordinate.X][startingCoordinate.Y].ToString()))
            {
                if (_obstacles.Any(x => x == map[startingCoordinate.X - 1][startingCoordinate.Y].ToString()))
                {
                    return false;
                }
                if (_obstacles.Any(x => x == map[startingCoordinate.X][startingCoordinate.Y - 1].ToString()))
                {
                    return false;
                }
                if (_obstacles.Any(x => x == map[startingCoordinate.X + 1][startingCoordinate.Y].ToString()))
                {
                    return false;
                }
                if (_obstacles.Any(x => x == map[startingCoordinate.X][startingCoordinate.Y + 1].ToString()))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
