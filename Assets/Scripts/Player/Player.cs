using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;   

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb; 
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    [SerializeField] private float maxHp = 100f;
    private float currentHp;
    [SerializeField] private Image hpBar;
    [SerializeField] private GameManager gameManager;
 
    private float horizontalMove;
    private float verticalMove;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        
    }
    void Start()
    {
        currentHp = maxHp;
        UpdateHpBar();  
    }
    void Update()
    {
        MovePlayer();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gameManager.PauseMenu();
        }
    }
    void MovePlayer()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

    
        Vector2 playerInput = new Vector2(horizontalMove, verticalMove);
        Vector2 playerVelocity = playerInput.normalized * speed;
        rb.linearVelocity = playerVelocity;
        if(playerInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (playerInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        if (playerInput != Vector2.zero)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }
    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Max(currentHp, 0);
        if(currentHp <= 0)
        { 
            Die(); 
        }
        UpdateHpBar();
    }
    public void Heal(float healAmount)
    {
        if(currentHp < maxHp)
        {
            currentHp += healAmount;
            currentHp = Mathf.Min(currentHp, maxHp);
            UpdateHpBar();
        }
    }
    public void Die()
    { 
        gameManager.GameOverMenu();
    }
    private void UpdateHpBar()
    {
        if(hpBar!=null)
        {
            hpBar.fillAmount = currentHp / maxHp;
        }
    }

}
