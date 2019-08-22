using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuToggle : MonoBehaviour
{
    [SerializeField] private Sprite OnImage;
    [SerializeField] private Sprite OffImage;
    [SerializeField] private GameObject Background;
    [SerializeField] public bool isOn;

    

    private Image backgroundImage;

    
    void Start()
    {


        isOn = PlayerPrefs.GetInt("MenuSound") == 1 ? true : false;
        backgroundImage = Background.GetComponent<Image>();

        Init();
    }

    public void ToggleChangeValue() {
        isOn = GetComponent<Toggle>().isOn;
        if (isOn) {
            PlayerPrefs.SetInt("MenuSound", 1);
            backgroundImage.sprite = OnImage;
            if(PlayerPrefs.HasKey("volume"))
                AudioListener.volume = PlayerPrefs.GetFloat("volume");
            else
                AudioListener.volume = 1f;
        }
        else {
            PlayerPrefs.SetInt("MenuSound", 0);
            backgroundImage.sprite = OffImage;
            AudioListener.volume = 0;
        }
    }

    private void Init() {
        if (isOn) {
            GetComponent<Toggle>().isOn = true;
            backgroundImage.sprite = OnImage;
            if (PlayerPrefs.HasKey("volume"))
                AudioListener.volume = PlayerPrefs.GetFloat("volume");
            else
                AudioListener.volume = 1f;
        }
        else {
            GetComponent<Toggle>().isOn = false;
            backgroundImage.sprite = OffImage;
            AudioListener.volume = 0;
        }
    }
        
}
