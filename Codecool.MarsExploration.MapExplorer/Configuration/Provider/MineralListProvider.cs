namespace Codecool.MarsExploration.MapExplorer.Configuration.Provider
{
    public class MineralListProvider : IMineralListProvider
    {
        public IEnumerable<string> GetMinerals(IEnumerable<string> possibleItems, int amount)
        {
            List<string> items = new();
            for (int i = 0; i < amount; i++)
            {
                items.Add(possibleItems.ToList().RandomItem());
            }
            return items;
        }
    }
}
