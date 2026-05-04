using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int MissCount = 0;
    public int MaxMisses = 7;
    public bool GameEnded = false;

    public int Score = 0;
    public int PointsPerMatch = 2;
    public int WinScore = 20;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI ResultText;
    public GameObject GameOverPanel;
    public GameObject NextLevelButton;
    public GameObject RetryLevelButton;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateScoreUI();
        GameOverPanel.SetActive(false);
    }

    public void AddMatchScore(int groupSize)
    {
        if (GameEnded) return;

        Score += PointsPerMatch;
        UpdateScoreUI();

        if (Score >= WinScore)
        {
            EndGame(true);
        }
    }

    public void CircleMissed()
    {
        if (GameEnded) return;

        MissCount++;

        if (MissCount >= MaxMisses)
        {
            EndGame(false);
        }
    }

    void EndGame(bool won)
    {
        GameEnded = true;
        Time.timeScale = 0f;
        GameOverPanel.SetActive(true);

        if (won)
        {
            ResultText.text = "FLOOR CLEARED";
            NextLevelButton.SetActive(true);
            RetryLevelButton.SetActive(false);
        }
        else
        {
            ResultText.text = "FLOOR FAILED";
            NextLevelButton.SetActive(false);
            RetryLevelButton.SetActive(true);
        }
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
       SceneManager.LoadScene("MainMenu");
    }

    void UpdateScoreUI()
    {
        ScoreText.text = "Score: " + Score;
    }
}