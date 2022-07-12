using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum MapSize
{
    MINIMUM,
    MEDIUM,
    LARGE,
    HIGH
}
public class MapSizeValue
{
    public const int minimum = 8;
    public const int medium = 16;
    public const int large = 20;
    public const int high = 24;
}
public class TileCreater : MonoBehaviour
{
    [FormerlySerializedAs("Map Size")] public MapSize mapSize;
    //z yukarı x saga
    public int row;
    public int column;

    public GameObject camera;
    public GameObject block;
    public GameObject frame;
    private GameObject _block_row; //return last row block
    private GameObject _block_column; //return last column block
    void Start()
    {
        block = GameObject.Find("Block");
        #region ChangeSize
        switch (mapSize)
        {
            case MapSize.MINIMUM:
                row = MapSizeValue.minimum;
                column = MapSizeValue.minimum;
                break;
            case MapSize.MEDIUM:
                row = MapSizeValue.medium;
                column = MapSizeValue.medium;
                break;
            case MapSize.LARGE:
                row = MapSizeValue.large;
                column = MapSizeValue.large;
                break;
            case MapSize.HIGH:
                row = MapSizeValue.high;
                column = MapSizeValue.high;
                break;
            default:
                Debug.Log("Incorrect size.");
                break;
        }
        #endregion
        for (int i = 0; i < row; i++)
        {
            _block_row = Instantiate(block, new Vector3(0, 0, 0), block.transform.rotation);
            _block_row.transform.position = Vector3.right * i;
            for (int j = 0; j < column; j++)
            {
                _block_column = Instantiate(block, new Vector3(0, 0,0), block.transform.rotation);
                _block_column.transform.position = new Vector3(i, 0,(-1 * j));
            }
        }
        float cameraPosX = column / 2f + 0.5f;
        float cameraPosZ = row / 2f + -0.5f;
        camera.transform.position = new Vector3(cameraPosX, 12, -cameraPosZ);
        camera.GetComponent<Camera>().orthographicSize = column/2f + 1f;
    }
}
