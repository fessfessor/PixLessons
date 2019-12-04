using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlatformerTools 
{

    // Показать хелс бар
    public static void  ShowHealthBar(GameObject obj) {
        if(obj.gameObject.transform.childCount > 0) {
            for (int i = 0; i < obj.gameObject.transform.childCount; i++) {
                if (obj.gameObject.transform.GetChild(i).transform.name == "HealthBar") {
                    obj.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }

    }


    

}
