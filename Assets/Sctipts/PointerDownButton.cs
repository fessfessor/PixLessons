
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointerDownButton : MonoBehaviour, IPointerDownHandler
{
    
    [SerializeField] private Button attack;    
    [SerializeField] private Button fire;
    [SerializeField] private Player player;
    [SerializeField] private ButtonType buttonType;

    public Button Attack { get => attack; }   
    public Button Fire { get => fire; }



    public void OnPointerDown(PointerEventData eventData) {

        if (buttonType == ButtonType.Attack) {
            player.Attack();
        }
        if (buttonType == ButtonType.Fire) {
            player.Shoot();
        }
    }


}

public enum ButtonType : byte
{
    Attack, Fire
}
