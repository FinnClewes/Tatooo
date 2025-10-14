using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawWithMouse : MonoBehaviour
{
    private LineRenderer line;
    private Vector3 previousPosition;

    [SerializeField] private float minDistance = 0.01f;
    [SerializeField] private Color lineColour = Color.black;

    // reference drawing area
    [SerializeField] private Collider2D drawingArea;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 1;
        previousPosition = transform.position;

        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = lineColour;
        line.endColor = lineColour;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(mousePos);
            currentPosition.z = 0f;

            // only draw if inside collider
            if (drawingArea != null && drawingArea.OverlapPoint(currentPosition))
            {
                if (Vector3.Distance(currentPosition, previousPosition) > minDistance)
                {
                    if (previousPosition == transform.position)
                    {
                        line.SetPosition(0, currentPosition);
                    }
                    else
                    {
                        line.positionCount++;
                        line.SetPosition(line.positionCount - 1, currentPosition);
                    }


                    previousPosition = currentPosition;
                }
            }
        }
    }
}

