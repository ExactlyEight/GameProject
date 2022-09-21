using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public bool isPaused;
    public bool isGodMode;
    
    // get god mode
    public bool GetGodMode()
    {
        return isGodMode;
    }
    
    // set god mode
    public void SetGodMode(bool value)
    {
        isGodMode = value;
    }
}

