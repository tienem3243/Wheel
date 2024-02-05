using MyBox;
using System.Collections.Generic;
using UnityEngine;

public class EditWindowManager : MonoBehaviourSingleton<EditWindowManager>  
{
  

    [SerializeField] private View[] _views;

   
    public void ApplyUpdate()
    {
        Manager.Instance.dataManager.DeleteChoice();  
        foreach (var item in _views)
        {
            item.UpdateView();
        }
    }
    public void Refresh()
    {
        Clear();
        Init();
    }
    public void Init()
    {
        foreach (var item in _views)
        {
            item.Initialize();
            item.Show();
        }
    }

    public  T GetView<T>()
    {
        for (int i = 0; i < _views.Length; i++)
        {
            if (_views[i] is T tview)
            {
                return tview;
            }
        }
        return default(T);
    }
    public void Clear()
    {
        foreach (var item in _views)
        {
            item.Clear();
        }
    }
}
