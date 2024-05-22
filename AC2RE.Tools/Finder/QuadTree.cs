using System.Numerics;

namespace AC2RE.Tools;

public class QuadTree
{
    public Bound bound { get; set; }
    public int capacitie { get; set; }
    public bool divided { get; set; }
    public QuadTree? nw { get; set; }
    public QuadTree? ne { get; set; }
    public QuadTree? sw { get; set; }
    public QuadTree? se { get; set; }
    public List<Vector3> points { get; set; }

    public QuadTree(Bound bound, int capacitie = 4) {
        this.bound = bound;
        this.capacitie = capacitie;
        points = new List<Vector3>();
        divided = false;
    }

    public void SubDivide() {
        var xC = (bound.xMin + bound.xMax) / 2;
        var yC = (bound.yMin + bound.yMax) / 2;

        nw = new QuadTree(new Bound(bound.xMin, yC, xC, bound.yMax));
        ne = new QuadTree(new Bound(xC, yC, bound.xMax, bound.yMax));
        sw = new QuadTree(new Bound(bound.xMin, bound.yMin, xC, yC));
        se = new QuadTree(new Bound(xC, bound.yMin, bound.xMax, yC));

        divided = true;
    }
}
