using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MoveController : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]public static float moveSpeed = 6f;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpTime;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float JumpMultiplier;
    [SerializeField] private Transform groundCheck;
    [SerializeField]private CapsuleCollider2D capsuleCollider;
    [SerializeField] private LayerMask groundLayer;
    [Header("Health")] 
    public int curHealth = 5;
    [SerializeField] private int maxHealth=5;
    [SerializeField] private int healHealth=2;
    [SerializeField] private int maxHealthPot =2;
    [SerializeField] public  static int curHealthPot =2;
    [SerializeField] private  TextMeshProUGUI counterHealthPotText;
    [Header("HealthBar")] 
    [SerializeField] private List<Image> allHeart;
    [SerializeField] private Sprite fillHeart;
    [SerializeField] private Sprite emptyHeart;
    [Header("Element")]
    [SerializeField] private  TextMeshProUGUI counterFireEle;
    [SerializeField] private  TextMeshProUGUI counterWaterEle;
    [SerializeField] private  TextMeshProUGUI counterEarthEle;
    static  public int FireEleC, WaterEleC, EarthEleC = 0;
    private Animator animator;
    static public float damageBullet = 1;
    private float xInput;
    private Vector2 vecGravity;
    private bool isHit = false;
    private bool isJumping;
    private float jumpCounter;
    private bool facingRight = true;
    [SerializeField] private  GameObject gameOverPanel;

    private AudioManager _audioManager;
    // Start is called before the first frame update
    private void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        vecGravity = new Vector2(0, -Physics.gravity.y);
        rb = GetComponent<Rigidbody2D>();
        curHealth = maxHealth;
       
        animator = GetComponent<Animator>();
        if (SceneManager.GetActiveScene().name == "First")
        {
             moveSpeed = 6f;
             damageBullet = 1;
             FireEleC = 0;
             WaterEleC  = 0;
             EarthEleC = 0;
             curHealthPot =2;
             GunController.spawnBulletTime = 2f;
             Upgrade.lvFire = 1;
             Upgrade.lvWater = 1;
             Upgrade.lvEarth = 1;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        FlipController();
        xInput = Input.GetAxisRaw("Horizontal");
        MovementX();
        if (Input.GetKeyDown(KeyCode.Space)&& isGrounded()) 
            Jump();
      
        if (rb.velocity.y > 0 && isJumping )
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime) isJumping = false;

            float t = jumpCounter / jumpTime;
            float currentJumpm = JumpMultiplier;
            if (t > 0.5f)
            {
                currentJumpm = JumpMultiplier * (1 - t);
            }
            rb.velocity += vecGravity * currentJumpm * Time.deltaTime;
        }
        FallMuit();
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            jumpCounter = 0;
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.6f);
            }
        }

        if (isDead())
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            Destroy(gameObject);
        }
        Heal();

        foreach (Image img in allHeart)
        {
            img.sprite = emptyHeart;
        }
        
        for (int i = 0; i < curHealth; i++)
        {
            allHeart[i].sprite = fillHeart;
        }
        animator.SetBool("isJumping",!isGrounded());
        UpdateText();
        
        counterFireEle.text = (FireEleC).ToString();
        counterWaterEle.text = (WaterEleC).ToString();
        counterEarthEle.text = (EarthEleC).ToString(); 
      
    }

    private void FixedUpdate()
    {
        animator.SetFloat("xVelocity" ,Mathf.Abs(rb.velocity.x) );
        animator.SetFloat("yVelocity" ,rb.velocity.y);
    }

    private  bool  isGrounded()
    {
        return  Physics2D.OverlapCapsule(groundCheck.position, new Vector2(capsuleCollider.size.x, capsuleCollider.size.y),CapsuleDirection2D.Horizontal,0,groundLayer);
    }
   
    private void Jump()
    {
        _audioManager.PlaySFX(_audioManager.jump);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isJumping = true;
        jumpCounter = 0;
        animator.SetBool("isJumping", !isGrounded());
    }

    private void FallMuit()
    {
        if (rb.velocity.y < 0 )
        {
            rb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }
    }
    private void MovementX()
    {
        rb.velocity = new Vector2(xInput*moveSpeed, rb.velocity.y);
    }

    private void FlipController()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < transform.position.x && facingRight)
            Flip();
        else if(mousePos.x > transform.position.x && !facingRight)
            Flip();
    }
    
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0,180,0);
    }

    private bool isDead()
    {
        return curHealth <= 0;
    }

    private void Heal()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (curHealthPot > 0)
            {
                _audioManager.PlaySFX(_audioManager.heal);
                curHealthPot -= 1;
                curHealth += healHealth;
                Debug.Log(curHealth);
                Debug.Log(curHealthPot);
            }
           
        }
    }
    private void UpdateText()
    {
        if (counterHealthPotText != null)
        {
            counterHealthPotText .text = curHealthPot.ToString(); // Or just: clickCount.ToString(); for only the number
        }
        else
        {
            Debug.LogError("Counter Text UI element not assigned!");
        }
    }
      IEnumerator GetHurt()
    {
        Physics2D.IgnoreLayerCollision(7,8);
        GetComponent<Animator>().SetLayerWeight(1,1);
        isHit = true;
        yield return new WaitForSeconds(2);
        GetComponent<Animator>().SetLayerWeight(1,0);
        Physics2D.IgnoreLayerCollision(7,8,false);
        isHit = false;
       
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "FireEle")
        {
            _audioManager.PlaySFX(_audioManager.getEle);
            counterFireEle.text = (FireEleC+=1).ToString(); 
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "WaterEle")
        {
            _audioManager.PlaySFX(_audioManager.getEle);
            counterWaterEle.text = (WaterEleC+=1).ToString(); 
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "EarthEle")
        {
            _audioManager.PlaySFX(_audioManager.getEle);
            counterEarthEle.text = (EarthEleC+=1).ToString(); 
            Destroy(col.gameObject);
        }
        if ((col.gameObject.tag == "EnemyBullet"||col.gameObject.tag == "Enemy")&&isHit==false)
        {
            _audioManager.PlaySFX(_audioManager.getHit);
            curHealth -= 1;
            StartCoroutine(GetHurt());
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.tag == "EnemyBullet"||col.gameObject.tag == "Enemy")&&isHit==false)
        {
            _audioManager.PlaySFX(_audioManager.getHit);
            curHealth -= 1;
            StartCoroutine(GetHurt());
        }
    }

    
}
