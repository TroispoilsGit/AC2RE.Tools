using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Numerics;

namespace AC2RE.Tools;
public class QuadTreeImageGenerator {
    private int width;
    private int height;

    public QuadTreeImageGenerator(int width, int height) {
        this.width = width;
        this.height = height;
    }

    public void GenerateImage(string filePath, Point[,] points) {
        using (var image = new Image<Rgba32>(width, height)) {
            // Fill the background with white color
            //image.Mutate(ctx => ctx.Fill(Color.White));

            // Add points as colored pixels
            for (int y = 0; y < points.GetLength(0); y++) {
                for (int x = 0; x < points.GetLength(1); x++) {
                    //Color color = Color.FromRgb((byte)points[x, y].slope, (byte)points[x, y].slope, (byte)points[x, y].slope);
                    image[x, 4079 - y] = points[x, y].slope < 40 ? Color.White : Color.Black;
                }
            }

            // Save the image
            image.Save(filePath);
        }
    }
}