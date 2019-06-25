using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour , IObjectDestroyer
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float force;
    [SerializeField] private float lifeTime;
    [SerializeField] private Animator animator;   
    [SerializeField] private float deathAnimationDuration;   
    private Player player;
    private IObjectDestroyer objectDestroyer;
    public float Force { get => force; set => force = value;}



    // Свой метод "уничтожения", помещаем обратно в пул объект, после его анимации
    public IEnumerator Destroy(GameObject gameObject, float duration) {       
        yield return new WaitForSeconds(duration);
        rb.gravityScale = 1;
        animator.SetBool("Explosive", false);
        player.ReturnBallToPoll(this);
    }


    public void Init(IObjectDestroyer objectDestroyer) {
        this.objectDestroyer = objectDestroyer;
    }

    public void SetImpulse(Vector2 direction, float force, Player player) {
        // Инициализируем класс игрока
        this.player = player;
        // Инициализируем интерфейс
        Init(this);

        // Непосредственно работаем с префабом
        rb.AddForce(direction* force, ForceMode2D.Impulse);
        StartCoroutine(LifeCircle());
    }

    

    private void OnTriggerEnter2D(Collider2D col) {
        //Debug.Log("PArent - " + player.transform.name + " COLL - " + col.transform.name);
        if (col.gameObject != player.gameObject) {
            // Тормозим объект в месте попадания
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            // Запускаем свое "уничтожение"
            if (objectDestroyer == null)
                Destroy(gameObject);
            else
                StartCoroutine(objectDestroyer.Destroy(gameObject, deathAnimationDuration));

            animator.SetBool("Explosive", true);

        }
    }

    private IEnumerator LifeCircle() {
        yield return new WaitForSeconds(lifeTime);
        if (objectDestroyer == null)
            Destroy(gameObject);
        else
           StartCoroutine(objectDestroyer.Destroy(gameObject, deathAnimationDuration));
        yield break;

    }

    
}


public interface IObjectDestroyer {   
    IEnumerator Destroy(GameObject gameObject, float delay);
}
