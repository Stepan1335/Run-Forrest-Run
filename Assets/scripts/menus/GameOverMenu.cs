using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    //lifes support
    [SerializeField]
    Text textScoreGameObject;
    Text scoreText;

    // Start is called before the first frame update
    void Start()
    {


        //change the amount of lifes
        scoreText = textScoreGameObject.GetComponent<Text>();
        scoreText.text = "Total Score: " + HUD.Score.ToString();

        AudioManager.Play(AudioClipName.GameOverMusic);
    }

    /// <summary>
    /// Handles the on click event from the Restart button
    /// </summary>
    public void HandleRestartButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.ButtonClick);

        // restart game 
        SceneManager.LoadScene("Gameplay");
    }

    /// <summary>
    /// Handles the on click event from the Quit button
    /// </summary>
    public void HandleQuitButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.ButtonClick);

        //go to main menu
        MenuManager.GoToMenu(MenuName.Main);
    }
}
