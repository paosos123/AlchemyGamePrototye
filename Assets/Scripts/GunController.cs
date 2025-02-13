using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Animator gunAnim;

    [SerializeField] private Transform gun;
    
    [SerializeField] private  float gunDistance = 1.5f;
    [SerializeField] private Transform bulletTranform;
    private bool gunFacingRight = true;
    private float timer;
    [Header("Bullet")] 
    [SerializeField] private GameObject bulletPerfab;
     public  static  float spawnBulletTime = 2f;
    [SerializeField] private float bulletSpeed;
    private AudioManager _audioManager;
    // Start is called before the first frame update
    void Start()
    { _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        gun.rotation=Quaternion.Euler(new Vector3(0,0,Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg ));
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gun.position = transform.position + Quaternion.Euler(0, 0, angle) * new Vector3(gunDistance, 0, 0);
        GunFlipController(mousePos);
        bulletTranform.rotation = Quaternion.Euler(0, 0, angle);

        if (Input.GetKeyDown(KeyCode.Mouse0)&&(timer > spawnBulletTime))
        {
            timer = 0;
           Shoot(angle);
        }
    }

    private void GunFlip()
    {
        gunFacingRight = !gunFacingRight;
        gun.localScale = new Vector3(gun.localScale.x, gun.localScale.y*-1,gun.localScale.z);
    }

    
    public void Shoot(float angle)
    {
    //    gunAnim.SetTrigger("Shoot");
        _audioManager.PlaySFX(_audioManager.shoot);
        GameObject bulletClone = Instantiate(bulletPerfab);
        bulletClone.transform.position = bulletTranform.position;
        bulletClone.transform.rotation = Quaternion.Euler(0, 0, angle);

        bulletClone.GetComponent<Rigidbody2D>().velocity = bulletTranform.right * bulletSpeed;
      
    }

    private void GunFlipController(Vector3 mousePos)
    {
        if (mousePos.x < gun.position.x && gunFacingRight)
            GunFlip();
        else if(mousePos.x>gun.position.x && !gunFacingRight)
            GunFlip();
    }
  
}
