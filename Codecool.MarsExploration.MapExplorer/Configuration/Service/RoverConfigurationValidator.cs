using Codecool.MarsExploration.MapExplorer.Configuration.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Configuration.Service
{
    public class RoverConfigurationValidator : IRoverConfigurationValidator
    {
        private readonly IEnumerable<string> _obstacles;
        public RoverConfigurationValidator(IEnumerable<string> obstacles)
        {
            _obstacles = obstacles;
        }

        private IEnumerable<char> GetNeighbours(Coordinate coordinate, string[] map)
        {
            var neighbours = new List<char>();
            if (coordinate.Y != 0)
            {
                neighbours.Add(map[coordinate.Y - 1][coordinate.X]);
            }
            if (coordinate.X != 0)
            {
                neighbours.Add(map[coordinate.Y][coordinate.X - 1]);
            }
            if (coordinate.Y != map.Length - 1)
            {
                neighbours.Add(map[coordinate.Y + 1][coordinate.X]);
            }
            if (coordinate.X != map[0].Length - 1)
            {
                neighbours.Add(map[coordinate.Y][coordinate.X + 1]);
            }
            return neighbours;
        }
        private bool CoordinateIsValid(Coordinate coordinate, string[] map)
        {
            var neighbours = GetNeighbours(coordinate, map);
            return neighbours.Any(x => !_obstacles.Contains(x.ToString()));
        }
        public bool Validate(RoverConfiguration roverConfig)
        {
            if (roverConfig.fileLocation == "" || roverConfig.mineralList.Count() == 0 || roverConfig.simulationSteps == 0)
            {
                return false;
            }
            var map = File.ReadAllLines(roverConfig.fileLocation);
            var startingCoordinate = roverConfig.startingCoordinate;
            return CoordinateIsValid(startingCoordinate, map);
        }
    }
}
