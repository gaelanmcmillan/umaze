using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public struct Circle2D {
    Point2D Center { get; set; }
    float Radius { get; set; }

    public Circle2D(float cx, float cy, float r) {
        Center = new Point2D(cx, cy);
        Radius = r;
    }
}

public struct Point2D {
    public float X { get; set; }
    public float Y { get; set; }

    public (float, float) XY {
        get { return (X, Y); }
        set { (X, Y) = value; }
    }

    public Point2D(float x, float y) {
        X = x;
        Y = y;
    }

    /// <summary>
    /// The <b>vector cross product</b>, assuming 2d points A and B are in 3d space with z = 0.
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <returns>Ax * By - Ay * Bx</returns>
    public static float operator ^(Point2D A, Point2D B) {
        return A.X * B.Y - A.Y * B.X;
    }

    /// <summary>
    /// The <b>vector dot product</b>.
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <returns>Ax * Bx + Ay * By</returns>
    public static float operator *(Point2D A, Point2D B) {
        return A.X * B.X + A.Y * B.Y;
    }

    // --- Vector • Scalar Arithmetic Operators --- //

    public static Point2D operator *(Point2D A, float scalar) {
        return new Point2D(A.X * scalar, A.Y * scalar);
    }
    public static Point2D operator /(Point2D A, float scalar) {
        return new Point2D(A.X / scalar, A.Y / scalar);
    }
    public static Point2D operator +(Point2D A, float scalar) {
        return new Point2D(A.X + scalar, A.Y + scalar);
    }
    public static Point2D operator -(Point2D A, float scalar) {
        return new Point2D(A.X - scalar, A.Y - scalar);
    }

    // --- //
}

public static class GeometryUtilities
{
    public static void Test(Action<string> debug, string toPrint)
    {
        debug(toPrint);
    }

    private static Circle2D Circumcircle(
        Point2D[] points,
        (int a, int b, int c) triangle
    )
    {
        return new Circle2D(0f, 0f, 1f);
    }

    /// <summary>
    /// Normalizes the coordiantes of the provided points between 0 and 1.
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public static Point2D[] NormalizePoints(Point2D[] points)
    {
        var res = points.Aggregate(
            new
            {
                xMin = float.MaxValue,
                yMin = float.MaxValue,
                xMax = float.MinValue,
                yMax = float.MinValue
            },
            (acc, point) => new
            {
                xMin = Math.Min(acc.xMin, point.X),
                yMin = Math.Min(acc.yMin, point.Y),
                xMax = Math.Max(acc.xMax, point.X),
                yMax = Math.Max(acc.yMax, point.Y),
            }
        );

        float dMax = Math.Max(res.xMax - res.xMin, res.yMax - res.yMin);

        return points
            .Select(p => new Point2D(
                    (p.X - res.xMin) / dMax,
                    (p.Y - res.yMin) / dMax)
            ).ToArray();

    }

    public static (int, int, int)[] DelaunayTriangulation(Point2D[] points)
    {
        // 1. Create a super-triangle from three points, inside of which all points are contained.
        Point2D[] normalizedPoints = NormalizePoints(points);
        // 2. For each point P...
        //      i.      Insert P into the triangulation. 
        //      ii.     Find the triangle T that encapsulates P.
        //      iii.    Create 3 new triangles by drawing an edge between P and each vertex of T.
        //      iv.     Perform Lawson's Swapping Algorithm.

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
