using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitCreater : MonoBehaviour
{
    [SerializeField]
    private bool fruitInMap;
    [SerializeField]
    private GameObject fruit;
    private GameObject currentFruit;
    [SerializeField]
    private TileCreater tileCreater;

    private void SearchFood()
    {
        //var food = GameObject.FindObjectOfType<Food>();
        var food = currentFruit;
        if (food == null)
        {
            fruitInMap = false;    
        }
    }
    private void CreateFruit()
    {
        if (fruitInMap == false)
        {
            currentFruit = Instantiate(fruit,new Vector3(0,0,0),fruit.transform.rotation);
            currentFruit.transform.position = new Vector3(Random.Range(0,tileCreater.row), 1,Random.Range(0,-tileCreater.column));
            fruitInMap = true;
        }
    }
    void Update()
    {
        SearchFood();
        CreateFruit();
    }
}
