using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A Scripts that handles additional Toggle UI Components and their color change
/// </summary>
/// <author>Michal Kokeš</author>
public class ToggleUIScript : MonoBehaviour
{
    public Sprite enabledSprite;
    public Sprite disabledSprite;
    public Image background;
    public GameObject checkmark;
    public Toggle toggle;
    public Color enabledColor;
    public Color disabledColor;
    public TextMeshProUGUI label;
    
    public void OnUpdate(bool isOn)
    {
        background.sprite = isOn ? enabledSprite : disabledSprite;
        checkmark.SetActive(!isOn);
        label.color = isOn ? enabledColor : disabledColor;
    }
    private void Start()
    {     
        toggle.onValueChanged.AddListener(OnUpdate);
        OnUpdate(toggle.isOn);
    }
}
