using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tractor : Vehicle {
    public override void Beep() {
        Debug.Log("Im class Tractor");
    }

    public override void BeepVirt() {
        Debug.Log("IM class Tractor Method BeepVirt");

        base.BeepVirt();

    }
}
