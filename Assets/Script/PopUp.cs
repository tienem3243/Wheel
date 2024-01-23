using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : View
{
    [SerializeField] TextMeshProUGUI descript;
    [SerializeField] Button btn;
    public void ShowMessenge(string itemName)
    {
        Debug.Log(itemName + "---->");
        descript.text = "Chúc mừng bạn nhận được \n "+ itemName;
        ViewManager.Show(this, false);
    }
    public override void Initialize()
    {
       btn.onClick?.AddListener(()=>Hide());
    }
   
}
