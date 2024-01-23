using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper
{
    public PopUp CreatePopUp(PopUp obj, Vector3 position)
    {
        var popup = MonoBehaviour.Instantiate(obj, position, Quaternion.identity);
        return popup;
    }
} 
