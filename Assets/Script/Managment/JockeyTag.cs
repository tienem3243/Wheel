using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JockeyTag : MonoBehaviour
{
    [SerializeField]TextMeshPro jockeyInfo;
    [SerializeField] GameObject model;
    public void SetJockeyInfo(string jockeyInfo)
    {
        this.jockeyInfo.text = jockeyInfo.Trim(); 
    }
    public GameObject GetTextObj()
    {
        return jockeyInfo.gameObject;
    }
    public void SetVisible(bool visible) 
    { 
        model.SetActive(visible);
    }
    public void Reset()
    {
        SetVisible(true);
    }
}
