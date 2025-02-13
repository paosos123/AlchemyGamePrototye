using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class enemyPatrol : MonoBehaviour
{
    [SerializeField] private GameObject pointA;

    [SerializeField] private GameObject pointB;
    [SerializeField] private float speed;
    private Rigidbody2D rb;

    private Animator anim;
    [SerializeField] private float health;
    private Transform currentPoint;
    private AudioManager _audioManager;
    // Start is called before the first frame update
    void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
       // anim.SetBool("isRunning",true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
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

        if (health <= 0)
        {
            _audioManager.PlaySFX(_audioManager.enemyDead);
            MoveController.EarthEleC+=1;
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
