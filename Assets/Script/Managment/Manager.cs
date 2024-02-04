using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviourSingleton<Manager>
{
    public DataManager dataManager;
    public EditWindowManager editWindowManager;
    private void Start()
    {
        dataManager.Init();
        editWindowManager.Init();
    }
 
}
