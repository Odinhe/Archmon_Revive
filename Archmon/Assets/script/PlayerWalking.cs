using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct PlayerStats
{
    public int health;
    public int attackDamage;
}
public class PlayerWalking : MonoBehaviour
{
    //set up all the intergers
    public PlayerStats Stats;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private int remainingJumps;
    private int playerAttack;
    private float attackCooldownTimer;
    private float dirX = 0f;

    public int maxJumps = 2;
    public float attackCooldown = 1f;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public TextMeshProUGUI PlayHP;

    [SerializeField] private float range;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D coll;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private void Start()
    {
        //get the component
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        remainingJumps = maxJumps;
        attackCooldownTimer = attackCooldown;
    }

    private void Update()
    {
        //dispalce word on scene
        Stats.health = PlayerHPManager.Instance.HP;
        PlayHP.text = "Player HP: " + Stats.health;
        playerAttack = PlayerAttackManager.Instance.Attack;
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        //when space is pushed, jup, and if ther are remaining jumps, player can jump twice
        if (Input.GetButtonDown("Jump") && remainingJumps > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            remainingJumps--;
        }

        //if f is pressed and attack cool down is ready, player start to attack
        if (Input.GetKeyDown(KeyCode.F) && attackCooldownTimer <= 0)
        {
            Attack();
            attackCooldownTimer = attackCooldown;
        }
        else
        {
            attackCooldownTimer -= Time.deltaTime;
        }
        //reset the jump power when on ground
        if (IsGrounded())
        {
            remainingJumps = maxJumps;
        }

        UpdateAnimationState();

        if(Stats.health <= 0)
        {
           SceneManager.LoadScene(0);
        }
    }

    private void UpdateAnimationState()
    {
        //flip the sprite based on different direction
        if (dirX != 0)
        {
            anim.SetBool("running", true);
            sprite.flipX = dirX < 0;
        }
        else
        {
            anim.SetBool("running", false);
        }
        //set the jump animation
        if (rb.velocity.y > .1f)
        {
            anim.SetBool("jump", true);
            anim.SetBool("fall", false);
        }
        else if (rb.velocity.y < -.1f)
        {
            anim.SetBool("fall", true);
            anim.SetBool("jump", false);
        }
        else
        {
            anim.SetBool("jump", false);
            anim.SetBool("fall", false);
        }


    }

    private void Attack()
    {
        //start playing the attack animation
        anim.SetTrigger("Attack");

        //check if player hit the monster or not
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(coll.bounds.size.x * range, coll.bounds.size.y, coll.bounds.size.z), 0, Vector2.left, 0, enemyLayers);
        //if hit, then deal damage
        if (hit.collider != null)
        {
            Debug.Log("hit");
            monsterDamageManager.Instance.AddDamage(playerAttack);
        }
            

    }

    private bool IsGrounded()
    {
        //check if player is on ground or not
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void OnDrawGizmosSelected()
    {
        //adraw a box to see the player's hit box
        if (attackPoint == null)
            return;

        Gizmos.DrawWireCube(coll.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(coll.bounds.size.x * range, coll.bounds.size.y, coll.bounds.size.z));
    }
}
