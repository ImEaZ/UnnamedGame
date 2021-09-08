using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemSpawn : MonoBehaviour
{
    public GameObject stone, wood, smallWood, log;
    public void spawn(string itemName, int amount, Vector2 spawnPoint)
    {
        switch (itemName)
        {
            case "stone":
                for (int i = 0; i < amount; i++)
                {
                    Instantiate(stone, spawnPoint, Quaternion.identity);
                }
                break;
            case "smallWood":
                for (int i = 0; i < amount; i++)
                {
                    Instantiate(smallWood, spawnPoint, Quaternion.identity);
                }
                break;
            case "wood":
                for (int i = 0; i < amount; i++)
                {
                    Instantiate(wood, spawnPoint, Quaternion.identity);
                }
                break;
            case "log":
                for (int i = 0; i < amount; i++)
                {
                    Instantiate(log, spawnPoint, Quaternion.identity);
                }
                break;
            default:
                break;
        }

    }

}
