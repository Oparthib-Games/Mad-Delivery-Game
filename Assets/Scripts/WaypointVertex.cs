using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class WaypointVertex : MonoBehaviour
{
    [SerializeField] private GameObject[] connectedVertex;

    [SerializeField] private LineRenderer[] connectedLineRenderers;

    [SerializeField] private Color edgeColor = Color.cyan;
    [SerializeField] private bool useRandomColor = true;
    [SerializeField] private bool hideMyEdge;

    void Start()
    {
        
    }

    private void OnEnable()
    {
        if (useRandomColor)
        {
            Color[] colors = new Color[] { Color.red, Color.blue, Color.green, Color.yellow, Color.cyan, Color.magenta };
            int randomIndex = Random.Range(0, colors.Length);
            edgeColor = colors[randomIndex];
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = edgeColor;
        

        foreach (GameObject vertex in connectedVertex)
        {
            if(!hideMyEdge)
            {
                Vector3 raysFrom = transform.position + new Vector3(-0.3f, 0, -0.3f);
                Vector3 raysTo = vertex.transform.position + new Vector3(0.3f, 0, 0.3f);
                Gizmos.DrawLine(raysFrom, raysTo);
            }
        }
    }

    public GameObject[] GetConnectedVertex()
    {
        return connectedVertex;
    }
}