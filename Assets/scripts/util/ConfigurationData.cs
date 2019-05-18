using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Provides access to configuration data
/// </summary>
public class ConfigurationData
{
	#region Fields

    const string ConfigurationDataFileName = "ConfigurationData.csv";
    Dictionary<ConfigurationDataValueName, float> values =
        new Dictionary<ConfigurationDataValueName, float>();

	#endregion

	#region Properties

	/// <summary>
	/// Get a character speed 
	/// </summary>
	public float ForrestSpeed
    {
        get { return values[ConfigurationDataValueName.ForrestSpeed]; }
	}

	/// <summary>
	/// Gets Jump Force for character
	/// </summary>
	public float ForrestJumpForce
    {
        get { return values[ConfigurationDataValueName.ForrestJumpForce]; }
	}

	/// <summary>
	/// Gets a amount of heaths that character have
	/// </summary>
	public float ForrestHealth
    {
        get { return values[ConfigurationDataValueName.ForrestHealth]; }
	}

    /// <summary>
    /// Gets a amount of lives that character have
    /// </summary>
    public float ForrestLives
    {
        get { return values[ConfigurationDataValueName.ForrestLives]; }
	}

	/// <summary>
	/// Gets the enemy speed for easy difficalty
	/// </summary>
	public float EasyEnemySpeed
    {
        get { return values[ConfigurationDataValueName.EasyEnemySpeed]; }
	}

	/// <summary>
	/// Gets the bullet speed for easy difficalty
	/// </summary>
	public float EasyEnemyBulletSpeed
    {
        get { return values[ConfigurationDataValueName.EasyEnemyBulletSpeed]; }
	}

	/// <summary>
	/// Gets the amount of healthes for easy difficalty
	/// </summary>
	public float EasyEnemyHealth
    {
        get { return values[ConfigurationDataValueName.EasyEnemyHealth]; }
	}

    /// <summary>
    /// Gets the enemy speed for medium difficalty
    /// </summary>
    public float MediumEnemySpeed
    {
        get { return values[ConfigurationDataValueName.MediumEnemySpeed]; }
    }

    /// <summary>
    /// Gets the bullet speed for medium difficalty
    /// </summary>
    public float MediumEnemyBulletSpeed
    {
        get { return values[ConfigurationDataValueName.MediumEnemyBulletSpeed]; }
    }

    /// <summary>
    /// Gets the amount of healthes for medium difficalty
    /// </summary>
    public float MediumEnemyHealth
    {
        get { return values[ConfigurationDataValueName.MediumEnemyHealth]; }
    }

    /// <summary>
    /// Gets the enemy speed for hard difficalty
    /// </summary>
    public float HardEnemySpeed
    {
        get { return values[ConfigurationDataValueName.HardEnemySpeed]; }
    }

    /// <summary>
    /// Gets the bullet speed for hard difficalty
    /// </summary>
    public float HardEnemyBulletSpeed
    {
        get { return values[ConfigurationDataValueName.HardEnemyBulletSpeed]; }
    }

    /// <summary>
    /// Gets the amount of healthes for hard difficalty
    /// </summary>
    public float HardEnemyHealth
    {
        get { return values[ConfigurationDataValueName.HardEnemyHealth]; }
    }

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor
    /// Reads configuration data from a file. If the file
    /// read fails, the object contains default values for
    /// the configuration data
    /// </summary>
    public ConfigurationData()
    {
        // read and save configuration data from file
        StreamReader input = null;


        try
        {                
            // create stream reader object
            input = File.OpenText(Path.Combine(
                Application.streamingAssetsPath, ConfigurationDataFileName));

            // populate values
            string currentLine = input.ReadLine();
            while (currentLine != null)
            {
                string[] tokens = currentLine.Split(',');
                ConfigurationDataValueName valueName = 
                    (ConfigurationDataValueName)Enum.Parse(
                        typeof(ConfigurationDataValueName), tokens[0]);
                values.Add(valueName, float.Parse(tokens[1]));
                currentLine = input.ReadLine();
            }
        }
        catch (Exception e)
        {
            // set default values if something went wrong
            SetDefaultValues();
        }
        finally
        {
            // always close input file
            if (input != null)
            {
                input.Close();
            }
        }
    }

    #endregion

    /// <summary>
    /// Sets the configuration data fields to default values
    /// csv string
    /// </summary>
    void SetDefaultValues()
    {
        values.Clear();
        values.Add(ConfigurationDataValueName.ForrestSpeed, 0.1f);
        values.Add(ConfigurationDataValueName.ForrestJumpForce, 1100);
        values.Add(ConfigurationDataValueName.ForrestHealth, 3);
        values.Add(ConfigurationDataValueName.ForrestLives, 3);
        values.Add(ConfigurationDataValueName.EasyEnemySpeed, 0.06f);
        values.Add(ConfigurationDataValueName.EasyEnemyBulletSpeed, 1.5f);
        values.Add(ConfigurationDataValueName.EasyEnemyHealth, 1);
        values.Add(ConfigurationDataValueName.MediumEnemySpeed, 0.075f);
        values.Add(ConfigurationDataValueName.MediumEnemyBulletSpeed, 2);
        values.Add(ConfigurationDataValueName.MediumEnemyHealth, 2);
        values.Add(ConfigurationDataValueName.HardEnemySpeed, 0.09f);
        values.Add(ConfigurationDataValueName.HardEnemyBulletSpeed, 2.5f);
        values.Add(ConfigurationDataValueName.HardEnemyHealth, 3);
    }
}