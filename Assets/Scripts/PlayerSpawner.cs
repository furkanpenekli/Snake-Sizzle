using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerHead;
    void Start()
    {
        Instantiate(playerHead, new Vector3(0,1,0),playerHead.transform.rotation);
    }
}
