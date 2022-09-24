using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMPro.TextMeshPro startButton;
    public TMPro.TextMeshPro exitButton;
    public List<TMPro.TextMeshPro> menuButtons = new List<TMPro.TextMeshPro>();
    private int selectedButton = 0;

    public AudioClip menuMove;
    public AudioClip menuSelect;

    // Start is called before the first frame update
    void Start()
    {
        menuButtons.Add(startButton);
        menuButtons.Add(exitButton);

        GetComponent<AudioSource>().playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        menuButtons.ForEach(button => {
            button.color = Color.white;
            if (button == menuButtons[selectedButton])
            {
                button.color = Color.red;
            }
        });
    }

    void OnMove(InputValue value) {
        Vector2 inputDirection = value.Get<Vector2>();
        if (inputDirection.y != 0) {
            selectedButton = (selectedButton + 1) % menuButtons.Count;
            GetComponent<AudioSource>().clip = menuMove;
            GetComponent<AudioSource>().Play();
        } else if (inputDirection.x != 0) {
            pressButton();
        }
    }

    void pressButton() {
        GetComponent<AudioSource>().clip = menuSelect;
        GetComponent<AudioSource>().Play();
        Debug.Log("xd" + selectedButton);

        SceneManager.LoadScene("Scene1");
    }
}
