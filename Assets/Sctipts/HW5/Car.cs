using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Vehicle {
    public override void Beep() {
        Debug.Log("Im class Car");
    }

    public override void BeepVirt() {
        Debug.Log("IM class Car Method BeepVirt");

        base.BeepVirt();
        
    }
}
