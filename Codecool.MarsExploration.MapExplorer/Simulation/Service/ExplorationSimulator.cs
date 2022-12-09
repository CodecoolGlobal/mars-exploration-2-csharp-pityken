using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapExplorer.Simulation.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codecool.MarsExploration.MapExplorer.Simulation.Service
{
    public class ExplorationSimulator : IExplorationSimulator
    {
        private SimulationContext _context;
        Random random = new Random();
        Coordinate prevCoordinate;
        public ExplorationSimulator(SimulationContext context)
        {
            _context= context;
        }
        
        public IEnumerable<Coordinate> GetResourceCoordinate(string resourceSymbol)
        {
            int symbolCounter = 0;
            foreach(var symbol in _context.ResourceSymbols) 
            {
                if (symbol == resourceSymbol) symbolCounter++;
            }
            
            var map = _context.map.Representation;
            List<Coordinate> coordinates = new List<Coordinate>();
            for(int i = 0; i < map.GetLength(0); i++)
            {
                for(int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i,j] == resourceSymbol) 
                    {
                        coordinates.Add(new Coordinate(i,j));
                    }
                }
            }
            return coordinates;
        }

        public Dictionary<string, List<Coordinate>> FillDictionary()
        {
            var symbols = _context.ResourceSymbols;
            Dictionary<string, List<Coordinate>> resourceCoordinates = new() { };
            foreach (var symbol in symbols) 
            {
                resourceCoordinates.Add(symbol, GetResourceCoordinate(symbol).ToList());
            }
            
            return resourceCoordinates;
        }

        public double CalculateDistance(Coordinate RoverCoord, Coordinate ResourceCoord) 
        {
            var result = Math.Sqrt( (Math.Pow(ResourceCoord.X - RoverCoord.X, 2)) + (Math.Pow(ResourceCoord.Y - RoverCoord.Y, 2)));
            return result;
        }

        public Coordinate FindNearestResourceCoordinate(int limit, Coordinate currPos)
        {
            var resCoords = FillDictionary();
            Dictionary<Coordinate, double> distanceDictionary = new() { };
            foreach (var resCoord in resCoords)
            {
                foreach (var coord in resCoord.Value)
                {
                    distanceDictionary.Add(coord, CalculateDistance(_context.Rover.currentPosition, coord));
                    //Console.WriteLine($"{_context.Rover.currentPosition.X}, {_context.Rover.currentPosition.Y}\t|\t{coord.X}, {coord.Y}, {CalculateDistance(_context.Rover.currentPosition, coord)}");
                }
            }
            var visibleResourceDict = distanceDictionary.Where(x => x.Value <= _context.Rover.SightDistance);

            var orderedDict = visibleResourceDict.OrderBy(x => x.Value);
            Console.WriteLine(limit); 
            
            if(orderedDict.Count() > 0)
            {
                return new Coordinate(orderedDict.First().Key.X, orderedDict.First().Key.Y);
            }
            
            //Console.WriteLine("postile-------------------->{0} {1}", posTiles.First().X, posTiles.First().Y);
            return new Coordinate(-1 , -1);

        }

        public IEnumerable<Coordinate> CheckNeighbours(Coordinate currPos)
        {
            var map = _context.map.Representation;
            var startPoint = currPos;
            List<Coordinate> possibleTiles = new();
            var neighbours = new List<Coordinate>();
            if (startPoint.X != 0)
            {
                neighbours.Add(new Coordinate(startPoint.X - 1, startPoint.Y));
            }
            if (startPoint.Y != 0)
            {
                neighbours.Add(new Coordinate(startPoint.X, startPoint.Y - 1));
            }
            if (startPoint.X != map.Length + 1)
            {
                neighbours.Add(new Coordinate(startPoint.X + 1, startPoint.Y));
            }
            if (startPoint.Y != map.Length + 1)
            {
                neighbours.Add(new Coordinate(startPoint.X, startPoint.Y + 1));
            }


            foreach(var neighbour in neighbours)
            {
                //Console.WriteLine("{0} {1}",neighbour.X, neighbour.Y);
                if (map[neighbour.X, neighbour.Y] != "#" && map[neighbour.X, neighbour.Y] != "&")
                {
                    possibleTiles.Add(new Coordinate(neighbour.X, neighbour.Y));
                }
            }
            return possibleTiles;
        }

            
        public Coordinate Move(Coordinate currentPos, Coordinate nearest)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Curr: {0} {1}", currentPos.X, currentPos.Y);
            Console.WriteLine("Near: {0} {1}", nearest.X, nearest.Y);
            Console.ForegroundColor = ConsoleColor.Gray;

            var possibleTiles = CheckNeighbours(currentPos).ToList();
            int newX = currentPos.X;
            int newY = currentPos.Y;

            if (nearest == new Coordinate(-1, -1))
            {
                var nextTile = possibleTiles.Where(x => x != prevCoordinate).ToList()[random.Next(possibleTiles.Count()-1)];
                prevCoordinate = currentPos;
                return nextTile;
            }
            

            else
            {
                
                //Set X Coordinate
                if (currentPos.X > nearest.X)
                {
                    newX = currentPos.X - 1;
                }
                else if (currentPos.X < nearest.X)
                {
                    newX = currentPos.X + 1;
                }
                else
                {
                    newX = possibleTiles.First().X;
                }

                //Set Y Coordinate
                if (currentPos.Y > nearest.Y)
                {
                    newY = currentPos.Y - 1;
                }
                else if (currentPos.Y < nearest.Y)
                {
                    newY = currentPos.Y + 1;
                }
                else
                {
                    newY = possibleTiles.First().Y;
                }

            }

            return new Coordinate(newX, newY);
        }

        public string PickUpResource(Coordinate currPos)
        {
            var symbol = _context.map.Representation[currPos.X, currPos.Y];
            return symbol;
        }
    }
}
