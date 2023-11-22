using System;
using System.Collections;
using System.Collections.Generic;
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


    [Header("Player stats")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    

    [Header("Sprites")]
    [SerializeField] private Sprite blockHigh;
    [SerializeField] private Sprite blockLow;
    [SerializeField] private Sprite hurt;
    [SerializeField] private Sprite idle1;
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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        sr.sprite = idle1;
        //coIdle = StartCoroutine(IdleLoop());
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
        coIdle = StartCoroutine(AttackAfter(kickDelay));
    }

    IEnumerator PunchAttack()
    {
        sr.sprite = punchWind;
        attackActive = true;
        yield return new WaitForSeconds(punchWindupTime);
        sr.sprite = punch;
        coIdle = StartCoroutine(AttackAfter(punchDelay));
    }

    IEnumerator AttackAfter(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        attackActive = false;

        sr.sprite = idle1;
        coIdle = StartCoroutine(IdleLoop());
    }



}
