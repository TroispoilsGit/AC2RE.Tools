using System.Numerics;
using AC2RE.Definitions;

namespace AC2RE.Tools;

public class QuadTree {
    public Bound bound { get; set; }
    public int capacitie { get; set; }
    public bool divided { get; set; }
    public QuadTree? nw { get; set; }
    public QuadTree? ne { get; set; }
    public QuadTree? sw { get; set; }
    public QuadTree? se { get; set; }
    public List<Point> points { get; set; }

    public QuadTree(Bound bound, int capacitie = 4) {
        this.bound = bound;
        this.capacitie = capacitie;
        points = new List<Point>();
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

    public bool Insert(Point point) {
        if (!Contains(bound, point)) return false;

        if (points.Count < capacitie) {
            points.Add(point);
            return true;
        }
        else {
            if (!divided)
                SubDivide();

            if (nw == null || ne == null || sw == null || se == null) return false;
            if (nw.Insert(point) || ne.Insert(point) || sw.Insert(point) || se.Insert(point)) {
                return true;
            }
        }

        return false;
    }

    public List<Point> QueryLandblock(CellId cellId) {
        Bound range = new Bound(cellId.landblockX * 16, cellId.landblockY * 16, 
        (cellId.landblockX + 1) * 16, (cellId.landblockY + 1) * 16);

        return Query(range, new());
    }

    public List<Point> QueryNeighbouringPoints(Vector2 point) {
        Bound range = new Bound(point.X, point.Y, point.X + 1.5f, point.Y + 1.5f);

        return Query(range, new());
    }

    public List<Point> Query(Bound range, List<Point> listPoints) {
        if (!bound.Intersect(range)) return listPoints;

        foreach (var point in points) {
            if (Contains(range, point)) listPoints.Add(point);
        }

        if (divided) {
            if (nw != null) nw.Query(range, listPoints);
            if (ne != null) ne.Query(range, listPoints);
            if (sw != null) sw.Query(range, listPoints);
            if (se != null) se.Query(range, listPoints);
        }

        return listPoints;
    }

    public bool Contains(Bound bound, Point point) {
        return point.point.X >= bound.xMin && point.point.X < bound.xMax &&
               point.point.Y >= bound.yMin && point.point.Y < bound.yMax;
    }
}
