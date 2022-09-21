using UnityEngine;

/// <summary>
/// Hides and locks the cursor
/// </summary>
/// < Author >Michal Kokeš</Author >
public class HideCursor : MonoBehaviour
{
    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
