using EZCameraShake;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using static PlayerAttacker;
using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{
    public static string getResource = "";
    public static GameObject interacting;
    public static bool enemyInRange = false;
    public GameObject waterDrop, indicator;
    public itemSpawn spawner;
    public Animator anim;
    public PlayerAttacker playerAttacker;
    public AttackerAnimator attackerAnim;
    public float speed;
    public float speedUp;
    public float rollingSpeed;
    public Rigidbody2D rg;
    public SpriteRenderer SPR;
    public VariableJoystick joy;
    public bool runBool;
    public bool rollBool;
    public int actionMode; // 1 = Harvest, 2 = Attack
    public float maxStamina;
    public float maxFood;
    public Slider stmSlider;
    public Slider foodSlider;
    public bool playerGotHit = false;
    public bool playerInvisibled = false;
    public bool kiilPlayer = false, playerIsDead = false;
    public bool AllowEnemyChase = true;
    float stmValue, foodValue, oldFoodValue;
    float Horizontal = 0, Vertical = 0;
    bool AllowCamShakeOnHitHardThing = false, allowMove = true, isTiring = false, hitWithUI = false, allowFlipWhenAttack = true, isRolling = false, allowToRoll = true;
    Sprite stump;
    bool MobileTest = false, AllowMouseInput = true;
    //sortingPlayer sorter;
    // Start is called before the first frame update
    void Start()
    {
        kiilPlayer = false;
        playerIsDead = false;
        MobileTest = FindObjectOfType<GameManager>().MobileTest;
        AllowMouseInput = FindObjectOfType<GameManager>().AllowMouseInput;
        stump = Resources.Load<Sprite>("treeStump");
        stmValue = maxStamina;
        stmSlider.maxValue = maxStamina;
        stmSlider.value = maxStamina;
        foodValue = maxFood;
        foodSlider.maxValue = maxFood;
        foodSlider.value = maxFood;
        staminaManger();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var checkIVPanel = FindObjectOfType<GameManager>().tabPanel.activeSelf;
        if (actionMode == 1)
        {
            FindObjectOfType<GameManager>().SetActiveActionBtn(checkIVPanel ? false : (string.IsNullOrWhiteSpace(getResource) ? false : true));
        }
        else
        {
            indicator.SetActive(false);
            FindObjectOfType<GameManager>().SetActiveActionBtn(checkIVPanel ? false : true);
        }

        bool run = Input.GetKey(KeyCode.LeftShift);
        Vector3 posTemp = transform.position;
        var joyDX = joy.Direction.x * 3f;
        var joyDY = joy.Direction.y * 3f;
        Horizontal = Math.Abs(joyDX) > 1 ? (joyDX > 0 ? 1 : -1) : joyDX;// (joy.Horizontal > 0 ? 1 : (joy.Horizontal < 0 ? -1 : 0));
        Vertical = Math.Abs(joyDY) > 1 ? (joyDY > 0 ? 1 : -1) : joyDY; //(joy.Vertical > 0 ? 1 : (joy.Vertical < 0 ? -1 : 0));

        string harvesterCase;
        #region Collect move position, harvester position & attacker position
        if (joy.Horizontal == 0 && joy.Vertical == 0)
        {
            // Keyboard (WASD)
            Horizontal = Convert.ToInt32(Input.GetAxisRaw("Horizontal"));
            Vertical = Convert.ToInt32(Input.GetAxisRaw("Vertical"));
            harvesterCase = Horizontal + "," + Vertical;

        }
        else
        {
            // Joystick from touching
            var XN = joy.Direction.normalized.x;
            var YN = joy.Direction.normalized.y;
            string joyX, joyY;
            if (XN > 0.5f)
            {
                joyX = "1";
            }
            else if (XN < -0.5f)
            {
                joyX = "-1";
            }
            else
            {
                joyX = "0";
            }

            if (YN > 0.5f)
            {
                joyY = "1";
            }
            else if (YN < -0.5f)
            {
                joyY = "-1";
            }
            else
            {
                joyY = "0";
            }
            harvesterCase = joyX + "," + joyY;
        }
        #endregion
        #region Move Harvester & Attacker
        switch (harvesterCase)
        {
            case "0,1": // ↑
                playerHarvester.harvester.offset = new Vector2(0f, 0.3f);
                // SPR.flipX = true is mean LEFT ←
                PlayerAttacker.attackDirection = SPR.flipX ? AttackDirection.aboveFlipLeft : AttackDirection.aboveFlipRight;
                break;

            case "1,1": // ↗
                playerHarvester.harvester.offset = new Vector2(0.4f, 0.3f);

                PlayerAttacker.attackDirection = AttackDirection.aboveRight;
                break;

            case "1,0": // →
                playerHarvester.harvester.offset = new Vector2(0.4f, 0.09f);

                PlayerAttacker.attackDirection = AttackDirection.right;
                break;

            case "1,-1": // ↘
                playerHarvester.harvester.offset = new Vector2(0.4f, -0.2f);

                PlayerAttacker.attackDirection = AttackDirection.belowRight;
                break;

            case "0,-1": // ↓
                playerHarvester.harvester.offset = new Vector2(0f, -0.2f);
                // SPR.flipX = true is mean LEFT ←
                PlayerAttacker.attackDirection = SPR.flipX ? AttackDirection.belowFlipLeft : AttackDirection.belowFlipRight;
                break;

            case "-1,-1": // ↙
                playerHarvester.harvester.offset = new Vector2(-0.4f, -0.2f);

                PlayerAttacker.attackDirection = AttackDirection.belowLeft;
                break;

            case "-1,0": // ←
                playerHarvester.harvester.offset = new Vector2(-0.4f, 0.09f);

                PlayerAttacker.attackDirection = AttackDirection.left;
                break;

            case "-1,1": // ↖
                playerHarvester.harvester.offset = new Vector2(-0.4f, 0.3f);

                PlayerAttacker.attackDirection = AttackDirection.aboveLeft;
                break;
        }
        #endregion

        if ((Input.GetKey("k") || kiilPlayer) && !playerIsDead)
        {
            playerIsDead = true;
            FindObjectOfType<GameManager>().ReadyToCLoaseTheCurtain();
            SoundManagerScript.PlaySound("dead");
            anim.Play("dead");
        }
        if (Input.GetKey(KeyCode.Tab))
        {
            FindObjectOfType<GameManager>().TabPanelToggle();
        }

        #region Player got HIT
        if (playerGotHit && !playerInvisibled && !playerIsDead) // Player got hit
        {

            CameraShaker.Instance.ShakeOnce(5, 20, 0, 0.6f);
            playerGotHit = false;
            playerInvisibled = true;
            if (FindObjectOfType<GameManager>().player.hpCurrent > 0)
            {
                --FindObjectOfType<GameManager>().player.hpCurrent;
            }

            if (FindObjectOfType<GameManager>().player.hpCurrent == 0)
            {
                kiilPlayer = true;
            }
            gameObject.GetComponent<PlayerGotHitAnimation>().goBlink();
        }
        else
        {
            playerGotHit = false;
        }
        #endregion

        #region Rolling section

        var rightClicked = Input.GetMouseButton(1) && AllowMouseInput;
        if ((rightClicked || rollBool) && (Horizontal != 0 || Vertical != 0) && allowToRoll && !anim.GetBool("roll") && !anim.GetBool("attack") && playerIsDead == false) // Player rolling
        {
            allowToRoll = false;
            isRolling = true;
            anim.SetBool("roll", true);
            Invoke("setRollAnimToFalse", 0.4f);
        }

        #endregion
        if (playerIsDead == false)
        {
            bool harvestAction;
            if (Application.platform == RuntimePlatform.Android || MobileTest)
            {
                harvestAction = hitWithUI;
            }
            else
            {
                harvestAction = Input.GetMouseButton(0) && AllowMouseInput;
            }
            //Debug.Log(getResource);
            if (harvestAction)
            {
                if (actionMode == 2) // Attack mode
                {
                    if (/*enemyInRange && */!anim.GetBool("rolling"))
                    {
                        allowMove = false;
                        cancelHarvesting();
                        checkFlipXPlayerOnMouse();
                        playAttack();
                    }
                }
                else
                {
                    switch (getResource)
                    {
                        case "stone":
                            checkFlipXPlayer();
                            hitStone();
                            break;
                        case "bush":
                            checkFlipXPlayer();
                            hitBush();
                            break;
                        case "berry":
                            checkFlipXPlayer();
                            hitBerry();
                            break;
                        case "tree":
                            checkFlipXPlayer();
                            hitTree();
                            break;
                        case "stump":
                            checkFlipXPlayer();
                            hitTree();
                            break;
                        //case "":
                        //    if (enemyInRange && !anim.GetBool("rolling"))
                        //    {
                        //        allowMove = false;
                        //        cancelHarvesting();
                        //        checkFlipXPlayerOnMouse();
                        //        playAttack();
                        //    }
                        //    break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                cancelAction();
            }
            if (allowMove)
            {

                if (Vertical != 0 || Horizontal != 0)
                {
                    if (Horizontal > 0) SPR.flipX = false;// anim.SetBool("Right", true);
                    else if (Horizontal < 0) SPR.flipX = true;// anim.SetBool("Right", false);

                    if ((run || runBool) && isTiring == false && isRolling == false)
                    {
                        anim.SetBool("Run", true);
                        anim.SetFloat("Speed", 1f);
                    }
                    else
                    {
                        anim.SetBool("Run", false);
                        anim.SetFloat("Speed", 0.5f);
                    }
                }
                else
                {
                    anim.SetFloat("Speed", 0f);
                }
                #region MainHouse Movement
                // Exit the main house
                if (FindObjectOfType<GameManager>().ExitingMainHouse || FindObjectOfType<GameManager>().LockJoy)
                {
                    anim.SetBool("Run", false);
                    anim.SetFloat("Speed", 0f);
                    Vertical = 0f;
                    Horizontal = 0f;
                }

                // Enter the main house walking
                if (FindObjectOfType<GameManager>().EnteringMainHouse)
                {
                    Vertical = 0.3f;
                    Horizontal = 0f;
                    anim.SetBool("Run", false);
                    anim.SetFloat("Speed", 0.5f);
                }

                #endregion
                if (Horizontal != 0)
                {
                    var HSpeedTemp = (speed + ((run || runBool) && isTiring == false && !isRolling ? speedUp : 0) + (isRolling ? rollingSpeed : 0));
                    //Debug.Log("Horizontal : " + HSpeedTemp);
                    posTemp.x += (Horizontal * HSpeedTemp * Time.fixedDeltaTime);
                }
                if (Vertical != 0)
                {
                    var VSpeedTemp = (speed + ((run || runBool) && isTiring == false && !isRolling ? speedUp : 0) + (isRolling ? rollingSpeed : 0));
                    //Debug.Log("Vertical : " + VSpeedTemp);
                    posTemp.y += (Vertical * VSpeedTemp * Time.fixedDeltaTime);
                }

                if (transform.position != posTemp)
                {
                    transform.position = posTemp;
                }
            }

        }


    }
    void checkFlipXPlayer()
    {

        if (interacting.transform.position.x > transform.position.x)
        {
            SPR.flipX = false;
        }
        else
        {
            SPR.flipX = true;
        }
    }
    public void cancelAttackAnim()
    {
        anim.SetBool("attack", false);
    }
    void checkFlipXPlayerOnMouse()
    {
        // SPR.flipX = true is mean LEFT ←
        switch (PlayerAttacker.attackDirection)
        {
            case AttackDirection.aboveFlipRight:
            case AttackDirection.aboveRight:
            case AttackDirection.right:
            case AttackDirection.belowRight:
            case AttackDirection.belowFlipRight:
                SPR.flipX = false;
                break;
            case AttackDirection.aboveFlipLeft:
            case AttackDirection.aboveLeft:
            case AttackDirection.left:
            case AttackDirection.belowLeft:
            case AttackDirection.belowFlipLeft:
                SPR.flipX = true;
                break;
        }

    }
    public void RevivePlayer()
    {
        anim.Play("idle");
        kiilPlayer = false;
        playerIsDead = false;
        playerInvisibled = false;
        FindObjectOfType<GameManager>().player.hpCurrent = FindObjectOfType<GameManager>().player.hpConstant;
    }
    void releaseFlipAttack()
    {
        allowFlipWhenAttack = true;
    }
    void lockFlipAttack()
    {
        allowFlipWhenAttack = false;
    }
    void cancelHarvesting()
    {
        anim.SetBool("rolling", false);
        anim.SetBool("hitStone", false);
        anim.SetBool("hitBush", false);
        anim.SetBool("hitTree", false);
    }
    public void cancelAttack()
    {
        anim.SetBool("attack", false);
        allowMove = true;
    }
    void cancelAction()
    {
        getResource = "";
        anim.SetBool("hitStone", false);
        anim.SetBool("hitBush", false);
        anim.SetBool("hitTree", false);
        if (!anim.GetBool("attack"))
        {
            allowMove = true;
        }
    }
    #region attack
    public void playAttack()
    {
        allowMove = false;
        anim.SetBool("attack", true);
    }
    public void AttackEnemy()
    {
        playerAttacker.attackEnemy();
    }
    public void PlaySwordFx()
    {
        attackerAnim.PlaySwordEffect();
    }
    public void UnlockPlaySwordFx()
    {
        attackerAnim.UnlockAnim();
    }
    #endregion
    #region action with the resource
    public void CamShakeOnHitHardThing()
    {
        if (AllowCamShakeOnHitHardThing)
        {
            CameraShaker.Instance.ShakeOnce(1, 10, 0, 0.6f);
        }
    }
    public void hitStone()
    {
        allowMove = false;
        anim.SetBool("hitStone", true);
    }
    public void hitBush()
    {
        allowMove = false;
        anim.SetBool("hitBush", true);
    }
    public void hitBerry()
    {
        allowMove = false;
        anim.SetBool("hitBush", true);
    }
    public void hitTree()
    {
        allowMove = false;
        anim.SetBool("hitTree", true);
    }
    #endregion
    public void playSound(string sound)
    {
        SoundManagerScript.PlaySound(sound);
    }

    #region Get resources & items
    public void getStone()
    {
        if (interacting == null)
            return;


        if (interacting.GetComponent<Harvesting>().hitCount >= 5)
        {
            spawner.spawn("stone", 6, interacting.transform.position);
            destroyThisItem();
        }
        else
        {
            if (interacting.name.Contains("stone2"))
            {
                interacting.GetComponent<stone2GotHit>().reNew = true;
            }
            else
            {
                interacting.GetComponent<stone1GotHit>().reNew = true;
            }
            interacting.GetComponent<Harvesting>().hitCount++;
        }
    }

    public void getItemFromBush()
    {

        if (interacting == null)
            return;

        if (interacting.GetComponent<Harvesting>().hitCount >= 2)
        {
            // Give items player here
            FindObjectOfType<GameManager>().getItemsFromBush(interacting.name);
            destroyThisItem();
        }
        else
        {
            interacting.GetComponent<bushGotHit>().reNew = true;
            interacting.GetComponent<Harvesting>().hitCount++;
        }
    }
    public void getWood()
    {

        if (interacting == null)
            return;

        if (interacting.GetComponent<Harvesting>().hitCount >= 5)
        {
            // Give items player here

            if (interacting.tag == "tree")
            {
                changeTreeIntoStump();
                spawner.spawn("wood", 6, interacting.transform.position);
                interacting.GetComponent<Harvesting>().hitCount = 0;
            }
            else
            {
                var ranLog = Random.Range(2, 4);
                spawner.spawn("log", ranLog, interacting.transform.position);
                destroyThisItem();
            }

        }
        else
        {
            if (interacting.CompareTag("tree"))
            {
                interacting.GetComponent<treeGotHit>().reNew = true;
            }
            else if (interacting.CompareTag("stump"))
            {
                interacting.GetComponent<stumpGotHit>().reNew = true;
            }
            interacting.GetComponent<Harvesting>().hitCount++;
        }
    }
    #endregion
    public void changeTreeIntoStump()
    {

        AddExpAndShowFloatingText("tree");
        interacting.tag = "stump";
        var transparenterTemp = interacting.GetComponent<stopTransparent>();
        transparenterTemp.stopTransparentFunction();
        getResource = "";
        var treeObj = interacting.GetComponent<SpriteRenderer>();
        var tempColor = treeObj.color;
        tempColor.a = 1f;
        treeObj.color = tempColor;
        treeObj.sprite = stump;
    }
    public void destroyThisItem()
    {
        string itemTag = interacting.tag;
        var itemIndex = interacting.GetComponent<Harvesting>().itemIndex;
        var areaCode = interacting.GetComponent<Harvesting>().areaCode;
        AddExpAndShowFloatingText(itemTag);
        FindObjectOfType<GameManager>().DestroyItemInArea(itemIndex, areaCode);
        
        Destroy(interacting);
        getResource = "";
    }

    public void AddExpAndShowFloatingText(string itemTag)
    {
        int exp = GetEXPFromItem(itemTag);
        string showText = exp.ToString() + "XP";
        // Add EXP for player from harvesting here
        var playerLevelUp = FindObjectOfType<GameManager>().AddExp(exp);
        if (playerLevelUp)
        {
            FindObjectOfType<GameManager>().player.level += 1;
        }

        SpawnFloatingText(showText, interacting.transform.position, playerLevelUp);
    }

    public int GetEXPFromItem(string itemTag)
    {
        switch (itemTag)
        {
            case "tree":
                return Random.Range(4, 7);
            case "stump":
                return Random.Range(6, 9);
            case "bush":
                return Random.Range(3, 6);
            case "berry":
                return Random.Range(4, 6);
            case "stone":
                return Random.Range(4, 8);
            default:
                return 0;
        }
    }
    public void switchToggleActionMode()
    {
        if (actionMode == 1)
        {
            switchActionModeToAttack();
        }
        else
        {
            switchActionModeToHarvest();
        }

    }
    public void switchActionModeToHarvest()
    {
        actionMode = 1;
    }
    public void switchActionModeToAttack()
    {
        actionMode = 2;
        FindObjectOfType<GameManager>().SetActionImageButton(null);
    }
    public void rollPressed()
    {
        rollBool = true;
    }

    public void rollUnPress()
    {
        rollBool = false;
    }

    public void runPressed()
    {
        runBool = true;
    }
    public void runUnPress()
    {
        runBool = false;
    }
    public void staminaManger()
    {
        var animationName = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        switch (animationName)
        {
            case "idle":
                if (stmValue < maxStamina)
                {
                    stmValue += 3;
                    oldFoodValue = foodValue;
                    var calSt = (maxStamina - (stmValue > maxStamina ? maxStamina : stmValue));
                    var calFd = (calSt > 5 ? 5 : calSt);
                    foodValue -= (calFd > foodValue ? 0 : calFd);
                    var fd = oldFoodValue - foodValue;
                    var addValue = stmValue + fd;
                    stmValue = (addValue > maxStamina ? maxStamina : addValue);
                }

                break;
            case "run":
                if (stmValue > 0)
                {
                    stmValue -= 7;
                    stmValue = (stmValue < 0 ? 0 : stmValue);
                }
                break;
            case "walk":
                if (stmValue < maxStamina)
                {
                    stmValue += 3;
                    oldFoodValue = foodValue;
                    var calSt = (maxStamina - (stmValue > maxStamina ? maxStamina : stmValue));
                    var calFd = (calSt > 5 ? 5 : calSt);
                    foodValue -= (calFd > foodValue ? 0 : calFd);
                    var fd = oldFoodValue - foodValue;
                    var addValue = stmValue + fd;
                    stmValue = (addValue > maxStamina ? maxStamina : addValue);
                }
                break;
            case "axeWithFX":
                if (stmValue > 0)
                {
                    stmValue -= 7f;
                    stmValue = (stmValue < 0 ? 0 : stmValue);
                }
                break;
            case "pickaxeWithFX":
                if (stmValue > 0)
                {
                    stmValue -= 7f;
                    stmValue = (stmValue < 0 ? 0 : stmValue);
                }
                break;

        }
        stmSlider.value = stmValue;
        foodSlider.value = foodValue;
        if (stmValue < 10)
        {
            waterDrop.SetActive(true);
        }
        else
        {
            waterDrop.SetActive(false);
        }
        if (stmValue == 0)
        {
            isTiring = true;
        }
        else
        {
            isTiring = false;
        }
        Invoke("staminaManger", 1f);


    }
    public void hitWithUIButton()
    {
        hitWithUI = true;
    }
    public void unHitWithUIButton()
    {
        hitWithUI = false;
    }
    public void setRollAnimToFalse()
    {
        anim.SetBool("roll", false);
        isRolling = false;
        Invoke("AllowRolling", 1.5f);
    }
    public void AllowRolling()
    {
        allowToRoll = true;
    }

    public void EnemyDetected()
    {
        enemyInRange = true;
        if (IsInvoking("NoEnemyDetected"))
        {
            CancelInvoke("NoEnemyDetected");
        }
        Invoke("NoEnemyDetected", 1f);
    }
    public void NoEnemyDetected()
    {
        enemyInRange = false;
    }

    public void setIndicatorPosition()
    {
        indicator.transform.position = interacting.transform.GetChild(0).position;
    }

    public void setIndicatorAvailibility(bool val)
    {
        indicator.SetActive(val);
    }

    public void SpawnFloatingText(string text, Vector3 position, bool playerLevelUp)
    {
        FindObjectOfType<GameManager>().SpawnFloatingText(text, position, playerLevelUp);
    }
}
