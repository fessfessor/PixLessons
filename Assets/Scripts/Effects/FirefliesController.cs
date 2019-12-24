using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirefliesController : MonoBehaviour
{
    
    public GameObject System01;
    public GameObject System02;
    public GameObject System03;

    private ParticleSystem particleSystem;
    private float radius;
    
    private GameObject[] systems;
    private GameObject player;
    private float particleSystemRadius = 10f;
     


    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        radius = particleSystem.shape.radius;
        systems = new[] {System01, System02, System03};
        player = GameManager.Instance.player;
    }

    
    void Update()
    {
        CheckParticleSystems();
    }

    void CheckParticleSystems()
    {
        bool playerIsRight = player.transform.position.x > systems[2].transform.position.x;
        bool playerIsLeft = player.transform.position.x < systems[0].transform.position.x;
        if (playerIsRight)
        {
            
            systems[0].transform.position *= Vector2.right * particleSystemRadius * 6;
            

        }

        if (playerIsLeft)
        {
            systems[2].transform.position *= Vector2.left * particleSystemRadius * 6;
        }

    }

    
   
}
