using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BlockLogic : MonoBehaviour
{
    GameLogic gameLogic;
    bool moveable = true;
    float timer = 0f;
    public GameObject rig;

    // Start is called before the first frame update
    void Start()
    {
        gameLogic = FindAnyObjectByType<GameLogic>();
    }

    void RegisterBlock()
    {
        foreach (Transform subBlock in rig.transform)
        {
            gameLogic.grid[Mathf.FloorToInt(subBlock.position.x), Mathf.FloorToInt(subBlock.position.y)] = subBlock;
        }
    }

    bool CheckValid()
    {
        foreach (Transform subBlock in rig.transform)
        {
            
            if(subBlock.transform.position.x >= GameLogic.width || subBlock.transform.position.x < 0 || subBlock.transform.position.y < 0)
            {
                return false;
            }
            if (subBlock.position.y < GameLogic.height && gameLogic.grid[Mathf.FloorToInt(subBlock.position.x), Mathf.FloorToInt(subBlock.position.y)] != null)
            {
                return false;
            }
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveable)
        {

            //Update the timer
            timer += 1 * Time.deltaTime;

            //Manual Drop
            if (Input.GetKey(KeyCode.DownArrow) && timer > GameLogic.quickDropTime)
            {
                gameObject.transform.position -= new Vector3(0, 1, 0);
                timer = 0f;
                if (!CheckValid() )
                {
                    moveable = false;
                    gameObject.transform.position += new Vector3(0, 1, 0);
                    RegisterBlock();
                    gameLogic.SpawnBlock();
                }
            }
            else if (timer > GameLogic.dropTime) //Autodrop
            {
                gameObject.transform.position -= new Vector3(0, 1, 0);
                timer = 0f;
                if (!CheckValid())
                {
                    moveable = false;
                    gameObject.transform.position += new Vector3(0, 1, 0);
                    RegisterBlock();
                    gameLogic.SpawnBlock();
                }
            }

            //Movement Left and Right
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                gameObject.transform.position -= new Vector3(1, 0, 0);
                if (!CheckValid())
                {
                    gameObject.transform.position += new Vector3(1, 0, 0);
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                gameObject.transform.position += new Vector3(1, 0, 0);
                if (!CheckValid())
                {
                    gameObject.transform.position -= new Vector3(1, 0, 0);
                }
            }

            //Rotation
            if(Input.GetKeyDown(KeyCode.Space))
            {
                rig.transform.eulerAngles -= new Vector3(0, 0, 90);
                if (!CheckValid())
                {
                    rig.transform.eulerAngles += new Vector3(0, 0, 90);
                }
            }
        }
    }
}
