using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    public bool isGrounded;

    public LayerMask groundLayer;
    public Transform groundPoint;




    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, .1f, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundPoint.position, .1f);
    }


}
