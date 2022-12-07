using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPaused;

    public GameObject pauseMenuUI;
    public GameObject initialButton;

    private void OnEnable()
    {
       // Pause();
    }

    public void TogglePause()
    {
        if (ScoreManager.Instance.IsGameOver) return;
        
        if (GameIsPaused)
            Resume();
        else
            Pause();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        //Time.timeScale = 1f;
        //SceneManager.LoadScene("Scene1");
    }
    public void LoadGame()
    {
        //pauseMenuUI.SetActive(false);
        //Time.timeScale = 1f;
        //GameIsPaused = false;

        Time.timeScale = 1f;
        LevelsManager.Instance.LoadLevelScene();
    }

    public void NextLevel()
    {
        LevelsManager.Instance.Level++;
        LoadGame();
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        initialButton.GetComponent<Button>().Select();
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}