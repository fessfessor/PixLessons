using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{

    [SerializeField] int damage;

    private void OnCollisionEnter2D(Collision2D col) {

        //Debug.Log(col.gameObject.transform.name);
        if(col.gameObject.transform.name == "Player") {
            // получаем здоровье игрока
            var health = GameManager.Instance.healthContainer[col.gameObject];
            health.takeHit(damage);

            PlatformerTools.ShowHealthBar(col.gameObject);


        }
    }
}
