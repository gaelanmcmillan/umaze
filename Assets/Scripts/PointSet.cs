using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// The pathing region is a closed plane, with a point set initially consisting only of the its four corners.
/// As the player builds in the region, points will be added to the set at each of the buildings corners.
///
/// Pathfinding will be done over an edge-constrained delaunay triangulation of the pointset. The constrained
/// edges will be the faces of the player-constructed buildings.
/// 
/// </summary>
///

public class PointSet : MonoBehaviour
{
    public Vector2[] points = new Vector2[5]
    {
        new Vector2(1f, 1f),
        new Vector2(1.5f, 1.5f),
        new Vector2(2f, 0),
        new Vector2(3f, 1.2f),
        new Vector2(2.5f, 0.8f),
    };

    public (int, int, int)[] triangles = new (int, int, int)[3]
    {
        (0,1,2),
        (1,2,3),
        (2,3,4),
    };

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {

    }

    // A point is a Vector2.

    // A triangle is a 3-tuple of points, listed in counter-clockwise order.
    

    /// <summary>Vec2 (x=x,y=y) -> Vec3 (x=x, y=0, z=y)</summary>

}
