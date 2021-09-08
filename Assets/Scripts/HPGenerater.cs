using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPGenerater : MonoBehaviour
{
    public GameObject HPGameObj;
    bool firstGenerate = true;
    void Start()
    {
        //for (int i = 0; i < FindObjectOfType<GameManager>().player.hpConstant;i++)
        //{
        //    var gameObjTemp = Instantiate(HPGameObj, gameObject.transform, false).gameObject;
        //    gameObjTemp.GetComponent<HPPresenter>().HPNumber = i+1;
        //}
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.childCount < FindObjectOfType<GameManager>().player.hpConstant && !IsInvoking("GenerateHP"))
        {
            Invoke("GenerateHP", 0f);
        }
    }

    void GenerateHP()
    {
        if (firstGenerate)
        {
            firstGenerate = false;
            for (int i = 0; i < FindObjectOfType<GameManager>().player.hpConstant; i++)
            {
                var gameObjTemp = Instantiate(HPGameObj, gameObject.transform, false).gameObject;
                gameObjTemp.GetComponent<HPPresenter>().HPNumber = i + 1;
            }
        }
        else
        {
            var gameObjTemp = Instantiate(HPGameObj, gameObject.transform, false).gameObject;
            gameObjTemp.GetComponent<HPPresenter>().HPNumber = gameObject.transform.childCount;
        }
        
    }
}
