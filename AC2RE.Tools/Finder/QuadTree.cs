using System.Drawing;
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

    public QuadTree(Bound bound, int capacitie = 4)
    {
        this.bound = bound;
        this.capacitie = capacitie;
        points = new List<Vector3>();
        divided = false;
    }

    public void SubDivide()
    {
        var xC = (bound.xMin + bound.xMax) / 2;
        var yC = (bound.yMin + bound.yMax) / 2;

        nw = new QuadTree(new Bound(bound.xMin, yC, xC, bound.yMax));
        ne = new QuadTree(new Bound(xC, yC, bound.xMax, bound.yMax));
        sw = new QuadTree(new Bound(bound.xMin, bound.yMin, xC, yC));
        se = new QuadTree(new Bound(xC, bound.yMin, bound.xMax, yC));

        divided = true;
    }

    public bool Insert(Vector3 point)
    {
        if (!Contains(bound, point)) return false;

        if (points.Count < capacitie)
        {
            points.Add(point);
            return true;
        }
        else
        {
            if (!divided)
                SubDivide();

            if(nw == null || ne == null || sw == null || se == null) return false;
            if (nw.Insert(point) || ne.Insert(point) || sw.Insert(point) || se.Insert(point))
            {
                return true;
            }
        }

        return false;
    }

    public List<Vector3> Query(Bound range, List<Vector3> listPoints) {
        if(!bound.Intersect(range)) return listPoints;

        foreach(var point in points) {
            if(Contains(range, point)) listPoints.Add(point);
        }

        if(divided) {
            nw.Query(range, listPoints);
            ne.Query(range, listPoints);
            sw.Query(range, listPoints);
            se.Query(range, listPoints);
        }

        return listPoints;
    }

    public bool Contains(Bound bound, Vector3 point)
    {
        return point.X >= bound.xMin && point.X < bound.xMax &&
               point.Y >= bound.yMin && point.Y < bound.yMax;
    }
}
