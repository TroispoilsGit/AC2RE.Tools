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

    public static float CalculateZ(Vector3 A, Vector3 B, Vector3 C, float x, float y)
    {
        // Vecteurs du triangle
        Vector3 v0 = B - A;
        Vector3 v1 = C - A;
        Vector3 v2 = new Vector3(x, y, 0) - new Vector3(A.X, A.Y, 0);

        // Calcul des produits scalaires
        float d00 = Vector3.Dot(v0, v0);
        float d01 = Vector3.Dot(v0, v1);
        float d11 = Vector3.Dot(v1, v1);
        float d20 = Vector3.Dot(v2, v0);
        float d21 = Vector3.Dot(v2, v1);

        // Calcul des coordonnées barycentriques
        float denom = d00 * d11 - d01 * d01;
        float v = (d11 * d20 - d01 * d21) / denom;
        float w = (d00 * d21 - d01 * d20) / denom;
        float u = 1.0f - v - w;

        // Interpolation de Z en utilisant les coordonnées barycentriques
        float z = u * A.Z + v * B.Z + w * C.Z;

        return z;
    }
}
