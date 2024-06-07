// See https://aka.ms/new-console-template for more information
using System.Drawing;
using System.Numerics;
using AC2RE.Definitions;
using AC2RE.Tools;

Console.WriteLine("Creation List.");
HeightMap heightMap = new HeightMap();
var points = heightMap.GeneratePositions();
Console.WriteLine("Creation List Done.");

Console.WriteLine("Creation QuadTree.");
QuadTree quadTree = new QuadTree(new Bound(0, 0, 40800, 40800));

var count = 0;
Console.WriteLine("Insert Points.");
for(int y = 0; y < points.GetLength(0);  y++) {
    for(int x = 0; x < points.GetLength(1); x++) {
        quadTree.Insert(points[x, y]);
        count++;
    }
}
Console.WriteLine($"Insert {count} Done.");
//Console.ReadLine();

var maxLenght = 255 * 16 * 10;
var dividedLenght = maxLenght / 2;

var pointsInBound = quadTree.Query(new Bound(0, 0, dividedLenght, dividedLenght), new());
Console.WriteLine("Point finding: " + pointsInBound.Count());
//pointsInBound.ForEach(p => { Console.WriteLine(p.ToString()); });
if(pointsInBound != null) {
    QuadTreeImageGenerator.GenerateImageByBoundQuery("divided.png", pointsInBound, dividedLenght);
}