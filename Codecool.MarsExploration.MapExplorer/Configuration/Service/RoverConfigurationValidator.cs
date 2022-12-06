using Codecool.MarsExploration.MapExplorer.Configuration.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codecool.MarsExploration.MapExplorer.Configuration.Service
{
    internal class RoverConfigurationValidator : IRoverConfigurationValidator
    {
        private IEnumerable<string> _obstacles;
        public RoverConfigurationValidator(IEnumerable<string> obstacles)
        {
            _obstacles = obstackles;
        }

        public bool Validate(RoverConfiguration roverConfig)
        {
            var map = File.ReadAllLines(roverConfig.fileLocation);
            var startingCoordinate = roverConfig.startingCoordinate;
            if (_obstackles.Any(x => x != map[startingCoordinate.X][startingCoordinate.Y].ToString()))
            {
                if (_obstackles.Any(x => x == map[startingCoordinate.X - 1][startingCoordinate.Y].ToString())) {
                    return false;
                }
                if (_obstackles.Any(x => x == map[startingCoordinate.X][startingCoordinate.Y - 1].ToString()))
                {
                    return false;
                }
                if (_obstackles.Any(x => x == map[startingCoordinate.X + 1][startingCoordinate.Y].ToString()))
                {
                    return false;
                }
                if (_obstackles.Any(x => x == map[startingCoordinate.X][startingCoordinate.Y + 1].ToString()))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
