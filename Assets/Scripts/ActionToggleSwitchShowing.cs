using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionToggleSwitchShowing : MonoBehaviour
{
    public GameObject HarvestMode; // 1
    public GameObject AttackMode; // 2
    void Update()
    {
        var actionMode = FindObjectOfType<GameManager>().playerMovementBridge.actionMode;
        if (actionMode == 1)
        {
            HarvestMode.SetActive(true);
            AttackMode.SetActive(false);
        }
        else if (actionMode == 2)
        {
            AttackMode.SetActive(true);
            HarvestMode.SetActive(false);
        }
    }
}
