using UnityEngine;

public class ClockScript : MonoBehaviour
{
    private TMPro.TextMeshProUGUI clock;
    void Start()
    {
        clock = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        int hour = (int)GameState.gameTime24;
        int minute = Mathf.FloorToInt((GameState.gameTime24 - hour) * 60);

        clock.text=$"{hour:D2}:{minute:D2}";
    }
}
