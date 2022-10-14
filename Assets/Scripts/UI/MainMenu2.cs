using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu2 : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}