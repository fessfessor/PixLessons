
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour, IPooledObject
{
    private Image healthFiller;
    private int healthCount;
    private Player player;

    private bool isDamaged;
    private int maxHealth;
    private int currentHealth;
    private ObjectPooler pooler;
    private SpriteRenderer sr;
    private GameObject canvas;
    private GameObject healthObj;
    private RectTransform canvasRectT;
    

    private Vector2 screenPoint;
    private HealthBarEnemy healthBar;

    private RectTransform healthRect;


    void Start()
    {
        
        canvas = GameObject.Find("MainUICanvas");
        canvasRectT = canvas.GetComponent<RectTransform>();
        sr = GetComponent<SpriteRenderer>();
        isDamaged = false;
        maxHealth = GetComponent<Health>().HealthCount;
        pooler = ObjectPooler.Instance;
        GameManager.Instance.pooledObjectContainer.Add(gameObject, this);

    }


    void Update() {
        // Спавним хелс бар, по координатам в центр-верх спрайта
        screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector3(sr.bounds.center.x, sr.bounds.max.y + 0.5f));
        currentHealth = GameManager.Instance.healthContainer[gameObject].HealthCount;

        // Проверка на дамаг
        if (maxHealth > currentHealth && !isDamaged) {
            // Спавним хелс бар, по координатам в центр-верх спрайта и родителем ставим этот объект
            healthObj = pooler.SpawnFromPool("EnemyHealthBar", new Vector3(sr.bounds.center.x, sr.bounds.max.y + 0.5f), Quaternion.identity, canvas);

            healthRect = healthObj.GetComponent<RectTransform>();
            //Безумная конструкция чтобы получить image заспавненного хелсбара
            healthFiller = healthObj.transform.GetChild(0).GetChild(0).GetComponent<Image>();
            // Спавним хелс бар на противнике
            healthRect.anchoredPosition = screenPoint - canvasRectT.sizeDelta / 2f;
            isDamaged = true;
        }

        if (isDamaged) {
            // Выставляем позицию для хелс бара над объектом          
            healthRect.anchoredPosition = screenPoint - canvasRectT.sizeDelta / 2f;           
            healthFiller.fillAmount = currentHealth / 100.0f;
            //Debug.Log(healthRect.anchoredPosition);
        }

        if(currentHealth <= 0) {
            pooler.ReturnToPool("EnemyHealthBar", healthObj);
            
        }        
            
                   
    }



    public void OnSpawnFromPool() {

    }

    public void OnReturnToPool() {
        //TODO Почему то у этого объекта при возвращении в пул меняется скейл на супер мелкий
        

        gameObject.SetActive(false);       
        healthFiller.fillAmount = 1f;
    }
}
