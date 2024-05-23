// See https://aka.ms/new-console-template for more information
using System.Drawing;
using System.Numerics;
using AC2RE.Tools;

Console.WriteLine("Creation List.");
HeightMap heightMap = new HeightMap();
var points = heightMap.GeneratePositions();

Console.WriteLine("Creation QuadTree.");
QuadTree quadTree = new QuadTree(new Bound(0, 0, 4080, 4080));

Console.WriteLine("Insert Points.");
for(int y = 0; y < points.GetLength(0);  y++) {
    for(int x = 0; x < points.GetLength(1); x++) {
        quadTree.Insert(points[x, y]);
    }
}

Console.WriteLine("Insert Done.");
Console.ReadLine();

var listPoints = quadTree.Query(new Bound(2030,2030,2050,2050), new());

foreach(var l in listPoints)
    Console.WriteLine(l.ToString());

Console.WriteLine("Done.");