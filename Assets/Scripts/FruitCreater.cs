using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCreater : MonoBehaviour
{
    public bool fruitInMap;
    public GameObject fruit;
    public GameObject currentFruit;

    public TileCreater tileCreater;
    void Start()
    {
        
    }
    void Update()
    {
        if (fruitInMap == false)
        {
            currentFruit = Instantiate(fruit,new Vector3(0,0,0),fruit.transform.rotation);
            currentFruit.transform.position = new Vector3(Random.Range(0,tileCreater.row), 1,Random.Range(0,-tileCreater.column));
            fruitInMap = true;
        }
    }
}
