using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public abstract class TagView<T> :  MonoBehaviour
{
    private T dataHolder;

    public T GetData() => dataHolder;
    public T SetHolder(T dataHolder) => this.dataHolder = dataHolder;

    public abstract void ApplyUpdate();
    public abstract void Initialize();
    public virtual void Hide() => gameObject.SetActive(false);
    public virtual void Show() => gameObject.SetActive(true);
    
}
