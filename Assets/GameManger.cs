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
    public TextMeshProUGUI resultText;
    public GameObject gameOverPanel;
    public GameObject nextLevelButton;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateScoreUI();
        gameOverPanel.SetActive(false);
    }

    public void AddMatchScore(int groupSize)
    {
        if (gameEnded) return;

        score += pointsPerMatch;
        UpdateScoreUI();

        if (score >= winScore)
        {
            EndGame(true);
        }
    }

    public void CircleMissed()
    {
        if (gameEnded) return;

        missCount++;

        if (missCount >= maxMisses)
        {
            EndGame(false);
        }
    }

    void EndGame(bool won)
    {
        gameEnded = true;
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);

        if (won)
        {
            resultText.text = "FLOOR CLEARED";
            nextLevelButton.SetActive(true);
        }
        else
        {
            resultText.text = "FLOOR FAILED";
            nextLevelButton.SetActive(false);
        }
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }
}