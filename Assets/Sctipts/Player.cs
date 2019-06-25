using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float force = 1.0f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float minHeight = -50.0f;
    [SerializeField] private GroundDetection groundD;
    private Vector3 direction;
    [SerializeField] private SpriteRenderer spriteR;
    [SerializeField] private Health health;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject SwordRight;
    [SerializeField] private GameObject SwordLeft;
    [SerializeField] private GameObject SpawnPoint;
     bool isRightDirection;

    private Vector3 jumpDirection;
    private bool isJumping;
    private bool canMove;
    private bool canAttack;
     bool isAttacking;
    private bool shootReady;

    [SerializeField] private float swordAttackTime;
    [SerializeField] private float shootForce;
    [SerializeField] private MagicBall magicBall;
    [SerializeField] private int ballsCoulnt = 3;

    private List<MagicBall> ballPool;

    

    

    
    // Start is called before the first frame update
    void Start()
    {
        ballPool = new List<MagicBall>();
        for (int i=0; i < ballsCoulnt; i++) {
            MagicBall poolingBall = Instantiate(magicBall, SpawnPoint.transform);
            ballPool.Add(poolingBall);
            poolingBall.gameObject.SetActive(false);
        }

        isAttacking = false;
        canAttack = true;
        canMove = true;
        shootReady = true;
        health = transform.GetComponent<Health>();

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        //Анимации
        animator.SetBool("isGrounded", groundD.isGrounded);
        if(!isJumping && !groundD.isGrounded)
            animator.SetTrigger("fallWithoutJump");

        isJumping = !groundD.isGrounded;

        animator.SetFloat("speed", Mathf.Abs(direction.x));
        animator.SetFloat("isFalling", rb.velocity.y);


        //Движение
        if (canMove) {
            Move();
        }
        


        // Обработка прыжка
        if ( Input.GetKeyDown(KeyCode.Space) && groundD.isGrounded) {
            Jump();
            animator.SetTrigger("startJump");
        }

        //Атака
        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack){
            StartCoroutine(SwordAttack());
           // canMove = false;
           // canAttack = false;
        }

        // телепортация обратно на платформу
        if (transform.position.y < minHeight)
            resetHeroPoition();

        if (direction.x > 0) {
            isRightDirection = true;
            spriteR.flipX = false;           
        }
        if (direction.x < 0) {
            isRightDirection = false;
            spriteR.flipX = true;
        }

    }


    private void Update() {
        //Стрельба
        if (shootReady) {
            if (Input.GetMouseButtonDown(1) && groundD.isGrounded) {               
                StartCoroutine(MagicAttack());
            }
            
        }
            
    }

    #region Attack

    IEnumerator MagicAttack() {
        animator.SetTrigger("isShooting");
        isAttacking = true;
        shootReady = false;
        canAttack = false;
        canMove = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1.2f);
        isAttacking = false;
        canMove = true;
        canAttack = true;
        yield return new WaitForSeconds(0.8f);
        shootReady = true;
       
    }

    

    // Корутина чтобы останавливать персонажа,когда он бьет мечом 
    // и чтобы нельзя было закликивать атаку
    IEnumerator SwordAttack() {
        Attack();
        isAttacking = true;
        //Если на земле тормозимся
        if(groundD.isGrounded)
        rb.velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(swordAttackTime);

        isAttacking = false;
        canMove = true;
        canAttack = true;
    }

    void Attack() {
        canAttack = false;
        canMove = false;
        animator.SetTrigger("isSwordAttack");


    }

    //методы для ивента. Появление\исчезание коллайдера меча
    void SwordAttackColliderStart() {
        rb.WakeUp();
        if (isRightDirection)
            SwordRight.SetActive(true);
        else
            SwordLeft.SetActive(true);
    }

    void SwordAttackColliderDone() {
        if (isRightDirection)
            SwordRight.SetActive(false);
        else
            SwordLeft.SetActive(false);
    }

    void CheckShoot() {

        MagicBall prefab = GetMagicBallFromPoll();
        
        prefab.SetImpulse(Vector2.right, spriteR.flipX ? -shootForce : shootForce, this);

    }

    void Jump() {
        isJumping = true;
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    #endregion



    #region Move
    void Move() {
        direction = Vector3.zero;

        if (Input.GetKey(KeyCode.A)) {
            direction = Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D)) {
            direction = Vector3.right;
        }
        direction *= speed;
        direction.y = rb.velocity.y;
        rb.velocity = direction;
    }

    void resetHeroPoition() {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
    }

    #endregion


    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Coin")) {
            PlayerInventory.Instance.cointsCount++;
            Debug.Log("coints = " + PlayerInventory.Instance.cointsCount);
            Destroy(col.gameObject);
        }
    }

    
   

    #region Pool

    private MagicBall GetMagicBallFromPoll() {
        if(ballPool.Count > 0) {
            var ballTemp = ballPool[0];
            ballPool.Remove(ballTemp);
            ballTemp.gameObject.SetActive(true);
            ballTemp.transform.parent = null;
            ballTemp.transform.position = SpawnPoint.transform.position;
            return ballTemp;
        }
        return Instantiate(magicBall, SpawnPoint.transform.position, Quaternion.identity);
    }

    public void ReturnBallToPoll(MagicBall ballTemp) {       
        if (!ballPool.Contains(ballTemp))
            ballPool.Add(ballTemp);

        ballTemp.gameObject.SetActive(false);
        ballTemp.transform.parent = SpawnPoint.transform;
        ballTemp.transform.position = SpawnPoint.transform.position;
        
    }




    #endregion


}
