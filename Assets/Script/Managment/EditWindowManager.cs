using MyBox;
using System.Collections.Generic;
using UnityEngine;

public class EditWindowManager : MonoBehaviour 
{
  

    [SerializeField] private View[] _views;

   
    public void ApplyUpdate()
    {
        foreach (var item in _views)
        {
            item.UpdateView();
        }
    }
    public void Refresh()
    {
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
}
