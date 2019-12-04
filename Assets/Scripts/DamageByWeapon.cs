using System.Collections;
using System.Collections.Generic;
using Sctipts;
using UnityEngine;

public class DamageByWeapon : MonoBehaviour
{
    [SerializeField] int damage;
    public int Damage { get => damage; set => damage = value; }

    [SerializeField] GameObject parent;
    [SerializeField] float splashDuration;
    

    private ObjectPooler pooler;
    private Player playerScript;


   

    private void Start()
    {
        playerScript = GameManager.Instance.player.GetComponent<Player>();
         pooler = ObjectPooler.Instance;
    }




    private void OnTriggerEnter2D(Collider2D col) {
        
        // Чтобы мы не дамажили своим же оружием себя
        if (parent != null && parent.transform.name != col.transform.name) {           
            if (GameManager.Instance.healthContainer.ContainsKey(col.gameObject)) {
                var health = GameManager.Instance.healthContainer[col.gameObject];               
                
                if (health != null && health.HealthCount > 0) {                        
                    health.takeHit(damage);

                    // Если мы убили врага, то отправляем событие об этом
                    if (health.HealthCount <= 0 && col.gameObject.transform.tag == "Enemy")
                        EventManager.Instance.PostNotification(EVENT_TYPE.PLAYER_KILL_ENEMY, this, col);


                        
                //Условия для меча, звук, брызги крови и восстановление хп при ударе
                    if(transform.tag == "HeroSword") {
                          

                        AudioManager.Instance.Play("SwordAttack");
                        //todo возможно стоит вынести данный функционал в отдельный компонент. Кровавые брызги при ударе. Проверка для того чтобы на анимации смерти не было крови 
                        if (health.HealthCount > 0)
                            StartCoroutine(Blood(col.transform.position));

                        //"Кровавая механика"
                        if (col.transform.tag == "Enemy")
                            EventManager.Instance.PostNotification(EVENT_TYPE.BLD_MELEE_HIT, this);                           
                    }
                        
                }
            }     
        }
    }

    IEnumerator Blood(Vector2 position)
    {
        
        GameObject splash = pooler.SpawnFromPool("BloodSplash", position, Quaternion.identity);
        SpriteRenderer sr = splash.GetComponent<SpriteRenderer>();
        if (!playerScript.isRightDirection)
            sr.flipX = true;
        yield return new WaitForSeconds(splashDuration);
        sr.flipX = false;
        pooler.ReturnToPool("BloodSplash", splash, 0f);
    }



    
        
       
            
 
}

