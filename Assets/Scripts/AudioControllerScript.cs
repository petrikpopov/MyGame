using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class AudioControllerScript : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    private float masterVolume;
    private float ambiendVolume;
    private float effectsVolume;
    private float musicVolume;
    void Start()
    {
        audioMixer.GetFloat(nameof(masterVolume), out masterVolume);
        audioMixer.GetFloat(nameof(ambiendVolume), out ambiendVolume);
        audioMixer.GetFloat(nameof(effectsVolume), out effectsVolume);
        audioMixer.GetFloat(nameof(musicVolume), out musicVolume);
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
}
