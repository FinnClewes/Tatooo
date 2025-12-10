using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Data;

public class DrawWithMouse : MonoBehaviour
{
    private LineRenderer currentLine;
    private Vector3 previousPosition;
    private List<LineRenderer> allLines = new List<LineRenderer>();

    [SerializeField] private float minDistance = 0.01f;
    [SerializeField] private Color lineColour = Color.black;
    [SerializeField] private LineRenderer stencilLine;
    [SerializeField] private float maxAllowedDistance;
    [SerializeField] private TextMeshProUGUI accuracyText;

    // reference drawing area
    [SerializeField] private Collider2D drawingArea;

    private Vector3[] stencilPoints;

    private void Start()
    {
        if (stencilLine != null)
        {
            stencilPoints = new Vector3[stencilLine.positionCount];
            stencilLine.GetPositions(stencilPoints);
            Debug.Log($"Loaded {stencilPoints.Length} stencil points from {stencilLine.name}");
        }
        else
        {
            Debug.LogWarning("Stencil Line not assigned in Inspector");
        }
    }

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
            if (drawingArea != null && drawingArea.OverlapPoint(currentPosition))
            {
                if (Vector3.Distance(currentPosition, previousPosition) > minDistance)
                {
                    currentLine.positionCount++;
                    currentLine.SetPosition(currentLine.positionCount - 1, currentPosition);
                    previousPosition = currentPosition;
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && currentLine != null)
        {
            float accuracy = CalculateAccuracy(currentLine);
            Debug.Log($"Tattoo Accuracy: {accuracy:F2}%");

            GameController.Instance.EndRound(accuracy);

            if (accuracyText != null )
            {
                accuracyText.text = $"Accuracy: {accuracy:F2}%";
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
        newLine.sortingOrder = 5;

        currentLine = newLine;
        previousPosition = Vector3.positiveInfinity;
        allLines.Add(newLine);
    }

    private float CalculateAccuracy(LineRenderer playerLine)
    {
        if (stencilPoints == null || stencilPoints.Length == 0)
        {
            Debug.LogWarning("No stencil assigned for accuracy check");
            return 0f;
        }

        Vector3[] playerPoints = new Vector3[playerLine.positionCount];
        playerLine.GetPositions(playerPoints);

        float totalDistance = 0f;
        int comparisons = 0;

        foreach (Vector3 p in playerPoints)
        {
            float minDist = float.MaxValue;
            foreach (Vector3 s in stencilPoints)
            {
                float dist = Vector3.Distance(p, s);
                if (dist < minDist) minDist = dist;
            }
            totalDistance += minDist;
            comparisons++;
        }

        float averageDistance = totalDistance / comparisons;
        float score = Mathf.Clamp01(1f - (averageDistance / maxAllowedDistance)) * 100f;

        return score;
    }
}
