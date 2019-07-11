
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
    private bool facingRight;

    private Vector3 pos, localScale;
    private float currentTime;
    private bool isShoot;
    private bool stopHoming;
    private Vector3 targetPosition;
    private GameObject ball;
    private GameObject player;
    

    
    void Start()
    {
        GameManager.Instance.ghostContainer.Add(gameObject, this);
        // Вначале призрак направлен вправо
        facingRight = true;
        pos = transform.position;
        localScale = transform.localScale;
        currentTime = 0f;
        isShoot = false;
        stopHoming = false;
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
            else { // Потом шар летит просто в последнюю координату
                ball.transform.position = Vector3.MoveTowards(ball.transform.position, targetPosition, shootBallSpeed * Time.deltaTime);
            }

            
            Destroy(ball, 5f);
        }

       

        //todo сделать что-нибудь с этим getComponent
        if(ball && ball.transform.position == targetPosition) {
            ball.GetComponent<EnemyMagicBall>().DestroyBall();
        }
            

            
    }

    void MoveRight() {
        pos += transform.right * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }

    void MoveLeft() {
        pos -= transform.right * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }

    // Корутина на остановку самонаводки
    IEnumerator Homing(){
        yield return new WaitForSeconds(timeOfHoming);
        stopHoming = true;
        targetPosition = player.transform.position;

    }

    //todo исправить ситуацию когда игрок заходит и выходит из зоны и создается куча шаров
    //Получаем в кого стрелять 
    // todo переписать на пул объектов, пока так
    public void StopAndShoot(GameObject player) {
        this.player = player;
        targetPosition = player.transform.position;
        ball = Instantiate(EnemyMagicBall, transform.position, Quaternion.identity, transform);
        
        stopHoming = false;
        StartCoroutine(Homing());



        Debug.Log("Shoot in " + player.transform.name);
    }

    // Способ убить призрака
    public void KillYouSelf() {

        animator.SetTrigger("isDeath");
        Debug.Log(transform.name + " kill!");
    }

    
}
