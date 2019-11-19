using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollow : MonoBehaviour
{
    [SerializeField] private ParticleSystem partSystem;
    [SerializeField] private ParticleSystem.Particle[] particles;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private float affectDistance;
    [SerializeField] private float followDelay;
    [SerializeField] private float followSpeed;
    private float sqrDist;
    
    private bool isReady = false;
    


    void Start()
    {
        
        partSystem = GetComponent<ParticleSystem>();
        sqrDist = affectDistance * affectDistance;
        StartCoroutine(FollowDelay());

    }

    
    void Update()
    {
        if (isReady) {
            particles = new ParticleSystem.Particle[partSystem.particleCount];
            partSystem.GetParticles(particles);

            for (int i = 0; i < particles.GetUpperBound(0); i++) {
                
                // Пока что жестко зашито следование за игроком. Как понадобится можно довести до ума с targetObject
                particles[i].position = Vector2.MoveTowards(particles[i].position,GameManager.Instance.player.transform.position - transform.position , Time.deltaTime * followSpeed);

            }
            partSystem.SetParticles(particles, particles.Length);
        }

     
    }

    IEnumerator FollowDelay() {
        yield return new WaitForSeconds(followDelay);
        isReady = true;
    }
}
