using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Collections;

public class GameController : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float timeLimit = 30f;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Score UI")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private Cash cashSystem;

    private float timeRemaining;
    private bool timerActive = false;
    private bool roundOver = false;

    public static GameController Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (!timerActive || roundOver) return;

        timeRemaining -= Time.deltaTime;
        if (timeRemaining < 0)
        {
            timeRemaining = 0;
            EndRound(0f);
        }

        timerText.text = timeRemaining.ToString("F1") + "s";
    }

    public void ResetTimer()
    {
        timeRemaining = timeLimit;
        timerActive = true;
        roundOver = false;
    }

    // Called by DrawWithMouse when player releases the mouse
    public void EndRound(float accuracy)
    {
        if (roundOver) return;
        roundOver = true;
        timerActive = false;

        float timeBonus = (timeRemaining / timeLimit) * 100f; // 0-100%
        float finalScore = (accuracy * 0.7f) + (timeBonus * 0.3f); // weighted final score

        scoreText.text = finalScore.ToString("F1");
        Debug.Log($"Final Score = Accuracy ({accuracy:F2}) + Time Bonus ({timeBonus:F1})");

        cashSystem.RewardMoney(finalScore);
    }
}
