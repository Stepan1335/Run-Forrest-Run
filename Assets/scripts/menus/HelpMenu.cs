using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenu : MonoBehaviour
{
    [SerializeField]
    GameObject previousPageOfHelpMenu;

    [SerializeField]
    GameObject nextPageOfHelpMenu;

    /// <summary>
    /// Handles the on click event from the quit button
    /// </summary>
    public void HandleQuitButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.ButtonClick);

        // go to main menu
        MenuManager.GoToMenu(MenuName.Main);
    }

    /// <summary>
    /// Handles the on click event from the left Arrow button
    /// </summary>
    public void HandleLeftArrowButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.ButtonClick);

        // unpause game and go to main menu
        //instantiate prefab
        Object.Instantiate(previousPageOfHelpMenu);
        Destroy(gameObject);
    }

    /// <summary>
    /// Handles the on click event from the right Arrow button
    /// </summary>
    public void HandleRightArrowButtonOnClickEvent()
    {
        AudioManager.Play(AudioClipName.ButtonClick);

        //instantiate prefab
        Object.Instantiate(nextPageOfHelpMenu);
        Destroy(gameObject);
    }
}
