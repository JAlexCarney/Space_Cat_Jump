using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject[] Menus;
    public void Start()
    {
        OpenMenu(0);
    }

    public void OpenMenu(int index) 
    {
        foreach (var menu in Menus)
        {
            menu.SetActive(false);
        }
        Menus[index].SetActive(true);
        gameObject.SetActive(false);
    }
}
