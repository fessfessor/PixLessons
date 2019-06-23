using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour
{
    public string name;
    abstract public void Beep();  
    
    virtual public void BeepVirt() {
        Debug.Log("Im class Vehicle method BeepVirt");
    }
    
}
