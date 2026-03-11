using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockBack : MonoBehaviour
{
    public Enemy_Movement enemy_Movement;
    public Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy_Movement = GetComponent<Enemy_Movement>();
    }
    public void KnockBack(Transform playertransfrorm,float knockBackForce, float kncokBackTime,float stunTime)
    {
        enemy_Movement.ChangeState(EnemyState.KncokBack);
        StartCoroutine(Stuntimer(kncokBackTime,stunTime));
        Vector2 direction = (transform.position - playertransfrorm.position).normalized;
        rb.velocity = direction * knockBackForce;
    }
    IEnumerator Stuntimer(float kncokBackTime, float stunTime)
    {
        yield return new WaitForSeconds(kncokBackTime);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        enemy_Movement.ChangeState(EnemyState.Idle);
    }
}
