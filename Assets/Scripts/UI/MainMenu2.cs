using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu2 : MonoBehaviour
{
    [SerializeField]
    private Slider SFX, MUSIC;
    [SerializeField]
    private Dropdown dropdown;
    private void Update()
    {
        LevelsManager.Instance.Level = dropdown.value;
        PlayerPrefs.SetFloat("sfx", SFX.value);
        PlayerPrefs.SetFloat("music", MUSIC.value);
        var quitKey = KeyCode.Escape;
        var nextKey = KeyCode.Space;

        if (Input.GetKeyDown(quitKey))
        {
            Application.Quit();

            Debug.Log("Quit Game.");
        }

        if (Input.GetKeyDown(nextKey)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayGame()
    {
        LevelsManager.Instance.Level = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ContineGame()
    {
        if (LevelsManager.Instance.Level > LevelsManager.Instance.UnLockLevel) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}