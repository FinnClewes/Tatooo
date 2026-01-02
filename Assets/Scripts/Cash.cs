using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cash : MonoBehaviour
{
    public static Cash Instance;

    public float playerMoney = 0f;

    [Header("Cash amounts")] 
    [SerializeField] private float highReward = 80f;
    [SerializeField] private float midReward = 50f;
    [SerializeField] private float lowReward = 25f;
    [SerializeField] private float failPenalty = 30f;

    [Header("Score bounds")]
    [SerializeField] private float highScore = 70f;
    [SerializeField] private float midScore = 50f;
    [SerializeField] private float lowScore = 20f;

     private TextMeshProUGUI cashText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Listen for scene changes
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find cash UI in the new scene
        cashText = GameObject.Find("Canvas/Cash")?.GetComponent<TextMeshProUGUI>();

        if(cashText == null)
        {
            Debug.LogWarning("Cash UI not found in scene: " + scene.name);
            return;
        }

        UpdateCashUI();
    }

    public void RewardMoney(float finalScore)
    {
        if (finalScore >= highScore)
        {
            playerMoney += highReward;
        }
        else if (finalScore < highScore && finalScore >= midScore)
        {
            playerMoney += midReward;
        }
        else if (finalScore < midScore && finalScore >= lowScore)
        {
            playerMoney += lowReward;
        }
        else
        {
            playerMoney -= failPenalty;
        }

        Debug.Log($"Total money: {playerMoney:F2}");
        UpdateCashUI();
    }

    private void UpdateCashUI()
    {
        if (cashText != null)
        {
            if (playerMoney >= 0)
            {
                cashText.text = $"€{playerMoney:F0}/€{DayManager.Instance.dailyCashGoal}";
            }
            else
            {
                playerMoney = 0;
                cashText.text = $"€{playerMoney:F0}/€{DayManager.Instance.dailyCashGoal}";
            }
        }
    }

    public void ForceRefreshUI()
    {
        UpdateCashUI();
    }
}
