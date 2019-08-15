using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuMuteButton : MonoBehaviour
{
    private Text text;
    private Button selfButton;


    
    void Start()
    {
        text = GetComponentInChildren<Text>();

        if (AudioListener.volume == 0) {
            text.text = "Sound : Off";
        }
        else {
            text.text = "Sound : On";
        }
    }

    public void ChangeText() {
       

        if (AudioListener.volume == 0) {
            text.text = "Sound : Off";
        }
        else {
            text.text = "Sound : On";
        }
    }

    
    
}
