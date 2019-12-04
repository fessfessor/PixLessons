
using System.Collections;
using UnityEngine;

public class GhostMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField]private float frequency = 20f;
    [SerializeField]private float magnitude = 0.5f;
    [SerializeField]private float timePatrol = 3f;
    [SerializeField]private float shootBallSpeed = 3f;
    [SerializeField]private float timeOfHoming = 0f;        // Время которое шар будет наводиться на игрока, чем больше , тем сложнее увернуться
    [SerializeField]public float shootFrequency;
    [SerializeField]private SpriteRenderer spriteRenderer;
    [SerializeField]private GameObject EnemyMagicBall;
    [SerializeField]private Animator animator;
    [SerializeField]public bool drawGizmos;
    private bool facingRight;

    private Vector3 pos, localScale;
    private float currentTime;
    private bool isShoot;
    private bool stopHoming;
    private Vector3 targetPosition;
    private GameObject ball;
    private GameObject player;
    private ObjectPooler pooler;
    

    private bool readyToReturn;
    

    
    void Start()
    {
        pooler = ObjectPooler.Instance;

        GameManager.Instance.ghostContainer.Add(gameObject, this);
        // Вначале призрак направлен вправо
        facingRight = true;
        pos = transform.position;
        localScale = transform.localScale;
        currentTime = 0f;
        isShoot = false;
        stopHoming = false;
        readyToReturn = true;
    }

    
    void Update()
    {
        //Debug.Log(currentTime);
        currentTime += Time.deltaTime;

        // timePatrol - время которое враг летит в одну сторону
        if(currentTime < timePatrol) {
            if (facingRight)
                MoveRight();
            else
                MoveLeft();

        }
        else{
            if (spriteRenderer.flipX) {
                spriteRenderer.flipX = false;
                facingRight = true;

            }
            else {
                spriteRenderer.flipX = true;
                facingRight = false;
            }

            currentTime = 0f;

        }

        // Если шар создан
        if (ball) {
            if (!stopHoming) { // Какое время наводимся на цель, беря ее координаты в апдейте
                ball.transform.position = Vector3.MoveTowards(ball.transform.position, player.transform.position, shootBallSpeed * Time.deltaTime);
            }
            else { // Потом шар летит просто в последнюю координату (targetPosition)
                ball.transform.position = Vector3.MoveTowards(ball.transform.position, targetPosition, shootBallSpeed * Time.deltaTime);
            }

            
            
        }


        // Возвращам шар обратно в пул когда достиг цели
        if(ball && (ball.transform.position == targetPosition) && readyToReturn) {
            pooler.ReturnToPool("EnemyMagicBall", ball);

            readyToReturn = false;
        }

        // Death
        if(GameManager.Instance.healthContainer[gameObject].HealthCount <= 0) {
            if(ball)
                pooler.ReturnToPool("EnemyMagicBall", ball);
            animator.SetTrigger("isDeath");
        }


            

            
    }

   


        #region Move
    void MoveRight() {
        pos += transform.right * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }

    void MoveLeft() {
        pos -= transform.right * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }
    #endregion

    // Корутина на остановку самонаводки
    IEnumerator Homing(){
        yield return new WaitForSeconds(timeOfHoming);
        stopHoming = true;
        //Последняя позиция игрока
        targetPosition = player.transform.position;

    }

    // Вызывается из дочернего объекта - ChostAreaCollider
    public void StopAndShoot(GameObject player) {
        this.player = player;
        targetPosition = player.transform.position;
        // Берем шар из пула
        ball = pooler.SpawnFromPool("EnemyMagicBall", transform.position, Quaternion.identity);
        readyToReturn = true;

        stopHoming = false;
        StartCoroutine(Homing());

    }



    
   

    
}
