using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    #region Singleton
    public static SpawnManager Instance;
    private void Awake() {
        Instance = this;
    }
    #endregion

    /*
     *  
     * По мере движения игрока перед ним достраивается уровень
     *  Т.к. есть стена смерти, то можно за ней убирать объекты обратно в пул и использовать повторно, 
     * избежав таким образом создания объектов во время игрового процесса
     * 
     * Так же надо сделать пул врагов. Они будут спавниться на особых точках и после прохождения через стену смерти возвращаться в пул
     * Точки спавна врагов будут заданы заранее
     * 
     * Решить возможную проблему с коннектом платформ друг к другу, т.к. была проблема с определением земли в этом месте
     * 
     * Есть какой-то первоначальный кусок земли, а затем , по мере движения игрока уровень достраивается
     * 
     */
    [SerializeField] private int bossSpawnCount = 10;
    public string isTest = "";


    private ObjectPooler pooler;
    private HashSet<string> groundNames;

    private GameObject firstBlock;
    private GameObject player;

    private GameObject currentLastBlock;
    private Vector3 currentEdge;

    //Количество типов
    private int countOfTypes;

    //Небольшой лог в который будут записываться все заспавненные блоки
    private List<GROUND_TYPE> spawnedGroundLog;


    private float distance;

    private int blockCount = 0;



    void Start()
    {
        

        player = GameManager.Instance.player;
        groundNames = new HashSet<string>();
        spawnedGroundLog = new List<GROUND_TYPE>();
        pooler = ObjectPooler.Instance;
        firstBlock = GameObject.Find("ground_first_block");

        countOfTypes = Enum.GetNames(typeof(GROUND_TYPE)).Length;

        // Текущая граница последнего объекта земли
        currentLastBlock = firstBlock;       
        currentEdge = firstBlock.transform.Find("edge").transform.position;

        //Получаем из пула список доступных префабов для постройки уровня. Ищем по тегу "ground_"
        groundNames = pooler.getSetOfNamesObjects("ground_");

        //foreach (var item in groundNames) {
        //    Debug.Log(item);
        //}

        // У всех кусков карты есть число после подчеркивания, с помощью рандомайзера выбираем кусок карты и достраиваем
        // Идентификаторы
        /*
         * ground_1   - кусок обыкновенной карты
         * ground_boss_1 - кусок с ареной первого босса
         * 
         */

        

        StartCoroutine(checkDistance());
    }

    
    void Update()
    {
        distance = Mathf.Abs(player.transform.position.x - currentEdge.x);

        //Debug.Log($"Distance - {distance}");

    }

    IEnumerator checkDistance() {
        while (true) {
            yield return new WaitForSeconds(0.5f);
            if (distance < 20) {
                SpawnLogic();               
            }
        }
        
    }

   



    private void SpawnGround(GROUND_TYPE type, int count = 1) {
        string blockName;
        GameObject block;
        List<string> groundType = new List<string>();

        foreach (var item in groundNames) {
            if (item.Contains(type.ToString()))
                groundType.Add(item);

        }
        //По дефолту спавнится 1 блок, но можно указать и несколько
        // Из нового списка с однотипными блоками выбираем случайный, для того чтобы его заспавнить
        for (int i = 0; i < count; i++) {
            blockName = groundType[Random.Range(0, groundType.Count)];
            if(isTest == "")
                block = pooler.SpawnFromPool(blockName, new Vector3(0, 0, -20), Quaternion.identity);
            else
                block = pooler.SpawnFromPool(isTest, new Vector3(0, 0, -20), Quaternion.identity);

            block.transform.position = currentEdge;

            //Край блока
            var blockEdge = block.transform.Find("edge").transform.position;

            currentLastBlock = block;
            currentEdge = blockEdge;


            spawnedGroundLog.Add(type);
            blockCount++;

            // Сообщаем что заспавнили блок земли
            Notification(block);
        }
        Debug.Log($"Спавним - {spawnedGroundLog.Last().ToString()}");
        Debug.Log($"Блоков до босса - {blockCount}");
    }



    private void SpawnLogic() {
        /* Пока что логика будет ограничиваться тем что нельзя заспавнить больше 2 в ряд одинаковых блоков
         * И тем что босс будет появляться каждые сколько то блоков 
         * 
         * 
         */
        if(blockCount < bossSpawnCount){
            if(spawnedGroundLog.Count > 2) {
                //Если уже есть с чем сравнивать, то смотрим на 2 последних блока и если они одинакового типа, не спавним такой же 3
                var lastBlockType = (spawnedGroundLog[spawnedGroundLog.Count - 1]);
                var preLastBlockType = spawnedGroundLog[spawnedGroundLog.Count - 2];
                //Если 2 блока одинаковые спавним третий иной
                if (lastBlockType == preLastBlockType) {
                    SpawnGround(GetRandomGround(false,lastBlockType));
                    //Debug.Log("Последние 2 блока одинаковые, спавним 3 другой!");
                }else
                    SpawnGround(GetRandomGround());
                
            }else
                SpawnGround(GetRandomGround());



        }else{ // Если время спавнить босса 
            if(blockCount < bossSpawnCount + 2) {
                if (!RandomPercent(25))
                    SpawnGround(GetRandomGround());
                else {
                    SpawnGround(GROUND_TYPE.withBoss);
                    blockCount = 0;
                }                   
            }
            else if(blockCount < bossSpawnCount + 3) {
                if (!RandomPercent(50))
                    SpawnGround(GetRandomGround());
                else {
                    SpawnGround(GROUND_TYPE.withBoss);
                    blockCount = 0;
                }
            }
            else if (blockCount < bossSpawnCount + 5) {
                if (!RandomPercent(75))
                    SpawnGround(GetRandomGround());
                else {
                    SpawnGround(GROUND_TYPE.withBoss);
                    blockCount = 0;
                }
            }
            else if (blockCount < bossSpawnCount + 7) {             
                    SpawnGround(GROUND_TYPE.withBoss);
            }

        }
        

    }


    //Иммитация вероятностей от 0 до 100%, передаем желаемую вероятность и получаем результат, попали или нет
    public static bool RandomPercent(int persent) {
        int rnd = Random.Range(0, 100);
        Debug.Log($"ROLL - {rnd}. PERSENT  - {persent}");
        if (rnd <= persent) 
            return true;        
        else
            return false;
        
    }

    private GROUND_TYPE GetRandomGround(bool withBoss = false, params GROUND_TYPE[] ExTypes) {
        GROUND_TYPE randomType = (GROUND_TYPE)Random.Range(1, countOfTypes+1);


        // По дефолту босс исключается из рандомной выдачи блоков     
        //Если нет исключений , ролим любой блок и возвращаем 
        if (!withBoss) {
            while (ExTypes.Contains(randomType) || (randomType == GROUND_TYPE.withBoss)) 
               {
                randomType = (GROUND_TYPE)Random.Range(1, countOfTypes+1);
               }
        }else {
            while (ExTypes.Contains(randomType)){
                randomType = (GROUND_TYPE)Random.Range(1, countOfTypes+1);
            }
        }
       
        return randomType;
        
      
    }



    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(currentEdge, 0.5f);
        
        
    }


    private void Notification(UnityEngine.Object sender) {
        EventManager.Instance.PostNotification(EVENT_TYPE.SPAWN_GROUND, this, sender);
    }


}

enum GROUND_TYPE { empty=1, withEnemy=2, withBoss=3, withTraps=4 }

enum SPAWN { SKELETON, GHOST, MOUSE , FlameCoin}

