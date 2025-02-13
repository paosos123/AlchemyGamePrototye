using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class enemyPatrol1 : MonoBehaviour
{
    [SerializeField] private GameObject pointA;

    [SerializeField] private GameObject pointB;
    [SerializeField] private float speed;
    [SerializeField] private float speedChase;
    private Rigidbody2D rb;
    private Transform playerTransform;
    private bool isChasing;
    [SerializeField] private float chaseDistance;
    private Animator anim;
    [SerializeField] private float health;
    private Transform currentPoint;
    [SerializeField]private GameObject player;
    private AudioManager _audioManager;
    // Start is called before the first frame update
    void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
       // anim.SetBool("isRunning",true);
       player =GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
       playerTransform = player.transform;
        if (isChasing)
        {
            if (transform.position.x > playerTransform.position.x)
            {
                transform.localScale = new Vector3(-1.3094f,1.3094f,1.3094f);
              
                transform.position += Vector3.left*speedChase*Time.deltaTime;
            }
            if (transform.position.x < playerTransform.position.x)
            {
                transform.localScale = new Vector3(1.3094f,1.3094f,1.3094f);
                transform.position += Vector3.right*speedChase*Time.deltaTime;
            }
            if (Vector2.Distance(transform.position, playerTransform.position)> chaseDistance)
            {
                isChasing = false;
            }
          
        }
        else
        {
           
            if (Vector2.Distance(transform.position, playerTransform.position)< chaseDistance)
            {
                isChasing = true;
            }
            Vector2 point = currentPoint.position - transform.position;
            if (currentPoint == pointB.transform)
            {
                if (transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(1.3094f,1.3094f,1.3094f);
                }
                rb.velocity = new Vector2(speed, 0);
            }   
            else
            {
                if (transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3(-1.3094f,1.3094f,1.3094f);
                }
                rb.velocity = new Vector2(-speed, 0);
            }
        
            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
            {
                // Switch to the other point
                if (currentPoint == pointB.transform)
                {
                    currentPoint = pointA.transform;
                    flip(); // Call flip function when changing direction
                }
                else
                {
                    currentPoint = pointB.transform;
                    flip(); // Call flip function when changing direction
                }
            }
       
        }

        if (health <= 0)
        {
            _audioManager.PlaySFX(_audioManager.enemyDead);
            MoveController.WaterEleC+=1;
            Destroy(gameObject);
        }
    }
    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }   
    private void OnDrawGizmos()
    {
        if (pointA != null && pointB != null) // Check if references are set to avoid errors
        {
            Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
            Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
            Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "PlayerBullet")
        {
            health -= MoveController.damageBullet;
            _audioManager.PlaySFX(_audioManager.enemyHit);
            Destroy(col.gameObject);
        }
    }
}
