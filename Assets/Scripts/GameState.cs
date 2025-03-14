using UnityEngine;

public class GameState
{
    public static float gameTime24 {set; get;} = 12.0f;

    public static float maxCoinSpawnDistance {set; get;} = 30.0f;
    public static float maxCoinSpawnProbability {set; get;} = 0.5f;

    //public static float radarVisibleRadius {set; get;} // радар
    public static float maxStamina {set; get;} = 10.0f;

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
    // public static bool isRadatVisible {set; get;} = true;
    public static bool isHintsVisible {set; get;} = true;


}
