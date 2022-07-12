using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public Player player;
    private FruitManager fruitManager;
    private SoundManager soundManager;
    private void Start()
    {
        fruitManager = GameObject.Find("FruitManager").GetComponent<FruitManager>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.CreateTail.Invoke();
            fruitManager.ateFruit++;
            Destroy(gameObject);
            fruitManager.fruitInMap = false;
            //create tail
        }
    }
}
