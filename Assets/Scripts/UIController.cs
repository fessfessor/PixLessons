
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    [SerializeField] private PressedButton left;
    [SerializeField] private PressedButton right;
    [SerializeField] private Button attack;
    [SerializeField] private Button jump;
    [SerializeField] private GameObject jumpObject;
    [SerializeField] private Button fire;

    private bool jumpButtonEnabled;



    public PressedButton Left { get => left; }
    public PressedButton Right { get => right; }
    public Button Attack { get => attack; }
    public Button Jump { get => jump; }
    public Button Fire { get => fire; }

    private void Start() {
        if (PlayerPrefs.HasKey("jumpButton")) {
            jumpButtonEnabled = PlayerPrefs.GetInt("jumpButton") == 0 ? false : true;
        }
        else {
            jumpButtonEnabled = false;
        }

        if (jumpButtonEnabled) {
            jumpObject.SetActive(true);
        }
        else {
            jumpObject.SetActive(false);
        }
            
    }

    



}
