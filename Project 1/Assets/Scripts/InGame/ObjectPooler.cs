using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    #region SINGLETON
    private static ObjectPooler _instance;
    public static ObjectPooler Instance 
    {
        get { return _instance; }
        set { _instance = value; }
    }
    private void Awake()
    {
        _instance = this;
    }
    #endregion
    [System.Serializable]
    public struct Pool 
    {
        public TypeObjectPool typeObjectPool;
        public GameObject prefab;
        public int size;
    }
    public enum TypeObjectPool
    {
        Bullet,
        MuzzleEffect,
        humanImpact,
        rockImpact,
        woodImpact,
        metalImpact

    }
    [SerializeField]
    private List<Pool> pools = new List<Pool>();
    Dictionary<TypeObjectPool, Queue<GameObject>> poolDictionary = new Dictionary<TypeObjectPool, Queue<GameObject>>();
    private void Start()
    {
        foreach (var pool in pools)
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.SetParent(gameObject.transform);
                obj.SetActive(false);
                queue.Enqueue(obj);
            }
            poolDictionary.Add(pool.typeObjectPool,queue);
        }
    }
    public GameObject SpawnObjectPool(TypeObjectPool typeObjectPool,Vector3 pos,Quaternion quaternion)
    {
        GameObject obj = poolDictionary[typeObjectPool].Dequeue();
        obj.SetActive(true);
        obj.transform.position = pos;
        obj.transform.rotation = quaternion;
        poolDictionary[typeObjectPool].Enqueue(obj);
        return obj;
    }
}
