using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    public GameObject tree, tree2, stump, stone1, stone2, bush, berryBush;
    public BoxCollider2D FirstIsland_SW;
    List<string> environmentList = new List<string>();
    List<spawnedItem> spawnedItems = new List<spawnedItem>();
    float minX = -13f, maxX = 21f, minY = -10f, maxY = 0f;
    int maxEnvironmentOnIsland = 20;
    int maxTreeSpawn = 9, maxStumpSpawn = 0, maxStone1Spawn = 4, maxStone2Spawn = 2, maxBushSpawn = 3, maxBerryBushSpawn = 2;
    int treeSpawned = 0, stumpSpawned = 0, stone1Spawned = 0, stone2Spawned = 0, bushSpawned = 0, berryBushSpawned = 0;
    float stepRandomNumberSize = 2f;
    bool allowSpawn = false, allowRegisterItem = true;
    List<int> availableChoices;
    void Start()
    {
        spawnedItems = new List<spawnedItem>();
        /* 
           ***Note***
           validChoices need to add just item that have max spawn number more than 0
         
         */
        availableChoices = new List<int>() { (int)evmType.tree, (int)evmType.stone1, (int)evmType.stone2, (int)evmType.bush, (int)evmType.berryBush };
        adjustMaxSpawnItem(availableChoices);
        environmentList.Add("tree");
        environmentList.Add("stump");
        environmentList.Add("stone1");
        environmentList.Add("stone2");
        environmentList.Add("bush");
        environmentList.Add("berryBush");
        allowSpawn = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (allowSpawn)
        {
            if (allowRegisterItem && spawnedItems.Count < maxEnvironmentOnIsland)
            {
                
                var ranEvm = availableChoices[Random.Range(0, availableChoices.Count)];
                spawnedItem spawnTemp = new spawnedItem();
                var item = environmentList[ranEvm];
                if (maxTreeSpawn > treeSpawned || maxStumpSpawn > stumpSpawned || maxStone1Spawn > stone1Spawned 
                    || maxStone2Spawn > stone2Spawned || maxBushSpawn > bushSpawned || maxBerryBushSpawn > berryBushSpawned)
                {
                    var corner = getBoxCollider2DCornerPosition(FirstIsland_SW);
                    var ranX = Random.Range(corner[0].x, corner[1].x);
                    float floorX = Mathf.Floor(ranX / stepRandomNumberSize);
                    float snapX = floorX * stepRandomNumberSize;

                    var ranY = Random.Range(corner[0].y, corner[2].y);
                    float floorY = Mathf.Floor(ranY / stepRandomNumberSize);
                    float snapY = floorY * stepRandomNumberSize;
                    Vector2 pos = new Vector2(snapX, snapY);
                    var checkArea = spawnedItems.Any(s => s.position == pos);
                    if (!checkArea)
                    {
                        spawnTemp.itemName = item;
                        spawnTemp.position = pos;
                        spawnedItems.Add(spawnTemp);
                        switch (item)
                        {
                            case "tree":
                                treeSpawned += 1;
                                break;
                            case "stump":
                                stumpSpawned += 1;
                                break;
                            case "stone1":
                                stone1Spawned += 1;
                                break;
                            case "stone2":
                                stone2Spawned += 1;
                                break;
                            case "bush":
                                bushSpawned += 1;
                                break;
                            case "berryBush":
                                berryBushSpawned += 1;
                                break;
                            default:
                                break;

                        }
                    }

                }
                else
                {
                    allowRegisterItem = false;
                }

            }
            else
            {
                allowSpawn = false;
                spawning();
            }

        }

    }

    Vector3[] getBoxCollider2DCornerPosition(BoxCollider2D box)
    {
        
        var top = box.bounds.center.y + (box.size.y / 2f);
        var left = box.bounds.center.x - (box.size.x / 2f);
        var right = box.bounds.center.x + (box.size.x / 2f);
        var bottom = box.bounds.center.y - (box.size.y / 2f);
        var topLeft = new Vector3(left, top);
        var bottomLeft = new Vector3(left, bottom);
        var topRight = new Vector3(right, top);
        var bottomRight = new Vector3(right, bottom);
        Vector3[] arry = { topLeft, topRight, bottomLeft, bottomRight };
        return arry;
    }
    public async void spawning()
    {
        foreach (var item in spawnedItems)
        {
            await environmentSpawn(item.itemName, 1, item.position);
        }

        FirstIsland_SW.transform.gameObject.SetActive(false);
    }
    public void adjustMaxSpawnItem(List<int> choices)
    {
        if (!choices.Any(s => s == (int)evmType.tree))
        {
            maxTreeSpawn = 0;
        }
        //if (!choices.Any(s => s == (int)evmType.tree2))
        //{
        //    maxTreeSpawn = 0;
        //}
        if (!choices.Any(s => s == (int)evmType.stump))
        {
            maxStumpSpawn = 0;
        }
        if (!choices.Any(s => s == (int)evmType.stone1))
        {
            maxStone1Spawn = 0;
        }
        if (!choices.Any(s => s == (int)evmType.stone2))
        {
            maxStone2Spawn = 0;
        }
        if (!choices.Any(s => s == (int)evmType.bush))
        {
            maxBushSpawn = 0;
        }
        if (!choices.Any(s => s == (int)evmType.berryBush))
        {
            maxBerryBushSpawn = 0;
        }
    }
    public bool checkRegisteredItem(Vector2 vector)
    {
        return spawnedItems.Any(s => s.position == vector);
    }
    //int GetRandomHealth()
    //{
    //    int randomHealth = Random.Range(minSpawnHealth, maxSpawnHealth);
    //    int numSteps = Mathf.Floor(randomHealth / stepSize);
    //    int adjustedHealth = numSteps * stepSize;

    //    return adjustedHealth;
    //}
    public Vector2 randomVector2D()
    {
        var ranX = Random.Range(minX, maxX);
        float floorX = Mathf.Floor(ranX / stepRandomNumberSize);
        float snapX = floorX * stepRandomNumberSize;

        var ranY = Random.Range(minY, maxY);
        float floorY = Mathf.Floor(ranY / stepRandomNumberSize);
        float snapY = floorY * stepRandomNumberSize;
        //var snapX = snapValuetoThePoint(ranX);
        //var snapY = snapValuetoThePoint(ranY);
        Vector2 v2 = new Vector2(snapX, snapY);
        return v2;
    }
    public float snapValuetoThePoint(float value)
    {
        // 0.0 - 0.2 = Floor
        // 0.3 - 0.6 = 0.5
        // 0.7 - 0.9 = +1
        float returnValue;
        var mod = value % 1;
        if (mod < 0.3f)
        {
            returnValue = Mathf.Floor(value);
        }
        else if (0.6f < mod)
        {
            returnValue = (Mathf.Floor(value) + 1f);
        }
        else
        {
            returnValue = (Mathf.Floor(value) + 0.5f);
        }
        return returnValue;
    }
    public void checkAvailibilityArea(Vector2 area, string environmentName)
    {
        switch (environmentName)
        {
            case "tree":

                break;
            case "stump":

                break;
            case "stone1":

                break;
            case "stone2":

                break;
            case "bush":

                break;
            case "berryBush":

                break;
            default:
                break;
        }
    }
    public async Task environmentSpawn(string itemName, int amount, Vector2 spawnPoint)
    {
        await Task.Run(() =>
        {

        });

        switch (itemName)
        {
            case "tree":
                var randNumb = Random.Range(0, 2);
                if (randNumb == 0)
                {
                    Instantiate(tree, spawnPoint, Quaternion.identity);
                }
                else
                {

                    Instantiate(tree2, spawnPoint, Quaternion.identity);
                }
                break;
            case "stump":
                Instantiate(stump, spawnPoint, Quaternion.identity);
                break;
            case "stone1":
                Instantiate(stone1, spawnPoint, Quaternion.identity);
                break;
            case "stone2":
                Instantiate(stone2, spawnPoint, Quaternion.identity);
                break;
            case "bush":
                Instantiate(bush, spawnPoint, Quaternion.identity);
                break;
            case "berryBush":
                Instantiate(berryBush, spawnPoint, Quaternion.identity);
                break;
            default:
                break;
        }
    }


}
