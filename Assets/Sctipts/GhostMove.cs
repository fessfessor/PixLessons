
using UnityEngine;

public class GhostMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float frequency = 20f;
    [SerializeField] float magnitude = 0.5f;
    [SerializeField] float timePatrol = 3f;
    [SerializeField] float shootBallSpeed = 3f;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject EnemyMagicBall;
    private bool facingRight;

    private Vector3 pos, localScale;
    private float currentTime;
    private bool isShoot;
    private Vector3 targetPosition;
    private GameObject ball;
    private GameObject player;
    

    
    void Start()
    {
        // Вначале призрак направлен вправо
        facingRight = true;
        pos = transform.position;
        localScale = transform.localScale;
        currentTime = 0f;
        isShoot = false;
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
            ball.transform.position = Vector3.MoveTowards(ball.transform.position, targetPosition, shootBallSpeed * Time.deltaTime);
            Destroy(ball, 1.5f);
        }

       

        //todo сделать что-нибудь с этим getComponent
        if(ball && ball.transform.position == targetPosition) {
            ball.GetComponent<EnemyMagicBall>().DestroyBall();
        }
            

        isShoot = false;     
    }

    void MoveRight() {
        pos += transform.right * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }

    void MoveLeft() {
        pos -= transform.right * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }


    // todo возможно сделать легкую самонаводку


    //Получаем в кого стрелять 
    // todo переписать на пул объектов, пока так
    public void StopAndShoot(GameObject player) {
        this.player = player;
        targetPosition = player.transform.position;
        ball = Instantiate(EnemyMagicBall, transform.position, Quaternion.identity);
        isShoot = true;

        Debug.Log("Shoot in " + player.transform.name);
    }

    
}
