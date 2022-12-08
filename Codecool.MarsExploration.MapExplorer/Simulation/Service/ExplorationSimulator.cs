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

        public Coordinate FindNearestResourceCoordinate()
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
            var visibleResourceDict = distanceDictionary.Where(x => x.Value <= _context.Rover.SightDistance ? Console.WriteLine(x) : Console.WriteLine("nem"));

            var orderedDict = visibleResourceDict.OrderBy(x => x.Value);
            foreach(var kvp in visibleResourceDict)
            {
                Console.WriteLine("visDict====>{0}", kvp.Value);
            }
            //Console.WriteLine("disDict====>{0}", distanceDictionary.Count());
            //Console.WriteLine("ResDict====>{0}", resCoords.Count());
            //Console.WriteLine("OrdDict====>{0}", orderedDict.Count());
            
            return new Coordinate(0, 0); 
                //new Coordinate(orderedDict.First().Key.X, orderedDict.First().Key.Y);

        }

        public IEnumerable<Coordinate> CheckNeighbours()
        {
            var map = _context.map.Representation;
            var startPoint = _context.Rover.currentPosition;
            List<Coordinate> possibleTiles = new();
            var neighbours = new List<Coordinate>();
            if (startPoint.Y != 0)
            {
                neighbours.Add(new Coordinate(startPoint.Y - 1, startPoint.X));
            }
            if (startPoint.X != 0)
            {
                neighbours.Add(new Coordinate(startPoint.Y, startPoint.X - 1));
            }
            if (startPoint.Y != map.Length - 1)
            {
                neighbours.Add(new Coordinate(startPoint.Y + 1, startPoint.X));
            }
            if (startPoint.X != map.Length - 1)
            {
                neighbours.Add(new Coordinate(startPoint.Y, startPoint.X + 1));
            }


            foreach(var neighbour in neighbours)
            {
                if (map[neighbour.X, neighbour.Y] != "#" && map[neighbour.X, neighbour.Y] != "&")
                {
                    possibleTiles.Add(new Coordinate(startPoint.X - 1, startPoint.Y));
                }
            }
            return possibleTiles;
        }

        public Coordinate Move(Coordinate currentPos)
        {
            var nearest = FindNearestResourceCoordinate();
            var possibleTiles = CheckNeighbours().ToList();
            int newX = currentPos.X;
            int newY = currentPos.Y;


            if (nearest is null) 
            {
                List<Coordinate> visitedTiles = new List<Coordinate>();
                var Rover = _context.Rover;
                visitedTiles.Add(Rover.currentPosition);
                foreach(var visitedTile in visitedTiles)
                {
                if (possibleTiles.Contains(visitedTile))
                    {
                        possibleTiles.Remove(visitedTile);
                    }
                }

                var nextTile = possibleTiles[random.Next(possibleTiles.Count())];
                return nextTile;
            }

            
                if(currentPos.X > nearest.X)
                {
                        newX = currentPos.X - 1;
                }
                else if(currentPos.X < nearest.X) 
                {
                        newX = currentPos.X + 1;
                }
                if(currentPos.Y > nearest.Y)
                {
                        newY = currentPos.Y - 1;
                }
                else if(currentPos.Y < nearest.Y) 
                {
                        newY = currentPos.Y + 1;
                }
            

            if(_context.map.GetByCoordinate(new Coordinate(newX, newY)) != " ") 
            {
                    newX = possibleTiles.First().X;
                    newY = possibleTiles.First().Y;
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
