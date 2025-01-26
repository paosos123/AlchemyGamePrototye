using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
   [SerializeField]private GameObject player;
    [SerializeField] private float force;  
    [SerializeField] private float timerDestoryitself;
    private Rigidbody2D rb;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player =GameObject.FindWithTag("Player");
        
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        //rot change degatree like this rot +90
        transform.rotation = quaternion.Euler(0,0,rot);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timerDestoryitself)
        {
            Destroy(gameObject);
        }

        
       
    }

     
}
