using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

/// <summary>
/// Provides difficulty-specific utilities
/// </summary>
public static class DifficultyUtils
{
	#region Fields

	static Difficulty difficulty;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the enemy speed for easy difficalty
    /// </summary>
    public static float EnemySpeed
    {
		get
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    return ConfigurationUtils.EasyEnemySpeed;
                case Difficulty.Medium:
                    return ConfigurationUtils.MediumEnemySpeed;
                case Difficulty.Hard:
                    return ConfigurationUtils.HardEnemySpeed;
                default:
                    return ConfigurationUtils.EasyEnemySpeed;
            }
		}
	}

    /// <summary>
    /// Gets the bullet speed
    /// </summary>
    public static float EnemyBulletSpeed
    {
		get
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    return ConfigurationUtils.EasyEnemyBulletSpeed;
                case Difficulty.Medium:
                    return ConfigurationUtils.MediumEnemyBulletSpeed;
                case Difficulty.Hard:
                    return ConfigurationUtils.HardEnemyBulletSpeed;
                default:
                    return ConfigurationUtils.EasyEnemyBulletSpeed;
            }
		}
	}

    /// <summary>
    /// Gets the amount of healthes
    /// </summary>
    public static float EnemyHealth
    {
		get
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    return ConfigurationUtils.EasyEnemyHealth;
                case Difficulty.Medium:
                    return ConfigurationUtils.MediumEnemyHealth;
                case Difficulty.Hard:
                    return ConfigurationUtils.HardEnemyHealth;
                default:
                    return ConfigurationUtils.EasyEnemyHealth;
            }
		}
	}

	#endregion

	#region Public methods

	/// <summary>
	/// Initializes the difficulty utils
	/// </summary>
	public static void Initialize()
    {
		EventManager.AddListener(EventName.GameStartedEvent,
			HandleGameStartedEvent);
	}

	#endregion

	#region Private methods

	/// <summary>
	/// Sets the difficulty and starts the game
	/// </summary>
	/// <param name="intDifficulty">int value for difficulty</param>
	static void HandleGameStartedEvent(int intDifficulty)
    {
		difficulty = (Difficulty)intDifficulty;
		SceneManager.LoadScene("Gameplay");
	}

	#endregion
}
