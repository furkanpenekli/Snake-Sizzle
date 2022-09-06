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

    public Snake snake;
    private void PlayEatSound()
    {
        int r = Random.Range(0, eatList.Count);
        eatSource.clip = eatList[r];
        eatSource.Play();
    }
    private void PlayLevelUpSound()
    {
        int range = Random.Range(0, levelUpList.Count);
        levelUpSource.clip = levelUpList[range];
        levelUpSource.Play();
    }
    void Update()
    {
        if (snake.isGrow == true)
        {
            PlayEatSound();
        }
    }
}
