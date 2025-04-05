using System.Text.RegularExpressions;
using UnityEngine;

public class DayNightScript : MonoBehaviour
{
    private float dayDuration = 3000.0f;
    private float rotateAngle;
    private float dayHour;
    private float dayPhase;
    private Light sun;
     private Light moon;
  
    void Start()
    {
        rotateAngle = -360.0f / dayDuration;
        dayHour = 12;
        sun = transform.Find("Sun").gameObject.GetComponent<Light>();
        moon = transform.Find("Moon").gameObject.GetComponent<Light>();
        GameEventController.AddListener(nameof(GameState), OnGameEvent);
    }

    // Update is called once per frame
    void Update()
    {
        DayPhase prevDayPhase = PhaseFromHour(dayHour);
        dayHour += 24 * Time.deltaTime / dayDuration;
        if(dayHour >= 24) 
        {
            dayHour -= 24;
        }
        GameState.gameTime24 = dayHour;
        DayPhase dayPhase = PhaseFromHour(dayHour); 
        if(prevDayPhase!=dayPhase) {
            DayPhaseChanged();
        }
        float coef;
        if(dayPhase == DayPhase.Night) 
        {
            float cosAng;
            if(dayHour < 4)
            {
                cosAng = (dayHour - 0) * 4 * Mathf.PI / (2*4);
            }
            else {
                cosAng =  (dayHour - 24) * Mathf.PI / (2*4);
            }
            coef = Mathf.Cos(cosAng) / 3;
            RenderSettings.sun = moon;
            moon.intensity = coef;
            coef *= 2.0f;
        }
        else
        {   
            float sinAng = (dayHour - 4) * Mathf.PI / 16;
            coef = Mathf.Sin(sinAng);
            RenderSettings.sun = sun;
            sun.intensity = coef;
        }
        RenderSettings.ambientIntensity = coef;
        RenderSettings.skybox.SetFloat("_Exposure", coef);

        this.transform.Rotate(0 , 0 , rotateAngle * Time.deltaTime);
    }

    private void DayPhaseChanged () {
        DayPhase dayPhase = PhaseFromHour(dayHour);
        if(dayPhase == DayPhase.Night) {
            if(GameState.nightSkybox != null) {
                RenderSettings.skybox = GameState.nightSkybox;
            }
        }
        else {
            if(GameState.daySkybox != null) {
                RenderSettings.skybox = GameState.daySkybox;
            }
        }
    }
    private DayPhase PhaseFromHour(float hour) 
    {
        if(hour > 20 || hour < 4) {
            return DayPhase.Night;
        }
        if(hour < 7) {
            return DayPhase.Dawn;
        }
        if(hour > 17) {
            return DayPhase.Dusk;
        }
        return DayPhase.Day;
    }

    private void OnGameEvent (string type, object payload) {
        if(nameof(GameState.daySkybox).Equals(payload)) 
        {
            if(PhaseFromHour(dayHour) != DayPhase.Night && GameState.daySkybox !=null) {
                RenderSettings.skybox = GameState.daySkybox;
            }
        }
        else if(nameof(GameState.nightSkybox).Equals(payload)) 
        {
            if(PhaseFromHour(dayHour) == DayPhase.Night && GameState.nightSkybox !=null) {
                RenderSettings.skybox = GameState.nightSkybox;
            }
        }
    }
    private void OnDestroy()
    {
        GameEventController.RemoveListener(nameof(GameState), OnGameEvent);
    }
}

enum DayPhase {
    Night,
    Dawn,
    Day,
    Dusk
}
