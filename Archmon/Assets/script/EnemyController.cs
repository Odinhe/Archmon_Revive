using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //set all the intergers
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private int monsterHp;
    [SerializeField] private float delay = 2f;
    [SerializeField] private LayerMask playerLayer;

    private float cooldownTimer = Mathf.Infinity;
    private Animator anim;
    public float moveSpeed = 2;
    public Transform player;
    public bool hitP = false;
    private Vector3 originalPosition;
    private bool isAttacking = false; // Flag to indicate whether the enemy is currently attacking
    private int damagetaken;
    public GameObject BlueGem;
    private Vector3 initialPosition;
    private bool death_tri = true;
    private bool isMoving = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        originalPosition = transform.position;
        isMoving = true;
    }

    private void Update()
    {
        bool blueGemInstantiated = false;
        anim.SetBool("walk", false);
        damagetaken += monsterDamageManager.Instance.DamageT;
        monsterDamageManager.Instance.resetDamage();
        initialPosition = transform.position;
        cooldownTimer += Time.deltaTime;
        //if monster don't have hp, they will die
        if (monsterHp < damagetaken && death_tri == true && !blueGemInstantiated)
        {
            isMoving = false;
            death_tri = false;
            anim.SetBool("death", true);
            Instantiate(BlueGem, initialPosition, Quaternion.identity);
            blueGemInstantiated = true;
            MoneyManager.Instance.AddMoney(20);
            Invoke("DestroyGameObject", delay);
        }
        if (!isAttacking)
        {
            //Attack when player is seen by the monster
            if (PlayerInSight())
            {
                if (cooldownTimer >= attackCooldown)
                {
                    anim.SetBool("walk", false);
                    cooldownTimer = 0;
                    if(hitP == true)
                    {
                      StartCoroutine(AttackCoroutine());
                    }
                }
            }
            else
            {
                //move back
                transform.position = Vector3.Lerp(transform.position, originalPosition, moveSpeed * Time.deltaTime);
            }
        }

        // adjest the enemy's position based on player position
        if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-7, 7, 7);
        }
        else
        {
            transform.localScale = new Vector3(7, 7, 7);
        }
    }
    //triggers when pmonster attack
    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        //play the animation
        anim.SetTrigger("meleeAttack");
        yield return new WaitForSeconds(0.7f);
        if (hitP)
        {
            //if hit, player take damage
            DamagePlayer();
        }
        isAttacking = false;
        hitP = false;
    }

    private bool PlayerInSight()
    {
        //check if player is in the area that monster was
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        if (hit.collider != null && isMoving == true)
        {
            //if is, monster start walking
            Vector3 direction = player.position - transform.position;
            anim.SetBool("walk",true);
            direction.Normalize();
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        //draw a box to see hit box
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        //player take damage
        if (PlayerInSight())
        PlayerHPManager.Instance.ReduceHp(damage);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //check collider
        if (collision.gameObject.CompareTag("Player"))
        {
            hitP = true;
        }
    }
    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}



