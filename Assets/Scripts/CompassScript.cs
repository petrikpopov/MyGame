using UnityEngine;

public class CompassScript : MonoBehaviour
{
    private GameObject content;
    private Transform arrow;
    private Transform character;
    private Transform coin;

    private readonly string[] listEnableEvents = {"SpawnCoin", "Disappear", nameof(GameState)};
    void Start()
    {
        content = transform.Find("Content").gameObject;
        arrow = content.transform.Find("Arrow");
        character = GameObject.Find("Character").transform;
        coin = GameObject.Find("Coin").transform;
        GameEventController.AddListener(listEnableEvents, OnGameEvent);
        content.SetActive(GameState.isCompasVisible);
    }

    // Update is called once per frame
    void Update()
    {
        if(!content.activeInHierarchy) {
            return;
        }

        if(coin == null) {
            var go = GameObject.FindGameObjectWithTag("Coin");
            if(go == null) return;
            else coin = go.transform;
        }
        
        Vector3 d = coin.position - character.position;
        Vector3 f = Camera.main.transform.forward;
        d.y = 0;
        f.y = 0;
        float angle = Vector3.SignedAngle(f, d, Vector3.down);
        arrow.eulerAngles = new Vector3(0,0,angle);
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
                content.SetActive(GameState.isCompasVisible);
            break;
        }
    }
    private void OnDestroy()
    {
        GameEventController.RemoveListener(listEnableEvents, OnGameEvent);
    }
}
