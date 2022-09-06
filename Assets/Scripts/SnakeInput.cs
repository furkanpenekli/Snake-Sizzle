using System;
using UnityEngine;
[RequireComponent(typeof(Movement))]
public class SnakeInput : MonoBehaviour
{
    [SerializeField]
    private KeyCode upKey = KeyCode.W;
    [SerializeField]
    private KeyCode downKey = KeyCode.S;
    [SerializeField]
    private KeyCode rightKey = KeyCode.D;
    [SerializeField]
    private KeyCode leftKey = KeyCode.A;
    
    private Movement movement;
    private void Start()
    {
        movement = GetComponent<Movement>();
    }

    public void UpdateDirectionFromInput()
    {
        if (Input.GetKeyDown(upKey))
        {
            movement.moveDirection = Movement.Direction.Up;
        }
        else if (Input.GetKeyDown(downKey))
        {
            movement.moveDirection = Movement.Direction.Down;
        }
        else if (Input.GetKeyDown(rightKey))
        {
            movement.moveDirection = Movement.Direction.Right;
        }
        else if (Input.GetKeyDown(leftKey))
        {
            movement.moveDirection = Movement.Direction.Left;
        }
    }
}