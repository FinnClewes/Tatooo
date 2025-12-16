using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance;

    [Header("Day Settings")]
    [SerializeField] private int customersPerDay = 5;
    [SerializeField] private float dailyCashGoal = 200f;
    [Header("Runtime Status")]
    public int customersRemaining;
    public bool dayActive = true;
    public CustomerData activeCustomer;

    private Cash cashSystem;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        cashSystem = Cash.Instance;
        StartNewDay();
    }

    public void StartNewDay()
    {
        customersRemaining = customersPerDay;
        dayActive = true;
        Debug.Log("New Day Started");
    }

    // Called from shop scene
    public void AcceptCustomer()
    {
        activeCustomer = FindObjectOfType<CustomerManager>().CurrentCustomer;
        SceneManager.LoadScene("DrawingScene");
    }

    public void RefuseCustomer()
    {
        customersRemaining--;

        FindObjectOfType<CustomerManager>().SpawnCustomer();
        CheckDayEnd();
    }

    // Called from drawing scene
    public void FinishTattoo()
    {
        customersRemaining--;
        SceneManager.LoadScene("ShopScene");
        CheckDayEnd();
    }

    private void CheckDayEnd()
    {
        if (customersRemaining > 0) return;

        dayActive = false;

        if (cashSystem.playerMoney >= dailyCashGoal)
        {
            Debug.Log("Day complete");
            StartNewDay();
        }
        else
        {
            Debug.Log("Game Over - cash goal not met");
        }
    }
}
