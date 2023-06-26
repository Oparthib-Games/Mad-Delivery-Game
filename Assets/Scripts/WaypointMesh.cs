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
        
    }

#if UNITY_EDITOR
    private void Update()
    {
        
    }
#endif

    [ContextMenu("Init Mesh")]
    void InitMesh()
    {
        graph = new int[vertices.Length, vertices.Length];

        InitGraph();
        ShowGraph();
    }

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
        string graphVisualize = "Showing Graph [Click Here]\n\n";

        for (int i = 0; i < graph.GetLength(0); i++)
        {
            for (int j = 0; j < graph.GetLength(0); j++)
            {
                graphVisualize = $"{graphVisualize} {graph[i, j]}  ";
            }
            graphVisualize = $"{graphVisualize}\n";
        }
        Debug.Log(graphVisualize);
    }

    [ContextMenu("Find Shortest Route")]
    public void FindShortestRoute()
    {

        int startNode = 2;
        int endNode = 13;

        List<int> shortestPath = DijkstraAlgorithm(graph, startNode, endNode);
        int shortestDist = FindShortestRouteDistance(shortestPath);

        string shortestRouteVisualize = "Shortest Route [Click Here]\n";
        shortestRouteVisualize = $"{shortestRouteVisualize} Shortest distance between {startNode} to {endNode} is {shortestDist}\n";
        foreach (int node in shortestPath)
        {
            shortestRouteVisualize = $"{shortestRouteVisualize} {node}  ";
        }
        Debug.Log(shortestRouteVisualize);
    }

    int FindShortestRouteDistance(List<int> shortestPath)
    {
        int distance = 0;
        for (int i = 0; i < shortestPath.Count; i++)
        {
            if(i != 0)
            {
                GameObject fromVertex = vertices[shortestPath[i-1]];
                GameObject toVertex = vertices[shortestPath[i]];

                int tempDist = Mathf.RoundToInt(Vector3.Distance(fromVertex.transform.position, toVertex.transform.position));
                distance += tempDist;
            }
        }

        return distance;
    }

    public static List<int> DijkstraAlgorithm(int[,] graph, int startNode, int endNode)
    {
        int numNodes = graph.GetLength(0);

        // Initialize distance array with infinity values
        int[] distances = new int[numNodes];
        for (int i = 0; i < numNodes; i++)
        {
            distances[i] = int.MaxValue;
        }

        // Initialize visited array
        bool[] visited = new bool[numNodes];

        // Initialize previous node array
        int[] previous = new int[numNodes];

        // Set distance of start node to 0
        distances[startNode] = 0;

        // Dijkstra's algorithm
        for (int i = 0; i < numNodes - 1; i++)
        {
            int minDistanceNode = GetMinDistanceNode(distances, visited);
            visited[minDistanceNode] = true;

            for (int j = 0; j < numNodes; j++)
            {
                if (!visited[j] && graph[minDistanceNode, j] != 0 && distances[minDistanceNode] != int.MaxValue &&
                    distances[minDistanceNode] + graph[minDistanceNode, j] < distances[j])
                {
                    distances[j] = distances[minDistanceNode] + graph[minDistanceNode, j];
                    previous[j] = minDistanceNode;
                }
            }
        }

        // Construct the shortest path
        List<int> path = new List<int>();
        int currentNode = endNode;
        while (currentNode != startNode)
        {
            path.Insert(0, currentNode);
            currentNode = previous[currentNode];
        }
        path.Insert(0, startNode);

        return path;
    }

    private static int GetMinDistanceNode(int[] distances, bool[] visited)
    {
        int minDistance = int.MaxValue;
        int minDistanceNode = -1;

        for (int i = 0; i < distances.Length; i++)
        {
            if (!visited[i] && distances[i] <= minDistance)
            {
                minDistance = distances[i];
                minDistanceNode = i;
            }
        }

        return minDistanceNode;
    }
}
