using UnityEngine;
using UnityEngine.UI;
public class StaminaScript : MonoBehaviour
{
   private GameObject content;
   private Image indicator;
    void Start()
    {
        Transform t = transform.Find("Content");
        indicator = t.Find("Indicator").GetComponent<Image>();
        content = t.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameState.maxStamina > 0) {
            indicator.fillAmount = Mathf.Clamp01(GameState.stamina / GameState.maxStamina);
        }
    }
}
