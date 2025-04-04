using UnityEngine;

public class GameState
{
    public static float gameTime24 {set; get;} = 12.0f;
    public static float maxCoinSpawnDistance {set; get;} = 30.0f;

    #region SkyBoxes
        private static Material _daySkybox;
        public static Material daySkybox 
        {
            get => _daySkybox;
            set {
                if(value != _daySkybox) {
                    _daySkybox = value;
                    GameEventController.EmitEvent(nameof(GameState), nameof(daySkybox));
                }
            }
        }

        private static Material _nightSkybox;
        public static Material nightSkybox 
        {
            get => _nightSkybox;
            set {
                if(value != _nightSkybox) {
                    _nightSkybox = value;
                    GameEventController.EmitEvent(nameof(GameState), nameof(nightSkybox));
                }
            }
        }
    #endregion

    #region activeSceneIndex
    public static int _activeSceneIndex = 0;
    public static int activeSceneIndex {
        set {
            if(_activeSceneIndex != value){
                _activeSceneIndex= value;
                GameEventController.EmitEvent(nameof(GameState), nameof(activeSceneIndex));
            }
        } get => _activeSceneIndex;
    }
    #endregion

    public static float _coinSpawnProbability {set; get;} = 0.5f;
    public const int minCoinsOnScene = 20;
    public static float coinSpawnProbability {
        get => _coinSpawnProbability;
        set {
            _coinSpawnProbability = value;
            GameEventController.EmitEvent(nameof(GameState), nameof(coinSpawnProbability));
        }
    }

    public const float maxPossibleStamina = 20.0f;
    public static float _maxStamina = maxPossibleStamina / 2;
    public static float maxStamina {
        get => _maxStamina;
        set {
            _maxStamina = value;
            GameEventController.EmitEvent(nameof(GameState), nameof(maxStamina));
        }
    }
    public static float stamina {get; set;} = maxStamina;
 
    #region coinSpawnDistance
    public const float coinSpawnDistanceMin = 3.0f;
    public const float coinSpawnDistanceMax = 80.0f;
    public const float coinSpawnZoneRation = 1.5f;
    
    private static float _coinSpawnDistance = 30.0f;
    public static float coinSpawnDistance
    {
        get => _coinSpawnDistance;
        set
        {
            if (_coinSpawnDistance != value)
            {
                _coinSpawnDistance = value;
                GameEventController.EmitEvent(nameof(GameState), nameof(coinSpawnDistance));
            }
        }
    }
    #endregion
   
    #region isClockVisible
    private static bool _isClockVisible {set; get;} = false;
    public static bool isClockVisible {
        set {
            if(_isClockVisible != value){
                _isClockVisible = value;
                GameEventController.EmitEvent(nameof(GameState), nameof(isClockVisible));
            }
        } get => _isClockVisible;
    }
    #endregion

    #region isCompasVisible
    private static bool _isCompasVisible {set; get;} = false;
    public static bool isCompasVisible {
        set {
            if(_isCompasVisible != value){
                _isCompasVisible = value;
                GameEventController.EmitEvent(nameof(GameState), nameof(isCompasVisible));
            }
        } get => _isCompasVisible;
    }
    #endregion

    #region isHintsVisible
    public static bool _isHintsVisible {set; get;} = false;
    public static bool isHintsVisible {
        set {
            if(_isHintsVisible != value){
                _isHintsVisible= value;
                GameEventController.EmitEvent(nameof(GameState), nameof(isHintsVisible));
            }
        } get => _isHintsVisible;
    }
    #endregion


}
