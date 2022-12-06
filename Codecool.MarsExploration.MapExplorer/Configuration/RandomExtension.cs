namespace Codecool.MarsExploration.MapExplorer.Configuration
{
    public static class RandomExtension
    {
        public static int RandomWithinRanges(this Random random, int minimum, int maximum)
        {
            return random.Next(minimum, maximum);
        }
        public static T RandomItem<T>(this List<T> items)
        {
            Random random = new Random();
            return items[random.Next(0,items.Count())];
        }
    }
}
