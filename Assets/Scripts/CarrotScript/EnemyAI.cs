using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int GotHit;
    public float inChargeDistance;
    public float travelDistance;
    public float attackRange;
    public float remainingDistanceOffset;
    public float attackAboveOffset;
    public Animator anim;
    public SpriteRenderer spriteR;
    public bool hurting = false;
    public int HP = 3;
    public int maxMeatSpawnAmount;
    public int floor;
    public GameObject meatItem, bloodParticle;
    AIDestinationSetter aiDestinationSetter;
    AIPath aIPath;
    Transform playerTransform;
    Vector3 spawnPoint;
    bool switchFromChasingPlayer = false, allowMove = true;
    float enemyOriginScale;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        GotHit = 0;
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
        aIPath = GetComponent<AIPath>();
        aiDestinationSetter.enemyState = (int)EnemyAIMoveState.Roaming;
        playerTransform = FindObjectOfType<GameManager>().playerMovementBridge.transform;
        spawnPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        aiDestinationSetter.roamingPosition = spawnPoint;
        enemyOriginScale = transform.localScale.x;

        shaderSpritesDefault = spriteR.material.shader;
        shaderGUItext = Shader.Find("GUI/Text Shader");
    }

    // Update is called once per frame
    void Update()
    {
        if (hurting)
        {
            if (i == 0)
            {
                whiteSprite();
            }

            return;
        }
        if (!allowMove)
        {
            return;
        }
        if (aIPath.remainingDistance > 15f) // เพื่อป้องกัน Monster อ้อมโลกเพื่อไปยังจุดหมาย
        {
            GenarateDestinationPoint();
        }
        var playerIsDead = FindObjectOfType<GameManager>().playerMovementBridge.playerIsDead;
        //var allowChase = FindObjectOfType<GameManager>().playerMovementBridge.AllowEnemyChase;
        var distanceFromPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceFromPlayer < inChargeDistance && !playerIsDead) // ถ้าอยู่ใกล้ Player
        {
            if (distanceFromPlayer < (attackRange - (playerTransform.position.y > transform.position.y ? attackAboveOffset : 0)))
            {
                FindObjectOfType<GameManager>().playerMovementBridge.EnemyDetected();
                allowMove = false;
                aiDestinationSetter.enemyState = (int)EnemyAIMoveState.Attacking;
                FlipToPlayerBeforeAttack();
                anim.SetInteger("AnimState", 3);
            }
            else
            {
                FindObjectOfType<GameManager>().playerMovementBridge.EnemyDetected();
                anim.SetInteger("AnimState", 1);
                switchFromChasingPlayer = true;
                aiDestinationSetter.target = playerTransform;
                aiDestinationSetter.enemyState = (int)EnemyAIMoveState.ChasingPlayer;
            }
        }
        else // Walk around spawn point
        {
            if (switchFromChasingPlayer)
            {
                switchFromChasingPlayer = false;
                anim.SetInteger("AnimState", 0);
                GenarateDestinationPoint();
            }
            else if (aIPath.remainingDistance < remainingDistanceOffset)
            {
                anim.SetInteger("AnimState", 0);
                if (!IsInvoking("GenarateDestinationPoint"))
                {
                    Invoke("GenarateDestinationPoint", 3f);
                }

            }
            else if (aIPath.remainingDistance >= remainingDistanceOffset)
            {
                anim.SetInteger("AnimState", 1);
            }

        }
    }

    public void whiteSprite()
    {
        spriteR.material.shader = shaderGUItext;
        var color = new Color(1f, 0.5f, 0.5f);
        spriteR.color = color;
        Invoke("normalSprite", 0.05f);
    }
    public void normalSprite()
    {
        spriteR.material.shader = shaderSpritesDefault;
        var color = Color.white;
        color.a = 1;
        spriteR.color = color;
        if (i < 1)
        {
            i++;
            Invoke("whiteSprite", 0.05f);
        }
        else
        {
            hurting = false;
            i = 0;
        }
    }
    public void UnlockMoving()
    {
        allowMove = true;
    }

    void GenarateDestinationPoint()
    {
        var ranX = Random.Range(-travelDistance, travelDistance);
        var ranY = Random.Range(-travelDistance, travelDistance);
        aiDestinationSetter.roamingPosition = new Vector3(spawnPoint.x + ranX, spawnPoint.y + ranY, spawnPoint.z);
        aiDestinationSetter.enemyState = (int)EnemyAIMoveState.Roaming;
    }

    public void FlipToPlayerBeforeAttack()
    {
        if (playerTransform.position.x > transform.position.x)
        {
            spriteR.flipX = true;
        }
        else
        {
            spriteR.flipX = false;
        }
    }
    public void Dead()
    {
        var ranFiber = UnityEngine.Random.Range(1, maxMeatSpawnAmount + 1);
        SpawnMeat(ranFiber, gameObject.transform.position);
        Instantiate(bloodParticle, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    public void SpawnMeat(int amount, Vector3 position)
    {

        for (int i = 0; i < amount; i++)
        {
            Instantiate(meatItem, position, Quaternion.identity);
        }
    }
}
