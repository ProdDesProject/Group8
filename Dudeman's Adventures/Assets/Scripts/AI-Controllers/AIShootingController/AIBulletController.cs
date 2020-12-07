using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBulletController : MonoBehaviour
{
    public float speed;

    private Transform player;
    private Vector2 target;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyBullet();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<RestartOnPlayerDeath>().TakeDamage(5);
            Destroy(this.gameObject);
        }
        else
        {
            DestroyBullet();
        }
        
    }

    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }

}
