using System;
using System.Collections.Generic;

public class DijkstraTesting
{
    public static List<int> FindShortestPath(int[,] graph, int startNode, int endNode)
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

    // Example usage
    public static void Main()
    {
        //int[,] graph = {
        //    { 0, 4, 2, 0, 0 },
        //    { 4, 0, 1, 5, 0 },
        //    { 2, 1, 0, 8, 10 },
        //    { 0, 5, 8, 0, 2 },
        //    { 0, 0, 10, 2, 0 }
        //};

        int[,] graph = {
            { 0, 4, 0, 0, 0, 0, 0, 8, 0 },
            { 4, 0, 8, 0, 0, 0, 0, 11, 0 },
            { 0, 8, 0, 7, 0, 4, 0, 0, 2 },
            { 0, 0, 7, 0, 9, 14, 0, 0, 0 },
            { 0, 0, 0, 9, 0, 10, 0, 0, 0 },
            { 0, 0, 4, 14, 10, 0, 2, 0, 0 },
            { 0, 0, 0, 0, 0, 2, 0, 1, 6 },
            { 8, 11, 0, 0, 0, 0, 1, 0, 7 },
            { 0, 0, 2, 0, 0, 0, 6, 7, 0 }
        };

        int startNode = 0;
        int endNode = 5;

        List<int> shortestPath = FindShortestPath(graph, startNode, endNode);

        Console.WriteLine("Shortest path from node " + startNode + " to node " + endNode + ":");
        foreach (int node in shortestPath)
        {
            Console.Write(node + " ");
        }
    }
}
