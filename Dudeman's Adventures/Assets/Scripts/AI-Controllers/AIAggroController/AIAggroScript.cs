﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAggroScript : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    float aggroRange;

    [SerializeField]
    float moveSpeed;

    [SerializeField]
    Transform castPoint;

    [SerializeField]
    bool isFacingLeft = true; // checkbox for the dev when placing an AI if it is facing Left or not !!! VERY IMPORTANT !!!

    Vector3 baseScale;

    Rigidbody2D rb2d;

    private bool isAggro = false;
    private bool isSearching = false;
    // Start is called before the first frame update
    void Start()
    {
        baseScale = transform.localScale;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 10f;     
    }

    // Update is called once per frame
    void Update()
    {
        if(DetectPlayer(aggroRange))
        {
            isAggro = true;
        }
        else
        {
            if(isAggro == true)
            {
                if(isSearching == false)
                {
                    isSearching = true;
                    //Player will be chased for 2 seconds and after that the AI stops. 
                    //Might wanna do a coroutine for this in the future.
                    Invoke("StopChasingPlayer", 2);
                }
            }
        }

        if (isAggro == true)
        {
            ChasePlayer();
        }
        // This code below is for a 'Dumber AI' that will chase the player even if they are
        // behind a wall. I am just leaving this here because don't know yet how we want to use the aggro mechanic.
       /* float distToPlayer = Vector2.Distance(transform.position, player.position);
        transform.Translate(0, 0, 0);
        
        if(distToPlayer < aggroRange)
        {
            ChasePlayer();
        }
        else
        {
            StopChasingPlayer();
        }*/
    }

    bool DetectPlayer(float distance)
    {
        bool val = false;
        float castDist = distance;

        if(isFacingLeft)
        {
            castDist = -distance;
        }

        Vector2 endPos = castPoint.position + Vector3.right * castDist;

        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Player"));

        if(hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else 
            {
                val = false;
            }

            Debug.DrawLine(castPoint.position, endPos, Color.blue);
        }
        
        else
        {
            Debug.DrawLine(castPoint.position, endPos, Color.red);
        }

        return val;

    }

    void ChasePlayer()
    {
        Vector3 newScale = baseScale;
        
        if(transform.position.x < player.position.x)
        {
            rb2d.velocity = new Vector2(moveSpeed, 0);
            newScale.x = -baseScale.x;
            isFacingLeft = false;
            transform.localScale = newScale;
        }
        else
        {
            rb2d.velocity = new Vector2(-moveSpeed, 0);
            newScale.x = baseScale.x;
            isFacingLeft = true;
            transform.localScale = newScale;
        }
        
    }

    void StopChasingPlayer()
    {
        isAggro = false;
        isSearching = false;
        rb2d.velocity = new Vector2(0, 0);
    }

    bool IsNearEdge()
    {
        bool val = true;

        float castDist = aggroRange;
        
        Vector3 targetPos = castPoint.position;
        targetPos.y -= castDist;

        Debug.DrawLine(castPoint.position, targetPos, Color.green);

        if(Physics2D.Linecast(castPoint.position, targetPos, 1 << LayerMask.NameToLayer("Terrain")))
        {
            val = false;
        }
        else
        {
            val = true;
        }

        return val;
    }

    //CollisionStay is used instead of Enter because of the zombie like nature of the mobs.
    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.name == "Player")
        {
             GameObject.Find("Player").GetComponent<RestartOnPlayerDeath>().TakeDamage(1);
        }

    }
    

}