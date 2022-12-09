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

        Coordinate FindNearestResourceCoordinate(int limit, Coordinate currPos);

        IEnumerable<Coordinate> CheckNeighbours(Coordinate currPos);

        Coordinate Move(Coordinate currentPos, Coordinate nearest);

        string PickUpResource(Coordinate currPos);
    }
}
