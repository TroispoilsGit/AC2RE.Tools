namespace AC2RE.Tools;

public class Bound
{
    public Bound(float xMin, float yMin, float xMax, float yMax)
    {
        this.xMin = xMin;
        this.yMin = yMin;
        this.xMax = xMax;
        this.yMax = yMax;
    }

    public float xMin { get; set; }
    public float xMax { get; set;}
    public float yMin { get; set;}
    public float yMax { get; set;}

    public bool Intersect(Bound bound) {
        return xMin < bound.xMax &&
                xMax > bound.xMin &&
                yMin < bound.yMax &&
                yMax > bound.yMin;
    }
}
