using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private int swordDamage;
    public int SwordDamage {
        get { return swordDamage; }
        set { swordDamage = value; }

    }
}
