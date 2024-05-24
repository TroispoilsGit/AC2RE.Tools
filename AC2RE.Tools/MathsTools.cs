using System.Numerics;

namespace AC2RE.Tools;

public static class MathsTools
{
    public static float CalculateSlope(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        // Calculate vectors U and V
        Vector3 U = p2 - p1;
        Vector3 V = p3 - p1;

        // Calculate the normal vector N = Vector3.Cross(U, V)
        Vector3 N = Vector3.Cross(U, V);

         // Check if the normal vector is zero, which means the points are collinear
        if (N == Vector3.Zero)
        {
            return 0;
        }

        // Normalize the normal vector
        Vector3 normalizedN = Vector3.Normalize(N);
        if (normalizedN.Z < 0) normalizedN = normalizedN * -1;
        

        // Reference normal vector for a horizontal plane (e.g., [0, 0, 1])
        Vector3 nRef = new Vector3(0.0f, 0.0f, 1.0f);

        // Calculate the dot product of the normalized vectors
        float dotProduct = Vector3.Dot(normalizedN, nRef);

        // Calculate the angle between the vectors in radians
        double angleInRadians = Math.Acos(dotProduct);

        // Convert the angle to degrees
        double angleInDegrees = angleInRadians * (180.0 / Math.PI);

        return (float)angleInDegrees;
    }
}
