using DG.Tweening;
using MyBox;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RacePositionUI : MonoBehaviour
{
    public int numberOfRows = 5; // Số hàng
    public int[] racersInEachRow ; // Số tay đua trong mỗi hàng

    public UIRacerProfile racerPrefab; // Prefab cho mỗi tay đua
    public Sprite racerIcon; // Biểu tượng của mỗi tay đua
    public Vector3 center;
    private List<UIRacerProfile> racerList= new List<UIRacerProfile>();
    [SerializeField] float cellWidth = 100f;
    [SerializeField] float cellHeight = 100f;
    [SerializeField] float spacingX = 10f;
    [SerializeField] float spacingY = 10f;
 
 
    void CreateRaceUI()
    {

        Sequence sequence = DOTween.Sequence();


        // Tạo các tay đua trong từng hàng
        for (int i = 0; i < numberOfRows; i++)
        {
           // float offsetX = transform.localPosition.x + middleX;
            float xPos = -( i * (cellWidth + spacingX));

            float middleY = (racersInEachRow[i] * (cellHeight + spacingY)) / 2;
            var offsetY = middleY;

            for (int j = 0; j < racersInEachRow[i]; j++)
            {
                // Tính toán vị trí của từng tay đua trong hàng
                float yPos = j * (cellHeight + spacingY);
                yPos -= offsetY;
                // Tạo một tay đua mới
                UIRacerProfile racer = Instantiate(racerPrefab, transform);
                racer.transform.localPosition = new Vector3(xPos, yPos, 0f);
                sequence.Insert(0.1f * j, racer.transform.DOLocalMove(transform.position + Vector3.up, 0.3f).From());
                racerList.Add(racer);   


                racer.SetInfo(racerIcon, "LoremIpsum");
          
            }
            

        }


    }
    
    private void Start()
    {
        CreateRaceUI(); 
    }
}
