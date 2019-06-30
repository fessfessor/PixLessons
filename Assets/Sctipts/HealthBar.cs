using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    Vector3 localScale;
    SpriteRenderer sr;
    Coroutine hideProcess;

    void Start()
    {
        localScale = transform.localScale;

        sr = gameObject.GetComponent<SpriteRenderer>();
        //Выключаем хелс бар при создании, он будет показываться только при получении урона
        sr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (sr.enabled && hideProcess == null) {
            StopAllCoroutines();
            hideProcess = StartCoroutine(hideHealthBar());
        }
            

        localScale.x = gameObject.GetComponentInParent<Health>().HealthCount * 0.01f;
        transform.localScale = localScale;
    }

    // После получения дамага выключаем хелс бар, чтобы не мешался через 2с
    IEnumerator hideHealthBar() {
        yield return new WaitForSeconds(2f);
        sr.enabled = false;
        hideProcess = null;

    }
    
}
