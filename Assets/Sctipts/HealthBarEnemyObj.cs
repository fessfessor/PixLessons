using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemyObj : MonoBehaviour, IPooledObject
{
    private RectTransform healthRect;
    private Image healthFiller;
    [SerializeField] GameObject filler;


    

    void Start()
    {
        healthFiller = filler.GetComponent<Image>();
        healthRect = GetComponent<RectTransform>();
        GameManager.Instance.pooledObjectContainer.Add(gameObject, this);
    }


    public void OnSpawnFromPool() {
        Debug.Log("Ols scale - " + healthRect.localScale);
        healthRect.localScale = new Vector3(1f, 1f, 1f);
        Debug.Log("New scale - " + healthRect.localScale);
        
        healthFiller.fillAmount = 1f;
    }

    public void OnReturnToPool() {

       // Debug.Log("Ols scale - " + healthRect.localScale);
        Debug.Log("Return");
       // healthRect.localScale = new Vector3(1f, 1f, 1f);
       // Debug.Log("New scale - " + healthRect.localScale);
        gameObject.SetActive(false);
       // healthFiller.fillAmount = 1f;
    }

}
