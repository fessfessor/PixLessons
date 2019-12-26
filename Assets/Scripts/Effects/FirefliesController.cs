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
        systems = new[] { System01, System02, System03 };
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

            systems[0].transform.position += Vector3.right * 6;
            ArrayScrollRight();

        }

        if (playerIsLeft)
        {
            systems[2].transform.position += Vector3.left * 6;
            ArrayScrollLeft();
        }

    }

    private void ArrayScrollRight()
    {
        GameObject temp0 = systems[0];
        GameObject temp1 = systems[1];
        GameObject temp2 = systems[2];

        systems[0] = temp2;
        systems[1] = temp0;
        systems[2] = temp1;
    }

    private void ArrayScrollLeft()
    {
        GameObject temp0 = systems[0];
        GameObject temp1 = systems[1];
        GameObject temp2 = systems[2];

        systems[0] = temp1;
        systems[1] = temp2;
        systems[2] = temp0;
    }



}
