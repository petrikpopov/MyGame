using UnityEngine;

public class CompassScript : MonoBehaviour
{
    private Transform arrow;
    private Transform character;
    private Transform coin;
    void Start()
    {
        arrow = transform.Find("Arrow");
        character = GameObject.Find("Character").transform;
        coin = GameObject.Find("Coin").transform;
        GameEventController.AddListener("SpawnCoin", OnCoinSpawnEvent);
        GameEventController.AddListener("Disappear", OnDisappearEvent);
    }

    // Update is called once per frame
    void Update()
    {
        if(coin == null) return;
        
        Vector3 d = coin.position - character.position;
        Vector3 f = Camera.main.transform.forward;
        d.y = 0;
        f.y = 0;
        float angle = Vector3.SignedAngle(f, d, Vector3.down);
        arrow.eulerAngles = new Vector3(0,0,angle);
    }

    private void OnDisappearEvent(string type, object payload) {
        if(payload.Equals("Coin")) {
            coin = null;
        }
    }
    private void OnCoinSpawnEvent(string type, object payload) {
        if(payload is GameObject newCoin) {
            this.coin = newCoin.transform;
        }
    }

    private void OnDestroy()
    {
        GameEventController.RemoveListener("SpawnCoin", OnCoinSpawnEvent);
        GameEventController.RemoveListener("Disappear", OnDisappearEvent);
    }
}
