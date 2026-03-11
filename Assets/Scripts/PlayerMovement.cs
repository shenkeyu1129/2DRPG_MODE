using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public int facingDrection = 1;
    public bool isShooting;
    private bool isKnockedBack;
    public PlayerCombat playerCombat;
    private void Update()
    {
        if (Input.GetButtonDown("Slash") && playerCombat.enabled == true)
        {
            playerCombat.Attack();
        }
    }


    // Update is called once per frame  
    void FixedUpdate()
    {
        if(isShooting == true)
        {
            rb.velocity = Vector2.zero;
        }
        else if(isKnockedBack == false)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            if (horizontal > 0 && transform.localScale.x < 0 || horizontal < 0 && transform.localScale.x > 0)
            {
                Flip();
            }
            anim.SetFloat("horizontal", Mathf.Abs(horizontal));
            anim.SetFloat("vertical", Mathf.Abs(vertical));

            rb.velocity = new Vector2(horizontal, vertical) * StatsManager.Instance.speed;
        }

    }

    /// <summary>
    /// ×óÓÒ·­×ª
    /// </summary>
    void Flip()
    {
        facingDrection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    public void Knockback(Transform enemy,float force,float stuntime)
    {
        isKnockedBack = true;
        Vector2 direction = (transform.position - enemy.position).normalized;
        rb.velocity = direction*force;
        StartCoroutine(KnockbackCounter(stuntime));
    }
    IEnumerator KnockbackCounter(float stuntime)
    {
        yield return new WaitForSeconds(stuntime);
        rb.velocity = Vector2.zero;
        isKnockedBack = false;
    }
}
