using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlsMenu : MonoBehaviour
{
    public GameObject TitleMenu;
    public GameObject MappingMenu;
    public GameObject TitlePrefab;
    public GameObject MappingPrefab;
    private struct ButtonMapping {
        public string action;
        public string button;
    };
    
    public void Start()
    {
        var buttonMappings = GetButtonMappings();
        foreach (var buttonMapping in buttonMappings) 
        {
            var title = Instantiate(TitlePrefab, TitleMenu.transform);
            title.GetComponent<TextMeshProUGUI>().text = buttonMapping.action + " :";
            var mapping = Instantiate(MappingPrefab, MappingMenu.transform);
            mapping.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = buttonMapping.button;
        }
    }

    public void UpdateMapping()
    {
        //TODO: Write code to update mapping
        // Listen for next Key
        // Set Mapping
    }

    private ButtonMapping[] GetButtonMappings() 
    {
        // TODO: Load actual controls from project settings
        return new ButtonMapping[] {
            new ButtonMapping { action = "Jump", button = "test1"},
            new ButtonMapping { action = "Attack", button = "test2" },
            new ButtonMapping { action = "EnableDebug", button = "test3" }
        };
    }
}
