using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codecool.MarsExploration.MapExplorer.Configuration.Model;
using Codecool.MarsExploration.MapExplorer.Configuration.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.MarsRover
{
    public class MarsRoverDeployer
    {
        private readonly IRoverConfigurationValidator _configValidator = new RoverConfigurationValidator();

        public MarsRoverDeployer()
        {

        }

        public void Deploy(bool canDeploy, RoverConfiguration roverConfig, )
        {
            if (!canDeploy) 
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("The Rover cannot deploy on the given coordinates!\n");
                Console.ForegroundColor = ConsoleColor.Gray;
                return;
            }
            roverConfig.startingCoordinate
        }
    }

}


