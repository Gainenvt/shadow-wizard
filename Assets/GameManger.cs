using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour

{private Material scoreMaterial;

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
    public GameObject PauseMenu;
    public GameObject QuitButton;
    private bool IsPaused = false;
    private bool TutorialFinished = false;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {   scoreMaterial = ScoreText.fontMaterial;
        UpdateScoreUI();
        GameOverPanel.SetActive(false);
        TutorialPanel.SetActive(true);
        PauseMenu.SetActive(false);

        Time.timeScale = 0f;
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
     
if (!TutorialFinished &&
    TutorialPanel.activeSelf &&
    !Keyboard.current.escapeKey.wasPressedThisFrame &&
    Keyboard.current.anyKey.wasPressedThisFrame)
{
    TutorialPanel.SetActive(false);
    TutorialFinished = true;
    Time.timeScale = 1f;
}

        // Pause menu
       if (TutorialFinished && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (IsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
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
    public void QuitGame()
    {
        Application.Quit();
    }
    IEnumerator ScoreGlowEffect()
{
    Vector3 originalScale = ScoreText.transform.localScale;

    float originalGlow = scoreMaterial.GetFloat(ShaderUtilities.ID_GlowPower);

    // BIGGER TEXT
    ScoreText.transform.localScale = originalScale * 1.2f;

    // STRONGER GLOW
    scoreMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 1.5f);

    yield return new WaitForSeconds(0.15f);

    // RESET
    ScoreText.transform.localScale = originalScale;

    scoreMaterial.SetFloat(ShaderUtilities.ID_GlowPower, originalGlow);
}


    void UpdateScoreUI()
    {
        ScoreText.text = "Matches: " + Score;
        StartCoroutine(ScoreGlowEffect());
    }
}