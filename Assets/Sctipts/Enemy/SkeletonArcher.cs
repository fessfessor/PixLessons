using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SkeletonArcher : MonoBehaviour, IEnemy
{
    public float TurnFreq = 5f;
    public float ShootFreq = 3f;
    public float ShootPower =10f;
    
    public int damage;
    public ArcherType type;
    public GameObject arrowPrefab;

    private Animator anim;
    private bool isDeath;
    private int currentHealth;
    private bool isAttacking = false;
    private GameObject enemy;
    private ObjectPooler pooler;
    private GameObject arrow;
    public SkeletonArcherState currentState;
    
    
    private bool isRightDirection = true;
    private bool isSwitchingVision;
    private Task switchVision;
    private Task shoot;
    


    private void Start()
    {
        
        pooler = ObjectPooler.Instance;
        anim = GetComponent<Animator>();
        currentState = SkeletonArcherState.Watching;
        switchVision = new Task(SwitchVision(), false);
        shoot = new Task(ShootCor(), false);
        currentState = SkeletonArcherState.Watching;

        //Move();
    }

    private void Update()
    {
        if (GameManager.Instance.healthContainer[gameObject].HealthCount <= 0 && !isDeath)
            Death();

        
        anim.SetBool("isAttacking",currentState == SkeletonArcherState.Shooting ||  currentState == SkeletonArcherState.Hitting);
        

            
        switch (currentState)
        {
            case SkeletonArcherState.Hitting:
            {
                
                break;
            }
            case SkeletonArcherState.Shooting:
            {
                

                if (!shoot.Running)
                {
                    StopAllCoroutines();
                    shoot.Start();
                    Debug.Log("Shoot START!");
                }
                    
                break;
            }
            case SkeletonArcherState.Watching:
            {
                
                if (!switchVision.Running)
                {
                    StopAllCoroutines();
                    switchVision.Start();
                    Debug.Log("WATCHING START!");
                    
                    
                }

                
                
                
                break;
            }
                
                
        }
        
        //if(currentState == SkeletonArcherState.Shooting)
         //   StopCoroutine("SwitchVision");
    }

    
    
    IEnumerator SwitchVision()
    {
        
        while (true)
        {
            Debug.Log("WATCHING START!");
            yield return new WaitForSeconds(TurnFreq);
            transform.rotation = Quaternion.Euler(0, transform.rotation == Quaternion.Euler(0, 0, 0) ? 180 : 0, 0);
            isRightDirection = transform.rotation.y <= 0;
            
        }
    }
    
    
    IEnumerator ShootCor()
    {
        while (currentState == SkeletonArcherState.Shooting)
        {
            Debug.Log("SHOOT!");
            anim.SetTrigger("Shoot");
            yield return new WaitForSeconds(ShootFreq);    
        }
 
    }
   

    
//Информация из дочернего объекта с коллайдером
    public void Attack(bool isAttacking, GameObject enemy)
    {
        
        currentState = isAttacking ? SkeletonArcherState.Shooting : SkeletonArcherState.Watching;
        this.enemy = enemy;

   
        
    }

    public void Death()
    {
        isDeath = true;
        anim.SetTrigger("Death");
    }

    public void Move()
    {
        //StartCoroutine(SwitchVision());
    }

    
    public void Damage()
    {
        if (isAttacking && enemy!=null) {
                GameManager.Instance.healthContainer[enemy].takeHit(damage);
        }
        
    }

    

    //Вызывается из анимации
    public void Shoot()
    {
        
        
//        arrow = pooler.SpawnFromPool("Arrow", transform.position, Quaternion.identity);
//       if(!isRightDirection)
//           arrow.transform.rotation = Quaternion.Euler(0,180,0);
//       else
//           arrow.transform.rotation = Quaternion.Euler(0,0,0);

        if (enemy != null)
        {
            arrow = Instantiate(arrowPrefab, transform.position,
                !isRightDirection ? Quaternion.Euler(0, 180, 0) : Quaternion.identity);
            
            arrow.GetComponent<Rigidbody2D>().AddForce((enemy.transform.position - transform.position).normalized * ShootPower, ForceMode2D.Impulse);
        }
            
    }

    public enum ArcherType
    {
        SIMPLE_SHOOT, SHOOT_AND_HIT, PATROL
    }
    
}

public enum SkeletonArcherState
{
    Watching,
    Shooting,
    Hitting
}


