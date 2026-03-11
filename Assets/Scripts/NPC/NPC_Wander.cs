using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Wander : MonoBehaviour
{
    [Header("Wander_Area")]
    public float wanderWidth = 5f;
    public float wanderHeight = 5f;
    public Vector2 startPosition;
    public float speed = 2;
    public float pasueDuration = 1.5f;
    public Vector2 target;
    private Rigidbody2D rb;
    private bool isPaused;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(PauseAndPickNewDestination());
    }
    private void Update()
    {
        if(isPaused)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        if(Vector2.Distance(transform.position,target)<0.1f)
        {
            StartCoroutine(PauseAndPickNewDestination());
        }
        Move();
    }

    IEnumerator PauseAndPickNewDestination()
    {
        isPaused = true;
        anim.Play("Idle");
        yield return new WaitForSeconds(pasueDuration);
        target = GetRandomTarget();
        isPaused = false;
        anim.Play("Walk");
    }

    private void Move()
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        if (direction.x < 0 && transform.localScale.x > 0 || direction.x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        rb.velocity = direction * speed;
    }
    private Vector2 GetRandomTarget()
    {
        float halfWidth = wanderWidth / 2;
        float halfHeight = wanderHeight / 2;
        int edge = Random.Range(0, 4);
        if (edge == 0)
        {
            // ×ó±ß
            return new Vector2(
                startPosition.x - halfWidth,
                Random.Range(startPosition.y - halfHeight, startPosition.y + halfHeight)
            );
        }
        else if (edge == 1)
        {
            // ÓÒ±ß
            return new Vector2(
                startPosition.x + halfWidth,
                Random.Range(startPosition.y - halfHeight, startPosition.y + halfHeight)
            );
        }
        else if (edge == 2)
        {
            // ÏÂ±ß
            return new Vector2(
                Random.Range(startPosition.x - halfWidth, startPosition.x + halfWidth),
                startPosition.y - halfHeight
            );
        }
        else
        {
            // ÉÏ±ß
            return new Vector2(
                Random.Range(startPosition.x - halfWidth, startPosition.x + halfWidth),
                startPosition.y + halfHeight
            );
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(startPosition, new Vector3(wanderWidth, wanderHeight, 0));
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(!enabled)return;
        StartCoroutine(PauseAndPickNewDestination());

    }
}
