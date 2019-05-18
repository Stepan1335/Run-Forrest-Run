using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : IntEventInvoker
{
    //health support
    [SerializeField]
    Image[] hearts;

    //Score support
    [SerializeField]
    Text scoreGameObject;
    Text score;
    string scorePrefix = "Score: ";
    static int currestPoints = 0;

    //Lifes support
    [SerializeField]
    Text lifeTextGameObject;
    Text lifes;
    string lifesSuffics = " LIFES";
    static float currentForrestLifes;

    #region Properties
    public static int CurresntAmoundOfCharacterLifes
    {
        get { return (int)currentForrestLifes; }
    }

    public static int Score
    {
        get { return currestPoints; }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // get a numbers of lifes
        currentForrestLifes = ConfigurationUtils.ForrestLives;
        currestPoints = 0;

        //Score support
        score = scoreGameObject.GetComponent<Text>();
        score.text = scorePrefix + currestPoints.ToString();

        //lifes support
        lifes = lifeTextGameObject.GetComponent<Text>();
        lifes.text = " X " + currentForrestLifes.ToString() + lifesSuffics;

        //events
        EventManager.AddListener(EventName.HealthChangedEvent, HealthController);
        EventManager.AddListener(EventName.PointsAddedEvent, ScoreChanged);
        EventManager.AddListener(EventName.LivesChangeEvent, ChangeLives);
        //Difficulty change event
        unityEvents.Add(EventName.DifficultyChangeEvent, new DifficultyChangeEvent());
        EventManager.AddInvoker(EventName.DifficultyChangeEvent, this);
    }

    /// <summary>
    /// change amount of hearts and show it on display
    /// </summary>
    /// <param name="healthDamage">The damage which character get from enemy</param>
    void HealthController(int currentCharacterHealth)
    {
        if (currentCharacterHealth > 0)
        {
            ShowingHeartsOnScreen(currentCharacterHealth);
        }
        else
        {
            //lose last health
            ShowingHeartsOnScreen(currentCharacterHealth);
            ChangeLives(-1);
        }

    }

    /// <summary>
    /// Show on screen a current amount of hearts
    /// </summary>
    /// <param name="currentCharacterHealth"> current number of health</param>
    void ShowingHeartsOnScreen( int currentCharacterHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentCharacterHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    /// <summary>
    /// change the score
    /// </summary>
    /// <param name="points">points that character get for killing a enemy</param>
    void ScoreChanged(int points)
    {
        currestPoints += points;
        score.text = scorePrefix + currestPoints.ToString();
        unityEvents[EventName.DifficultyChangeEvent].Invoke(currestPoints);
    }

    /// <summary>
    /// Change a lifes and instantiate a DeathMenuPrefab
    /// </summary>
    /// <param name>number whuch shows reduse one life or add one</param>
    void ChangeLives(int livesChange)
    {
        currentForrestLifes += livesChange;
        //change the sufics
        if (currentForrestLifes == 1)
        {
            lifesSuffics = " LIFE";
        }
        else
        {
            lifesSuffics = " LIVES";
        }
        
        lifes.text = " X " + currentForrestLifes.ToString() + lifesSuffics;

        //get a right menu 
        if (livesChange < 0) // show menu only if character lose a life
        {
            if (currentForrestLifes > 0)
            {
                MenuManager.GoToMenu(MenuName.Death);
            }
            else
            {
                MenuManager.GoToMenu(MenuName.GameOver);
            }
        }

    }
}
