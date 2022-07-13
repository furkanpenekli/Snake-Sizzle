using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FruitManager : MonoBehaviour
{
    public GameObject fruit;
    public GameObject currentFruit;
    
    //public bool fruitInMap;
    public int ateFruit;
    public bool ate;
    public float fruitGoal;
    public int fruitGoalMultiplier;
    
    public SoundManager soundManager;
    public Player player;
    public Text fruitText;
    public Text goalText;
    public Image fruitBar;
    void Start()
    {
        ate = false;
        ateFruit = 0;
        fruitGoal = 2;
        fruitGoalMultiplier = 2;

        /*player = GameObject.Find("Player").GetComponent<Player>();
        tileCreater = GameObject.Find("TileManager").GetComponent<TileCreater>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        fruitText = GameObject.Find("FruitText").GetComponent<Text>();
        goalText = GameObject.Find("FruitGoalText").GetComponent<Text>();
        fruitBar = GameObject.Find("FruitBar").GetComponent<Image>();
        fruit = GameObject.Find("Fruit_Apple");*/

        fruitBar.fillAmount = ateFruit / fruitGoal;
    }
    private void Update()
    {
        fruitText.text = ateFruit.ToString();
        goalText.text = fruitGoal.ToString();
        
        if (ateFruit >= fruitGoal)
        {
            soundManager.PlayLevelUpSound();
            fruitGoal = fruitGoal * fruitGoalMultiplier;
        }
        fruitBar.fillAmount = ateFruit / fruitGoal;
    }
}
