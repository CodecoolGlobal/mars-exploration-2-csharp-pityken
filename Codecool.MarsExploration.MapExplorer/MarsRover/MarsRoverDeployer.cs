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

        public MarsRoverDeployer()
        {

        }

        public void Deploy(bool canDeploy, RoverConfiguration roverConfig, Map map)
        {
            if (!canDeploy) 
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("The Rover cannot deploy on the given coordinates!\n");
                Console.ForegroundColor = ConsoleColor.Gray;
                return;
            }
            
            var startPoint = roverConfig.startingCoordinate;
            map.Representation[startPoint.X, startPoint.Y] = "S";

            if (map.Representation[startPoint.X - 1, startPoint.Y] == " ")
            {
                map.Representation[startPoint.X - 1, startPoint.Y] = "r";
            }

            else if (map.Representation[startPoint.X + 1, startPoint.Y] == " ")
            {
                map.Representation[startPoint.X + 1, startPoint.Y] = "r";
            }

            else if (map.Representation[startPoint.X, startPoint.Y - 1] == " ")
            {
                map.Representation[startPoint.X, startPoint.Y - 1] = "r";
            }

            else if (map.Representation[startPoint.X, startPoint.Y + 1] == " ")
            {
                map.Representation[startPoint.X, startPoint.Y + 1] = "r";
            }
        }
    }
}