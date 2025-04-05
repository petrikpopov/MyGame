using System;
using UnityEngine;

public class MenuQualityScript : MonoBehaviour
{
    [SerializeField]
    private Material[] daySkyBoxes = new Material[0] ;
    [SerializeField]
    private Material[] nightSkyBoxes = new Material[0];
    private Material defauldSkyBox;
    private TMPro.TMP_Dropdown graphicsDropdown;
    private TMPro.TMP_Dropdown fogDropdown;
    private TMPro.TMP_Dropdown daySkyDropdown;
    private TMPro.TMP_Dropdown nightSkyDropdown;
    void Start()
    {
        Transform layout = transform.Find("Content/Quality/Layout");
        graphicsDropdown = layout.Find("Graphics/Dropdown").GetComponent<TMPro.TMP_Dropdown>();
        fogDropdown = layout.Find("Fog/Dropdown").GetComponent<TMPro.TMP_Dropdown>();
        daySkyDropdown = layout.Find("DaySky/Dropdown").GetComponent<TMPro.TMP_Dropdown>();
        nightSkyDropdown = layout.Find("NightSky/Dropdown").GetComponent<TMPro.TMP_Dropdown>();
        InitQualityDropdown();
        InitFogDropdown();
        InitDayDropdown();
        InitNightDropdown();
        GameEventController.AddListener(nameof(GameState), OnGameStateEvent);
    }
    private void InitDayDropdown () {
        daySkyDropdown.ClearOptions();
        foreach(Material m in daySkyBoxes) {
            daySkyDropdown.options.Add(new(m.name));
        }
        defauldSkyBox = RenderSettings.skybox;
        if(defauldSkyBox!=null) {
            daySkyDropdown.options.Add(new(defauldSkyBox.name));
            daySkyDropdown.value = daySkyBoxes.Length; 
        }
        else {
            daySkyDropdown.value = -1;
        }        
    }
    private void InitNightDropdown () {
        nightSkyDropdown.ClearOptions();
        foreach(Material m in nightSkyBoxes) {
            nightSkyDropdown.options.Add(new(m.name));
        }
        defauldSkyBox = RenderSettings.skybox;
        if(defauldSkyBox!=null) {
            nightSkyDropdown.options.Add(new(defauldSkyBox.name));
            nightSkyDropdown.value = nightSkyBoxes.Length;      
        }
        else {
            nightSkyDropdown.value = -1;
        }
    }
    public void OnDaySkyDropdownChange(int index) {
        if(index < daySkyBoxes.Length) {
           GameState.daySkybox = daySkyBoxes[index];
        } 
        else {
            GameState.daySkybox = defauldSkyBox;
        }
    }
    public void OnNightSkyDropdownChange(int index) {
        if(index < nightSkyBoxes.Length) {
           GameState.nightSkybox = nightSkyBoxes[index];
        } 
        else {
            GameState.nightSkybox = defauldSkyBox;
        }
    }
    private void InitFogDropdown () {
        fogDropdown.ClearOptions();
        fogDropdown.options.Add(new("Off"));
        foreach (string name in Enum.GetNames(typeof(FogMode))) {
            fogDropdown.options.Add(new (name));
        }
        if(RenderSettings.fog) {
            fogDropdown.value = (int)RenderSettings.fogMode;
        }
        else {
            fogDropdown.value = 0;
        }
    }
    public void OnFogDropdownChange(int index) {
        if(index == 0) {
            RenderSettings.fog = false;
        }
        else {
            RenderSettings.fog = true;
            RenderSettings.fogMode = (FogMode)index;
        }
    }
    private void InitQualityDropdown() {
        graphicsDropdown.ClearOptions();
        foreach (string name in QualitySettings.names) {
            graphicsDropdown.options.Add(new (name));
        }
        int currntQualityLevel = QualitySettings.GetQualityLevel();
        graphicsDropdown.value = currntQualityLevel;
    }

    public void OnGraphicsDropdownChange(int index) {
        QualitySettings.SetQualityLevel(index, true);
    }

    private void OnGameStateEvent (string type, object payload) {
        if(nameof(GameState.activeSceneIndex).Equals(payload)) {
            InitFogDropdown();
            InitDayDropdown();
            InitNightDropdown();
        }
    }
    void OnDestroy()
    {
        GameEventController.RemoveListener(nameof(GameState), OnGameStateEvent);
    }
}
