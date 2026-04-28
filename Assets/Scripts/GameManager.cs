using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int enemyKillTarget = 4;
    private int killCount = 0;
    private bool gameOver = false;
    private bool isPaused = false;
    public AudioSource gameMusic;
    public AudioSource winSound;

    [Header("UI")]
    public GameObject gameOverUI;
    public GameObject winUI;
    public GameObject pauseUI;
    public TMP_Text killCounterText;
    public Button restartButton;
    public Button restartButton2;
    public Button restartButton3;
    public Button resumeButton;
    public Button quitButton;

    [Header("Player")]
    public PlayerMovement playerMovement;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gameOverUI.SetActive(false);
        winUI.SetActive(false);
        pauseUI.SetActive(false);
        Time.timeScale = 1f;

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);
        if (restartButton2 != null)
            restartButton2.onClick.AddListener(RestartGame);
        if (restartButton3 != null)
            restartButton3.onClick.AddListener(RestartGame);
        if (resumeButton != null)
            resumeButton.onClick.AddListener(ResumeGame);
        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
    }

    void Update()
    {
        HandlePause();
    }

    void HandlePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver)
        {
            isPaused = !isPaused;
            pauseUI.SetActive(isPaused);
            Time.timeScale = isPaused ? 0f : 1f;
            if (isPaused)
                playerMovement.DisableMovement();
            else
            {
                playerMovement.canMove = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    public void EnemyKilled()
    {
        if (gameOver) return;
        killCount++;
        killCounterText.text = "Enemies Left: " + (enemyKillTarget - killCount) + " / " + enemyKillTarget;
        if (killCount >= enemyKillTarget)
            WinGame();
    }

    public void PlayerDied()
    {
        if (gameOver) return;
        gameOver = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        if (playerMovement != null) playerMovement.DisableMovement();
        if (gameMusic != null) gameMusic.Stop();
    }

    void WinGame()
    {
        gameOver = true;
        winUI.SetActive(true);
        Time.timeScale = 0f;
        if (playerMovement != null) playerMovement.DisableMovement();
        if (gameMusic != null) gameMusic.Stop();
        if (winSound != null) winSound.Play();
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        playerMovement.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}