using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Enemy.SkeletonDragonBoss
{
    public class DragonAttackArea : MonoBehaviour
    {
        [SerializeField]private GameObject parent;
        [SerializeField]private ATTACK_AREA_TYPE areaType;

        private GameObject playerTrigger;
        private SkeletonDragonBoss parentScript;
        

        void Start()
        {
            playerTrigger = GameManager.Instance.playerTrigger;
            parentScript = parent.GetComponent<SkeletonDragonBoss>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            var colGameObj = col.gameObject;
            if (colGameObj == playerTrigger)
            {
                SetPlayerInArea(true);
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            var colGameObj = col.gameObject;
            if (colGameObj == playerTrigger)
            {
                SetPlayerInArea(false);
            }
        }


        private void SetPlayerInArea(bool playerInArea)
        {
            switch (areaType)
            {
                case ATTACK_AREA_TYPE.Short:
                parentScript.PlayerInShortArea = playerInArea;
                break;
                case ATTACK_AREA_TYPE.Medium:
                break;
                case ATTACK_AREA_TYPE.Long:
                parentScript.PlayerInLongArea = playerInArea;
                break;
                case ATTACK_AREA_TYPE.Back:
                parentScript.PlayerInBackArea = playerInArea;
                break;
                case ATTACK_AREA_TYPE.Walk:
                parentScript.PlayerInWalkArea = playerInArea;
                break;
            }
        }
        enum ATTACK_AREA_TYPE{Short, Medium ,Long, Back, Walk}
    }
}