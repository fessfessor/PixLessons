using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameCoin : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start() {
        GameManager.Instance.flameCoinContainer.Add(gameObject,this);
    }

    public void StartDestroy() {
        animator.SetTrigger("TakeFlame");
    }

    public void EndDestroy() {
        Destroy(gameObject);
    }
}
