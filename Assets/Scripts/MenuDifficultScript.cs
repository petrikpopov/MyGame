using UnityEngine;
using UnityEngine.UI;

public class MenuDifficultScript : MonoBehaviour
{
    private Toggle compassToggle;
    private Toggle clockToggle;
    void Start()
    {
        Transform layout1 = transform.Find("Content/GameDifficulty/Layout1");
        compassToggle = layout1.Find("CompassToggle").GetComponent<Toggle>();
        compassToggle.isOn = GameState.isCompasVisible;

        clockToggle = layout1.Find("ClockToggle").GetComponent<Toggle>();
        clockToggle.isOn = GameState.isClockVisible;
    }

    void Update()
    {
        
    }
    public void onCompasToggle (bool value) 
    {
        GameState.isCompasVisible = value;
    }

    public void onClockToggle (bool value) 
    {
        GameState.isClockVisible = value;
    }
}
