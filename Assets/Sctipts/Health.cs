﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour
{
    public int health;
    


    public void takeHit(int damage) {
        health -= damage;
        if (health <= 0)
            Destroy(gameObject);      
    }

   

}
