using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class transparenter : MonoBehaviour
{
    public SpriteRenderer obj; 
    string[] blacklistTag = { "resource", "enemyDetector", "Harvester" };
    List<string> blacklistTagList = new List<string>();
    //sortingPlayer sorter;
    private void Start()
    {
        blacklistTagList.AddRange(blacklistTag);
        //var foundCanvasObjects = FindObjectsOfType<sortingPlayer>();
        //sorter = foundCanvasObjects.Where(o => o.tag == "Player").FirstOrDefault();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.transform.parent.gameObject.tag != "stump" && !blacklistTagList.Contains(collision.tag))
        {
            //sorter.RegisterGameObjects(gameObject);
            var tempColor = obj.color;
            tempColor.a = 0.5f;
            obj.color = tempColor;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (gameObject.transform.parent.gameObject.tag != "stump" && !blacklistTagList.Contains(collision.tag))
        {
            //sorter.RegisterGameObjects(gameObject);
            var tempColor = obj.color;
            tempColor.a = 0.5f;
            obj.color = tempColor;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.transform.parent.gameObject.tag != "stump" && !blacklistTagList.Contains(collision.tag))
        {
            ReleaseGameObject();
            var tempColor = obj.color;
            tempColor.a = 1f;
            obj.color = tempColor;
        }

    }
    public void turnOffCollider()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }
    public void ReleaseGameObject()
    {
        //sorter.ReleaseGameObjects(gameObject);
    }
}
