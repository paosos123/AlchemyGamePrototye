using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveController : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed;
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
    [SerializeField] private int curHealthPot =2;
    [SerializeField] private  TextMeshProUGUI counterHealthPotText;
    [Header("HealthBar")] 
    [SerializeField] private List<Image> allHeart;
    [SerializeField] private Sprite fillHeart;
    [SerializeField] private Sprite emptyHeart;
    
    private Animator animator;
    static public int damageBullet = 1;
    private float xInput;
    private Vector2 vecGravity;

    private bool isJumping;
    private float jumpCounter;
    private bool facingRight = true;
    // Start is called before the first frame update
    private void Start()
    {
        vecGravity = new Vector2(0, -Physics.gravity.y);
        rb = GetComponent<Rigidbody2D>();
        curHealth = maxHealth;
        curHealthPot = maxHealthPot;
        animator = GetComponent<Animator>();
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
    /*static  IEnumerator GetHurt()
    {
        Physics2D.IgnoreLayerCollision(7,8);
        yield return new WaitForSeconds(2);
        Physics2D.IgnoreLayerCollision(7,8,false);
    }*/
   
}
