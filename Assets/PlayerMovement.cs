using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveAxis;
    [SerializeField] private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveVector = new Vector2 (moveSpeed * moveAxis * Time.deltaTime, rb.velocity.y);
        rb.velocity = moveVector;
    }

    void OnMovement(InputValue inputValue)
    {
        moveAxis = inputValue.Get<float>();
        //Debug.Log(inputValue.Get<float>()); 
            
    }

}
