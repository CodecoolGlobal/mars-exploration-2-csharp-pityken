using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.MapLoader;

internal class MapLoader : IMapLoader
{
    public Map Load(string mapFile)
    {
        var rawMap = File.ReadAllLines(mapFile);
        var dimension = rawMap.Length;
        var loadedMap = new string[dimension,dimension];
        for(var i = 0; i < dimension; i++)
        {
            rawMap[i].Select(x => loadedMap[i,rawMap[i].IndexOf(x)]);
        }
        foreach(var line in rawMap) 
        {
            line.Select(x => loadedMap[Array.IndexOf(rawMap, line), line.IndexOf(x)]);
        }
        return new Map(loadedMap, true);
    }
}
