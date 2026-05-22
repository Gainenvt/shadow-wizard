using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int MissCount = 0;
    public int MaxMisses = 7;
    public bool GameEnded = false;

    public int Score = 0;
    public int PointsPerMatch = 2;
    public int WinScore = 6;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI ResultText;
    public GameObject GameOverPanel;
    public GameObject NextLevelButton;
    public GameObject RetryLevelButton;
    public GameObject MainMenuButton;
    public GameObject TutorialPanel;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateScoreUI();
        GameOverPanel.SetActive(false);
        TutorialPanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game at the start
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
            MainMenuButton.SetActive(true);

        }
        else
        {
            ResultText.text = "FLOOR FAILED";
            NextLevelButton.SetActive(false);
            RetryLevelButton.SetActive(true);
            MainMenuButton.SetActive(true);
        }
    }
    void Update()
    {
        if (TutorialPanel.activeSelf && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            TutorialPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Retrying level...");
    }
    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 2");
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
       SceneManager.LoadScene("MainMenu");
    }

    void UpdateScoreUI()
    {
        ScoreText.text = "Matches: " + Score;
    }
}