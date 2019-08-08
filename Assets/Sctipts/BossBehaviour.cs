using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBehaviour : MonoBehaviour, IBoss
{

    [SerializeField] private ParticleSystem ps;
    private Animator animator;
    private Collider2D coll;
    private Rigidbody2D rb;
    [SerializeField] GameObject healthBarBoss;
    [SerializeField] string bossName;
    [SerializeField] Text bossNameText;
    [SerializeField] GameObject LeftArea;
    [SerializeField] GameObject RightArea;
    [SerializeField] float checkFreq;
    [SerializeField] float speed;
    [SerializeField] int layer;
    [SerializeField] GameObject mainCamera;
    private  SpriteRenderer sr;

    private bool inArea = false;
    private Vector3 playerPosition;
    private Collider2D[] hits;
    private CameraController cameraController;
    private GameObject player = null;
    private bool playerRight;
    
    

    
    




    void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        cameraController = mainCamera.GetComponent<CameraController>();

        // Выключаем коллайдер,гравитацию и частицы для босса пока он не заспавнен
        rb.isKinematic = true;
        coll.enabled = false;
        ps.Stop();
    }

    
    void Update()
    {
        if (player)
        {
            Move();
        }
           // Debug.Log("Player position - " + player.transform.position);
        
        
            
    }


    // Каждый босс наследует этот интерфейс и соответственно будет легко настраиваться поведение при разных видах спавна
    public void OnSpawnBloodBoss()
    {
        throw new System.NotImplementedException();
    }

    public void OnSpawnLightBoss()
    {
        
    }

    public void OnSpawnSimpleBoss()
    {
        animator.SetTrigger("Spawn");
        healthBarBoss.SetActive(true);
        bossNameText.text = bossName; 
        EnableBoss(true);
        //Камера сдвигается в центр при битве с боссом, чтобы было удобнее
        cameraController.bossCamera = true;
    }



    private void EnableBoss(bool enabled)
    {
        if (enabled)
        {
            rb.isKinematic = false;
            coll.enabled = true;
            ps.Play();
            //LeftArea.SetActive(true);
            //RightArea.SetActive(true);
            StartCoroutine(CheckArea());

        }
        else
        {
            rb.isKinematic = true;
            coll.enabled = false;
            ps.Stop();
            //LeftArea.SetActive(false);
            //RightArea.SetActive(false);
            StopCoroutine(CheckArea());
        }
    }

    private void Charge()
    {

    }

    private void Underground()
    {

    }

    private void Move()
    {
        animator.SetTrigger("Run");
        if (playerRight)
        {
            sr.flipX = true;
            rb.velocity = Vector2.right * speed;           
        }
        else if (!playerRight)
        {
            rb.velocity = Vector2.left * speed;
            sr.flipX = false;           
        }
    }

    IEnumerator CheckArea()
    {       
        while (true)
        {
            yield return new WaitForSeconds(checkFreq);


            //Находим в радиусе игрока
            hits =  Physics2D.OverlapCircleAll(transform.position, 15f);
            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {                   
                    if (hits[i].gameObject.transform.name == "Player")
                    {
                        player = hits[i].gameObject;
                        // Определяем с какой стороны игрок
                        if (player.transform.position.x - transform.position.x < 0)
                            playerRight = false;
                        else
                            playerRight = true;
                        break;
                    }
                    else
                    {
                        player = null;
                    }                       
                }                  
            }
        }
        
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 15);
        
    }


}
