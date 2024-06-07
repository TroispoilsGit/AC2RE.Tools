using AC2RE.Definitions;
using Spectre.Console;

DatReader datReader = new DatReader("/Users/troispoils/Documents/DatFiles/cell_1.dat");

AnsiConsole.Markup("[underline red]Cell files loaded :[/] " + datReader.dids.Count() + "\n");
AnsiConsole.Markup("[underline red]Cell tree size :[/] " + datReader.filesystemTree.offsetToNode.Count() + "\n");

var root = new Tree("[Green]Cell_1 Root Tree[/]");


var treeNode = root.AddNode(datReader.filesystemTree.offsetToNode.First().Key.ToString("X8"));
addTree(treeNode, datReader.filesystemTree.offsetToNode.First().Value);

AnsiConsole.Write(root);

DataId did = new(0x7F7FFFFF);
CLandBlockData cLandBlock;
using(var data = datReader.getFileReader(did)) {
    cLandBlock = new CLandBlockData(data);

    for(int i = 0; i < cLandBlock.heights.Count(); i++) {
        cLandBlock.heights[i] = 0;
    }
}

cLandBlock.heights.ForEach(x => { Console.WriteLine(x); });

using(var writer = datReader.getFileWriter(did)) {
    writer.BaseStream.Position = 0;
    Console.WriteLine(writer.BaseStream.Length);
    //cLandBlock.write(writer);
}

using(var data = datReader.getFileReader(did)) {
    var cLandBlockData = new CLandBlockData(data);

    foreach(var i in cLandBlockData.heights) {
        Console.WriteLine(i);
    }
}

void addTree(TreeNode treeNode, BTNode bTNode)
{
    foreach (var node in bTNode.childOffsets)
    {
        foreach (var childs in datReader.filesystemTree.offsetToNode)
        {
            if (node == childs.Key)
            {
                var newTreeNodes = treeNode.AddNode(childs.Key.ToString("X8"));
                addTree(newTreeNodes, childs.Value);
            }
        }
    }
    foreach (var data in bTNode.entries)
    {
        treeNode.AddNode(data.did.ToString() + $" [[{data.offset:X8}]] ({data.size}byte)");
    }
}