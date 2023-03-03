using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PointSetDebugView : MonoBehaviour
{
    [SerializeField]
    [Range(0.005f, 0.1f)]
    float pointRadius = 0.025f;

    Vector3 PointToVector3(Vector2 point)
    {
        return new Vector3(point.x, 0, point.y);
    }

    Vector3[] PointsToWorldSpace(Vector2[] points)
    {
        return points
            .Select(point => PointToVector3(point) + gameObject.transform.position)
            .ToArray();
    }

    void ShowPoints(Vector3[] worldPoints)
    {
        Gizmos.color = Color.magenta;

        foreach (var point in worldPoints)
        {
            Gizmos.DrawSphere(point, pointRadius);
        }
    }

    void ShowTriangles(Vector3[] worldPoints, (int, int, int)[] triangles)
    {
        Gizmos.color = Color.cyan;

        foreach (var (a, b, c) in triangles)
        {
            Gizmos.DrawLine(worldPoints[a], worldPoints[b]);
            Gizmos.DrawLine(worldPoints[b], worldPoints[c]);
            Gizmos.DrawLine(worldPoints[c], worldPoints[a]);
        }
    }

    private void OnDrawGizmos()
    {
        var pts = PointsToWorldSpace(GetComponent<PointSet>().points);
        var tris = GetComponent<PointSet>().triangles;
        ShowPoints(pts);
        ShowTriangles(pts, tris);
    }
}
