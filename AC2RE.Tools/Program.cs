// See https://aka.ms/new-console-template for more information
using System.Numerics;
using AC2RE.Tools;

Console.WriteLine("Creation List.");
List <Vector3> vectors = new();
for(int i = 0; i < 50000; i ++) {
    vectors.Add(new Vector3(
        new Random().Next(0, 4079),
        new Random().Next(0, 4079),
        new Random().Next(0, 255)
    ));
}

Console.WriteLine("Creation QuadTree.");
QuadTree quadTree = new QuadTree(new Bound(0, 0, 4080, 4080));

Console.WriteLine("Insert Points.");
foreach(var point in vectors)
    quadTree.Insert(point);


Console.WriteLine("Insert Done.");
Console.ReadLine();

var listPoints = quadTree.Query(new Bound(50,50,100,100), new());

foreach(var l in listPoints)
    Console.WriteLine(l.ToString());

Console.WriteLine("Done.");