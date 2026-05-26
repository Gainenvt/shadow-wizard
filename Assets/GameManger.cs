using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private Material scoreMaterial;
    public static GameManager Instance;
    public int MissCount = 0;
    public int MaxMisses = 7;
    public bool GameEnded = false;
    public int Score = 0;
    public int PointsPerMatch = 2;
    public int WinScore = 6;
    public TextMeshProUGUI TutorialText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI ResultText;
    public GameObject GameOverPanel;
    public GameObject NextLevelButton;
    public GameObject RetryLevelButton;
    public GameObject MainMenuButton;
    public GameObject PauseMenu;
    

    private bool IsPaused = false;

    private Coroutine glowCoroutine;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        scoreMaterial = ScoreText.fontMaterial;

        ScoreText.text = "Matches: " + Score;

        GameOverPanel.SetActive(false);
        PauseMenu.SetActive(false);
        if (TutorialText != null)
        {
        StartCoroutine(TutorialFade());
        }
        Time.timeScale = 1f;



    }

    void Update()
    {
         if (Keyboard.current == null)
        return;

    // Pause State
    if (IsPaused)
    {
        if (Keyboard.current.escapeKey.wasReleasedThisFrame)
        {
            ResumeGame();
        }

        return;
    }

    // Gameplay State
    if (Keyboard.current.escapeKey.wasReleasedThisFrame)
    {
        PauseGame();

        return;
    }
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

    public void PauseGame()
    {
        if (GameEnded) return;

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

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );

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
        Vector3 originalScale =
            ScoreText.transform.localScale;

        float originalGlow =
            scoreMaterial.GetFloat(
                ShaderUtilities.ID_GlowPower
            );

        // Bigger text
        ScoreText.transform.localScale =
            originalScale * 1.2f;

        // Stronger glow
        scoreMaterial.SetFloat(
            ShaderUtilities.ID_GlowPower,
            1.5f
        );

        yield return new WaitForSecondsRealtime(0.15f);

        // Reset
        ScoreText.transform.localScale =
            originalScale;

        scoreMaterial.SetFloat(
            ShaderUtilities.ID_GlowPower,
            originalGlow
        );
    }

    void UpdateScoreUI()
    {
        ScoreText.text = "Matches: " + Score;

        if (glowCoroutine != null)
        {
            StopCoroutine(glowCoroutine);
        }

        glowCoroutine =
            StartCoroutine(ScoreGlowEffect());
    }
    IEnumerator TutorialFade()
{
    // Start fully visible
    Color textColor = TutorialText.color;

    textColor.a = 1f;

    TutorialText.color = textColor;

    // Stay visible briefly
    yield return new WaitForSecondsRealtime(3f);
    float duration = 2f;

    float timer = 0f;

    while (timer < duration)
    {
        timer += Time.unscaledDeltaTime;

        float alpha =
            Mathf.Lerp(1f, 0f, timer / duration);

        textColor.a = alpha;

        TutorialText.color = textColor;

        yield return null;
    }

    TutorialText.gameObject.SetActive(false);
}
}