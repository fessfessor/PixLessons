﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageByWeapon : MonoBehaviour
{
    public string colTriggerTag;


    //Сюда надо передать тег ЧЕМ получаем дамаг при касании триггера
    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag(colTriggerTag)) {
            Health health = gameObject.GetComponent<Health>();
            health.takeHit(col.gameObject.GetComponent<Damage>().SwordDamage);
            //Debug.Log("Triggered by " + col.gameObject.transform.name);
            //Показать хелс бар
            // Берем все чайлды, находим хелс бар и показываем его
            for (int i = 0; i < gameObject.transform.childCount; i++) {
                if (gameObject.transform.GetChild(i).transform.name == "HealthBar") {
                    gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
                }
            }

        }
    }
}