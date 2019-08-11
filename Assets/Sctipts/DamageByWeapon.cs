using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageByWeapon : MonoBehaviour
{
    [SerializeField] int damage;
    public int Damage { get => damage; set => damage = value; }

    [SerializeField] GameObject parent;

    private ObjectPooler pooler;
    private GameObject BloodSplash;
    private List<GameObject> splashes;

    private void Start()
    {
        splashes = new List<GameObject>();
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
                        StartCoroutine(Blood(col.transform.position));
                        

                    }
                }
            
                
        }
    }

    IEnumerator Blood(Vector2 position)
    {
               
        splashes.Add(pooler.SpawnFromPool("BloodSplash", position, Quaternion.identity));
        int index = splashes.Count - 1;
        yield return new WaitForSeconds(1f);
        splashes[index].GetComponent<Animator>().WriteDefaultValues();
        pooler.ReturnToPool("BloodSplash", splashes[index]);
        splashes.Remove(splashes[index]);
        

    }
        
       
            
 
}

