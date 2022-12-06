using Codecool.MarsExploration.MapExplorer.Configuration.Model;

namespace Codecool.MarsExploration.MapExplorer.Configuration.Service
{
    public interface IRoverConfigurationValidator
    {
        bool Validate(RoverConfiguration roverConfig);
    }
}
