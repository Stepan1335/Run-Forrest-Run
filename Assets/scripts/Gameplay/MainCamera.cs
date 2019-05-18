using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    GameObject forrest;

    // Start is called before the first frame update
    void Start()
    {
        forrest = GameObject.FindGameObjectWithTag("Forrest");
    }

    // Update is called once per frame
    void Update()
    {
        //Follow the characters
        if ( forrest != null && forrest.transform.position.y > -4.7) //don't follow the character if he falls from platform
        {
            if (forrest != null && forrest.transform.position.x > 0 && forrest.transform.position.x < 26) // follow the character in some range
            {
                transform.position = new Vector3(forrest.transform.position.x, forrest.transform.position.y + 3.5f, -10f);
            }
            else if (forrest != null)
            {
                if (forrest.transform.position.x < 0)
                {
                    transform.position = new Vector3(0, forrest.transform.position.y + 3.5f, -10f);
                }
                else if (forrest.transform.position.x >= 26)
                {
                    transform.position = new Vector3(26, forrest.transform.position.y + 3.5f, -10f);
                }
                
            }
        }





        // check for pausing game
        if (Input.GetKeyDown(KeyCode.Escape) && GameObject.FindGameObjectWithTag("PauseMenu") == null)
        {

                MenuManager.GoToMenu(MenuName.Pause);
                //AudioManager.Play(AudioClipName.PauseGame);
            
        }
    }
}
