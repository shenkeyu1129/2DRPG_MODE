using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;
    public static ObjectPoolManager Instance;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // 初始化
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            GameObject parent = new GameObject(pool.tag + "_Pool");
            parent.transform.SetParent(transform);
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.SetParent(parent.transform);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    //获取对象
    public GameObject SpawnFromPool(string tag,Vector3 position,Quaternion rotation)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            return null;
        }
        Queue<GameObject> objetPool = poolDictionary[tag];
        if(objetPool.Count == 0)
        {
            Pool poolInfo = pools.Find(p => p.tag == tag);
            if(poolInfo != null)
            {
                GameObject obj = Instantiate(poolInfo.prefab);
                obj.transform.SetParent(transform.Find(tag + "_Pool"));
                return SetupPooledObject(obj, position, rotation);
            }
            return null;
        }

        GameObject pooledObject = objetPool.Dequeue();
        return SetupPooledObject(pooledObject, position, rotation);
    }
    // 对象设置和激活
    private GameObject SetupPooledObject(GameObject obj, Vector3 position, Quaternion rotation)
    {
        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        // 获取IPoolable接口并调用OnObjectSpawn
        IPoolable poolableObj = obj.GetComponent<IPoolable>();
        if (poolableObj != null)
        {
            poolableObj.OnObjectSpawn();
        }

        return obj;
    }

    // 返回对象到池
    public void ReturnToPool(string tag, GameObject obj)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return;
        }

        // 重置对象状态
        obj.SetActive(false);

        // 返回到队列
        poolDictionary[tag].Enqueue(obj);
    }
}

// 池对象需要实现的接口
public interface IPoolable
{
    void OnObjectSpawn();

}
