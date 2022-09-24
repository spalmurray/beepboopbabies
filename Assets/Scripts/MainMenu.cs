using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    public TMPro.TextMeshPro startButton;
    public TMPro.TextMeshPro exitButton;
    public List<TMPro.TextMeshPro> menuButtons = new List<TMPro.TextMeshPro>();
    private int selectedButton = 0;

    // Start is called before the first frame update
    void Start()
    {
        menuButtons.Add(startButton);
        menuButtons.Add(exitButton);
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
        if (inputDirection.y > 0) {
            selectedButton = (selectedButton + 1) % menuButtons.Count;
        } else if (inputDirection.y < 0) {
            selectedButton = (selectedButton + 1) % menuButtons.Count;
        } else if (inputDirection.x != 0) {

        }
    }

    void pressButton() {
        if (selectedButton == 0) {
            // Start Game
        } else if (selectedButton == 1) {
            // Exit Game
        }
    }
}
