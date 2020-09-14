using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirefliesGismo : MonoBehaviour
{
    public int count = 0;
    private ParticleSystem particleSys;
    private float radius;

    private void Start()
    {
        particleSys = GetComponent<ParticleSystem>();
        radius = particleSys.shape.radius;
    }



    void OnDrawGizmos() {
        Vector2 start = new Vector2(transform.position.x - radius, transform.position.y);
        Vector2 end = new Vector2(transform.position.x + radius, transform.position.y);
        switch (count) {
            case 1:
                Gizmos.color = Color.blue;
                break;
            case 2:
                Gizmos.color = Color.red;
                break;
            case 3:
                Gizmos.color = Color.white;
                break;

        }
        Gizmos.DrawLine(start, end);
    }
}
