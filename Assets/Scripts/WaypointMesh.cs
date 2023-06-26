using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
#endif
[ExecuteInEditMode]
public class WaypointMesh : MonoBehaviour
{
    [SerializeField] private LineRenderer mainLineRenderer;
    [SerializeField] private GameObject[] vertices;


    // Our graph supposed to be like this
    int[,] graphDemo = new int[,]
    {
        {0, 4, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        {4, 0, 0, 17, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0 },
        {20, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        {0, 17, 0, 0, 3, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        {0, 0, 4, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        {0, 0, 0, 4, 0, 0, 3, 5, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        {0, 0, 0, 0, 0, 3, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        {0, 0, 0, 0, 0, 5, 4, 0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0 },
        {0, 0, 0, 0, 0, 10, 0, 0, 0, 5, 0, 0, 0, 12, 0, 0, 0, 0 },
        {0, 0, 0, 0, 0, 0, 0, 10, 5, 0, 4, 0, 0, 0, 0, 0, 0, 0 },
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 10, 0, 5, 0, 0, 0, 0 },
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 0, 4, 0, 0, 0, 0, 0 },
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 12, 0, 0, 0, 0 },
        {0, 0, 0, 0, 0, 0, 0, 0, 12, 0, 5, 0, 12, 0, 4, 0, 0, 0 },
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 12, 0, 4 },
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 12, 0, 4, 0 },
        {0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 12 },
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 12, 0 },
    };

    public int[,] graph;

    void Start()
    {
        graph = new int[vertices.Length, vertices.Length];

        InitGraph();
        ShowGraph();
    }

#if UNITY_EDITOR
    private void Update()
    {
        
    }
#endif

    void InitGraph()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            for (int j = 0; j < vertices.Length; j++)
            {
                WaypointVertex waypointVertex = vertices[i].GetComponent<WaypointVertex>();

                GameObject[] connectedVertex  = waypointVertex.GetConnectedVertex();

                bool exists = System.Array.Exists(connectedVertex, obj => obj == vertices[j]);

                int distance = 0;

                if(exists)
                {
                    distance = Mathf.RoundToInt(Vector3.Distance(vertices[i].transform.position, vertices[j].transform.position));
                }

                graph[i, j] = exists ? distance : 0;
            }
        }

        Debug.Log("Graph Initialized");
    }

    void ShowGraph()
    {
        string graphXAxis = "Showing Graph [Click Here]\n\n";

        for (int i = 0; i < graph.GetLength(0); i++)
        {
            for (int j = 0; j < graph.GetLength(0); j++)
            {
                graphXAxis = $"{graphXAxis} {graph[i, j]}  ";
            }
            graphXAxis = $"{graphXAxis}\n";
        }
        Debug.Log(graphXAxis);
    }
}
