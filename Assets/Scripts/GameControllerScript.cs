// using UnityEngine;

// public class GameControllerScript : MonoBehaviour
// {
//     [SerializeField] private GameObject coinPrefab;
//      [SerializeField] private GameObject character;

//     private float minCoinCharacterDistance;
//     private float maxCoinCharacterDistance;
//     private float minCoinMapOffset = 50.0f;
//     private float maxCoinHeight = 2.5f;
//     private float minCoinHeight = 1.0f;
//     private readonly string[] listEnableEvents = {"Disappear", nameof(GameState)};

//     void Start()
//     {
//         GameEventController.AddListener(listEnableEvents, OnGameEvent);
//         OnGameEvent(nameof(GameState), null);
//     }

//     private void OnGameEvent (string type, object payload) {
//         switch(type) {
//             case "Disappear" : OnDisappearEvent(type, payload); break;
//             case nameof(GameState): minCoinCharacterDistance = GameState.coinSpawnDistance;
//                                     maxCoinCharacterDistance = GameState.coinSpawnDistance * GameState.coinSpawnZoneRation;
//                                     break;
//         }
//     }
//     private void OnDisappearEvent(string type, object payload)
//     {
//         if(payload.Equals("Coin")) {
//             Vector3 coinPosition ;
//             Vector3 coinDelta ;
//             int lim = 0;
//             do {
//                 coinDelta = character.transform.position + new Vector3(Random.Range(-maxCoinCharacterDistance, maxCoinCharacterDistance), 0 , Random.Range(-maxCoinCharacterDistance, maxCoinCharacterDistance));
//                 coinPosition = character.transform.position + coinDelta;
//                 lim+=1;
//             } while (lim < 100 && (coinDelta.magnitude > maxCoinCharacterDistance 
//                                 || coinDelta.magnitude < minCoinCharacterDistance 
//                                 || coinPosition.x < minCoinMapOffset || coinPosition.z < minCoinMapOffset || coinPosition.x > 1000 - minCoinMapOffset || coinPosition.z > 1000 - minCoinMapOffset) );
//             coinPosition.y  = Terrain.activeTerrain.SampleHeight(coinPosition) + Random.Range(minCoinHeight, maxCoinHeight);
//             GameObject coin = GameObject.Instantiate(coinPrefab);
//             coin.transform.position  = coinPosition;

//             GameEventController.EmitEvent("SpawnCoin", coin);
//         }
//     }

//     private void OnDestroy()
//     {
//         GameEventController.RemoveListener(listEnableEvents, OnGameEvent);
//     }
// }

using UnityEngine;
using UnityEngine.Device;

public class GameControllerScript : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject character;

    private float minCoinCharacterDistance;
    private float maxCoinCharacterDistance;
    private float minCoinMapOffset = 50.0f;
    private float minCoinHeight = 1.0f;
    private float maxCoinHeight = 2.5f; 
    
    private readonly string[] listenableEvents = { "Disappear", nameof(GameState) };


    void Start()
    {
        GameEventController.AddListener(listenableEvents, OnGameEvent);
        OnGameEvent(nameof(GameState), null);
    }

    private void OnGameEvent(string type, object payload)
    {
        switch (type)
        {
            case "Disappear": OnDisappearEvent(type, payload); break;
            case nameof(GameState):
                minCoinCharacterDistance = GameState.coinSpawnDistance;
                maxCoinCharacterDistance = GameState.coinSpawnDistance * GameState.coinSpawnZoneRation;
                break;
        }
    }
    private void OnDisappearEvent(string type, object payload)
    {
        if(payload.Equals("Coin"))
        {
            int minCoins = 1 + (int)(GameState.minCoinsOnScene * GameState.coinSpawnProbability);
            int presentCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
            if(presentCoins > minCoins * 2) {
                return;
            }
            int rnd = Random.Range(0, minCoins);
            for (int i = 0; i < rnd; i++)
            {
                 SpawnCoin();
            }
            if(GameObject.FindGameObjectsWithTag("Coin").Length <= minCoins)
            {
                SpawnCoin();
            }
            if(presentCoins + rnd <= minCoins) {
                for(int i = 0; i <= minCoins - presentCoins - rnd; i++){
                    SpawnCoin();
                }
            }
        }
        // Debug.Log(type + " " + payload);
    }

    private void SpawnCoin()
    {
        Vector3 coinDelta;
        Vector3 coinPosition;
        int lim = 0;
        do
        {
            coinDelta = new Vector3(
                Random.Range(-maxCoinCharacterDistance, maxCoinCharacterDistance),
                0,
                Random.Range(-maxCoinCharacterDistance, maxCoinCharacterDistance)
            );
            coinPosition = character.transform.position + coinDelta;
            lim += 1;
        } while (lim < 100 && (
            coinDelta.magnitude > maxCoinCharacterDistance
            || coinDelta.magnitude < minCoinCharacterDistance
            || coinPosition.x < minCoinMapOffset
            || coinPosition.z < minCoinMapOffset
            || coinPosition.x > 1000 - minCoinMapOffset
            || coinPosition.z > 1000 - minCoinMapOffset
        ));

        coinPosition.y = Terrain.activeTerrain.SampleHeight(coinPosition) +
            Random.Range(minCoinHeight, maxCoinHeight);

        GameObject coin = GameObject.Instantiate(coinPrefab);
        coin.transform.position = coinPosition;

        GameEventController.EmitEvent("SpawnCoin", coin);
    }

    private void OnDestroy()
    {
        GameEventController.RemoveListener(listenableEvents, OnGameEvent);
    }
}
