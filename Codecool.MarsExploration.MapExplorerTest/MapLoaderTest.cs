using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;


namespace Codecool.MarsExploration.MapExplorerTest;

public class MapLoaderTest
{
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;
    private readonly string _mapFile = $@"{WorkDir}\Resources\exploration-0.map";
    private Map _fileMap;
    private string[] _fileString;
    private IMapLoader _mapLoader;

    [SetUp]
    public void SetUp()
    {
        _mapLoader = new MapLoader();
        _fileString = File.ReadAllLines(_mapFile);
        _fileMap = _mapLoader.Load(_mapFile);
    }

    [TestCase(0, 10)]
    public void TestMapLoader_LoadSameSymbolAtSetCoordinate(int x, int y)
    {
        var charInRead = _fileString[x].Split("").ElementAt(y);
        var charInMap = _fileMap.Representation[x, y];
        Assert.That(charInMap, Is.EqualTo(charInRead));
    }

    //[Test]
    //public void TestMapLoader_LoadSameSymbolAtEveryCoordinate() 
    //{

    //}

}
