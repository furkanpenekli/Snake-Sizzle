using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    
    public AudioSource eatSource;
    public AudioSource levelUpSource;
    
    public List<AudioClip> eatList = new List<AudioClip>();
    public List<AudioClip> levelUpList = new List<AudioClip>();

    public void PlayEatSound()
    {
        int r = Random.Range(0, eatList.Count);
        eatSource.clip = eatList[r];
        eatSource.Play();
    }
    public void PlayLevelUpSound()
    {
        int r = Random.Range(0, levelUpList.Count);
        levelUpSource.clip = levelUpList[r];
        levelUpSource.Play();
    }
    void Update()
    {
        
    }
}
