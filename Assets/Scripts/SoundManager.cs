using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] List<AudioSource> clickButtonSounds;
    [SerializeField] List<AudioSource> hoverButtonSounds;



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
        Instance.GetComponent<AudioSource>().PlayOneShot(GetRandomClickButtonSound());
    }

    public void PlayHoverSound()
    {
        Instance.GetComponent<AudioSource>().PlayOneShot(GetRandomHoverButtonSound());
    }
}
