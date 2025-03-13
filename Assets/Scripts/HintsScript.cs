using UnityEngine;

public class HintsScript : MonoBehaviour
{
    private Transform coin;
    private GameObject leftHint;
    private GameObject rightHint;
    void Start()
    {
        coin = GameObject.Find("Coin").transform;
        leftHint = transform.Find("LeftHint").gameObject;
        rightHint = transform.Find("RightHint").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 wvpR = Camera.main.WorldToViewportPoint(coin.position + Camera.main.transform.right * 0.75f);
       Vector3 wvpL = Camera.main.WorldToViewportPoint(coin.position - Camera.main.transform.right * 0.75f);

        if (wvpR.z > 0 && wvpL.z > 0) {
            if(wvpR.x < 0) {
                leftHint.SetActive(true);
                rightHint.SetActive(false);
            } 
            else if (wvpL.x > 1) {
                leftHint.SetActive(false);
                rightHint.SetActive(true);
            }
            else {
                leftHint.SetActive(false);
                rightHint.SetActive(false);
            }
        } 
        else {
            float a = Vector3.SignedAngle(Camera.main.transform.forward, coin.position - Camera.main.transform.position, Vector3.down);

            if(a < 0) {
                leftHint.SetActive(false);
                rightHint.SetActive(true);
            } 
            else {
                leftHint.SetActive(true);
                rightHint.SetActive(false);
            }
        }
    }
}
