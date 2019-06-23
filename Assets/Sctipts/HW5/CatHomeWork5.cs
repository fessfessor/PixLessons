using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatHomeWork5 : MonoBehaviour
{
    private string name;
    private int age;
    
    public int Weight { get; set; }
    private int height;
    private int tailSize;


    void Mew() {
        Debug.Log("name - " + Name + " age - " + age + " weight - " + Weight + " height - " + height + " tailSize - " + tailSize);
    }

    private void Start() {
        CatHomeWork5 cat = new CatHomeWork5("B", 1, 3, 4, 5);
        cat.Mew();
    }

    public CatHomeWork5() {
        
    }

    public CatHomeWork5 (string name, int age, int weight, int height, int tailSize) {
        this.name = name;
        this.age = age;
        this.Weight = weight;
        this.height = height; 
        this.tailSize = tailSize;

    }

    public string Name {
        get { return name; }        
    }




}
