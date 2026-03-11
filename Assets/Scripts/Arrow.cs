using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour,IPoolable
{
    public LayerMask enemyLayer;
    public LayerMask obstacleLayer;
    public SpriteRenderer sr;
    public Sprite buriedSprite;
    public Sprite initialSprite;
    public Rigidbody2D rb;
    public Vector2 direction = Vector2.right;
    public float lifeSpawn = 5;
    public float speed;
    public int damge;
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;
    private string poolTag = "Arrow";
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
       
    }
    private void Update()
    {
        rb.velocity = direction * speed;
        RotateArrow();
    }
    private void RotateArrow()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if((enemyLayer.value &(1 << collision.gameObject.layer))>0)
        {
            collision.gameObject.GetComponent<EnemyHealth>().ChangeHealth(-damge);
            collision.gameObject.GetComponent<EnemyKnockBack>().KnockBack(transform, knockbackForce, knockbackTime, stunTime);
            //AttachToTarget(collision.transform);
            ReturnToPool();
        }
        else if ((obstacleLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            //AttachToTarget(collision.transform);
            ReturnToPool();
        }
        
    }
    private void AttachToTarget(Transform target)
    {
        sr.sprite = buriedSprite;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        transform.SetParent(target);
    }
    //实现接口
    public void OnObjectSpawn()
    {
        // 设置自动返回
        StartCoroutine(ReturnAfterLifetime());
    }
    private IEnumerator ReturnAfterLifetime()
    {
        yield return new WaitForSeconds(lifeSpawn);
        ReturnToPool();
    }


    private void ReturnToPool()
    {
        // 返回到池中
        ObjectPoolManager.Instance.ReturnToPool(poolTag, gameObject);
    }


}
