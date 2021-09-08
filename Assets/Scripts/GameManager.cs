using Assets.Scripts.Models;
using EZCameraShake;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static PlayerTemp;

public class GameManager : MonoBehaviour
{
    public string Language;
    public PlayerTemp player;
    public bool savePlayerData = false;
    public bool resetPlayerData = false;
    public bool scanAstarArea = false;
    public bool setLanguage = false;
    public Transform PlayerSpawnPoint;
    public Transform MainHouseEnterPoint;
    public Transform FrontMainHousePoint;
    public CameraFollow camFollow;

    // Island areas
    public GameObject FirstIsland_SW, FirstIsland_W1, FirstIsland_W2, FirstIsland_W3
        , FirstIsland_C1, FirstIsland_C2, FirstIsland_C3
        , FirstIsland_N1, FirstIsland_N2, FirstIsland_N3;

    public static int islandID;

    public AstarPath Astar;
    public Dialog dialog;
    public Camera mainCam;
    public IVExpSlider IVExpSlider;
    public IVExpText IVExpText;
    public LevelUpRippleAnim lvlUpAnim;
    public LevelUpAnimController lvlUpAnimController;
    public GameObject FloatingTextObj;
    public GameObject DialogCanvas, ControllerCanvas, InGameCanvas, TransitionCanvas, tabPanel, GetItemLogList, actionSwitchBtn, actionBtn, interactBtn, ivBtn, runBtn, rollBtn;
    public GameObject Busket, BusketPanel;
    public TransitionManager transitionMng;
    public Transform itemLogUI;
    public bool allowToOpenBusket = false;
    bool tabPanelUnlock = true, busketPanelUnlock = true;
    public PlayerMovement playerMovementBridge;
    public BinSectionScript binSection;
    public playerInventoryManager playerIVManager;
    public int multiplyAmt = 1;
    public Sprite stone, wood, hardWood, fiber, berry;
    public int IVSwapSourceIndex = -1, IVSwapTargetIndex = -1;
    public bool draggingItem = false, mouseOnAllDropBtn = false, mouseOnHalfDropBtn = false, showMoveToBinBtn = false;
    public List<PlayerInventoryListModel> binList = new List<PlayerInventoryListModel>();
    public List<PlayerInventoryListModel> busketItemTemp = new List<PlayerInventoryListModel>();
    public bool MobileTest = false, AllowMouseInput = true;
    public bool EnteringMainHouse = false, LockJoy = false, ExitingMainHouse = false, AllowToExitFromMainHouse = false;
    string interactObjective;
    int maxStackItem = 200;
    public int maxStackLevel;
    private void Start()
    {
        playerIVManager.setMaxStackValue(maxStackItem);
        IVSwapSourceIndex = -1;
        IVSwapTargetIndex = -1;
        player.LoadPlayer();
        SetActiveActionSwitchBtn(true);
    }
    private void Update()
    {
        if (savePlayerData) { savePlayerData = false; player.SavePlayer(); }
        if (resetPlayerData) { resetPlayerData = false; player.ResetPlayerData(); }

        if (scanAstarArea) { scanAstarArea = false; Astar.Scan(); }
        if (setLanguage) { setLanguage = false; dialog.SetDialogLanguage(); }
        if (player.level != MaxExpLevelRange.localLevel)
        {
            MaxExpLevelRange.localLevel = player.level;
            maxStackLevel = MaxExpLevelRange.GetMaxStackLevel();
            IVExpSlider.UpdateMaxStackLevel(maxStackLevel);
            IVExpText.UpdateMaxStackLevel(maxStackLevel);
        }
    }
    public void scanObstacleArea()
    {
        Astar.Scan();
    }
    public void saveGame()
    {
        savePlayerData = true;
    }
    public void PlayerCollectItem(string itemName)
    {
        string name = getItemName(itemName);
        switch (name)
        {
            case "stone":
                //player.stone += 1;
                addItemToList(name);
                break;
            case "wood":
                //player.wood += 1;
                addItemToList(name);
                break;
            case "woodHard":
                //player.hardWood += 1;
                addItemToList(name);
                break;
            case "fiber":
                //player.fiber += 1;
                addItemToList(name);
                break;
            case "berry":
                //player.fiber += 1;
                addItemToList(name);
                break;
        }
    }
    string getItemName(string itemName)
    {
        var replaceName = itemName.Replace("Item", ":");
        var splitName = replaceName.Split(':');
        return splitName[0].Replace("Small", "");
    }

    public void TabPanelToggle()
    {
        if (tabPanelUnlock)
        {
            tabPanelUnlock = false;
            if (tabPanel.activeSelf)
            {
                tabPanel.SetActive(false);
                if (Application.platform == RuntimePlatform.Android || MobileTest)
                {
                    actionBtn.SetActive(actionBtn.activeSelf ? true : actionBtn.activeSelf);
                    ivBtn.SetActive(true);
                    runBtn.SetActive(true);
                    rollBtn.SetActive(true);
                }

                Invoke("unlockTabPanelPress", 0.3f);
            }
            else
            {
                tabPanel.SetActive(true);
                actionBtn.SetActive(false);
                ivBtn.SetActive(false);
                runBtn.SetActive(false);
                rollBtn.SetActive(false);

                Invoke("unlockTabPanelPress", 0.3f);
            }
        }
    }
    void unlockTabPanelPress()
    {
        tabPanelUnlock = true;
    }
    GameObject getIndexChildrenFromName(GameObject parent, string name)
    {
        //int any = -1;
        var thisChild = parent.transform.Find(name);

        if (thisChild == null)
            return null;

        return thisChild.gameObject;
        //for (int i = 0; i < parent.transform.childCount; i++)
        //{
        //    if (parent.transform.GetChild(i).name == name)
        //    {
        //        any = i;
        //        break;
        //    }

        //}
        //return any;
    }
    string getNameFromItemName(string itemName)
    {
        // Name for itemLog
        switch (itemName)
        {
            case "stone":
                return "Stone";
            case "wood":
                return "Wood";
            case "woodHard":
                return "Hard wood";
            case "fiber":
                return "Fiber";
            case "berry":
                return "Berry";
            case "log":
                return "Log";
            case "vine":
                return "Vine";
            case "meat":
                return "Raw meat";
            default:
                return "NotFoundItem : " + itemName;
        }
    }

    void showGetItemLog(string name, int qty, Sprite image)
    {
        //var childInd = getIndexChildrenFromName(GetItemLogList, name);
        var childTemp = GetItemLogList.transform.Find(name);
        if (childTemp != null)
        {
            // Child index from itemLogUI prefab in Prefabs folder
            childTemp.gameObject.GetComponent<fadeOutItemLogUI>().resetFading();
            var oldText = childTemp.gameObject.transform.GetChild(3).gameObject.GetComponent<Text>().text;
            childTemp.gameObject.transform.GetChild(3).gameObject.GetComponent<Text>().text = (Convert.ToInt32(oldText) + qty).ToString();
            childTemp.gameObject.GetComponent<fadeOutItemLogUI>().alpha = 1f;
        }
        else
        {
            #region Create new item
            GameObject newObj = CreateItemLogGameObject();
            newObj.name = name;
            for (int i = 0; i < newObj.transform.childCount; i++)
            {
                switch (newObj.transform.GetChild(i).name)
                {
                    case "itemImage":
                        newObj.transform.GetChild(i).GetComponent<Image>().enabled = true;
                        newObj.transform.GetChild(i).GetComponent<Image>().sprite = image;
                        break;

                    case "itemName":
                        newObj.transform.GetChild(i).GetComponent<Text>().text = getNameFromItemName(name);
                        break;

                    case "itemQty":
                        newObj.transform.GetChild(i).GetComponent<Text>().text = qty.ToString();
                        break;
                }
            }
            #endregion
            newObj.transform.SetParent(GetItemLogList.transform);
            newObj.transform.SetSiblingIndex(0);
        }

    }

    public GameObject CreateItemLogGameObject()
    {
        return Instantiate(itemLogUI, GetItemLogList.transform, false).gameObject;
    }

    public void addItemIntoPlayer(string itemName, int? bushItemQty)
    {
        // AddItemIntoInventory
        if (bushItemQty == null)
        {
            itemName = itemName.Replace("Item(Clone)", "");
        }
        int addQty = (bushItemQty == null ? multiplyAmt : (int)bushItemQty);
        switch (itemName)
        {
            case "stone":
                player.stone += addQty;
                break;
            case "wood":
                player.wood += addQty;
                break;
            case "log":
                player.log += addQty;
                break;
            case "fiber":
                player.fiber += addQty;
                break;
            case "vine":
                player.vine += addQty;
                break;
            case "berry":
                player.berry += addQty;
                break;
            case "meat":
                player.meat += addQty;
                break;
        }
        var sprite = Resources.Load<Sprite>($"itemLog/{itemName}Item");
        showGetItemLog(itemName, addQty, sprite);
    }
    public void addItemToList(string itemName)
    {
        if (player.inventoryList.Any(s => s.itemName == itemName))
        {
            var item = player.inventoryList.Where(s => s.itemName == itemName).FirstOrDefault();
            var ind = player.inventoryList.IndexOf(item);
            player.inventoryList[ind].qty += multiplyAmt;
        }
        else
        {
            var newInd = findFreeIndex();
            if (newInd != -1)
            {
                PlayerInventoryListModel newItem = new PlayerInventoryListModel()
                {
                    itemName = itemName,
                    qty = multiplyAmt,
                    index = newInd
                };
                player.inventoryList.Add(newItem);
            }
        }
        var sprite = Resources.Load<Sprite>($"itemLog/{itemName}Item");
        showGetItemLog(itemName, multiplyAmt, sprite);
    }
    public void addItemFromBushToList(string itemName, int qty)
    {
        if (player.inventoryList.Any(s => s.itemName == itemName))
        {
            var item = player.inventoryList.Where(s => s.itemName == itemName).FirstOrDefault();
            var ind = player.inventoryList.IndexOf(item);
            player.inventoryList[ind].qty += (qty * multiplyAmt);
        }
        else
        {
            var newInd = findFreeIndex();
            if (newInd != -1)
            {
                PlayerInventoryListModel newItem = new PlayerInventoryListModel()
                {
                    itemName = itemName,
                    qty = (qty * multiplyAmt),
                    index = newInd
                };
                player.inventoryList.Add(newItem);
            }
        }
    }
    public void getItemsFromBush(string itemName)
    {
        if (itemName.Contains("bushWithBerry"))
        {
            itemName = "berry";
            var ranBerry = UnityEngine.Random.Range(1, 3);
            /* Old method 
             var sprite = Resources.Load<Sprite>($"itemLog/{itemName}Item");

             addItemFromBushToList(itemName, ranBerry);
             showGetItemLog(itemName, ranBerry * multiplyAmt, sprite);
             */
            addItemIntoPlayer(itemName, ranBerry * multiplyAmt);

            itemName = "wood";
            var ranWood = UnityEngine.Random.Range(0, 1);
            if (ranWood > 0)
            {
                /* Old method 
                var woodsprite = Resources.Load<Sprite>($"itemLog/{itemName}Item");
                addItemFromBushToList(itemName, ranWood);
                showGetItemLog(itemName, ranWood * multiplyAmt, woodsprite);
                */
                addItemIntoPlayer(itemName, ranWood * multiplyAmt);
            }


            itemName = "fiber";
            var ranFiber = UnityEngine.Random.Range(0, 3);
            if (ranFiber > 0)
            {
                /* Old method 
                var fibersprite = Resources.Load<Sprite>($"itemLog/{itemName}Item");
                addItemFromBushToList(itemName, ranFiber);
                showGetItemLog(itemName, ranFiber * multiplyAmt, fibersprite);
                */
                addItemIntoPlayer(itemName, ranFiber * multiplyAmt);
            }
        }
        else
        {
            itemName = "wood";
            var ranWood = UnityEngine.Random.Range(0, 3);
            if (ranWood > 0)
            {
                /* Old method 
                var woodsprite = Resources.Load<Sprite>($"itemLog/{itemName}Item");
                addItemFromBushToList(itemName, ranWood);
                showGetItemLog(itemName, ranWood * multiplyAmt, woodsprite);
                */
                addItemIntoPlayer(itemName, ranWood * multiplyAmt);
            }


            itemName = "fiber";
            var ranFiber = UnityEngine.Random.Range(1, 5);
            if (ranFiber > 0)
            {
                /* Old method 
                var fibersprite = Resources.Load<Sprite>($"itemLog/{itemName}Item");
                addItemFromBushToList(itemName, ranFiber);
                showGetItemLog(itemName, ranFiber * multiplyAmt, fibersprite);
                */
                addItemIntoPlayer(itemName, ranFiber * multiplyAmt);
            }

            itemName = "vine";
            var ranVine = UnityEngine.Random.Range(1, 5);
            if (ranVine > 0)
            {
                addItemIntoPlayer(itemName, ranVine * multiplyAmt);
            }
        }
    }
    public int findFreeIndex()
    {
        var listTemp = player.inventoryList;
        for (int i = 0; i < 16; i++)
        {
            var checkInd = listTemp.Any(s => s.index == i);
            if (!checkInd)
            {
                return i;
            }
        }
        return -1;
    }
    public void SetActiveActionBtn(bool value)
    {
        if (Application.platform == RuntimePlatform.Android || MobileTest)
        {
            actionBtn.SetActive(value);
        }

    }
    public void SetActionImageButton(string actionCase)
    {
        if (tabPanel.activeSelf)
        {
            return;
        }
        if (playerMovementBridge.actionMode == 1)
        {
            switch (actionCase)
            {
                case "stone":
                    actionBtn.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("ActionBtn/pickaxe");
                    actionBtn.transform.GetChild(0).gameObject.SetActive(true);
                    playerMovementBridge.setIndicatorAvailibility(true);
                    playerMovementBridge.setIndicatorPosition();
                    break;
                case "bush":
                case "berry":
                    actionBtn.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("ActionBtn/hand2");
                    actionBtn.transform.GetChild(0).gameObject.SetActive(true);
                    playerMovementBridge.setIndicatorAvailibility(true);
                    playerMovementBridge.setIndicatorPosition();
                    break;
                case "tree":
                case "stump":
                    actionBtn.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("ActionBtn/axe");
                    actionBtn.transform.GetChild(0).gameObject.SetActive(true);
                    playerMovementBridge.setIndicatorAvailibility(true);
                    playerMovementBridge.setIndicatorPosition();
                    break;
                default:
                    playerMovementBridge.unHitWithUIButton();
                    playerMovementBridge.setIndicatorAvailibility(false);
                    actionBtn.transform.GetChild(0).gameObject.SetActive(false);
                    break;
            }
        }
        else
        {
            actionBtn.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("ActionBtn/sword");
            actionBtn.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    #region Inventory management
    public void registerIVSwapSourceItem(int ind)
    {
        IVSwapSourceIndex = ind;
    }
    public void registerIVSwapTargetItem(int ind)
    {
        IVSwapTargetIndex = ind;
    }
    public void releaseIVSwapSourceItem()
    {
        IVSwapSourceIndex = -1;
    }
    public void releaseIVSwapTargetItem()
    {
        IVSwapTargetIndex = -1;
    }
    public void swapItemInList()
    {
        playerIVManager.swapIndexItemInList(player.inventoryList, IVSwapSourceIndex, IVSwapTargetIndex);
        IVSwapSourceIndex = -1;
        IVSwapTargetIndex = -1;
    }

    public void showBinChoices()
    {
        showMoveToBinBtn = true;
        binSection.showDropBtn();
    }

    public void hideBinChoices()
    {
        showMoveToBinBtn = false;
        binSection.hideDropBtn();
    }

    public void moveItemToBin(bool half) // Use in move to bin button on interface
    {
        hideBinChoices();
        var srcIdx = player.inventoryList.Where(s => s.index == IVSwapSourceIndex).First();
        player.inventoryList = playerIVManager.moveToBin(player.inventoryList, binList, player.inventoryList.IndexOf(srcIdx), half);
        //foreach (var item in binList)
        //{
        //    print($"ind : {item.index}, Name : {item.itemName}, QTY : {item.qty}");
        //}
        
    }
    public void DiscardItemsInTheBin()
    {
        if (binList.Count > 0)
        {
            binList = new List<PlayerInventoryListModel>();
        }
    }
    public void DropTheBin()
    {
        if (binList.Count > 0)
        {
            var margin = playerMovementBridge.SPR.flipX ? -1.25f : 1.25f;
            var additionalRange = new Vector3(margin, 1.5f);
            var newBusket = Busket;
            var spawnedItem = Instantiate(newBusket, playerMovementBridge.transform.position + additionalRange, Quaternion.identity);
            spawnedItem.GetComponent<busketItems>().itemList = CreateNewBusket(binList);
            binList = new List<PlayerInventoryListModel>();
        }
        
    }
    public void ShowDropItemLog()
    {

    }
    public List<PlayerInventoryListModel> CreateNewBusket(List<PlayerInventoryListModel> binList)
    {
        List<PlayerInventoryListModel> newBusket = new List<PlayerInventoryListModel>();
        foreach (var item in binList)
        {
            newBusket.Add(item);
        }
        return newBusket;
    }
    #endregion

    #region Busket Panel

    public void BusketPanelToggle()
    {
        if (busketPanelUnlock && allowToOpenBusket)
        {
            busketPanelUnlock = false;
            if (BusketPanel.activeSelf) // Close Busket panel
            {
                BusketPanel.SetActive(false);
                if (Application.platform == RuntimePlatform.Android || MobileTest)
                {
                    actionBtn.SetActive(actionBtn.activeSelf ? true : actionBtn.activeSelf);
                    ivBtn.SetActive(true);
                    runBtn.SetActive(true);
                    rollBtn.SetActive(true);
                }
            }
            else // Open Busket panel
            {
                BusketPanel.SetActive(true);
                actionBtn.SetActive(false);
                ivBtn.SetActive(false);
                runBtn.SetActive(false);
                rollBtn.SetActive(false);
            }
            Invoke("unlockBusketPanelPress", 0.3f);
        }
    }
    void unlockBusketPanelPress()
    {
        busketPanelUnlock = true;
    }

    #endregion


    public void cancelAttack()
    {
        playerMovementBridge.cancelAttackAnim();
    }




    #region Environment
    public void DestroyItemInArea(int index, int areaCode)
    {
        var areaID = (idlandID)areaCode;
        switch (areaID)
        {
            case idlandID.First_SW:
                FirstIsland_SW.GetComponent<FirstIslandSpawner>().spawnedItems.RemoveAt(index);
                FirstIsland_SW.GetComponent<FirstIslandSpawner>().SpawnDestroyedItemInvoke();
                break;
            case idlandID.First_W1:
                FirstIsland_W1.GetComponent<FirstIslandSpawner>().spawnedItems.RemoveAt(index);
                FirstIsland_W1.GetComponent<FirstIslandSpawner>().SpawnDestroyedItemInvoke();
                break;
            case idlandID.First_W2:
                FirstIsland_W2.GetComponent<FirstIslandSpawner>().spawnedItems.RemoveAt(index);
                FirstIsland_W2.GetComponent<FirstIslandSpawner>().SpawnDestroyedItemInvoke();
                break;
            case idlandID.First_W3:
                FirstIsland_W3.GetComponent<FirstIslandSpawner>().spawnedItems.RemoveAt(index);
                FirstIsland_W3.GetComponent<FirstIslandSpawner>().SpawnDestroyedItemInvoke();
                break;
            case idlandID.First_C1:
                FirstIsland_C1.GetComponent<FirstIslandSpawner>().spawnedItems.RemoveAt(index);
                FirstIsland_C1.GetComponent<FirstIslandSpawner>().SpawnDestroyedItemInvoke();
                break;
            case idlandID.First_C2:
                FirstIsland_C2.GetComponent<FirstIslandSpawner>().spawnedItems.RemoveAt(index);
                FirstIsland_C2.GetComponent<FirstIslandSpawner>().SpawnDestroyedItemInvoke();
                break;
            case idlandID.First_C3:
                FirstIsland_C3.GetComponent<FirstIslandSpawner>().spawnedItems.RemoveAt(index);
                FirstIsland_C3.GetComponent<FirstIslandSpawner>().SpawnDestroyedItemInvoke();
                break;
            case idlandID.First_N1:
                FirstIsland_N1.GetComponent<FirstIslandSpawner>().spawnedItems.RemoveAt(index);
                FirstIsland_N1.GetComponent<FirstIslandSpawner>().SpawnDestroyedItemInvoke();
                break;
            case idlandID.First_N2:
                FirstIsland_N2.GetComponent<FirstIslandSpawner>().spawnedItems.RemoveAt(index);
                FirstIsland_N2.GetComponent<FirstIslandSpawner>().SpawnDestroyedItemInvoke();
                break;
            case idlandID.First_N3:
                FirstIsland_N3.GetComponent<FirstIslandSpawner>().spawnedItems.RemoveAt(index);
                FirstIsland_N3.GetComponent<FirstIslandSpawner>().SpawnDestroyedItemInvoke();
                break;
            default:
                break;
        }
    }


    #endregion

    #region TransitionManager
    public void SetActiveInGameUICanvas(bool val)
    {
        InGameCanvas.SetActive(val);
    }
    public void SetActiveTransitionCanvas(bool val)
    {
        TransitionCanvas.SetActive(val);
    }
    public void ReadyToCLoaseTheCurtain()
    {
        TransitionCanvas.SetActive(true);
        SetActiveInGameUICanvas(false);
        Invoke("CloseTheCurtain", 1f);
    }
    public void CloseTheCurtain()
    {
        transitionMng.CloseTheCurtain();
    }

    #endregion

    #region Set Player
    public bool AddExp(int exp)
    {
        var roofOfExp = player.level * maxStackLevel;
        player.exp += exp;
        return roofOfExp <= player.exp; // return true if player level up on add exp
    }
    public void SetPlayerToSpawnPoint()
    {
        playerMovementBridge.transform.position = PlayerSpawnPoint.position;
    }

    public void RespawnPlayer()
    {
        Time.timeScale = 1;
        playerMovementBridge.RevivePlayer();
        transitionMng.OpenTheCurtain();
    }

    public void ExitFromMainHouse()
    {
        transitionMng.CloseTheCurtainForExitTheMainHouse();
    }

    public void SetExitMainHouseToFalse()
    {
        ExitingMainHouse = false;
    }
    public void SetNotMoveInMainHouseToFalse()
    {

        LockJoy = false;
    }

    public void LockCameraToPlayer()
    {
        camFollow.smoothSpeed = 99999f;
    }
    public void UnlockCameraToPlayer()
    {
        camFollow.smoothSpeed = 0.125f;
    }

    #endregion


    #region Interacting

    public void SetInteractTypeAndTurnOnBtn(string type, string interactObjectiveParam)
    {
        playerMovementBridge.actionMode = 1;
        SetActiveActionSwitchBtn(false);
        interactObjective = interactObjectiveParam;
        // 1. Touch, 2.Talk, 3.Hammered
        switch (type)
        {
            case "Touch":
                interactBtn.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("ActionBtn/hand2");
                break;
            case "GoToBed":
                interactBtn.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("ActionBtn/zz");
                break;
            case "Talk":
                interactBtn.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("ActionBtn/chat");
                break;
            case "Hammer":
                interactBtn.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("ActionBtn/hammer");
                break;
        }
        interactBtn.SetActive(true);

    }
    public void SetActiveActionSwitchBtn(bool val)
    {
        if (player.swordLvl > 0)
        {
            actionSwitchBtn.SetActive(val);
        }
        else
        {
            actionSwitchBtn.SetActive(false);
        }
        
    }

    public void CloseDialogCanvas()
    {
        ControllerCanvas.SetActive(true);
        DialogCanvas.SetActive(false);
    }

    public void ActionInteracting()
    {
        switch (interactObjective)
        {
            case "EnterMainHouse":
                AllowToExitFromMainHouse = false;
                LockCameraToPlayer();
                // CloseTheCurtain (Set player to MainHouse In Function)
                transitionMng.CloseTheCurtainForEnterTheMainHouse();
                break;
            case "ReadTheLetter":
                ControllerCanvas.SetActive(false);
                DialogCanvas.SetActive(true);
                dialog.sentences = Language == "th" ? DialogStory.NoteInMainHouseTh : DialogStory.NoteInMainHouseEn;
                dialog.StartType();
                break;
        }
    }
    public void SetPlayerToMainHouseEnterPoint()
    {
        playerMovementBridge.transform.position = MainHouseEnterPoint.position;
        playerMovementBridge.SPR.sortingLayerName = "PlayerInHosue";
        UnlockCameraToPlayer();
    }
    public void SetPlayerToFrontMainHousePoint()
    {
        playerMovementBridge.transform.position = FrontMainHousePoint.position;
        playerMovementBridge.SPR.sortingLayerName = "Default";
    }

    #endregion



    #region Floating Text
    public void SpawnFloatingText(string text, Vector3 spawnPoint, bool playerLevelUp)
    {
        if (playerLevelUp)
        {
            PlayLevelUpAnim();
            return;
        }
        //FloatingTextObj.GetComponent<FloatingTextSetter>().SetTextAndShow(text);
        //Instantiate(FloatingTextObj, spawnPoint, Quaternion.identity);
    }

    #endregion

    #region Level up event
    public void PlayLevelUpAnim()
    {
        Vector3 onScreenPos;
        onScreenPos = mainCam.WorldToScreenPoint(playerMovementBridge.transform.position);
        lvlUpAnim.LevelUp(onScreenPos);
        lvlUpAnimController.StartLevelUpAnim();

        //StartCoroutine(WaitAnim());
    }
    IEnumerator WaitAnim()
    {
        int loopTime = 5;
        //To wait, type this:
        for (int i = 0; i < loopTime; i++)
        {
            //Stuff before waiting
            CameraShaker.Instance.ShakeOnce(i, 20, 0, 0.1f);
            if (i == loopTime - 1)
            {
                Vector3 onScreenPos;
                onScreenPos = mainCam.WorldToScreenPoint(playerMovementBridge.transform.position);
                lvlUpAnim.LevelUp(onScreenPos);
            }
            yield return new WaitForSeconds(0.2f);
            //Stuff after waiting.
            
        }

    }
    #endregion
}
