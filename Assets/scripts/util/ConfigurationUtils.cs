using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides utility access to configuration data
/// </summary>
public static class ConfigurationUtils
{
	#region Fields

	static ConfigurationData configurationData;

    #endregion

    #region Properties

    /// <summary>
    /// Get a character speed 
    /// </summary>
    public static float ForrestSpeed
    {
		get { return configurationData.ForrestSpeed; }
	}

    /// <summary>
    /// Gets Jump Force for character
    /// </summary>
    public static float ForrestJumpForce
    {
		get { return configurationData.ForrestJumpForce; }
	}

    /// <summary>
    /// Gets a amount of heaths that character have
    /// </summary>
    public static float ForrestHealth
    {
        get { return configurationData.ForrestHealth; }
    }

    /// <summary>
    /// Gets a amount of lives that character have
    /// </summary>
    public static float ForrestLives
    {
        get { return configurationData.ForrestLives; }
    }

    /// <summary>
    /// Gets the enemy speed
    /// </summary>
    public static float EnemySpeed
    {
		get { return DifficultyUtils.EnemySpeed; }
	}

    /// <summary>
    /// Gets the bullet speed
    /// </summary>
    public static float EnemyBulletSpeed
    {
		get { return DifficultyUtils.EnemyBulletSpeed; }
	}

    /// <summary>
    /// Gets the amount of healthes
    /// </summary>
    public static float EnemyHealth
    {
        get { return DifficultyUtils.EnemyHealth; }
    }

    #endregion

    #region Properties that should only be used by DifficultyUtils

    /// <summary>
    /// Gets the enemy speed for easy difficalty
    /// This property should only be used by DifficultyUtils
    /// </summary>
    public static float EasyEnemySpeed
    {
		get { return configurationData.EasyEnemySpeed; }
	}

    /// <summary>
    /// Gets the enemy speed for medium difficalty
    /// This property should only be used by DifficultyUtils
    /// </summary>
    public static float MediumEnemySpeed
    {
		get { return configurationData.MediumEnemySpeed; }
	}

    /// <summary>
    /// Gets the enemy speed for hard difficalty
    /// This property should only be used by DifficultyUtils
    /// </summary>
    /// <value>hard minimum spawn delay</value>
    public static float HardEnemySpeed
    {
		get { return configurationData.HardEnemySpeed; }
	}

    /// <summary>
    /// Gets the bullet speed for easy difficalty
    /// This property should only be used by DifficultyUtils
    /// </summary>
    public static float EasyEnemyBulletSpeed
    {
		get { return configurationData.EasyEnemyBulletSpeed; }
	}

    /// <summary>
    /// Gets the bullet speed for medium difficalty
    /// This property should only be used by DifficultyUtils
    /// </summary>
    public static float MediumEnemyBulletSpeed
    {
		get { return configurationData.MediumEnemyBulletSpeed; }
	}

    /// <summary>
    /// Gets the bullet speed for hard difficalty
    /// This property should only be used by DifficultyUtils
    /// </summary>
    public static float HardEnemyBulletSpeed
    {
		get { return configurationData.HardEnemyBulletSpeed; }
	}

    /// <summary>
    /// Gets the amount of healthes for easy difficalty
    /// This property should only be used by DifficultyUtils
    /// </summary>
    public static float EasyEnemyHealth
    {
		get { return configurationData.EasyEnemyHealth; }
	}

    /// <summary>
    /// Gets the amount of healthes for medium difficalty
    /// This property should only be used by DifficultyUtils
    /// </summary>
    public static float MediumEnemyHealth
    {
		get { return configurationData.MediumEnemyHealth; }
	}

    /// <summary>
    /// Gets the amount of healthes for hard difficalty
    /// This property should only be used by DifficultyUtils
    /// </summary>
    public static float HardEnemyHealth
    {
		get { return configurationData.HardEnemyHealth; }
	}   

	#endregion

	#region Public methods

	/// <summary>
	/// Initializes the configuration data by creating the ConfigurationData object 
	/// </summary>
	public static void Initialize()
	{
        configurationData = new ConfigurationData();
	}

	#endregion
}
