using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayNumberUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayText;

    void Start()
    {
        UpdateDayText();
    }

    public void UpdateDayText()
    {
        if (DayManager.Instance == null) return;

        dayText.text = $"Day {DayManager.Instance.dayNumber}";
    }
}
