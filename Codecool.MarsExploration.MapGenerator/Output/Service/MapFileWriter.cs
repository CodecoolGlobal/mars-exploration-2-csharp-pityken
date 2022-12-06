using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapGenerator.Output.Service;

public class MapFileWriter : IMapFileWriter
{
    public void WriteMapFile(Map map, string file)
    {
        var pathDir = Path.GetDirectoryName(file);
        if (!Directory.Exists(pathDir))
        {
            Directory.CreateDirectory(pathDir);
        }
        File.WriteAllText(file, map.ToString());
    }
}
