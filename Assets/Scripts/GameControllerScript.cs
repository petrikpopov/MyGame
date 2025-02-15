using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
     [SerializeField] private GameObject character;

    private float minCoinCharacterDistance = 10.0f;
    private float maxCoinCharacterDistance = 29.0f;
    private float minCoinMapOffset = 50.0f;
    private float maxCoinHeight = 2.5f;
    private float minCoinHeight = 1.0f;
    void Start()
    {
        GameEventController.AddListener("Disappear", OnDisappearEvent);
    }

    private void OnDisappearEvent(string type, object payload)
    {
        if(payload.Equals("Coin")) {
            Vector3 coinPosition ;
            Vector3 coinDelta ;
            int lim = 0;
            do {
                coinDelta = character.transform.position + new Vector3(Random.Range(-maxCoinCharacterDistance, maxCoinCharacterDistance), 0 , Random.Range(-maxCoinCharacterDistance, maxCoinCharacterDistance));
                coinPosition = character.transform.position + coinDelta;
                lim+=1;
            } while (lim < 100 && (coinDelta.magnitude > maxCoinCharacterDistance 
                                || coinDelta.magnitude < minCoinCharacterDistance 
                                || coinPosition.x < minCoinMapOffset || coinPosition.z < minCoinMapOffset || coinPosition.x > 1000 - minCoinMapOffset || coinPosition.z > 1000 - minCoinMapOffset) );
            coinPosition.y  = Terrain.activeTerrain.SampleHeight(coinPosition) + Random.Range(minCoinHeight, maxCoinHeight);
            GameObject.Instantiate(coinPrefab).transform.position  = coinPosition;
        }
        Debug.Log(type + " " + payload);
    }

    private void OnDestroy()
    {
        GameEventController.RemoveListener("Disappear", OnDisappearEvent);
    }
}
