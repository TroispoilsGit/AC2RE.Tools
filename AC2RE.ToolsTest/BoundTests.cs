using Xunit;
using AC2RE.Tools;

namespace AC2RE.ToolsTest;

public class BoundTests
{
    [Fact]
    public void TestIntersection_ShouldReturnTrue_WhenBoundsOverlap()
    {
        // Arrange
        Bound bound1 = new Bound(0, 0, 10, 10);
        Bound bound2 = new Bound(5, 5, 15, 15);

        // Act
        bool result = bound1.Intersect(bound2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestIntersection_ShouldReturnTrue_WhenBoundsIntoAnotherBound()
    {
        // Arrange
        Bound bound1 = new Bound(0, 0, 10, 10);
        Bound bound2 = new Bound(3, 3, 5, 5);

        // Act
        bool result = bound1.Intersect(bound2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void TestIntersection_ShouldReturnFalse_WhenBoundsDoNotOverlap()
    {
        // Arrange
        Bound bound1 = new Bound(0, 0, 10, 10);
        Bound bound2 = new Bound(20, 20, 30, 30);

        // Act
        bool result = bound1.Intersect(bound2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void TestIntersection_ShouldReturnFalse_WhenBoundsTouch()
    {
        // Arrange
        Bound bound1 = new Bound(0, 0, 10, 10);
        Bound bound2 = new Bound(10, 10, 20, 20);

        // Act
        bool result = bound1.Intersect(bound2);

        // Assert
        Assert.False(result);
    }
}