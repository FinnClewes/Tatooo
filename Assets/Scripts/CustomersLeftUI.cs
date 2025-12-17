using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomersLeftUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI customersText;

    void Start()
    {
        UpdateText();
    }

    private void OnEnable()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        if (DayManager.Instance == null) return;

        customersText.text = $"Customers Left: {DayManager.Instance.customersRemaining}";
    }
}
