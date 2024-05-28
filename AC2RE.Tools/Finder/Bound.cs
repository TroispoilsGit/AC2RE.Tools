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
    public float xMax { get; set; }
    public float yMin { get; set; }
    public float yMax { get; set; }

    public bool Intersect(Bound bound)
    {
        // Vérifie le chevauchement
        bool isOverlapping = xMin < bound.xMax &&
                             xMax > bound.xMin &&
                             yMin < bound.yMax &&
                             yMax > bound.yMin;

        // Vérifie si l'une est entièrement à l'intérieur de l'autre
        bool isContained = (xMin >= bound.xMin && xMax <= bound.xMax &&
                            yMin >= bound.yMin && yMax <= bound.yMax) ||
                           (bound.xMin >= xMin && bound.xMax <= xMax &&
                            bound.yMin >= yMin && bound.yMax <= yMax);

        //Console.WriteLine(this.ToString() + " - " + bound.ToString() + " " + isOverlapping + " " + isContained);

        return isOverlapping || isContained;
    }

    public override string ToString() {
        return $"{xMin} {yMin} {xMax} {yMax}";
    }
}
