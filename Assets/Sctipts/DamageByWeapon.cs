using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageByWeapon : MonoBehaviour
{
    [SerializeField] int damage;
    public int Damage { get => damage; set => damage = value; }

    [SerializeField] GameObject parent;
    [SerializeField] float splashDuration;
    [SerializeField] Side side;

    private ObjectPooler pooler;
   

    private void Start()
    {
       
        pooler = ObjectPooler.Instance;
    }




    private void OnTriggerEnter2D(Collider2D col) {
        
        // Чтобы мы не дамажили своим же оружием себя
        if (parent != null && parent.transform.name != col.transform.name) {           
                if (GameManager.Instance.healthContainer.ContainsKey(col.gameObject)) {
                    var health = GameManager.Instance.healthContainer[col.gameObject];
                // Если есть здоровье, его больше 0 и 
                
                if (health != null && health.HealthCount > 0) {
                        health.takeHit(damage);

                    //todo возможно стоит вынести данный функционал в отдельный компонент. Кровавые брызги при ударе. Проверка для того чтобы на анимации смерти не было крови 
                        if(health.HealthCount > 0)
                            StartCoroutine(Blood(col.transform.position));
                        
                        

                    }
                }
            
                
        }
    }

    IEnumerator Blood(Vector2 position)
    {
        
        GameObject splash = pooler.SpawnFromPool("BloodSplash", position, Quaternion.identity);
        SpriteRenderer sr = splash.GetComponent<SpriteRenderer>();
        if (side == Side.left)
            sr.flipX = true;
        yield return new WaitForSeconds(splashDuration);
        sr.flipX = false;
        pooler.ReturnToPool("BloodSplash", splash, 0f);
    }



    enum Side { left, right }
        
       
            
 
}

