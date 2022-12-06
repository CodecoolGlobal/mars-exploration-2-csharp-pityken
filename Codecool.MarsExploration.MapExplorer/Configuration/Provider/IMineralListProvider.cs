namespace Codecool.MarsExploration.MapExplorer.Configuration.Provider
{
    public interface IMineralListProvider
    {
        IEnumerable<string> GetMinerals(IEnumerable<string> possibleItems, int amount);
    }
}
