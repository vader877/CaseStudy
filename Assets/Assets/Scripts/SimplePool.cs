using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;



public class SimplePool : MonoBehaviour
{
    [System.Serializable]
    public class PoolObject
    {
        public string name;
        public GameObject type;
        public int poolSize;

        //public int maxCount { get; set; }
        //public int minCount { get; set; }
    }

    public List<PoolObject> ObjectTypes;
    public Dictionary<string, ConcurrentQueue<GameObject>> SceneObjectPool;

    public static SimplePool Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneObjectPool = new Dictionary<string, ConcurrentQueue<GameObject>>();

        foreach (PoolObject temp in ObjectTypes)
        {
            ConcurrentQueue<GameObject> queue = new ConcurrentQueue<GameObject>();
            for(int i = 0; i < temp.poolSize; i++)
            {
                GameObject obj = Instantiate(temp.type);
                obj.GetComponent<BasicLaneObject>().poolable = true;
                queue.Enqueue(obj);
                obj.SetActive(false);
            }
            SceneObjectPool.Add(temp.name, queue);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position)
    {
        GameObject obj = null;

        for (int x = 0; x < SceneObjectPool[tag].Count; x++)
        {
            SceneObjectPool[tag].TryDequeue(out obj);
            if (obj.GetComponent<BasicLaneObject>().poolable)
            {
                obj.SetActive(true);
                obj.transform.position = position;
                obj.GetComponent<BasicLaneObject>().Spawn();
                SceneObjectPool[tag].Enqueue(obj);
                break;
            }
            SceneObjectPool[tag].Enqueue(obj);

        }

        return obj;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
