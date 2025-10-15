using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawWithMouse : MonoBehaviour
{
    private LineRenderer currentLine;
    private Vector3 previousPosition;
    private List<LineRenderer> allLines = new List<LineRenderer>();

    [SerializeField] private float minDistance = 0.01f;
    [SerializeField] private Color lineColour = Color.black;

    // reference drawing area
    [SerializeField] private Collider2D drawingArea;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateNewLine();
        }

        if (Input.GetMouseButton(0) && currentLine != null)
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(mousePos);
            currentPosition.z = 0f;

            // only draw if inside collider
            if (drawingArea != null || drawingArea.OverlapPoint(currentPosition))
            {
                if (Vector3.Distance(currentPosition, previousPosition) > minDistance)
                {
                    
                    currentLine.positionCount++;
                    currentLine.SetPosition(currentLine.positionCount - 1, currentPosition);
                    previousPosition = currentPosition;
                }
            }
        }
    }

    private void CreateNewLine()
    {
        GameObject lineObj = new GameObject("LineStroke");
        lineObj.transform.parent = transform;

        LineRenderer newLine = lineObj.AddComponent<LineRenderer>();
        newLine.material = new Material(Shader.Find("Sprites/Default"));
        newLine.startColor = lineColour;
        newLine.endColor = lineColour;
        newLine.startWidth = 0.05f;
        newLine.endWidth = 0.05f;
        newLine.positionCount = 0;

        currentLine = newLine;
        previousPosition = Vector3.positiveInfinity;
        allLines.Add(newLine);
    }
}

