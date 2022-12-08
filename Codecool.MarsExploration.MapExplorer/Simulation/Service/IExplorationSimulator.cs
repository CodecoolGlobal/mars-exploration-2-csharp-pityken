using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codecool.MarsExploration.MapExplorer.Simulation.Service
{
    public interface IExplorationSimulator
    {
        IEnumerable<Coordinate> GetResourceCoordinate(string resourceSymbol);

        Dictionary<string, List<Coordinate>> FillDictionary();

        double CalculateDistance(Coordinate RoverCoord, Coordinate ResourceCoord);

        Coordinate FindNearestResourceCoordinate();

        IEnumerable<Coordinate> CheckNeighbours();

        Coordinate Move(Coordinate currentPos);

        string PickUpResource(Coordinate currPos);
    }
}
