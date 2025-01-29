using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float timer;
    
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.right = rb.velocity;
        timer += Time.deltaTime;
        if(timer>=5)
            Destroy(gameObject);
    }
    
    /*void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.tag == "Enemy")
            {
                Debug.Log("hit");
            }
          
        }*/
   /* void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("hit");
        }
    }*/
}
