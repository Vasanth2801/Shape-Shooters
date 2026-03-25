using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private bool isGamePaused;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0f;
    }

    public void WinScreen()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}