using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRacerProfile : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI info;
    [SerializeField] RectTransform background;
    /*[SerializeField] private float marginX=10f;
    [SerializeField] private float marginY=5f;

    [ButtonMethod]
    private void UpdateBackground()
    {
        var infoSize = info.GetComponent<RectTransform>().sizeDelta;
        infoSize.x += marginX;
        infoSize.y += marginY;
        background.sizeDelta = infoSize;
    }*/
    public void SetInfo(Sprite sprite, string info)
    {
        image.sprite = sprite;
        this.info.text = info;
       
    }
    public void SelectAnim()
    {
        
    }

}
