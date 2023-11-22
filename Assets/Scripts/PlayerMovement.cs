using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveAxis;

    private bool isGrounded = false; //can doublejukp fix later if not lazy
    private SpriteRenderer sr;
    private bool attackActive = false;
    [HideInInspector] public GameObject kickChild;
    [HideInInspector] public GameObject punchChild;
    private bool blockUpper;
    private bool blockLower;

    [HideInInspector] public PlayerMovement enemyPlayerScript; //used on char spawner
    [HideInInspector] public TMP_Text enemyHP;

    [Header("Player stats")]
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public float jumpHeight;
    [HideInInspector] public float health;
    [HideInInspector] public float punchDamage = 15;
    [HideInInspector] public float kickDamage = 25;

    [Header("Sprites")]
    [SerializeField] private Sprite blockHigh;
    [SerializeField] private Sprite blockLow;
    [SerializeField] public Sprite hurt;
    [SerializeField] public Sprite idle1;
    [SerializeField] private Sprite idle2;
    [SerializeField] private Sprite jump;
    [SerializeField] private Sprite kick;
    [SerializeField] private Sprite kickWind;
    [SerializeField] private Sprite punch;
    [SerializeField] private Sprite punchWind;
    [SerializeField] private Sprite walk1;
    [SerializeField] private Sprite walk2;




    [Header("Coroutine wait times")]
    [SerializeField] private float kickWindupTime;
    [SerializeField] private float punchWindupTime;
    [SerializeField] private float idleLoopTime = 1f;
    [SerializeField] private float walkLoopTime = 0.5f;

    [SerializeField] private float kickDelay = 0.5f;
    [SerializeField] private float punchDelay = 0.3f;

    private Coroutine coWalk;
    private Coroutine coIdle;
    [SerializeField] private float blockDuration = 0.4f;
    [SerializeField] public float hurtTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        
        sr.sprite = idle1;
        //coIdle = StartCoroutine(IdleLoop());
    }

    public void EnableChildCollision()
    {
        transform.GetChild(0).GetComponent<ChildCollision>().enabled = true;
        transform.GetChild(1).GetComponent<ChildCollision>().enabled = true;
        //Debug.Log("enabled");
    }

    public void SetChildReference( int type)
    {
        AttackBoxMove(true, type);
    }

    
    private void AttackBoxMove(bool moveAway, int type)
    {
        if (type == 1) 
        {
            if (moveAway)
            {
                kickChild.transform.position += new Vector3(10000, 0, 0);
            }
            else kickChild.transform.position -= new Vector3(10000, 0, 0);
        }else if(type == 2)
        {
            if (moveAway)
            {
                punchChild.transform.position += new Vector3(10000, 0, 0);
            }
            else punchChild.transform.position -= new Vector3(10000, 0, 0);
        }
        
    }

    public void ChildCollision(GameObject child)
    {
        //Debug.Log(enemyHP.text);
        if (child.tag == "Punch" && enemyPlayerScript.blockUpper)
        {
            Debug.Log("Blocked upper");
            return;
        }else if (child.tag == "Kick" && enemyPlayerScript.blockLower)
        {
            Debug.Log("Blocked lower");
            return;
        }
        if(child.tag == "Punch")
        {
            health -= punchDamage;//this player effect enemy health
        }else if (child.tag == "Kick")
        {
            health -= kickDamage;
        }


            
        enemyHP.text = $"{health}/100";
        if (!enemyPlayerScript.attackActive)
        {
            enemyPlayerScript.StopAllCoroutines();
            enemyPlayerScript.sr.sprite = enemyPlayerScript.hurt;
            enemyPlayerScript.StartCoroutine(HurtTime());
        }
        if (health <= 0) { Destroy(enemyPlayerScript.gameObject); }
    }

    // Update is called once per frame
    void Update()
    {

        if (attackActive) { return; }
        Vector2 moveVector = new Vector2 (moveSpeed * moveAxis * Time.deltaTime, rb.velocity.y);
        rb.velocity = moveVector;
    }

    void OnMovement(InputValue inputValue)
    {
        moveAxis = inputValue.Get<float>();

        if(!isGrounded || attackActive) { return; }

        if (inputValue.Get<float>()!= 0) 
        {
            StopAllCoroutines();
            coWalk = StartCoroutine(WalkLoop());

        }
        else
        {
            sr.sprite = idle1;
            StopCoroutine(coWalk);
            coIdle = StartCoroutine(IdleLoop());
        }
        //Debug.Log(inputValue.Get<float>()); 
            
    }


    void OnJump(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            if(isGrounded && !attackActive)
            {
                Vector2 moveVector = new Vector2(rb.velocity.x, jumpHeight);
                rb.velocity = moveVector;
                StopAllCoroutines();
                sr.sprite = jump;
            }
        }
    }

    void OnBlockKick(InputValue inputValue)
    {
        if (inputValue.isPressed && !attackActive)
        {
            StopAllCoroutines();
            attackActive = true;
            blockLower = true;
            sr.sprite = blockLow;
            StartCoroutine(BlockTime());
        }
    }

    void OnBlockPunch(InputValue inputValue)
    {
        if (inputValue.isPressed && !attackActive)
        {
            
            StopAllCoroutines();
            attackActive = true;
            blockUpper = true;
            sr.sprite = blockHigh;
            StartCoroutine(BlockTime());
        }
    }

    void OnKick(InputValue inputValue)
    {
        if (inputValue.isPressed && !attackActive) 
        {
            StopAllCoroutines();
            StartCoroutine(KickAttack()); 
        }
    }

    void OnPunch(InputValue inputValue)
    {
        if (inputValue.isPressed && !attackActive)
        {
            StopAllCoroutines();
            StartCoroutine(PunchAttack());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground" && !attackActive)
        {
            StopAllCoroutines();
            sr.sprite = idle1; // to instantly land
            coIdle = StartCoroutine(IdleLoop());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            isGrounded=true;
            

        }
        else
        {
            isGrounded = false;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            isGrounded = false;


        }
    }

    IEnumerator IdleLoop()
    {
        yield return new WaitForSeconds(idleLoopTime);
        if (sr.sprite == idle1)
        {
            sr.sprite = idle2;
        }
        else
        {
            sr.sprite = idle1;
        }
         coIdle = StartCoroutine(IdleLoop());

    }

    IEnumerator WalkLoop()
    {
        yield return new WaitForSeconds(walkLoopTime);
        if (sr.sprite == walk1)
        {
            sr.sprite = walk2;
        }
        else
        {
            sr.sprite = walk1;
        }
        coWalk = StartCoroutine(WalkLoop());
    }

    IEnumerator KickAttack()
    {
        sr.sprite = kickWind;
        attackActive = true;
        yield return new WaitForSeconds(kickWindupTime);
        sr.sprite = kick;
        AttackBoxMove(false, 1);
        coIdle = StartCoroutine(AttackAfter(kickDelay, 1));
    }

    IEnumerator PunchAttack()
    {
        sr.sprite = punchWind;
        attackActive = true;
        yield return new WaitForSeconds(punchWindupTime);
        sr.sprite = punch;
        AttackBoxMove(false, 2);
        coIdle = StartCoroutine(AttackAfter(punchDelay, 2));
    }

    IEnumerator AttackAfter(float delayTime, int type)
    {

        yield return new WaitForSeconds(delayTime);
        attackActive = false;
        if (type == 1) { AttackBoxMove(true, 1); } else if (type == 2) { AttackBoxMove(true, 2); }
        sr.sprite = idle1;
        coIdle = StartCoroutine(IdleLoop());
    }
    IEnumerator BlockTime()
    {

        yield return new WaitForSeconds(blockDuration);
        attackActive = false;
        blockLower = false;
        blockUpper = false;

        sr.sprite = idle1;
        coIdle = StartCoroutine(IdleLoop());
    }

    IEnumerator HurtTime()
    {

        yield return new WaitForSeconds(enemyPlayerScript.hurtTime);
        
        enemyPlayerScript.sr.sprite = enemyPlayerScript.idle1;
        enemyPlayerScript.coIdle = enemyPlayerScript.StartCoroutine(enemyPlayerScript.IdleLoop());
    }


}