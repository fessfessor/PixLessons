using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LocalizedText : MonoBehaviour
{
    public string key;
    private Text textComponent;
    

    void Start()
    {
        textComponent = GetComponent<Text>();
        if (LocalizationManager.Instance.isReady) 
            textComponent.text = LocalizationManager.Instance.GetLocalizedValue(key);
    }

    private void Update() {
        if (LocalizationManager.Instance.isReady) {
            textComponent.text = LocalizationManager.Instance.GetLocalizedValue(key);
        }

    }


}
