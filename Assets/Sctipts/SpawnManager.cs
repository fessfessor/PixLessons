using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Vector3 currentEdge;



    void Start()
    {
        player = GameManager.Instance.player;
        groundNames = new HashSet<string>();
        pooler = ObjectPooler.Instance;
        firstBlock = GameObject.Find("ground_first_block");

        // Текущая граница последнего объекта земли
        currentEdge = firstBlock.transform.position; 

        //Получаем из пула список доступных префабов для постройки уровня. Ищем по тегу "ground_"
        groundNames = pooler.getSetOfNamesObjects("ground_");


        Debug.Log($"Grounds - {string.Join(",",groundNames)}  Count - {groundNames.Count}");


    }

    
    void Update()
    {
        
    }

    private void SpawnGround() {


    }
}
