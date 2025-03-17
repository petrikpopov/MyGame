using UnityEngine;
using UnityEngine.UI;

public class MenuDifficultScript : MonoBehaviour
{
    private Toggle compassToggle;
    private Toggle clockToggle;
    private Toggle hintsToggle;
    void Start()
    {
        Transform layout1 = transform.Find("Content/GameDifficulty/Layout1");
        compassToggle = layout1.Find("CompassToggle").GetComponent<Toggle>();
        compassToggle.isOn = GameState.isCompasVisible;

        clockToggle = layout1.Find("ClockToggle").GetComponent<Toggle>();
        clockToggle.isOn = GameState.isClockVisible;

        hintsToggle = layout1.Find("HintsToggle").GetComponent<Toggle>();
        hintsToggle.isOn = GameState.isHintsVisible;

        Transform layout2 = transform.Find("Content/GameDifficulty/Layout2");
    }

    void Update()
    {
        
    }
    public void OnCoinZooneSlider(float value) 
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

    public void onHintsToggle (bool value) 
    {
        GameState.isHintsVisible = value;
    }
}
