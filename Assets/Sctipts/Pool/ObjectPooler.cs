using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    #region Singleton
    public static ObjectPooler Instance;
    private void Awake() {
        Instance = this;
    }
    #endregion

    [System.Serializable]
    public class Pool {
        public string tag;
        public GameObject prefab;
        public int size;

    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        //Наполняем пул
        foreach (var pool in pools) {
            //для каждого пула создаем коллекцию
            Queue<GameObject> objectPool = new Queue<GameObject>();
            // наполняем ее объектами
            for(int i=0; i < pool.size; i++) {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            // Сохраняем созданный и наполненный пул в коллекцию
            poolDictionary.Add(pool.tag, objectPool);
        }

    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation) {
        // Если нет искомого пула, то ничего не делаем
        if (!poolDictionary.ContainsKey(tag)) {
            Debug.LogWarning("Pool with tag " + tag + " doesn`t exist!");
            return null;
        }
        
        GameObject objectToSpawn =  poolDictionary[tag].Dequeue();        
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
       
        return objectToSpawn;
    }

    //Возвращаем в пул
    public void ReturnToPool(string tag, GameObject obj) {
        Debug.Log( tag + " " +poolDictionary[tag].Count);
        //Если нет такого объекта, возвращаем его в пул
        if (!poolDictionary[tag].Contains(obj)) {
            obj.SetActive(false);
            obj.transform.parent = transform;
            obj.transform.position = transform.position;

            poolDictionary[tag].Enqueue(obj);

            Debug.Log(poolDictionary[tag].Count);
        }

        


    }

    

    

   
}
