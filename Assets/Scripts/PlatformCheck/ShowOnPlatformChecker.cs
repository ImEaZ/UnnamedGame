using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnPlatformChecker : MonoBehaviour
{
    void Start()
    {
        var MobileTest = FindObjectOfType<GameManager>().MobileTest;
        if (Application.platform == RuntimePlatform.Android || MobileTest)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
