
using Assets.Scripts.PlayerLogic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image health;
    [SerializeField] private float healthDelta = 0.003f;
    private float healthCount;
    private float currentHealth;
    private Player player;
    private float playerHealthFloat;



    void Start()
    {
        player = FindObjectOfType<Player>();
        playerHealthFloat = player.Health.HealthCount;
        healthCount = player.Health.HealthCount / playerHealthFloat;
    }

    
    void Update()
    {
        currentHealth = player.Health.HealthCount / playerHealthFloat;

        if (currentHealth > healthCount)
            healthCount += healthDelta;
        if (currentHealth < healthCount) 
            healthCount -= healthDelta;           
        if (Mathf.Abs(currentHealth - healthCount) < healthDelta)
            healthCount = currentHealth;

        health.fillAmount = healthCount;


        
    }
}
