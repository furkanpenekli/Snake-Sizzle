using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public struct KeyMapping
{
    public KeyCode moveUp;
    public KeyCode moveDown;
    public KeyCode moveRight;
    public KeyCode moveLeft;
}

public enum Direction
{
    Up,
    Down,
    Right,
    Left
}

public class Snake : MonoBehaviour
{
    public KeyMapping keyMapping;

    public GameOverMenu gameOverMenu;
    public FruitManager fruitManager;
    public TileCreater tileCreater;
    public SoundManager soundManager;
    public Text speedText;
    public Text tailText;

    public float speed;
    private float time;
    private float timeDelay;
    private Direction moveDirection = Direction.Up;
    private List<GameObject> tails;

    public GameObject tailPrefab;

    public UnityEvent CreateTail;

    public int GetTailSize()
    {
        return tails.Count;
    }

    void Start()
    {
        tails = new List<GameObject>();

        CreateTail.AddListener(Grow);

        /*speedText = GameObject.Find("SpeedText").GetComponent<Text>();
        tailText = GameObject.Find("TailText").GetComponent<Text>();
        fruitManager = GameObject.Find("FruitManager").GetComponent<FruitManager>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();*/

        time = 0f;
        timeDelay = 1f;
        speed = 1f;

        tails.Add(gameObject);
    }

    private void UpdateDirectionFromInput()
    {
        if (Input.GetKeyDown(keyMapping.moveUp))
        {
            moveDirection = Direction.Up;
        }
        else if (Input.GetKeyDown(keyMapping.moveDown))
        {
            moveDirection = Direction.Down;
        }
        else if (Input.GetKeyDown(keyMapping.moveRight))
        {
            moveDirection = Direction.Right;
        }
        else if (Input.GetKeyDown(keyMapping.moveLeft))
        {
            moveDirection = Direction.Left;
        }
    }

    private Quaternion GetRotation(Direction direction)
    {
        switch (moveDirection)
        {
            case Direction.Up:
                return Quaternion.AngleAxis(0, Vector3.up);
            case Direction.Down:
                return Quaternion.AngleAxis(180, Vector3.up);
            case Direction.Right:
                return Quaternion.AngleAxis(90, Vector3.up);
            case Direction.Left:
                return Quaternion.AngleAxis(-90, Vector3.up);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private Vector3 GetDirectionVector(Direction direction)
    {
        switch (moveDirection)
        {
            case Direction.Up:
                return Vector3.forward;
            case Direction.Down:
                return Vector3.back;
            case Direction.Right:
                return Vector3.right;
            case Direction.Left:
                return Vector3.left;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void DoMovement()
    {
        var tailPositions = new Vector3[tails.Count];
        for (var i = 0; i < tails.Count - 1; i++)
        {
            tailPositions[i] = tails[i].transform.position;
        }

        var tailRotations = new Quaternion[tails.Count];
        for (var i = 0; i < tails.Count - 1; i++)
        {
            tailRotations[i] = tails[i].transform.rotation;
        }
        
        var nextRotation = GetNextRotation();
        var nextPosition = GetNextPosition();
        
        // Handle continuous movement.
        var position = new Vector3Int(
            Mathf.RoundToInt(nextPosition.x),
            Mathf.RoundToInt(nextPosition.y),
            Mathf.RoundToInt(nextPosition.z));

        if (position.z > 0 && moveDirection == Direction.Up)
        {
            nextPosition.z = -(tileCreater.column - 1);
        }
        else if (position.z < -(tileCreater.column - 1) && moveDirection == Direction.Down)
        {
            nextPosition.z = 0;
        }
        else if (position.x > tileCreater.row - 1 && moveDirection == Direction.Right)
        {
            nextPosition.x = 0;
        }
        else if ((position.x < 0) && moveDirection == Direction.Left)
        {
            nextPosition.x = tileCreater.row - 1;
        }

        transform.SetPositionAndRotation(nextPosition, nextRotation);

        if (tails.Count >= 2)
        {
            for (var i = 0; i < tails.Count - 1; i++)
            {
                tails[i + 1].transform.SetPositionAndRotation(tailPositions[i], tailRotations[i]);
            }
        }
    }

    private void Update()
    {
        UpdateDirectionFromInput();
        
        time += speed * Time.deltaTime;
        
        if (time >= timeDelay)
        {
            time = 0;
            DoMovement();
        }

        //UI
        speedText.text = speed.ToString();
        tailText.text = GetTailSize().ToString();
    }
    
    public void Grow()
    {
        tails.Add((GameObject)Instantiate(tailPrefab, tails.Last().transform.position,
            tails.Last().transform.rotation));

        speed += 0.1f;
        soundManager.PlayEatSound();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameOverMenu.gameOver = true;
        }
    }

    private Vector3 GetNextPosition()
    {
        return transform.position + GetDirectionVector(moveDirection);
    }

    private Quaternion GetNextRotation()
    {
        return GetRotation(moveDirection);
    }
    
    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        var nextRotation = GetNextRotation();
        var nextPosition = GetNextPosition();
        
        UnityEditor.Handles.color = UnityEditor.Handles.zAxisColor;
        UnityEditor.Handles.ArrowHandleCap(
            0,
            nextPosition,
            nextRotation * Quaternion.LookRotation(Vector3.forward),
            1f,
            EventType.Repaint
        );
#endif
    }
}