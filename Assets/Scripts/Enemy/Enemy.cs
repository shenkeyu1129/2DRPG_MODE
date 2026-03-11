using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour, IPoolable
{
    private string poolTag = "Enemy";
    public Vector3[] transforms;
    public EnemyHealth enemyHealth;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameObject obj = ObjectPoolManager.Instance.SpawnFromPool(
             poolTag,
             transforms[Random.Range(0, 9)],
             Quaternion.identity);
        }
        if(enemyHealth.currentHealth<=0)
        {
            ReturnToPool();
        }
    }
    
    //实现接口
    public void OnObjectSpawn()
    {
        
    }


    public void ReturnToPool()
    {
        // 返回到池中
        ObjectPoolManager.Instance.ReturnToPool(poolTag, gameObject);
    }
}
