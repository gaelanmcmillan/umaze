using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public struct Circle2D
{
    Point2D Center { get; set; }
    float Radius { get; set; }

    public Circle2D(float cx, float cy, float r)
    {
        Center = new Point2D(cx, cy);
        Radius = r;
    }
}

public struct Point2D
{
    public float X { get; set; }
    public float Y { get; set; }

    public (float, float) XY
    {
        get { return (X, Y); }
        set { (X, Y) = value; }
    }

    public Point2D(float x, float y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// The <b>vector cross product</b>, assuming 2d points A and B are in 3d space with z = 0.
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <returns>Ax * By - Ay * Bx</returns>
    public static float operator ^(Point2D A, Point2D B)
    {
        return A.X * B.Y - A.Y * B.X;
    }

    /// <summary>
    /// The <b>vector dot product</b>.
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <returns>Ax * Bx + Ay * By</returns>
    public static float operator *(Point2D A, Point2D B)
    {
        return A.X * B.X + A.Y * B.Y;
    }

    // --- Vector • Scalar Arithmetic Operators --- //

    public static Point2D operator *(Point2D A, float scalar)
    {
        return new Point2D(A.X * scalar, A.Y * scalar);
    }
    public static Point2D operator /(Point2D A, float scalar)
    {
        return new Point2D(A.X / scalar, A.Y / scalar);
    }
    public static Point2D operator +(Point2D A, float scalar)
    {
        return new Point2D(A.X + scalar, A.Y + scalar);
    }
    public static Point2D operator -(Point2D A, float scalar)
    {
        return new Point2D(A.X - scalar, A.Y - scalar);
    }

    // --- //
}

public static class GeometryUtilities
{
    private static Circle2D Circumcircle(Point2D[] points, (int a, int b, int c) triangle)
    {
        return new Circle2D(0f, 0f, 1f);
    }

    /// <summary>
    /// Normalizes the coordinates of the provided points between 0 and 1.
    /// </summary>
    /// <param name="points">The points to normalize</param>
    /// <returns>Point2D[] — The input `points`, normalized between 0 and 1.</returns>
    public static Point2D[] NormalizePoints(Point2D[] points)
    {
        // MinMax reduction on points
        var (xMin, yMin, xMax, yMax) = points.Aggregate(
            (
                float.MaxValue,
                float.MaxValue,
                float.MinValue,
                float.MinValue
            ),
            (acc, point) => (
                Math.Min(acc.Item1, point.X),
                Math.Min(acc.Item2, point.Y),
                Math.Max(acc.Item3, point.X),
                Math.Max(acc.Item4, point.Y)
            )
        );

        // We divide by the widest difference
        // between x and y
        float dMax = Math.Max(xMax - xMin,
                                yMax - yMin);

        var normalizedPoints = points
            .Select(p => new Point2D(
                    (p.X - xMin) / dMax,  // = x_hat
                    (p.Y - yMin) / dMax)) // = y_hat
            .ToArray();

        return normalizedPoints;
    }

    public static (int, int, int)[] ConstrainedDelaunayTriangulation(
        (int, int)[] constrainedEdges, Point2D[] points)
    {
        // Referencepocket: https://www.newcastle.edu.au/__data/assets/pdf_file/0019/22519/23_A-fast-algortithm-for-generating-constrained-Delaunay-triangulations.pdf
        // Steps of the algorithm
        //  1. Normalize points
        Point2D[] normalizedPoints = NormalizePoints(points);
        int N = normalizedPoints.Length;
        int n = (int) Math.Ceiling(Math.Sqrt(N));
        //  2. Sort into bins (going to skip this step for now)
        // Point2D[,] bins = new Point2D[n, n];


        //  3. Establish the supertriangle, inside of which all points are contained.

        //  4. For each point `P`, perform steps (5-7)

        //  5. Insert `P` into the triangulation. 
        //      5.1.    Find the triangle `T` that encapsulates `P`.
        //      5.2.    Create 3 new triangles by drawing an edge between `P` and each vertex of `T`.

        //  6. Initialize a stack for the triangles which are adjacent to edges opposite `P`.
        //     There are at most three such triangles.

        //  7. Restore the Delaunay triangulation using Lawson's Swapping Algorithm.
        //      7.1.    Pop a triangle opposite `P` from the stack
        //      7.2.    If `P` is outside (or on) the circumcurcle for this triangle, return to 7.1.
        //              Otherwise, the triangle containing `P` as a vertex and the unstacked triangle form a
        //              convex quadrilateral whose diagonal is drawn in the wrong direction.
        //                      __             __
        //              Bad:   |\ |    Good:  | /|
        //                     |_\|           |/_|



        List<(int, int, int)> triangles = new();

        triangles.Add((1, 2, 3));

        // Lawson's Swapping Algorithm...
        //      i. Put each triangle that has an edge opposite of P on a stack.
        //      ii. While the stack is not empty...
        //              a.  Unstack a triangle T_adj.
        //              b.  Check to see if P is inside its circumcircle.
        //                  If P ∈ Circumcircle, then swap the diagonal in the convex quadrilateral formed by
        //                  P's triangle and T_adj.

        return new (int a, int b, int c)[] { (0, 0, 0) };
    }
}
