using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance;

    [Header("Day Settings")]
    [SerializeField] private int customersPerDay = 5;
    [SerializeField] public float originalCashGoal = 200f;

    [Header("Customers")]
    [SerializeField] private CustomerData[] customers;

    [Header("Runtime Status")]
    public int customersRemaining;
    public bool dayActive = true;
    public float dailyCashGoal;
    public int dayNumber = 0;
    public CustomerData activeCustomer { get; private set; }

    private CustomerData lastCustomer;
    private Cash cashSystem;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;            
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (Cash.Instance == null)
        {
            Debug.LogError("Cash instance not found");
            return;
        }

        cashSystem = Cash.Instance;
        StartNewDay();
    }

    public void StartNewDay()
    {
        customersRemaining = customersPerDay;
        
        dayActive = true;
        PickNextCustomer();
        UpdateCustomersUI();
        dayNumber++;

        FindObjectOfType<DayNumberUI>()?.UpdateDayText();

        Debug.Log("New Day Started");
    }

    private void PickNextCustomer()
    {
        if (customers == null || customers.Length == 0)
        {
            Debug.Log("No customers assigned to DayManager");
            return;
        }    
               
        CustomerData next;
        do
        {
            next = customers[Random.Range(0, customers.Length)];
        }
        while (customers.Length > 1 && next == lastCustomer);

        activeCustomer = next;
        lastCustomer = next;

        CustomerManager cm = FindObjectOfType<CustomerManager>();
        if (cm != null)
        {
            cm.EnterAndRefresh();
        }

        Debug.Log("New customer: " + activeCustomer.customerName);
    }
     
      ////////////////
     // SHOP SCENE //
    ////////////////
    public void AcceptCustomer()
    {
        SceneManager.LoadScene("DrawingScene");
    }

    public void RefuseCustomer()
    {
        customersRemaining--;
        PickNextCustomer();

        CustomerManager cm = FindObjectOfType<CustomerManager>();
        if (cm != null) 
            cm.StartCoroutine(cm.ExitAndRefresh());

        UpdateCustomersUI();

        CheckDayEnd();
    }

    private void UpdateCustomersUI()
    {
        CustomersLeftUI ui = FindObjectOfType<CustomersLeftUI>();
        if (ui != null)
            ui.UpdateText();
    }

    ///////////////////
    // DRAWING SCENE //
    ///////////////////
    public void FinishTattoo()
    {
        customersRemaining--;
        PickNextCustomer();
        SceneManager.LoadScene("ShopScene");
        UpdateCustomersUI();
        CheckDayEnd();
    }

    private void CheckDayEnd()
    {
        if (customersRemaining > 0) return;

        dayActive = false;

        if (cashSystem.playerMoney >= dailyCashGoal)
        {
            Debug.Log("Day complete");

            dailyCashGoal += 10;
            Cash.Instance.playerMoney = 0;
            Cash.Instance.ForceRefreshUI();
            
            StartNewDay();
        }
        else
        {
            Debug.Log("Game Over - cash goal not met");
            Cash.Instance.playerMoney = 0;
            dailyCashGoal = originalCashGoal;
            dayNumber = 0;
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
