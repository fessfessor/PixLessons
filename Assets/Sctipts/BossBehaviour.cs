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
    [SerializeField] int layer;

    private bool inArea = false;
    private Vector3 playerPosition;
    private Collider2D hit = null;
    
    




    void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Выключаем коллайдер,гравитацию и частицы для босса пока он не заспавнен
        rb.isKinematic = true;
        coll.enabled = false;
        ps.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Player position - " + playerPosition + " inArea - " + inArea);
        
        if (hit)
            Debug.Log("Check - " + hit.name);
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
    }

    // Координаты игрока приходят из зон атак
    public void SetPlayerPosition(Vector3 position, bool inArea, GameObject side)
    {
        playerPosition = position;
        this.inArea = inArea;
         

        //Debug.Log(side.transform.name);
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

    IEnumerator CheckArea()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(checkFreq);
            hit =  Physics2D.OverlapCircle(transform.position, 15f, layer);
   
        }
        
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 15);
        
    }


}
