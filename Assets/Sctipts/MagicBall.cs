using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour , IPooledObject
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float force;
    [SerializeField] private float lifeTime;
    [SerializeField] private Animator animator;   
    [SerializeField] private float deathAnimationDuration;   
    private Player player;
    private IPooledObject pooledObject;
    private ObjectPooler pooler;
    
    public float Force { get => force; set => force = value;}



    

    public void Init(IPooledObject pooledObject) {
        this.pooledObject = pooledObject;
    }

    public void SetImpulse(Vector2 direction, float force, Player player) {
        // Инициализируем класс игрока
        this.player = player;
        // Инициализируем интерфейс
        Init(this);

        // Инициализируем пул
        if(!pooler)
            pooler = ObjectPooler.Instance;

        // Непосредственно работаем с префабом
        rb.AddForce(direction* force, ForceMode2D.Impulse);
        StartCoroutine(LifeCircle());
    }

    

    private void OnTriggerEnter2D(Collider2D col) {
        // Маска для моих монеток, чтобы шар пролетал сквозь
        bool isNonTriggerObj = false;
        if (GameManager.Instance.flameCoinContainer.ContainsKey(col.gameObject) 
            || col.gameObject.transform.CompareTag("NonShootable")
            )
        isNonTriggerObj = true;
        
        //Debug.Log("PArent - " + player.transform.name + " COLL - " + col.transform.name);
        if (col.gameObject != player.gameObject && !isNonTriggerObj) {
            // Тормозим объект в месте попадания
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            // Запускаем свое "уничтожение"
            if (pooledObject == null)
                Destroy(gameObject);
            else
                StartCoroutine(pooledObject.OnReturnToPool(gameObject, deathAnimationDuration));

            animator.SetBool("Explosive", true);
            AudioManager.Instance.Play("FireballExplode");

        }
    }

    private IEnumerator LifeCircle() {
        yield return new WaitForSeconds(lifeTime);
        if (pooledObject == null)
            Destroy(gameObject);
        else
           StartCoroutine(pooledObject.OnReturnToPool(gameObject, deathAnimationDuration));
        yield break;

    }


    // Свой метод "уничтожения", помещаем обратно в пул объект, после его анимации
    public IEnumerator OnReturnToPool(GameObject gameObject, float delay) {
        yield return new WaitForSeconds(delay);
        animator.WriteDefaultValues();
        rb.gravityScale = 1;
        //Здесь вызываю возврат объекта в пул
        pooler.ReturnToPool("MagicBall", this.gameObject);
    }

    //Действия при спавне здесь не требуются
    public IEnumerator OnSpawnFromPool(float delay) {
        throw new System.NotImplementedException();
    }
}


