using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing;
using System.Numerics;
using SixLabors.ImageSharp.Drawing.Processing;

namespace AC2RE.Tools;
public class QuadTreeImageGenerator
{
    private int width;
    private int height;

    public QuadTreeImageGenerator(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public void GenerateImage(string filePath, Point[,] points, Finding find)
    {
        using (var image = new Image<Rgb24>(width, height))
        {
            // Fill the background with white color
            //image.Mutate(ctx => ctx.Fill(Color.White));

            // Add points as colored pixels
            for (int y = 0; y < points.GetLength(0); y++)
            {
                for (int x = 0; x < points.GetLength(1); x++)
                {
                    var pos = points[x, y].realPoint;
                    var passable = points[x, y].passable;
                    byte slope = (byte)(points[x, y].slope * byte.MaxValue / 90);
                    Color color = Color.FromRgb((byte)points[x, y].slope, (byte)points[x, y].slope, (byte)points[x, y].slope);
                    //image[x, 40790 - y] = points[x, y].passable ? Color.White : Color.Black;
                    //var color = passable ? Color.White : Color.Black;
                    //var color = Color.FromRgb(slope, slope, slope);

                    RectangleF rectangle = new RectangleF((int)pos.X, (int)pos.Y, 1, 1);

                    Brush brush = Brushes.Solid(color);

                    image.Mutate(ctx => ctx.Fill(brush, rectangle));
                }
            }

            image.Mutate(ctx => ctx.Fill(Brushes.Solid(Color.Red), new RectangleF((int)find.start.X, (int)find.start.Y, 1, 1)));
            foreach(var path in find.pathList) {
                image.Mutate(ctx => ctx.Fill(Brushes.Solid(Color.Red), new RectangleF((int)path.X, (int)path.Y, 1, 1)));
            }
            image.Mutate(ctx => ctx.Fill(Brushes.Solid(Color.Red), new RectangleF((int)find.goal.X, (int)find.goal.Y, 1, 1)));
            // Save the image
            image.Save(filePath);
        }
    }

    public Color GetColorBySlope(float slope)
    {
        var colorSlope = (Byte)(slope * Byte.MaxValue / 90);

        return Color.FromRgb(colorSlope, colorSlope, colorSlope);
    }
}