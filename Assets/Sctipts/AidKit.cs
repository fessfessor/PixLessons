using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AidKit : MonoBehaviour
{
    [SerializeField]
    private int healthSize;

    private void OnTriggerEnter2D(Collider2D col) {
        Health health = col.gameObject.GetComponent<Health>();
        if (health != null) {
            health.health += healthSize;
            Destroy(gameObject);
        }
        Debug.Log(col.gameObject.transform.childCount);
        //Показать хелс бар
        // Берем все чайлды, находим хелс бар и показываем его
        for (int i = 0; i < col.gameObject.transform.childCount; i++) {
            if (col.gameObject.transform.GetChild(i).transform.name == "HealthBar") {
                Debug.Log(col.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled);
                col.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
                Debug.Log(col.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled);
            }
            Debug.Log(col.gameObject.transform.GetChild(i).transform.name);
        }
    }



}
