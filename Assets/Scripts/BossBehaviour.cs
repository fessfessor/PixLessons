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
    private bool bossActive = false;
    private Vector3 playerPosition;
    private Collider2D[] hits;
    private CameraController cameraController;
    private GameObject player = null;
    private bool playerRight;
    private DetectInArea detecter;
    private Health health;
    private int healthCount;
    
    


    void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        cameraController = mainCamera.GetComponent<CameraController>();
        detecter = GetComponent<DetectInArea>();
        health = GetComponent<Health>();

        // Выключаем коллайдер,гравитацию и частицы для босса пока он не заспавнен
        rb.isKinematic = true;
        coll.enabled = false;
        ps.Stop();
    }

    
    void Update()
    {
        healthCount = health.HealthCount;

        //Ищем игрока
        if (bossActive)
            player = detecter.target;
        else
            player = null;

        if (player)
        {
            Move();

            if (player.transform.position.x - transform.position.x < 0)
                playerRight = false;
            else
                playerRight = true;
        }

        //Выбор действия
        if (bossActive) {
            int rand = (int)Random.value * 3;
        }


    }


    // Каждый босс наследует этот интерфейс и соответственно будет легко настраиваться поведение при разных видах спавна
    public void OnSpawnBloodBoss()
    {
        //Спавнится усиленный босс, с большим количеством хп, + хп игрока будет срезано, но награды будет больше
        health.HealthCount = healthCount + (healthCount / 100) * 20;
        int playerHealth = GameManager.Instance.healthContainer[GameManager.Instance.player].HealthCount;
        GameManager.Instance.healthContainer[GameManager.Instance.player].HealthCount -= playerHealth / 100 * 50;
        speed += 1.5f;

        animator.SetTrigger("Spawn");
        healthBarBoss.SetActive(true);
        bossNameText.text = bossName;
        EnableBoss(true);
        //Камера сдвигается в центр при битве с боссом, чтобы было удобнее
        cameraController.bossCamera = true;
    }

    public void OnSpawnLightBoss()
    {
        //Спавнется ослабленный босс с меньшим количеством хп. Допустим 20%. Награды будет меньше
        health.HealthCount = healthCount - (healthCount / 100) * 20;

        animator.SetTrigger("Spawn");
        healthBarBoss.SetActive(true);
        bossNameText.text = bossName;
        EnableBoss(true);
        //Камера сдвигается в центр при битве с боссом, чтобы было удобнее
        cameraController.bossCamera = true;
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
            bossActive = true;

        }
        else
        {           
            rb.isKinematic = true;
            coll.enabled = false;
            ps.Stop();
            bossActive = false;

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

    


    


}
