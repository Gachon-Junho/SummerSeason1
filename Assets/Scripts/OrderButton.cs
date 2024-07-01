using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderButton : MonoBehaviour
{
    public GameObject SymbolText;
    public GameObject PeopleManager;

    public bool IsDescending { get; private set; } = true;

    public void SwitchOrder()
    {
        IsDescending = !IsDescending;
        SymbolText.GetComponent<TextMeshProUGUI>().text = IsDescending ? "\u25bc" : "\u25b2";
        
        PeopleManager.GetComponent<PeopleManager>().OrderDirectionChanged(IsDescending);
    }
}
