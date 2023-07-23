using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] List<AudioSource> clickButtonSounds;
    [SerializeField] List<AudioSource> hoverButtonSounds;
    [SerializeField] List<AudioSource> pickUpSounds;
    [SerializeField] List<AudioSource> meleeAttackSounds;
    [SerializeField] List<AudioSource> blockSounds;
    [SerializeField] List<AudioSource> arrowFireSounds;



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
    private AudioClip GetRandomPickUpSound()
    {
        int randomIndex = Random.Range(0, pickUpSounds.Count);
        return pickUpSounds[randomIndex].clip;
    }
    private AudioClip GetRandomBlockSound()
    {
        int randomIndex = Random.Range(0, blockSounds.Count);
        return blockSounds[randomIndex].clip;
    }
    private AudioClip GetRandomMeleeAtackSound()
    {
        int randomIndex = Random.Range(0, meleeAttackSounds.Count);
        return meleeAttackSounds[randomIndex].clip;
    }
    private AudioClip GetRandomArrowFireSound()
    {
        int randomIndex = Random.Range(0, arrowFireSounds.Count);
        return arrowFireSounds[randomIndex].clip;
    }

    public void PlayClickSound()
    {
        Instance.GetComponent<AudioSource>().PlayOneShot(GetRandomClickButtonSound());
    }

    public void PlayHoverSound()
    {
        Instance.GetComponent<AudioSource>().PlayOneShot(GetRandomHoverButtonSound());
    }
    public void PlayPickUpSound()
    {
        Instance.GetComponent<AudioSource>().PlayOneShot(GetRandomPickUpSound());
    }
    public void PlayMeleeAttackSound()
    {
        Instance.GetComponent<AudioSource>().PlayOneShot(GetRandomMeleeAtackSound());
    }
    public void PlayBlockSound()
    {
        Instance.GetComponent<AudioSource>().PlayOneShot(GetRandomBlockSound());
    }
    public void PlayArrowFireSound()
    {
        Instance.GetComponent<AudioSource>().PlayOneShot(GetRandomArrowFireSound());
    }
}
