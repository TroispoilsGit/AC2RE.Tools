using System.Numerics;
using AC2RE.Tools;

namespace AC2RE.ToolsTest;

public class MathsToolsTest
{
    [Fact]
    public void TestCalculateSlope_FlatTerrain()
    {
        Vector3 p1 = new Vector3(1, 2, 0);
        Vector3 p2 = new Vector3(2, 2, 0);
        Vector3 p3 = new Vector3(1, 1, 0);

        float slope = MathsTools.CalculateSlope(p1, p2, p3);

        Assert.Equal(0, slope, 5); // Expect slope to be 0 degrees
    }

    [Fact]
    public void TestCalculateSlope_45DegreeSlope()
    {
        Vector3 p1 = new Vector3(0, 0, 0);
        Vector3 p2 = new Vector3(1, 0, 0);
        Vector3 p3 = new Vector3(0, 1, 1);

        float slope = MathsTools.CalculateSlope(p1, p2, p3);

        Assert.Equal(45, slope, 5); // Expect slope to be approximately 45 degrees
    }

    [Fact]
    public void TestCalculateSlope_VerticalSlope()
    {
        Vector3 p1 = new Vector3(0, 0, 0);
        Vector3 p2 = new Vector3(1, 0, 0);
        Vector3 p3 = new Vector3(0, 0, 2);

        float slope = MathsTools.CalculateSlope(p1, p2, p3);

        Assert.Equal(90, slope, 5); // Expect slope to be approximately 90 degrees
    }

    [Fact]
    public void TestCalculateSlope_NegativeZ()
    {
        Vector3 p1 = new Vector3(0, 0, 0);
        Vector3 p2 = new Vector3(1, 0, 0);
        Vector3 p3 = new Vector3(0, 1, -1);

        float slope = MathsTools.CalculateSlope(p1, p2, p3);

        Assert.Equal(45, slope, 5); // Expect slope to be approximately 45 degrees
    }
}
