
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image health;
    [SerializeField] private float healthDelta = 0.003f;
    private float healthCount;
    private float currentHealth;
    private Player player;



    void Start()
    {
        player = FindObjectOfType<Player>();
        healthCount = player.Health.HealthCount / 100.0f;
    }

    
    void Update()
    {
        currentHealth = player.Health.HealthCount / 100.0f;

        if (currentHealth > healthCount)
            healthCount += healthDelta;
        if (currentHealth < healthCount) 
            healthCount -= healthDelta;           
        if (Mathf.Abs(currentHealth - healthCount) < healthDelta)
            healthCount = currentHealth;

        health.fillAmount = healthCount;


        
    }
}
