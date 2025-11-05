using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StencilShape : MonoBehaviour
{
    [SerializeField] private Collider2D drawingArea;
    [SerializeField] private int pointCount = 12;
    [SerializeField] private Color stencilColor = Color.cyan;

    // Start is called before the first frame update
    void Start()
    {
        LineRenderer line = GetComponent<LineRenderer>();

        line.positionCount = pointCount;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = stencilColor;
        line.endColor = stencilColor;
        line.useWorldSpace = true;

        if (drawingArea == null)
        {
            Debug.LogError("StencilShape: No drawingArea assigned");
            return;
        }

        Bounds bounds = drawingArea.bounds;

        Vector3[] points = new Vector3[pointCount];
        for (int i = 0; i < pointCount; i++)
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            points[i] = new Vector3(x, y, 0f);
        }

        System.Array.Sort(points, (a, b) => a.x.CompareTo(b.x));

        line.SetPositions(points);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
