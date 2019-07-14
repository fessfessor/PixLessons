using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMagicBall : MonoBehaviour, IPooledObject
{
    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D ballCollider;
    [SerializeField] private int damage;
    [SerializeField] private bool CanReflectBySword;    
    ObjectPooler pooler;

    private void Start() {
        if (!pooler)
            pooler = ObjectPooler.Instance;
    }


    public IEnumerator OnReturnToPool(GameObject gameObject, float delay) {

        // Отыгрывает анимация взрыва, выключаем коллайдер
        animator.SetTrigger("isExplosion");
        ballCollider.enabled = false;
        // Ждем необходимое время для анимации
        yield return new WaitForSeconds(delay);        
        // Возвращаем объект в первоначальное состояние
        ballCollider.enabled = true;
        animator.WriteDefaultValues();
        pooler.ReturnToPool("EnemyMagicBall", gameObject);

    }

    public IEnumerator OnSpawnFromPool(float delay) {
        throw new System.NotImplementedException();
    }


    

    



    private void OnTriggerEnter2D(Collider2D col) {
        bool isPlayer = col.gameObject.transform.name == "Player";
        

        if (isPlayer) {
            GameManager.Instance.healthContainer[col.gameObject].takeHit(damage);
            StartCoroutine(OnReturnToPool(gameObject, 0.5f));
            
        }
        else if (col.gameObject.CompareTag("HeroSword") && CanReflectBySword) { // Если шлепаем мечом , например по прзрачному шару, то уничтожаем его
            //Debug.Log("DESTROY BAAAAAALL!");
            StartCoroutine(OnReturnToPool(gameObject, 0.5f));
        }


    }
}
