using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson2HomeWork : MonoBehaviour
{
    // 1)
    public float squareSide = 1.0f;
    public float rectangleA = 2.0f;
    public float rectangleB = 3.0f;
    public float radius = 1.0f;

    // 2)
    public int appleCount = 10;
    public int orangeCount = 5;
    public int tomatoesCount = 15;

    // 3)
    
    long distanceToMoon = 300_000_000_000;
    long paperWidth = 1;
    long count  = 0;
    


    // Start is called before the first frame update
    void Start()
    {
        // 1)
       // Debug.Log("Square area = " + Mathf.Pow(squareSide, 2) + " Rectangle area = " + (rectangleA * rectangleB) + " Circle area = " + Mathf.PI * Mathf.Pow(radius, 2));
        Debug.Log( Mathf.Pow(squareSide, 2) + " " + (rectangleA * rectangleB) + " " + Mathf.PI * Mathf.Pow(radius, 2));


        // 2)
        if (appleCount > orangeCount && appleCount > tomatoesCount) {
            if (orangeCount > tomatoesCount)
                Debug.Log("Яблок больше всего - " + appleCount + " , апельсинов - " + orangeCount + " , томатов - " + tomatoesCount);
            else
                Debug.Log("Яблок больше всего - " + appleCount + " , томатов - " + tomatoesCount + " , апельсинов - " + orangeCount);
        }
        else if(orangeCount > appleCount && orangeCount > tomatoesCount) {
            if (appleCount > tomatoesCount)
                Debug.Log("Апельсинов больше всего - " + orangeCount + " , яблок - " + appleCount + " , томатов - " + tomatoesCount);
            else
                Debug.Log("Апельсинов больше всего - " + orangeCount + " , томатов - " + tomatoesCount + " , яблок - " + appleCount);

        }
        else if (tomatoesCount > appleCount && tomatoesCount > orangeCount) {
            if (orangeCount > appleCount)
                Debug.Log("Томатов больше всего - " + tomatoesCount + " , апельсинов - " + orangeCount + " , яблок - " + appleCount);
            else
                Debug.Log("Томатов больше всего - " + tomatoesCount + " , яблок - " + appleCount + " , апельсинов - " + orangeCount);

        }


        // 3)
        while (paperWidth < distanceToMoon) {          
            paperWidth *= 2;               
                count++;                       
        }
        Debug.Log("Количество складываний - " + count);


        //5 Методы 
        Debug.Log("2 урок, пункт 5, метод 1 - " + checkAandB(42, 66));  
        Debug.Log("2 урок, пункт 5, метод 1 - " + checkAandB(13, 5));

        msg();
        Debug.Log("2 урок, пункт 5, метод 3 - " + circleRadius(5.0f));

    }


    bool checkAandB(int a, int b) {
        return a > b ? true : false;
    }

    void msg() {
        Debug.Log("2 урок, пункт 5, метод 2");
    }

    float circleRadius(float radius) {
        return Mathf.PI * Mathf.Pow(radius, 2);

    }


}
