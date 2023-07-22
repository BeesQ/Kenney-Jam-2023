using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] List<AudioSource> clickButtonSounds;
    [SerializeField] List<AudioSource> hoverButtonSounds;



    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundManager>();
                if (_instance == null)
                {
                    Debug.LogError("SoundManager instance not found in the scene.");
                }
            }
            return _instance;
        }
    }

    private AudioClip GetRandomClickButtonSound()
    {
        int randomIndex = Random.Range(0, clickButtonSounds.Count);
        return clickButtonSounds[randomIndex].clip;
    }

    private AudioClip GetRandomHoverButtonSound()
    {
        int randomIndex = Random.Range(0, hoverButtonSounds.Count);
        return hoverButtonSounds[randomIndex].clip;
    }

    public void PlayClickSound()
    {
        _instance.GetComponent<AudioSource>().PlayOneShot(GetRandomClickButtonSound());
    }

    public void PlayHoverSound()
    {
        _instance.GetComponent<AudioSource>().PlayOneShot(GetRandomHoverButtonSound());
    }
}
