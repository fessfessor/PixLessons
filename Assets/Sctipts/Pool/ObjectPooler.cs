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

                // Сразу добавляем объект в коллекцию
                if (!GameManager.Instance.pooledObjectContainer.ContainsKey(obj))
                    GameManager.Instance.pooledObjectContainer.Add(obj, obj.GetComponent<IPooledObject>());

                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            // Сохраняем созданный и наполненный пул в коллекцию
            poolDictionary.Add(pool.tag, objectPool);
        }

    }

   

    // Простой возврат в пул
    public void ReturnToPool(string tag, GameObject obj) {
      //  Debug.Log("Pooler - " +  tag + " " +poolDictionary[tag].Count + " Contains - " + GameManager.Instance.pooledObjectContainer.ContainsKey(obj));
        //Если нет такого объекта, возвращаем его в пул
        if (!poolDictionary[tag].Contains(obj)) {
            //Дополнительные действия для объектов с анимациями и т.п.
            if (GameManager.Instance.pooledObjectContainer.ContainsKey(obj)) {
                GameManager.Instance.pooledObjectContainer[obj].OnReturnToPool();               
            } 
            
            obj.SetActive(false);           
            obj.transform.SetParent(transform);
            obj.transform.position = transform.position;

            poolDictionary[tag].Enqueue(obj);

            //Debug.Log("Pooler after return - " + poolDictionary[tag].Count);
        }
    }



    //Возврат в пул объекта с задержкой
    public void ReturnToPool(string tag, GameObject obj, float delay ) {
        if (!poolDictionary[tag].Contains(obj)) {
            StartCoroutine(ReturnObjWithDelay( tag,  obj, delay));            
        }
    }

    IEnumerator ReturnObjWithDelay(string tag, GameObject obj, float delay) {
        yield return new WaitForSeconds(delay);
        if (GameManager.Instance.pooledObjectContainer.ContainsKey(obj)) {
            GameManager.Instance.pooledObjectContainer[obj].OnReturnToPool();
        }
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        obj.transform.position = transform.position;

        poolDictionary[tag].Enqueue(obj);

    }


    // Обычный спавн
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation) {
        // Если нет искомого пула, то ничего не делаем
        if (!poolDictionary.ContainsKey(tag)) {
            Debug.LogWarning("Pool with tag " + tag + " doesn`t exist!");
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        //Debug.Log("Spawn - " + GameManager.Instance.pooledObjectContainer.ContainsKey(objectToSpawn) + objectToSpawn.transform.name);

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        if (GameManager.Instance.pooledObjectContainer.ContainsKey(objectToSpawn)) {
            GameManager.Instance.pooledObjectContainer[objectToSpawn].OnSpawnFromPool();
        }
        return objectToSpawn;
    }

    

    //Перегрузка с родителем
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation, GameObject parent) {
        // Если нет искомого пула, то ничего не делаем
        if (!poolDictionary.ContainsKey(tag)) {
            Debug.LogWarning("Pool with tag " + tag + " doesn`t exist!");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
       // Debug.Log("Spawn - " + GameManager.Instance.pooledObjectContainer.ContainsKey(objectToSpawn) + " " + objectToSpawn.transform.name);
        objectToSpawn.SetActive(true);
        //Debug.Log("Spawn 2 - " + GameManager.Instance.pooledObjectContainer.ContainsKey(objectToSpawn) + " " + objectToSpawn.transform.name);
        objectToSpawn.transform.position = position;

        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.transform.SetParent(parent.transform, false);

        if (GameManager.Instance.pooledObjectContainer.ContainsKey(objectToSpawn)) {
            GameManager.Instance.pooledObjectContainer[objectToSpawn].OnSpawnFromPool();
        }

        return objectToSpawn;
    }

    //Получаем список тегов пулов
    public HashSet<string> getSetOfNamesObjects(string regex = "") {
        HashSet<string> names = new HashSet<string>();
        if(regex == "") {
            foreach (var pool in pools) {
                names.Add(pool.tag);
            }
        }
        else {
            foreach (var pool in pools) {
                if(pool.tag.Contains(regex))
                    names.Add(pool.tag);
            }
        }
        
        return names;
    }






}
