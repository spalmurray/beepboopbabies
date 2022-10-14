using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TextMeshPro startButton;
    public TextMeshPro exitButton;
    public List<TextMeshPro> menuButtons = new();

    public AudioClip menuMove;
    public AudioClip menuSelect;
    private int selectedButton;

    // Start is called before the first frame update
    private void Start()
    {
        menuButtons.Add(startButton);
        menuButtons.Add(exitButton);

        GetComponent<AudioSource>().playOnAwake = false;
    }

    // Update is called once per frame
    private void Update()
    {
        menuButtons.ForEach(button =>
        {
            button.color = Color.white;
            if (button == menuButtons[selectedButton]) button.color = Color.red;
        });
    }

    private void OnMove(InputValue value)
    {
        var inputDirection = value.Get<Vector2>();
        if (inputDirection.y != 0)
        {
            selectedButton = (selectedButton + 1) % menuButtons.Count;
            GetComponent<AudioSource>().clip = menuMove;
            GetComponent<AudioSource>().Play();
        }
        else if (inputDirection.x != 0)
        {
            pressButton();
        }
    }

    private void pressButton()
    {
        GetComponent<AudioSource>().clip = menuSelect;
        GetComponent<AudioSource>().Play();
        Debug.Log("xd" + selectedButton);

        SceneManager.LoadScene("Scene1");
    }
}