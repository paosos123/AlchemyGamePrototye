using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPerfabs;
    [SerializeField] private Transform bulletPos;
    [SerializeField] private float spawneBulletTime;
    [SerializeField] private float distanceAttack;
    [SerializeField]private GameObject player;
    [SerializeField] private int health;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        player =GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float distance = Vector2.Distance(transform.position, player.transform.position);
        
        if (distance < distanceAttack)
        {
            timer += Time.deltaTime;
            if (timer > spawneBulletTime)
            {
                timer = 0;
                Shooting();
                
            }
        }
       
    }
    private  void Shooting()
    {
        Instantiate(bulletPerfabs, bulletPos.position,Quaternion.identity);
    }

   
    void OnTriggerEnter2D(Collider2D col)
    {
            if(col.gameObject.tag == "PlayerBullet")
            {
                Destroy(gameObject);
            }
    }
}
