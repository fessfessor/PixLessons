﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("TRIGGER ");
    }

    private void OnTriggerExit2D(Collider2D collision) {
        Debug.Log("TRIGGER EXIT");
    }
}
