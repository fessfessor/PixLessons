using System;
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

    //коллекции с разными видами блоков
   

    private float distance;



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
            blockName = groundType[Random.Range(0, groundType.Count - 1)];
            block = pooler.SpawnFromPool(blockName, new Vector3(0, 0, -20), Quaternion.identity);
            block.transform.position = currentEdge;

            //Край блока
            var blockEdge = block.transform.Find("edge").transform.position;

            currentLastBlock = block;
            currentEdge = blockEdge;


            //spawnedGroundLog.Add(blockName);
        }               
    }

    private void SpawnLogic() {
        // Пока что логика будет ограничиваться тем что нельзя заспавнить больше 2 в ряд одинаковых блоков

        if (spawnedGroundLog.Count < 2) {
            SpawnGround((GROUND_TYPE)Random.Range(0, countOfTypes - 1));
        }
        else {
            //Если уже есть с чем сравнивать, то смотрим на 2 последних блока и если они одинакового типа, не спавним такой же 3
            var lastType = (spawnedGroundLog[spawnedGroundLog.Count - 1]);
            var preLastType = spawnedGroundLog[spawnedGroundLog.Count - 2];
            //if(GetTypeByString() == preLastB) {
//
            //}

        }
        
        SpawnGround(GROUND_TYPE.empty);




    }



    



    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(currentEdge, 0.5f);
        
        
    }


    private GROUND_TYPE GetTypeByString(string s) {
        string[] enumNames = Enum.GetNames(typeof(GROUND_TYPE));

        foreach (var item in enumNames) {
            if (item.Contains(s))
                return ParseEnum<GROUND_TYPE>(item);
        }
        throw new Exception("Нет такого элемента в перечислении!");
    }


    public static T ParseEnum<T>(string value) {
        return (T)Enum.Parse(typeof(T), value, true);
    }


}

enum GROUND_TYPE { empty, withEnemy, withBoss, withTraps }

