using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGrid : MonoBehaviour
{
    [SerializeField]
    int rows = 10;

    [SerializeField]
    int cols = 10;

    bool[,] grid;

    static (int dx, int dy)[] deltas =
    {
        (-1,  0), // up
        ( 1,  0), // down
        ( 0, -1), // left
        ( 0,  1), // right
    };

    void ShowGridNodes()
    {
        Vector3 cubeSize = Vector3.one * 0.3f;
        for (int row = 0; row < rows; ++row)
        {
            for (int col = 0; col < cols; ++col)
            {
                Vector3 cubePos = new Vector3(row, 1, col) + gameObject.transform.position;
                Gizmos.color = grid[row, col] ? Color.red : Color.green;
                Gizmos.DrawCube(cubePos, cubeSize);

                Gizmos.color = Color.black;

                //UnityEditor.Handles.BeginGUI();
                UnityEditor.Handles.Label(cubePos, string.Format("[{0},{1}]", row, col));

                if (row < rows-1)
                {
                    Gizmos.DrawLine(cubePos, cubePos + Vector3.right);
                }
                
                if (col < cols-1)
                {
                    Gizmos.DrawLine(cubePos, cubePos + Vector3.forward);
                }
            }
        }
    }

    private void Awake()
    {
        grid = new bool[rows, cols];
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int r = 0; r < rows; ++r)
        {
            for (int c = 0; c < cols; ++c)
            {
                Debug.Log(string.Format("[{0},{1}]: {2}", r, c, grid[r,c]));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        ShowGridNodes();
    }
}
