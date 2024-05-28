using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AC2RE.Tools {
    public class Point {
        public Vector3 point { get; set; }
        public Vector2 realPoint { get; set; }
        public bool passable { get; set; }
        public float slope { get; set; }

        public override string? ToString() {
            return $"[{passable}] : {realPoint} ({slope}°)";
        }
    }
}
