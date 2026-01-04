using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAnimator : MonoBehaviour
{
    [SerializeField] private float slideDuration = 0.5f;
    [SerializeField] private float slideDistance = 800f;

    private RectTransform rect;
    private Vector2 onScreenPos;
    private Vector2 offScreenLeft;
    private Vector2 offScreenRight;

    private void Awake()
    {
        rect = GetComponent<RectTransform>(); 
    }

    private void Start()
    {
        InitializePositions();
    }

    private void InitializePositions()
    {
        if (rect == null) return;

        onScreenPos = rect.anchoredPosition;
        offScreenLeft = onScreenPos + Vector2.left * slideDistance;
        offScreenRight = onScreenPos + Vector2.right * slideDistance;

        rect.anchoredPosition = offScreenLeft; // Start off screen  
    }

    public IEnumerator PlayEnter()
    {
        //rect.anchoredPosition = offScreenLeft;
        yield return SlideTo(onScreenPos);
    }

    public IEnumerator PlayExit() // Exits cust. 1, enters cust. 2
    {
        yield return SlideTo(offScreenRight);
        rect.anchoredPosition = offScreenLeft;
        yield return SlideTo(onScreenPos);
    }

    private IEnumerator SlideTo(Vector2 target)
    {
        Vector2 start = rect.anchoredPosition;
        float time = 0f;

        while (time < slideDuration)
        {
            time += Time.deltaTime;
            float t = time / slideDuration;
            t = Mathf.SmoothStep(0, 1, t);

            rect.anchoredPosition = Vector2.Lerp(start, target, t);
            yield return null;
        }

        rect.anchoredPosition = target;
    }
}
