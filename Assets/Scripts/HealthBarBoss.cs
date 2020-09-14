using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBarBoss : MonoBehaviour
{
    private float currentHealth;
    private float healthCount;
    [SerializeField] private Image healthFiller; 
    [SerializeField] private Health health; 
    [SerializeField] private float healthDelta;
    private float bossHealthFloat;


    
    void Start()
    {
        bossHealthFloat = health.HealthCount;
        currentHealth = health.HealthCount;
        healthCount = currentHealth / bossHealthFloat;
        
    }

    
    void Update()
    {
        //currentHealth = GameManager.Instance.healthContainer[gameObject].HealthCount;
        //healthFiller.fillAmount = currentHealth / 100.0f;

        // Текущее здоровье в процентах, так чтобы максимум был 1, HealthCount делим на максимум который был.
        currentHealth = health.HealthCount / bossHealthFloat;

        if (currentHealth > healthCount)
            healthCount += healthDelta;
        if (currentHealth < healthCount)
            healthCount -= healthDelta;
        if (Mathf.Abs(currentHealth - healthCount) < healthDelta)
            healthCount = currentHealth;

        healthFiller.fillAmount = healthCount ;
        
    }
}
