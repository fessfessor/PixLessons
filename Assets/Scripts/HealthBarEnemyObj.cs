using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemyObj : MonoBehaviour, IPooledObject
{
    private RectTransform healthRect;
    private Image healthFiller;
    [SerializeField] GameObject filler;


    

    void OnEnable()
    {
        healthFiller = filler.GetComponent<Image>();
        healthRect = GetComponent<RectTransform>();
               
    }


    public void OnSpawnFromPool() {        
        healthRect.localScale = new Vector3(1f, 1f, 1f);
        healthFiller.fillAmount = 1f;
    }

    public void OnReturnToPool() {
        gameObject.SetActive(false);
    }

}
