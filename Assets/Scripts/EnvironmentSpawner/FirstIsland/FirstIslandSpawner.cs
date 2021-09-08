using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class FirstIslandSpawner : MonoBehaviour
{
    Grid grid;
    public GameObject tree, tree2, stump, stone1, stone2, bush, berryBush;
    public BoxCollider2D SpawnArea;
    List<Vector2> registeredPoint = new List<Vector2>();
    List<string> environmentList = new List<string>() { "tree", "stump", "stone1", "stone2", "bush", "berryBush" };
    public List<spawnedItem> spawnedItems = new List<spawnedItem>();
    public int maxEnvironmentOnIsland = 8;
    public float stepRandomNumberSize = 2f;
    List<int> availableChoices = new List<int>() { (int)evmType.tree, (int)evmType.stone1, (int)evmType.stone2, (int)evmType.bush, (int)evmType.berryBush };
    Vector3[] corner;
    public int areaCode;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("F1");
        GameManager.islandID = areaCode;
    }
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -100000);
        grid = Grid.FindObjectOfType<Grid>();
        corner = getBoxCollider2DCornerPosition(SpawnArea);
        for (int i = 0; i < maxEnvironmentOnIsland; i++)
        {
            var ranEvm = availableChoices[Random.Range(0, availableChoices.Count)];
            spawnedItem spawnTemp = new spawnedItem();
            var item = environmentList[ranEvm];
            var ranX = Random.Range(corner[0].x, corner[1].x);
            float floorX = Mathf.Floor(ranX / stepRandomNumberSize);
            float snapX = floorX * stepRandomNumberSize;

            var ranY = Random.Range(corner[0].y, corner[2].y);
            float floorY = Mathf.Floor(ranY / stepRandomNumberSize);
            float snapY = floorY * stepRandomNumberSize;
            Vector2 pos = new Vector2(snapX, snapY);

            // Snap to Grid
            Vector3Int gridPos = grid.LocalToCell(pos);
            Vector2 finalGridPos = grid.GetCellCenterLocal(gridPos);

            var checkArea = registeredPoint.Any(s => s == finalGridPos);
            if (!checkArea)
            {
                spawnTemp.itemName = item;
                spawnTemp.position = finalGridPos;
                registeredPoint.Add(finalGridPos);
                spawnedItems.Add(spawnTemp);
            }
            else
            {
                i--;
            }

            if (i == (maxEnvironmentOnIsland - 1))
            {
                spawning();
            }
        }

    }

    public async void spawning()
    {
        foreach (var item in spawnedItems)
        {
            var ind = spawnedItems.IndexOf(item);
            await environmentSpawn(item.itemName, ind, 1, item.position);
            if (ind == spawnedItems.Count - 1)
            {
                //SpawnArea.transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                //SpawnArea.transform.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
    public async Task environmentSpawn(string itemName, int index, int amount, Vector2 spawnPoint)
    {
        await Task.Run(() => { }); // มีไว้ไม่ให้มันขึ้นเตือน ไม่มีใช้ await ด้านใน

        switch (itemName)
        {
            case "tree":
                var randNumb = Random.Range(0, 2);
                if (randNumb == 0)
                {
                    tree.GetComponent<Harvesting>().itemIndex = index;
                    tree.GetComponent<Harvesting>().areaCode = areaCode;
                    Instantiate(tree, spawnPoint, Quaternion.identity);
                }
                else
                {
                    tree2.GetComponent<Harvesting>().itemIndex = index;
                    tree2.GetComponent<Harvesting>().areaCode = areaCode;
                    Instantiate(tree2, spawnPoint, Quaternion.identity);
                }
                break;
            case "stump":
                stump.GetComponent<Harvesting>().itemIndex = index;
                stump.GetComponent<Harvesting>().areaCode = areaCode;
                Instantiate(stump, spawnPoint, Quaternion.identity);
                break;
            case "stone1":
                stone1.GetComponent<Harvesting>().itemIndex = index;
                stone1.GetComponent<Harvesting>().areaCode = areaCode;
                Instantiate(stone1, spawnPoint, Quaternion.identity);
                break;
            case "stone2":
                stone2.GetComponent<Harvesting>().itemIndex = index;
                stone2.GetComponent<Harvesting>().areaCode = areaCode;
                Instantiate(stone2, spawnPoint, Quaternion.identity);
                break;
            case "bush":
                bush.GetComponent<Harvesting>().itemIndex = index;
                bush.GetComponent<Harvesting>().areaCode = areaCode;
                Instantiate(bush, spawnPoint, Quaternion.identity);
                break;
            case "berryBush":
                berryBush.GetComponent<Harvesting>().itemIndex = index;
                berryBush.GetComponent<Harvesting>().areaCode = areaCode;
                Instantiate(berryBush, spawnPoint, Quaternion.identity);
                break;
            default:
                break;
        }
    }

    public void SpawnDestroyedItemInvoke()
    {
        Invoke("SpawnDestroyedItem", 3f);
    }
    public async void SpawnDestroyedItem()
    {
        var ranEvm = availableChoices[Random.Range(0, availableChoices.Count)];
        spawnedItem spawnTemp = new spawnedItem();
        var item = environmentList[ranEvm];
        var ranX = Random.Range(corner[0].x, corner[1].x);
        float floorX = Mathf.Floor(ranX / stepRandomNumberSize);
        float snapX = floorX * stepRandomNumberSize;

        var ranY = Random.Range(corner[0].y, corner[2].y);
        float floorY = Mathf.Floor(ranY / stepRandomNumberSize);
        float snapY = floorY * stepRandomNumberSize;
        Vector2 pos = new Vector2(snapX, snapY);

        // Snap to Grid
        Vector3Int gridPos = grid.LocalToCell(pos);
        Vector2 finalGridPos = grid.GetCellCenterLocal(gridPos);

        var checkArea = registeredPoint.Any(s => s == finalGridPos);
        if (!checkArea)
        {
            spawnTemp.itemName = item;
            spawnTemp.position = pos;
            registeredPoint.Add(finalGridPos);
            spawnedItems.Add(spawnTemp);

            await spawnNewEnvironment(spawnTemp.itemName, spawnedItems.Count - 1, 1, spawnTemp.position);
        }
        else
        {
            SpawnDestroyedItem();
        }
    }
    public async Task spawnNewEnvironment(string itemName, int index, int amount, Vector2 spawnPoint)
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
                    tree.GetComponent<Harvesting>().itemIndex = index;
                    Instantiate(tree, spawnPoint, Quaternion.identity);
                }
                else
                {
                    tree2.GetComponent<Harvesting>().itemIndex = index;
                    Instantiate(tree2, spawnPoint, Quaternion.identity);
                }
                break;
            case "stump":
                stump.GetComponent<Harvesting>().itemIndex = index;
                Instantiate(stump, spawnPoint, Quaternion.identity);
                break;
            case "stone1":
                stone1.GetComponent<Harvesting>().itemIndex = index;
                Instantiate(stone1, spawnPoint, Quaternion.identity);
                break;
            case "stone2":
                stone2.GetComponent<Harvesting>().itemIndex = index;
                Instantiate(stone2, spawnPoint, Quaternion.identity);
                break;
            case "bush":
                bush.GetComponent<Harvesting>().itemIndex = index;
                Instantiate(bush, spawnPoint, Quaternion.identity);
                break;
            case "berryBush":
                berryBush.GetComponent<Harvesting>().itemIndex = index;
                Instantiate(berryBush, spawnPoint, Quaternion.identity);
                break;
            default:
                break;
        }
    }


    Vector3[] getBoxCollider2DCornerPosition(BoxCollider2D box)
    {
        float areaMargin = 0.5f; // Keep in mind if you assign too high number it could crashed your game
        var top = box.bounds.center.y + (box.size.y / 2f);
        var left = box.bounds.center.x - (box.size.x / 2f);
        var right = box.bounds.center.x + (box.size.x / 2f);
        var bottom = box.bounds.center.y - (box.size.y / 2f);
        var topLeft = new Vector3(left + areaMargin, top - areaMargin);
        var bottomLeft = new Vector3(left + areaMargin, bottom + areaMargin);
        var topRight = new Vector3(right - areaMargin, top - areaMargin);
        var bottomRight = new Vector3(right - areaMargin, bottom + areaMargin);
        Vector3[] arry = { topLeft, topRight, bottomLeft, bottomRight };
        return arry;
    }
}
