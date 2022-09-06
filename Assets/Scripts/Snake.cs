using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using Unity.VisualScripting;

public class Snake : MonoBehaviour
{
    public int length { get { return tails.Count; } }

    public bool isAlive { get; private set; } = true;

    private bool _isGrow;
    public bool isGrow
    {
        get => isGrow;
    }
    private bool _isShrink;
    public bool isShrink
    {
        get => _isShrink;
    }
    [SerializeField]
    private Food _targetFood;
    public Food targetFood
    {
        get => _targetFood;
        set => _targetFood = value;
    } 
    public List<GameObject> tails;
    public GameObject tailPrefab;
    private SnakeInput snakeInput;
    private Movement movement;
    private void Grow()
    {
        var lastNode = tails.Last();
        var newNode = Instantiate(tailPrefab, lastNode.transform.position, lastNode.transform.rotation);
        tails.Add(newNode);
    }
    private void Shrink()
    {
        var lastNode = tails.Last();
        Destroy(lastNode);
        tails.Remove(lastNode);
    }
    private void Start()
    {
        snakeInput = GetComponent<SnakeInput>();
        movement = GetComponent<Movement>();
        tails = new List<GameObject>();
        tails.Add(gameObject);
    }

    private void Update()
    {
        
    }

    public void EatFood(Collider other)
    {
        var food = other.GetComponent<Food>();
        if (food != null)
        {
            targetFood = food;
            var isRotten = food.isRotten;
            if (isRotten)
            {
                Shrink();
            }
            else
            {
                Grow();
            }
            targetFood = null;
            Destroy(food.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(length > 2 && other.CompareTag("Player"))//
        {
            isAlive = false;
        }
        else if (length > 2 && other.CompareTag("Tail"))//
        {
            isAlive = false;
        }
        EatFood(other);
    }
}
