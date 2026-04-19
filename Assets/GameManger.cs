using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int missCount = 0;
    public int maxMisses = 7;
    public bool gameEnded = false;
    public int score = 0;
    public int pointsPerMatch = 2;
    public int winScore = 20;
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateScoreUI();
    }

    public void AddMatchScore(int groupSize)
    {
        if (gameEnded) return;

        score += pointsPerMatch;

        UpdateScoreUI();

        if (score >= winScore && !gameEnded)
        {
            gameEnded = true;
            Debug.Log("YOU WIN");
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void CircleMissed()
    {
        if (gameEnded) return;

        missCount++;

        if (missCount >= maxMisses && !gameEnded)
        {
            gameEnded = true;
            Debug.Log("Game Over!");
        }
    }



}