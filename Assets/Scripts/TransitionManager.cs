using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    public Image BG;
    public GameObject centerTextObj, RespawnBtnObj;
    public Text centerText;
    Color currentColor;
    // Start is called before the first frame update
    void Start()
    {
        currentColor = BG.color;
    }

    #region Player lost all HP

    public void CloseTheCurtain()
    {
        currentColor.a += 0.01f;
        BG.color = currentColor;
        if (BG.color.a >= 0.6f)
        {
            centerTextObj.SetActive(true); // Turn on Text
        }
        if (BG.color.a < 1f)
        {
            Invoke("CloseTheCurtain", 0.01f);
        }
        else
        {
            RespawnBtnObj.SetActive(true); // Turn on Respawn Button
            Invoke("SetPlayerToSpawnPoint", 0.1f);
            Time.timeScale = 0;
        }
    }

    void SetPlayerToSpawnPoint()
    {
        FindObjectOfType<GameManager>().SetPlayerToSpawnPoint();
    }

    public void OpenTheCurtain()
    {
        currentColor.a -= 0.01f;
        BG.color = currentColor;

        if (RespawnBtnObj.activeSelf || centerTextObj.activeSelf)
        {
            RespawnBtnObj.SetActive(false);
            centerTextObj.SetActive(false);
        }
        
        if (BG.color.a > 0f)
        {
            Invoke("OpenTheCurtain", 0.02f);
        }
        else
        {
            FindObjectOfType<GameManager>().SetActiveInGameUICanvas(true);
            FindObjectOfType<GameManager>().SetActiveTransitionCanvas(false);
        }
    }
    #endregion

    #region MainHosue transition

    public void CloseTheCurtainForEnterTheMainHouse()
    {
        FindObjectOfType<GameManager>().EnteringMainHouse = true;
        FindObjectOfType<GameManager>().LockJoy = true;
        FindObjectOfType<GameManager>().SetActiveInGameUICanvas(false);
        FindObjectOfType<GameManager>().SetActiveTransitionCanvas(true);
        currentColor.a += 0.02f;
        BG.color = currentColor;
        if (BG.color.a < 1f)
        {
            Invoke("CloseTheCurtainForEnterTheMainHouse", 0.01f);
        }
        else
        {
            Invoke("SetPlayerToMainHouseEnterPoint", 0.1f);
            Invoke("OpenTheCurtainForTheMainHouse", 0.5f);
        }
    }
    public void CloseTheCurtainForExitTheMainHouse()
    {
        FindObjectOfType<GameManager>().SetActiveInGameUICanvas(false);
        FindObjectOfType<GameManager>().SetActiveTransitionCanvas(true);
        FindObjectOfType<GameManager>().ExitingMainHouse = true;
        currentColor.a += 0.02f;
        BG.color = currentColor;
        if (BG.color.a < 1f)
        {
            Invoke("CloseTheCurtainForExitTheMainHouse", 0.01f);
        }
        else
        {
            Invoke("SetPlayerToFrontMainHousePoint", 0.1f);
            Invoke("OpenTheCurtainForTheMainHouse", 0.5f);
        }
    }
    public void OpenTheCurtainForTheMainHouse()
    {
        currentColor.a -= 0.02f;
        BG.color = currentColor;
        if (BG.color.a > 0f)
        {
            Invoke("OpenTheCurtainForTheMainHouse", 0.02f);
        }
        else
        {
            Invoke("StopEnteringMainHouse", 0.3f);
            FindObjectOfType<GameManager>().SetActiveInGameUICanvas(true);
            FindObjectOfType<GameManager>().SetActiveTransitionCanvas(false);
            FindObjectOfType<GameManager>().SetActiveActionSwitchBtn(false);
        }
    }
    void StopEnteringMainHouse()
    {
        FindObjectOfType<GameManager>().AllowToExitFromMainHouse = true;
        FindObjectOfType<GameManager>().EnteringMainHouse = false;
    }

    public void SetPlayerToMainHouseEnterPoint()
    {
        FindObjectOfType<GameManager>().SetPlayerToMainHouseEnterPoint();
    }
    public void SetPlayerToFrontMainHousePoint()
    {
        FindObjectOfType<GameManager>().SetPlayerToFrontMainHousePoint();
    }

    #endregion

}
