using System;
using UnityEngine;

public class MenuQualityScript : MonoBehaviour
{
    [SerializeField]
    private Material[] daySkyBoxes;
    private Material defauldSkyBox;
    private TMPro.TMP_Dropdown graphicsDropdown;
    private TMPro.TMP_Dropdown fogDropdown;
    private TMPro.TMP_Dropdown daySkyDropdown;
    void Start()
    {
        Transform layout = transform.Find("Content/Quality/Layout");
        graphicsDropdown = layout.Find("Graphics/Dropdown").GetComponent<TMPro.TMP_Dropdown>();
        fogDropdown = layout.Find("Fog/Dropdown").GetComponent<TMPro.TMP_Dropdown>();
        daySkyDropdown = layout.Find("DaySky/Dropdown").GetComponent<TMPro.TMP_Dropdown>();
        InitQualityDropdown();
        InitFogDropdown();
        InitDayDropdown();
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
        }      
    }
    public void OnDaySkyDropdownChange(int index) {
        if(index < daySkyBoxes.Length) {
            RenderSettings.skybox = daySkyBoxes[index];
        } 
        else {
            RenderSettings.skybox = defauldSkyBox;
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
        }
    }
    void OnDestroy()
    {
        GameEventController.RemoveListener(nameof(GameState), OnGameStateEvent);
    }
}
