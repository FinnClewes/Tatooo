using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private CustomerData[] customers;

    [Header("UI")]
    [SerializeField] private Image portraitImage;
    [SerializeField] private TextMeshProUGUI nameText;

    public CustomerData CurrentCustomer {  get; private set; }
    
    void Start()
    {
        SpawnCustomer();    
    }

    public void SpawnCustomer()
    {
        CurrentCustomer = customers[Random.Range(0, customers.Length)];

        portraitImage.sprite = CurrentCustomer.portrait;
        nameText.text = CurrentCustomer.customerName;
    }
}
