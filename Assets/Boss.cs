using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private AudioManager _audioManager;
    [SerializeField] private float health;
    [SerializeField] private GameObject win;
    // Start is called before the first frame update
    void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Time.timeScale = 0;
            win.SetActive(true);
            Destroy(gameObject);
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
