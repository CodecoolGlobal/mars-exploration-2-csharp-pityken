using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codecool.MarsExploration.MapExplorer.Configuration.Model;
using Codecool.MarsExploration.MapExplorer.Configuration.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.MarsRover
{
    public class MarsRoverDeployer
    {
        
        public MarsRoverDeployer()
        {

        }

        public MarsRover Deploy(bool canDeploy, RoverConfiguration roverConfig, Map map)
        {
         
            
            var startPoint = roverConfig.startingCoordinate;
            Coordinate currPos = startPoint;
            map.Representation[startPoint.X, startPoint.Y] = "S";
            var neighbours = new List<Coordinate>();
            if (startPoint.X != 0)
            {
                neighbours.Add(new Coordinate(startPoint.X - 1, startPoint.Y));
            }
            if (startPoint.Y != 0)
            {
                neighbours.Add(new Coordinate(startPoint.X, startPoint.Y - 1));
            }
            if (startPoint.X != map.Representation.Length + 1)
            {
                neighbours.Add(new Coordinate(startPoint.X + 1, startPoint.Y));
            }
            if (startPoint.Y != map.Representation.Length + 1)
            {
                neighbours.Add(new Coordinate(startPoint.X, startPoint.Y + 1));
            }

            foreach (var neighbour in neighbours)
            {
                if (map.Representation[neighbour.X, startPoint.Y] == " ")
                {
                    map.Representation[neighbour.X, neighbour.Y] = "r";
                    currPos = new Coordinate(neighbour.X, neighbour.Y);
                }
            }
            
            MarsRover Rover = new MarsRover(1, currPos, 10, new List<Coordinate>(){});
            return Rover;
        }
    }
}