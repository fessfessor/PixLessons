using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance { get; private set; }

    [Header("SkeletonArcher")]
    public int ArrowDamage = 15;

    private void Awake()
    {
        Instance = this;
    }





}
