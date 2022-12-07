using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapExplorer.Simulation.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codecool.MarsExploration.MapExplorer.Simulation.Service
{
    public class ExplorationSimulator : IExplorationSimulator
    {
        private SimulationContext _context;


        public ExplorationSimulator(SimulationContext context)
        {
            _context= context;
        }
        
        public IEnumerable<Coordinate> GetResourceCoordinates(string resourceSymbol)
        {
            int symbolCounter = 0;
            foreach(var symbol in _context.ResourceSymbols) 
            {
                if (symbol == resourceSymbol) symbolCounter++;
            }
            
            var map = _context.map.Representation;
            List<Coordinate> coordinates = new List<Coordinate>();
            for(int i = 0; i < map.Length; i++)
            {
                for(int j = 0; j < map.Length; j++)
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
                resourceCoordinates.Add(symbol, GetResourceCoordinates(symbol).ToList());
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
                foreach(var coord in resCoord.Value)
                {
                    distanceDictionary.Add(coord, CalculateDistance(_context.StartingCoordinates, coord));
                }
            }
            var orderedDict = distanceDictionary.OrderBy(x => x.Value);
            return new Coordinate(orderedDict.First().Key.X, orderedDict.First().Key.Y);
        }

        void Simulate()
        {
            var map = _context.map.Representation;
            var Start = new Tile();
            Start.Y = _context.StartingCoordinates.Y;
            Start.X = _context.StartingCoordinates.X;

            var Finish = new Tile();
            Finish.X = FindNearestResourceCoordinate().X;
            Finish.Y = FindNearestResourceCoordinate().Y;

            var activeTiles = new List<Tile>();
            activeTiles.Add(Start);
            var visitedTiles = new List<Tile>();
        }

        private static List<Tile> GetPath(Map loadedMap, Tile currentTile, Tile target, string symbol)
        {
            var map = loadedMap.Representation;
            var optimalTiles = new List<Tile>()
            {
                new Tile { X = currentTile.X, Y = currentTile.Y - 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new Tile { X = currentTile.X, Y = currentTile.Y + 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new Tile { X = currentTile.X - 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new Tile { X = currentTile.X + 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
            };

            optimalTiles.ForEach(tile => tile.SetDistance(target.X, target.Y));

            var border = map.Length - 1;

            return optimalTiles.Where(tile => tile.X >= 0 && tile.X <= border)
                                  .Where(tile => tile.Y >= 0 && tile.Y <= border)
                                      .Where(tile => map[tile.X, tile.Y] == "" || map[tile.X, tile.Y] == symbol).ToList();
        }
    }
}
