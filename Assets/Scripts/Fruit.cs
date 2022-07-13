using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject player_1;
    public GameObject player_2;
    private FruitManager fruitManager;
    public FruitCreater fruitCreater;
    private void Start()
    {
        /*fruitManager = GameObject.Find("FruitManager").GetComponent<FruitManager>();
        player = GameObject.Find("Player").GetComponent<Player>();*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == player_1.GetComponent<BoxCollider>())
        {
            player_1.GetComponent<Snake>().CreateTail.Invoke();
            player_1.GetComponent<Snake>().fruitManager.ateFruit++;
            Destroy(gameObject);
            fruitCreater.fruitInMap = false;
            
        }
        else if (other == player_2.GetComponent<BoxCollider>())
        {
            player_2.GetComponent<Snake>().CreateTail.Invoke();
            player_2.GetComponent<Snake>().fruitManager.ateFruit++;
            Destroy(gameObject);
            fruitCreater.fruitInMap = false;
            
        }
    }
}
