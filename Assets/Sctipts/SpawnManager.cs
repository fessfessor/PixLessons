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
     *  Должен быть отдельный пул для объектов уровня
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

    private List<string> groundType;

    private float distance;



    void Start()
    {
        groundType = new List<string>();
        player = GameManager.Instance.player;
        groundNames = new HashSet<string>();
        pooler = ObjectPooler.Instance;
        firstBlock = GameObject.Find("ground_first_block");

        // Текущая граница последнего объекта земли
        currentLastBlock = firstBlock;
        //currentEdge = firstBlock.GetComponent<Collider2D>().bounds.max;
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
        // Спавним первые 3 куска земли



        //SpawnGround(GROUND_TYPE.empty);
        StartCoroutine(checkDistance());

        //Debug.Log($"Grounds - {string.Join(",",groundNames)}  Count - {groundNames.Count}");


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
                SpawnGround(GROUND_TYPE.empty);
            }
        }
        
    }

    private void DespawnGround() {

    }

    private void SpawnGround(GROUND_TYPE type, int count = 1) {
        string blockName;
        GameObject block;       
        //Debug.Log($"Enum - {type.ToString()}");

        // заполняем коллекцию в зависимости от типа, возможно это стоит сделать зараннее
        foreach (var el in groundNames) {
            if (el.Contains(type.ToString())) 
                groundType.Add(el);
                       
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

            /*
            var blockCol = block.GetComponent<Collider2D>();
            
            //Ставим блок впритык к предыдущему
            block.transform.position = new Vector3( currentEdge.x + blockCol.bounds.size.x / 2, 
                                                    currentEdge.y - blockCol.bounds.size.y / 2, 
                                                    currentEdge.z);

            // Устанавливаем новую границу и новый конечный объект
            currentLastBlock = block;
            currentEdge = new Vector3(  block.transform.position.x + blockCol.bounds.size.x / 2,
                                        block.transform.position.y + blockCol.bounds.size.y / 2,
                                        block.transform.position.z);

            */


        }               

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(currentEdge, 0.5f);
        
        
    }
}

enum GROUND_TYPE { empty, withEnemy, withBoss, withTraps }

