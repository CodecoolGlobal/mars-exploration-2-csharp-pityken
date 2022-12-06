using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codecool.MarsExploration.MapExplorer.MarsRover
{
    public record MarsRover(int Id, int[,] Position, int SightDistance, IEnumerable<int[,]> Coordinates);
    
}
