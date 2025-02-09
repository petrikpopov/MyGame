using UnityEngine;

public class CoinScript : MonoBehaviour
{
     private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameEventController.EmitEvent("Collected", "Coin");
        animator.SetTrigger("OnCollected");
    }

    public void OnDisappearClipEnd()
    {
        GameEventController.EmitEvent("Disappear", "Coin");
        Destroy(this.gameObject);
    }
}
