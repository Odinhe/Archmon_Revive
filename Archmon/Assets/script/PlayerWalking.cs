using UnityEngine;

public class PlayerWalking : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private int remainingJumps;
    private float attackCooldownTimer;
    public int maxJumps = 2;
    public float attackCooldown = 1f;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    [SerializeField] private float range;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D coll;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        remainingJumps = maxJumps;
        attackCooldownTimer = attackCooldown;
    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && remainingJumps > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            remainingJumps--;
        }

        if (Input.GetKeyDown(KeyCode.F) && attackCooldownTimer <= 0)
        {
            Attack();
            attackCooldownTimer = attackCooldown;
        }
        else
        {
            attackCooldownTimer -= Time.deltaTime;
        }
        if (IsGrounded())
        {
            remainingJumps = maxJumps;
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (dirX != 0)
        {
            anim.SetBool("running", true);
            sprite.flipX = dirX < 0;
        }
        else
        {
            anim.SetBool("running", false);
        }

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
        anim.SetTrigger("Attack");

        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(coll.bounds.size.x * range, coll.bounds.size.y, coll.bounds.size.z), 0, Vector2.left, 0, enemyLayers);

        if (hit.collider != null)
            Debug.Log("hit");
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireCube(coll.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(coll.bounds.size.x * range, coll.bounds.size.y, coll.bounds.size.z));
    }
}
