using UnityEngine;

public class HintsScript : MonoBehaviour
{
    private Transform coin;
    private GameObject leftHint;
    private GameObject rightHint;
    private readonly string[] listEnableEvents = {"SpawnCoin", "Disappear", nameof(GameState)};
    void Start()
    {
        coin = GameObject.Find("Coin").transform;
        leftHint = transform.Find("LeftHint").gameObject;
        rightHint = transform.Find("RightHint").gameObject;
        GameEventController.AddListener(listEnableEvents, OnGameEvent);
    }

    // Update is called once per frame
    void Update()
    {
        if(coin == null) {
            var go = GameObject.FindGameObjectWithTag("Coin");
            if(go == null) return;
            else coin = go.transform;
        }

       Vector3 wvpR = Camera.main.WorldToViewportPoint(coin.position + Camera.main.transform.right * 0.75f);
       Vector3 wvpL = Camera.main.WorldToViewportPoint(coin.position - Camera.main.transform.right * 0.75f);

        if (wvpR.z > 0 && wvpL.z > 0) {
            if(wvpR.x < 0) {
                leftHint.SetActive(GameState.isHintsVisible);
                rightHint.SetActive(false);
            } 
            else if (wvpL.x > 1) {
                leftHint.SetActive(false);
                rightHint.SetActive(GameState.isHintsVisible);
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
                rightHint.SetActive(GameState.isHintsVisible);
            } 
            else {
                leftHint.SetActive(GameState.isHintsVisible);
                rightHint.SetActive(false);
            }
        }
    }

    private void OnGameEvent(string type, object payload) {
        switch(type) {
            case "SpawnCoin" :
            if(payload is GameObject newCoin) {
                this.coin = newCoin.transform; 
            }
            break;
            case "Disappear" :
                if(payload.Equals("Coin")) {
                    coin = null;
                }
            break;
            case nameof(GameState): 
                // content.SetActive(GameState.isCompasVisible);
            break;
        }
    }
    private void OnDestroy()
    {
        GameEventController.RemoveListener(listEnableEvents, OnGameEvent);
    }
}
