using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public static float dropTime = 0.9f;
    public static float quickDropTime = 0.05f;
    public static int width = 15, height = 30;
    public GameObject[] blocks;
    public Transform[,] grid = new Transform[width, height];

    
    void Start()
    {
        SpawnBlock();
    }


    public void SpawnBlock()
    {
        float guess = Random.Range(0, 1f);
        guess *= blocks.Length;
        Instantiate(blocks[Mathf.FloorToInt(guess)]);
    }
}
