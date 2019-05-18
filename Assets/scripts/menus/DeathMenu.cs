using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//instantiate when character lose all health and show how much lifes character get
public class DeathMenu : IntEventInvoker
{
    //lifes support
    [SerializeField]
    Text textLifesGameObject;
    Text textLifes;

    // Start is called before the first frame update
    void Start()
    {
        // pause the game when added to the scene
        //Time.timeScale = 0;

        //change the amount of lifes
        textLifes = textLifesGameObject.GetComponent<Text>();
        textLifes.text = "Lives: " + HUD.CurresntAmoundOfCharacterLifes.ToString();

        //get another life event support
        unityEvents.Add(EventName.GetAnotherLifeEvent, new GetAnotherLifeEvent());
        EventManager.AddInvoker(EventName.GetAnotherLifeEvent, this);

        //Audio support
        AudioManager.Play(AudioClipName.CharacterLoseLife);
    }

    /// <summary>
    /// Handles the on click event from the Restart button
    /// </summary>
    public void HandleContinueButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.ButtonClick);

        // restart game and destroy menu
        unityEvents[EventName.GetAnotherLifeEvent].Invoke(0);
        Time.timeScale = 1;
        Destroy(gameObject);
    }

    /// <summary>
    /// Handles the on click event from the Quit button
    /// </summary>
    public void HandleQuitButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.ButtonClick);

        // unpause game, destroy menu, and go to main menu
        Time.timeScale = 1;
        Destroy(gameObject);
        MenuManager.GoToMenu(MenuName.Main);
    }
}
