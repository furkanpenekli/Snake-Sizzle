using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public struct PlayerController
    {
        public string up;
        public string down;
        public string right;
        public string left;
    }

    public GameOverMenu gameOverMenu;
    public FruitManager fruitManager;
    public TileCreater tileCreater;
    public SoundManager soundManager;
    public Text speedText;
    public Text tailText;

    public float speed;
    public float time;
    public float timeDelay;
    string moveDir;
    private int j;
    private int a;
    private int i;
    private int tailSize;

    PlayerController playerController;
    [SerializeField] public int playerIndex;

    public List<GameObject> tails;
    public List<Vector3> tempPos;
    public List<Quaternion> tempRot;
    public GameObject tailPrefab;
    public GameObject direction;

    public UnityEvent CreateTail;

    void Start()
    {
        //setting up controls
        playerController.up = "up";
        playerController.down = "down";
        playerController.right = "right";
        playerController.left = "left";
        
        tails = new List<GameObject>();
        tempPos = new List<Vector3>();
        tempRot = new List<Quaternion>();

        CreateTail = new UnityEvent();
        CreateTail.AddListener(Grow);

        /*speedText = GameObject.Find("SpeedText").GetComponent<Text>();
        tailText = GameObject.Find("TailText").GetComponent<Text>();
        fruitManager = GameObject.Find("FruitManager").GetComponent<FruitManager>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();*/

        tailSize = 0;
        i = 0;
        j = 0;
        a = 0;
        time = 0f;
        timeDelay = 1f;
        speed = 1f;

        tails.Add(gameObject);
        tempPos.Add(gameObject.transform.position);
        tempRot.Add(gameObject.transform.rotation);
    }

    private void Update()
    {
        //ChangeMoveDirection
        if(playerIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                moveDir = playerController.up;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                moveDir = playerController.down;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                moveDir = playerController.right;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                moveDir = playerController.left;
            }
        }else if (playerIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                moveDir = playerController.up;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                moveDir = playerController.down;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                moveDir = playerController.right;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                moveDir = playerController.left;
            }
        }

        //Rotate
        switch (moveDir)
        {
            case "up":
                    MoveRotation(0, direction);
                break;
            case "down":
                    MoveRotation(180, direction);
                break;
            case "right":
                    MoveRotation(90, direction);
                break;
            case "left":
                    MoveRotation(-90, direction);
                break;
        }

        time += speed * Time.deltaTime;
        //Movement
        if (time >= timeDelay)
        {
            time = 0;

            for (j = 0; j < tails.Count - 1; j++)
            {
                tempPos[j] = tails[j].transform.position;
            }

            //tempRotation
            for (a = 0; a < tails.Count - 1; a++)
            {
                tempRot[a] = tails[a].transform.rotation;
            }

            direction.transform.Translate(Vector3.forward);

            //Continuous Movement
            var position = new Vector3Int(
                Mathf.RoundToInt(direction.transform.position.x),
                Mathf.RoundToInt(direction.transform.position.y),
                Mathf.RoundToInt(direction.transform.position.z));

            if ((position.z > 0) &&
                direction.transform.rotation == Quaternion.AngleAxis(0, Vector3.up)) //up
            {
                direction.transform.position =
                    new Vector3(direction.transform.position.x, 1, -(tileCreater.column - 1));
            }
            else if ((position.z < -(tileCreater.column - 1)) &&
                     direction.transform.rotation == Quaternion.AngleAxis(180, Vector3.up)) //down
            {
                direction.gameObject.transform.position = new Vector3(direction.transform.position.x, 1, 0);
            }
            else if (position.x > (tileCreater.row - 1) &&
                     direction.transform.rotation == Quaternion.AngleAxis(90, Vector3.up)) //right
            {
                direction.transform.position = new Vector3(0, 1, direction.transform.position.z);
            }
            else if ((position.x < 0) &&
                     direction.transform.rotation == Quaternion.AngleAxis(-90, Vector3.up)) //left
            {
                direction.transform.position =
                    new Vector3(tileCreater.row - 1, 1, direction.transform.position.z);
            }


            gameObject.transform.position = direction.transform.position;
            transform.rotation = direction.transform.rotation;
            //tempPosition

            if (tails.Count >= 2)
                for (i = 0; i < tails.Count - 1; i++)
                {
                    tails[i + 1].transform.position = tempPos[i];
                    tails[i + 1].transform.rotation = tempRot[i];
                }
        }

        //UI
        speedText.text = speed.ToString();
        tailText.text = tailSize.ToString();
    }

    private void FixedUpdate()
    {
    }

    void MoveRotation(float angle, GameObject dir)
    {
        dir.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }

    public void Grow()
    {
        tempPos.Add(tails.Last().transform.position);
        tempRot.Add(tails.Last().transform.rotation);
        tails.Add((GameObject)Instantiate(tailPrefab, tails.Last().transform.position,
            tails.Last().transform.rotation));

        speed += 0.1f;
        tailSize++;
        soundManager.PlayEatSound();
    }
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player")) 
        { 
            gameOverMenu.gameOver = true;
        }
    }
}