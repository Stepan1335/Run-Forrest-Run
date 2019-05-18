using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The audio manager
/// </summary>
public static class AudioManager
{
    static bool initialized = false;
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips =
        new Dictionary<AudioClipName, AudioClip>();

    /// <summary>
    /// Gets whether or not the audio manager has been initialized
    /// </summary>
    public static bool Initialized
    {
        get { return initialized; }
    }

    /// <summary>
    /// Initializes the audio manager
    /// </summary>
    /// <param name="source">audio source</param>
    public static void Initialize(AudioSource source)
    {
        initialized = true;
        audioSource = source;
        audioClips.Add(AudioClipName.ButtonClick, 
            Resources.Load<AudioClip>("ButtonClick"));
        audioClips.Add(AudioClipName.GameOverMusic,
            Resources.Load<AudioClip>("GameOverMusic"));
        audioClips.Add(AudioClipName.MusicGameplay,
            Resources.Load<AudioClip>("MusicGameplay"));
        audioClips.Add(AudioClipName.MusicFight,
             Resources.Load<AudioClip>("MusicFight"));
        audioClips.Add(AudioClipName.HealthPickup,
             Resources.Load<AudioClip>("HealthPickup"));
        audioClips.Add(AudioClipName.GenericPickup,
              Resources.Load<AudioClip>("GenericPickup"));
        audioClips.Add(AudioClipName.EnemyDie,
            Resources.Load<AudioClip>("EnemyDie"));
        audioClips.Add(AudioClipName.EnemySteps,
    Resources.Load<AudioClip>("EnemySteps"));
        audioClips.Add(AudioClipName.EnemyAttack,
            Resources.Load<AudioClip>("EnemyAttack"));
        audioClips.Add(AudioClipName.CharacterAttack,
            Resources.Load<AudioClip>("CharacterAttack"));
        audioClips.Add(AudioClipName.CharacterJump,
             Resources.Load<AudioClip>("CharacterJump"));
        audioClips.Add(AudioClipName.CharacterLand,
             Resources.Load<AudioClip>("CharacterLand"));
        audioClips.Add(AudioClipName.CharacterSteps,
              Resources.Load<AudioClip>("CharacterSteps"));
        audioClips.Add(AudioClipName.CharacterLoseHealth,
            Resources.Load<AudioClip>("CharacterLoseHealth"));
        audioClips.Add(AudioClipName.CharacterLoseLife,
            Resources.Load<AudioClip>("CharacterLoseLife"));
    }
    /// <summary>
    /// Plays the audio clip with the given name
    /// </summary>
    /// <param name="name">name of the audio clip to play</param>
    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }
}
