using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AC2RE.Tools
{
    public class Point
    {
        public Vector3 point { get; set; }
        public bool passable { get; set; }
        public float slope { get; set; }
        public int terrainType { get; set; }
        public int sceneIndex { get; set; }
        public int roadIndex { get; set; }

        public override string? ToString()
        {
            return $"Point Details:\n" +
                   $"  Passable: {passable}\n" +
                   $"  Position: {point}\n" +
                   $"  Slope: {slope}°\n" +
                   $"  Terrain Type: {terrainType}\n" +
                   $"  Scene Index: {sceneIndex}\n" +
                   $"  Road Index: {roadIndex}";
        }
    }
}
