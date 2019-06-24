using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float force;
    [SerializeField] private float lifeTime;
    [SerializeField] private Animator animator;   
    [SerializeField] private GameObject parent;     
    public float Force { get => force; set => force = value;}


    public void SetImpulse(Vector2 direction, float force) {
        rb.AddForce(direction* force, ForceMode2D.Impulse);
        StartCoroutine(LifeCircle());
    }

    

    private void OnTriggerEnter2D(Collider2D col) {
        //Debug.Log("PArent - " + parent.transform.name + " COLL - " + col.transform.name);
        if (col.gameObject.transform.name != parent.transform.name) {
            // Тормозим объект в месте попадания
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            Destroy(gameObject, 0.36f);
            animator.SetTrigger("Explosive");

        }
    }

    private IEnumerator LifeCircle() {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
        yield break;

    }

}
