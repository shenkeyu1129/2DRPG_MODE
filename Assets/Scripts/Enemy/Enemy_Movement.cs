using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    public float playerDetectRange = 5;
    public Transform detectionPoint;
    public LayerMask playerLayer;
    private Rigidbody2D rb;
    private Transform player;
    public float attackCoolDown = 2;
    private float attackCoolDownTimer;
    public float speed;
    public float AttackRange =1.5f;
    private int facingDrection = -1;
    private Animator anim;
    private EnemyState enemystate,newstate;
    // Start is called before the first frame update
    void Start()
    {

        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        if(enemystate != EnemyState.KncokBack)
        {
            CheckPlayer();
            if (attackCoolDownTimer > 0)
            {
                attackCoolDownTimer -= Time.deltaTime;
            }
            if (enemystate == EnemyState.Chasing)
            {
                Chase();
            }
            else if (enemystate == EnemyState.Attacking)
            {
                rb.velocity = Vector2.zero;
            }
        }
       

    }
    private void CheckPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position,playerDetectRange,playerLayer);
        if (hits.Length > 0)
        {
            player = hits[0].transform;
            if (Vector2.Distance(transform.position, player.position) <= AttackRange && attackCoolDownTimer <= 0)
            {
                attackCoolDownTimer = attackCoolDown;
                ChangeState(EnemyState.Attacking);
            }
            else if(Vector2.Distance(transform.position, player.position)> AttackRange && enemystate !=EnemyState.Attacking)
            {
                ChangeState(EnemyState.Chasing);
            }
            
        }
        else
        {
            rb.velocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
        
           

    }
    void Chase()
    {
       
        if (player.position.x > transform.position.x && facingDrection == 1 || player.position.x < transform.position.x && facingDrection == -1)
        {
            Flip();
        }
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }
    /// <summary>
    /// ×óÓÒ·­×ª
    /// </summary>
    void Flip()
    {
        facingDrection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    public void ChangeState(EnemyState newstate)
    {
        if(enemystate == EnemyState.Idle)
        {
            anim.SetBool("Idle", false);
        }
        else if(enemystate == EnemyState.Chasing)
        {
            anim.SetBool("Chasing", false);
        }
        else if (enemystate == EnemyState.Attacking)
        {
            anim.SetBool("Attacking", false);
        }
        enemystate = newstate;
        if (enemystate == EnemyState.Idle)
        {
            anim.SetBool("Idle", true);
        }
        else if (enemystate == EnemyState.Chasing)
        {
            anim.SetBool("Chasing", true);
        }
        else if (enemystate == EnemyState.Attacking)
        {
            anim.SetBool("Attacking", true);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionPoint.position, playerDetectRange);
    }
}
public enum EnemyState
{
    Idle,
    Chasing,
    Attacking,
    KncokBack
}
