using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.MapLoader;

public class MapLoader : IMapLoader
{
    public Map Load(string mapFile)
    {
        var rawMap = File.ReadAllLines(mapFile);
        var dimension = rawMap.Length;
        var loadedMap = new string[dimension,dimension];
        for( int i = 0; i < dimension; i++)
        {
            for( int j = 0; j < dimension; j++)
            {
                char c = rawMap[i][j];
                loadedMap[i, j] += c;
            }
        }
        return new Map(loadedMap, true);
    }
}
