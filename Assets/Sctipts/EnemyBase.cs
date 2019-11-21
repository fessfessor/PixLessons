using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemies", menuName = "Databases Enemy")]
public class EnemyBase : ScriptableObject
{   
    
        [SerializeField] private List<Enemy> enemies;
        [SerializeField] private Enemy currentEnemy;


        private int currentIndex;



        public void CreateEnemy() {
            if (enemies == null)
                enemies = new List<Enemy>();

            Enemy enemy = new Enemy();
            enemies.Add(enemy);
            currentEnemy = enemy;
            currentIndex = enemies.Count - 1;
        }


        public void RemoveEnemy() {
            if (enemies == null || currentEnemy == null)
                return;

            enemies.Remove(currentEnemy);

            if (enemies.Count > 0)
                currentEnemy = enemies[0];
            else
                CreateEnemy();
            currentIndex = 0;

        }

        public void NextEnemy() {
            if (currentIndex + 1 < enemies.Count) {
                currentIndex++;
                currentEnemy = enemies[currentIndex];
            }
        }

        public void PrevEnemy() {
            if (currentIndex > 0) {
                currentIndex--;
                currentEnemy = enemies[currentIndex];
            }
        }

        public Enemy GetEnemyOfID(int id) {
            return enemies.Find(t => t.ID == id);
        }
    }







[System.Serializable]
public class Enemy {
    [SerializeField] private int id;
    public int ID { get => id; }

    [SerializeField] private string enemyName;
    public string EnemyName { get => enemyName; }

    [SerializeField] private string description;
    public string Description { get => description; }

    [SerializeField] private int enemyHealth;
    public int EnemyHealth { get => enemyHealth; set => enemyHealth = value; }

    [SerializeField] private int damage;
    public int Damage { get => damage; set => damage = value; }

    [SerializeField] private int speed;
    public int Speed { get => speed; set => speed = value; }

    [SerializeField] private ENEMY_DANGER dangerClass;
    public ENEMY_DANGER DangerClass { get => dangerClass; set => dangerClass = value; }

    public override string ToString() {
        return $"Id - {id} Description - {description} .Health - {enemyHealth} .Damage - {damage}. Speed - {speed} .Danger class - {dangerClass.ToString()}";
    }
}
