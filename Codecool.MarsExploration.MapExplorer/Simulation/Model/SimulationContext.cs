using Codecool.MarsExploration.MapExplorer.MarsRover;
using Codecool.MarsExploration.MapExplorer.Configuration.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;
using Codecool.MarsExploration.MapExplorer.Exploration;

namespace Codecool.MarsExploration.MapExplorer.Simulation.Model
{
    public record SimulationContext(int Steps, int StepLimit, MarsRover.MarsRover Rover, Coordinate StartingCoordinates, Map map, List<string> ResourceSymbols, ExplorationOutcome Outcome);
}
