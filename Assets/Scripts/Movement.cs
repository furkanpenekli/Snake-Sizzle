using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public TileCreater tileCreater;
    private SnakeInput snakeInput; 
    private Snake snake;
    private float time;
    private float timeDelay;
    void Start()
    {
        timeDelay = 1f;
        time = 0;
        
        speed = 1f;
        snake = GetComponent<Snake>();
        snakeInput = GetComponent<SnakeInput>();
    }
    private const float MinSpeed = 1;
    private const float MaxSpeed = 10;
    
    private float _speed = 1;
    public float speed
    {
        get { return _speed; }
        set { _speed = Mathf.Clamp(value, MinSpeed, MaxSpeed); }
    }
    public enum Direction
    {
        Up,
        Down,
        Right,
        Left
    }
    private Direction _direction;
    public Direction moveDirection
    {
        get { return _direction; }
        set { _direction = value; }
    }
    private Quaternion GetRotation(Direction direction)
    {
        switch (direction)
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
        switch (direction)
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
    public void DoMovement()
    {
        var tailPositions = new Vector3[snake.tails.Count];
        for (var i = 0; i < snake.tails.Count - 1; i++)
        {
            tailPositions[i] = snake.tails[i].transform.position;
        }

        var tailRotations = new Quaternion[snake.tails.Count];
        for (var i = 0; i < snake.tails.Count - 1; i++)
        {
            tailRotations[i] = snake.tails[i].transform.rotation;
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

        if (snake.tails.Count >= 2)
        {
            for (var i = 0; i < snake.tails.Count - 1; i++)
            {
                snake.tails[i + 1].transform.SetPositionAndRotation(tailPositions[i], tailRotations[i]);
            }
        }
    }
    private void Turn()
    {
        snakeInput.UpdateDirectionFromInput();
        time += speed * Time.deltaTime;
        if (time >= timeDelay)
        {
            time = 0;
            DoMovement();
        }
    }
    private void Update()
    {
        Turn();
    }

    private Vector3 GetNextPosition()
    {
        return transform.position + GetDirectionVector(moveDirection);
    }
    private Quaternion GetNextRotation()
    {
        return GetRotation(moveDirection);
    }
}
