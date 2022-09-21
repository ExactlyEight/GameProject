using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to switch between the different menus
/// </summary>
/// <author>Michal Kokeš</author>
public class MenuUIScript : MonoBehaviour
{
    public GameObject mainMenu;
    [Tooltip("Use SwitchMenuTransforms(string transformName) with the same names as in the list below to switch between them.")]
    [SerializeField] private List<Transform> menuTransforms = new();
    
    void Start()
    {
        // set all menus to inactive
        foreach (Transform menuTransform in menuTransforms)
        {
            menuTransform.gameObject.SetActive(false);
        }
    }
    
    // function to switch between the menu transforms
    public void SwitchMenuTransforms(string transformName)
    {
        // loop through all the transforms
        foreach (var mTransform in menuTransforms)
        {
            // set the transform to active if the name matches the parameter
            mTransform.gameObject.SetActive(mTransform.name.Equals(transformName));
        }
    }
    
    // Update function to get menu key input and change menu visibility
    void Update()
    {
        // if the escape key is pressed switch the state of the main menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mainMenu.SetActive(!mainMenu.activeSelf);
        }
    }
}
