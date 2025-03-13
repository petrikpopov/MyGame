using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioControllerScript : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    private float masterVolume;
    private float ambiendVolume;
    private float effectsVolume;
    private float musicVolume;
    private Slider masterSlider;
    private Slider ambientSlider;
    private Slider effectsSlider;
    private Slider musicSlider;
    void Start()
    {
        GetVolumens();
       
        Transform grp = transform.Find("Content/SoundVolumes/Layout");

        masterSlider = grp.Find("Master/Slider").GetComponent<Slider>();
        masterSlider.value = DbToVolume(masterVolume);

        ambientSlider = grp.Find("Ambient/Slider").GetComponent<Slider>();
        ambientSlider.value= DbToVolume(ambiendVolume);

        effectsSlider = grp.Find("Effects/Slider").GetComponent<Slider>();
        effectsSlider.value= DbToVolume(effectsVolume);

        musicSlider = grp.Find("Music/Slider").GetComponent<Slider>();
        musicSlider.value= DbToVolume(musicVolume);
    }

    private void GetVolumens () {
        if(PlayerPrefs.HasKey(nameof(masterVolume))) {
            masterVolume = PlayerPrefs.GetFloat(nameof(masterVolume));
            audioMixer.SetFloat(nameof(masterVolume),masterVolume);
        }
        else if(!audioMixer.GetFloat(nameof(masterVolume), out masterVolume)) {
           Debug.LogError("Error getting float" + nameof(masterVolume));
        }

        if(PlayerPrefs.HasKey(nameof(ambiendVolume))) {
            ambiendVolume = PlayerPrefs.GetFloat(nameof(ambiendVolume));
            audioMixer.SetFloat(nameof(ambiendVolume),ambiendVolume);
        }
        else if(!audioMixer.GetFloat(nameof(ambiendVolume), out ambiendVolume)){
            Debug.LogError("Error getting float" + nameof(ambiendVolume));
        }

        if(PlayerPrefs.HasKey(nameof(effectsVolume))) {
            effectsVolume = PlayerPrefs.GetFloat(nameof(effectsVolume));
            audioMixer.SetFloat(nameof(effectsVolume),effectsVolume);
        }
        else if(!audioMixer.GetFloat(nameof(effectsVolume), out effectsVolume)) {
            Debug.LogError("Error getting float" + nameof(effectsVolume));
        }

        if(PlayerPrefs.HasKey(nameof(musicVolume))) {
            musicVolume = PlayerPrefs.GetFloat(nameof(musicVolume));
            audioMixer.SetFloat(nameof(musicVolume),musicVolume);
        }
        else if(!audioMixer.GetFloat(nameof(musicVolume), out musicVolume)) {
            Debug.LogError("Error getting float" + nameof(musicVolume));
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus)) 
        {
            ChangeMasterValume();
        } 
        if(Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus)) 
        {
            ChangeMasterValume(step:1);
        } 
    }

    private void ChangeMasterValume(float step = -1) {
        if(audioMixer.GetFloat(nameof(masterVolume), out masterVolume)) 
        {
            masterVolume = Mathf.Clamp(masterVolume + step * (Mathf.Abs(masterVolume + 3.0f) * 0.25f + 3.0f) , -80f, 20f);
            audioMixer.SetFloat(nameof(masterVolume), masterVolume);
        } 
        else 
        {
            Debug.Log("GetFloat Error");
        }
    }

    public void OnMasterSliderChange (float value) {
       masterVolume = -80 + 100 * Mathf.Sqrt(value);
       audioMixer.SetFloat(nameof(masterVolume), masterVolume);
       PlayerPrefs.SetFloat(nameof(masterVolume), masterVolume);
    }
    public void OnAmbientSliderChange (float value) {
       ambiendVolume = -80 + 100 * Mathf.Sqrt(value);
       audioMixer.SetFloat(nameof(ambiendVolume), ambiendVolume);
       PlayerPrefs.SetFloat(nameof(ambiendVolume), ambiendVolume);
    }
    public void OnEffectsSliderChange (float value) {
       effectsVolume = -80 + 100 * Mathf.Sqrt(value);
       audioMixer.SetFloat(nameof(effectsVolume), effectsVolume);
       PlayerPrefs.SetFloat(nameof(effectsVolume), effectsVolume);
    }
    public void OnMusicSliderChange (float value) {
       musicVolume = -80 + 100 * Mathf.Sqrt(value);
       audioMixer.SetFloat(nameof(musicVolume), musicVolume);
       PlayerPrefs.SetFloat(nameof(musicVolume), musicVolume);
    }
    private void Onestroy()
    {
        PlayerPrefs.Save();
    }
    private float DbToVolume(float db) {
        return Mathf.Pow((db + 80.0f) / 100.0f, 2.0f);
    }
    private float VolumeToDb(float volume) {
        return -80.0f + 100.0f * Mathf.Sqrt(volume);
    }
}
