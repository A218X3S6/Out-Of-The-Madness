using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    [SerializeField] private AudioSource effects, music;


    private void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayMovementClip(AudioClip clip)
    {
        effects.PlayOneShot(clip);
    }

    public void PlayGameOverClip(AudioClip clip)
    {
        effects.PlayOneShot(clip);
    }

    public void PlayGetHitClip(AudioClip clip)
    {
        effects.PlayOneShot(clip);
    }

    public void PlayPickUpClip(AudioClip clip)
    {
        effects.PlayOneShot(clip);
    }

    public void PlayPowerUpClip(AudioClip clip)
    {
        effects.PlayOneShot(clip);
    }

    public void ChangeMusicVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void ToggleMusic()
    {
        music.mute = !music.mute;
    }
    public void ToggleEffect()
    {
        effects.mute = !effects.mute;
    }
}
