using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cash : MonoBehaviour
{
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

    private void Start()
    {
        Debug.Log("Cash is active");
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
    }
}
