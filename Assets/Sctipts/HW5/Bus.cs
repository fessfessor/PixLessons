using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bus : Vehicle {
    public override void Beep() {
        Debug.Log("Im class Bus");

    }

    public override void BeepVirt() {
        Debug.Log("IM class Bus Method BeepVirt");

        base.BeepVirt();

    }
}
