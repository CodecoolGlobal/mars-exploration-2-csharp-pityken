namespace Codecool.MarsExploration.MapExplorer.Configuration
{
    public static class RandomExtension
    {
        public static T RandomItem<T>(this List<T> items)
        {
            Random random = new Random();
            return items[random.Next(0,items.Count())];
        }
    }
}
