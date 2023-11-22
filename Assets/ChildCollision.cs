using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCollision : MonoBehaviour
{
    private PlayerMovement pm;
    private string playerTag;
    private string enemyTag;
    private void Start()
    {
        pm=transform.parent.GetComponent<PlayerMovement>();
        playerTag = transform.parent.tag;

        if (playerTag == "Player")
        {
            enemyTag = "Player2";
        }else enemyTag = "Player";

        if(gameObject.tag == "Kick") { pm.kickChild = gameObject; pm.SetChildReference(1); }
        else if (gameObject.tag == "Punch") { pm.punchChild = gameObject; pm.SetChildReference(2); }
        

        


    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == enemyTag)
        {

            pm.ChildCollision(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("stop hit something");
    }

    //private void OnCollisionEnter2D(Collision2D col)
    //{
    //    Debug.Log("collider");
    //    if (col.transform.tag == enemyTag)
    //    {
    //        pm.ChildCollision(gameObject);
    //    }
    //}
}
