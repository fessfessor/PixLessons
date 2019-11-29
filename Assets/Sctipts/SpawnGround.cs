using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpawnGround : MonoBehaviour, IPooledObject
{
    private WaitForSeconds timer;
    private ObjectPooler pooler;
    private Collider2D collider2d;

    // Зависимость шанса спавна блока от пройденной дистанции. Возможно когда нибудь будет использоваться
    public AnimationCurve chanceFromDistance;


    private void Start() {
        timer = new WaitForSeconds(0.5f);
        pooler = ObjectPooler.Instance;
        collider2d = GetComponent<Collider2D>();
      
    }

    public void OnReturnToPool() {
        // Тут реализовываем возврат куска земли в первоначальное состояние - со всеми врагами и расходными материалами
        

    }

    public void OnSpawnFromPool() {    
        
    }

    

    private void OnTriggerStay2D(Collider2D col) {
        if(col.gameObject == GameManager.Instance.darkWall) {
            if(Mathf.Abs(GameManager.Instance.darkWall.transform.position.x - collider2d.bounds.max.x) < 1)
                pooler.ReturnToPool(transform.name.Replace("(Clone)", ""), gameObject);
        }
    }

}
