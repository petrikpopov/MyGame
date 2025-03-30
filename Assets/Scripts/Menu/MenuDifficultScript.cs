using UnityEngine;
using UnityEngine.UI;

public class MenuDifficultScript : MonoBehaviour
{
    private Toggle compassToggle;
    private Toggle clockToggle;
    private Toggle hintsToggle;
    private Slider coinSpawnZoneSlider;
    private Slider coinProbabilitySlider;
    private Slider staminaSlider;
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

        coinSpawnZoneSlider = layout2.Find("CoinSpawnZone/Slider").GetComponent<Slider>();
        coinSpawnZoneSlider.value = 1 - Mathf.Sqrt(
        (GameState.coinSpawnDistance - GameState.coinSpawnDistanceMin) /
        (GameState.coinSpawnDistanceMax - GameState.coinSpawnDistanceMin));

        coinProbabilitySlider = layout2.Find("CoinProbability/Slider").GetComponent<Slider>();
        coinProbabilitySlider.value = GameState.coinSpawnProbability;

        staminaSlider = layout2.Find("Stamina/Slider").GetComponent<Slider>();
        staminaSlider.value = GameState.maxStamina;
    }

    void Update()
    {
        
    }
    public void OnStaminaSlider(float value) {
        GameState.maxStamina = value * GameState.maxPossibleStamina;
    }
    public void OnCoinProbabilitySlider(float value) {
        GameState.coinSpawnProbability = value;
    }
    public void OnCoinZooneSlider(float value) 
    {
        float x = 1 - value;
        GameState.coinSpawnDistance = GameState.coinSpawnDistanceMin + (GameState.coinSpawnDistanceMax - GameState.coinSpawnDistanceMin) * x * x;
        Debug.Log("CoinSpawnDistance: " + GameState.coinSpawnDistance);
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
