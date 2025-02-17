using System.Text.RegularExpressions;
using UnityEngine;

public class DayNightScript : MonoBehaviour
{
    private float dayDuration = 20.0f;
    private float rotateAngle;
    private float dayHour;
    private float dayPhase;
    private Light sun;
     private Light moon;
    private Material daySkyBox;
    void Start()
    {
        rotateAngle = -360.0f / dayDuration;
        dayHour = 12;
        sun = transform.Find("Sun").gameObject.GetComponent<Light>();
        moon = transform.Find("Moon").gameObject.GetComponent<Light>();
        daySkyBox = RenderSettings.skybox;
    }

    // Update is called once per frame
    void Update()
    {
        dayHour += 24 * Time.deltaTime / dayDuration;
        if(dayHour >= 24) 
        {
            dayHour -= 24;
        }
        DayPhase dayPhase = PhaseFromHour(dayHour); 
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
        }
        else
        {   
            float sinAng = (dayHour - 4) * Mathf.PI / 16;
            coef = Mathf.Sin(sinAng);
            RenderSettings.sun = sun;
            sun.intensity = coef;
        }
        RenderSettings.ambientIntensity = coef;
        daySkyBox.SetFloat("_Exposure", coef);

        this.transform.Rotate(0 , 0 , rotateAngle * Time.deltaTime);
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
}

enum DayPhase {
    Night,
    Dawn,
    Day,
    Dusk
}
