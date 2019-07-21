
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private PressedButton left;
    [SerializeField] private PressedButton right;
    [SerializeField] private Button attack;
    [SerializeField] private Button jump;
    [SerializeField] private Button fire;
    
    

    public PressedButton Left { get => left; }
    public PressedButton Right { get => right; }
    public Button Attack { get => attack; }
    public Button Jump { get => jump; }
    public Button Fire { get => fire; }

   
}
