
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image health;
    private int healthCount;
    private Player player;


    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    
    void Update()
    {
        health.fillAmount = player.Health.HealthCount / 100.0f;
    }
}
