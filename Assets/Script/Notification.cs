using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Notification : MonoBehaviourSingleton<Notification>
{
    [SerializeField] TextMeshProUGUI notify;
    public void SetText(string text)
    {
        notify.text = text; 
    }
}
