using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StencilShape : MonoBehaviour
{
    [SerializeField] private Collider2D drawingArea;
    [SerializeField] private int pointCount = 12;
    [SerializeField] private Color stencilColor = Color.cyan;
    [SerializeField] private float width = 0.2f;
    [SerializeField] private float segmentLength = 0.8f;
    [SerializeField] private float turnStrength = 45f;

    // Start is called before the first frame update
    void Start()
    {
        LineRenderer line = GetComponent<LineRenderer>();

        line.positionCount = pointCount;
        line.startWidth = width;
        line.endWidth = width;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = stencilColor;
        line.endColor = stencilColor;
        line.useWorldSpace = true;
        line.sortingOrder = 1;

        if (drawingArea == null)
        {
            Debug.LogError("StencilShape: No drawingArea assigned");
            return;
        }

        Bounds bounds = drawingArea.bounds;

        Vector3[] points = new Vector3[pointCount];

        // Start near center
        points[0] = bounds.center;
        Vector2 direction = Random.insideUnitCircle.normalized;

        for (int i = 1; i < pointCount; i++)
        {
            float angle = Random.Range(-turnStrength, turnStrength);
            direction = Quaternion.Euler(0, 0, angle) * direction;

            Vector3 next = points[i - 1] + (Vector3)(direction * segmentLength);

            // If next point is outside bounds, turn instead of snapping
            if (!bounds.Contains(next))
            {
                direction = Quaternion.Euler(0, 0, 180f) * direction;
                next = points[i - 1] + (Vector3)(direction * segmentLength);
            }

            points[i] = next;
        }

        line.SetPositions(points);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
