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
                // float ForceToAdd = (particles[i].startLifetime - particles[i].remainingLifetime) * (10 * Vector3.Distance(targetObject.transform.position, particles[i].position));
                // particles[i].velocity = (targetObject.transform.position - particles[i].position) * ForceToAdd * Time.deltaTime * followSpeed;
                //todo Непонятно что с Y координатой
                particles[i].position = Vector3.MoveTowards(particles[i].position, Vector2.one, Time.deltaTime * followSpeed);
                //Debug.Log(particles[i].position);

                

            }

            

            partSystem.SetParticles(particles, particles.Length);
        }



        
            
    }

    IEnumerator FollowDelay() {
        yield return new WaitForSeconds(followDelay);
        isReady = true;
    }
}
