using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownMenu : MonoBehaviour
{
    Dropdown dropdownMenu;

    private void Awake()
    {
        dropdownMenu = GetComponent<Dropdown>();
        switch (PlayerPrefs.GetInt("IsDuck"))
        {
            case 0: dropdownMenu.value = 0; break;
            case 1: dropdownMenu.value = 1; break;
        }
    }

    private void Update()
    {
        switch (PlayerPrefs.GetInt("IsDuck"))
        {
            case 0: dropdownMenu.value = 0; break;
            case 1: dropdownMenu.value = 1; break;
        }
    }

    public void PlayerTransformation()
    {
        switch (dropdownMenu.value)
        {
            case 0: PlayerPrefs.SetInt("IsDuck", 0); break;
            case 1: PlayerPrefs.SetInt("IsDuck", 1); break;

        }
    }
}
