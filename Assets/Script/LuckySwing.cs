using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class LuckySwing : MonoBehaviour
{

    public ItemSpawn spawn; // Number of objects to spawn
    public float circleRadius = 3f; // Radius of the circle
    public int numberOfSpins;
    public float rotationDuration;
    public float goal;
    private bool isSpinning;
    public UnityEvent onSpinEnd;
    private Transform lastChoice;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, circleRadius);
        DrawLineToTargetAngle();
        Gizmos.color = Color.red;
        for (int i = 0; i < spawn.data.itemList.Count; i++)
        {
            float angle = i * Mathf.PI * 2 / spawn.items.Count;
            float x = Mathf.Cos(angle) * circleRadius;
            float y = Mathf.Sin(angle) * circleRadius;
            Vector3 spawnPosition = new Vector3(x, y, 0f);
         
            Gizmos.DrawWireSphere(spawnPosition+transform.position,0.5f);
        }
        Gizmos.color = Color.blue;
        if(lastChoice!=null)
        Gizmos.DrawWireSphere(lastChoice.position, 0.5f);
    }
    void Start()
    {
        spawn.Spawn();
        if (spawn.items.Count > 0)
            SpawnUIObjects();
        
    }

    Item GetRandomWeightedItem(Item[] itemList)
    {
        // Calculate total weight
        float totalWeight = 0f;
        foreach (var item in itemList)
        {
            totalWeight += item.weight;
        }

        // Generate a random value within the total weight range
        float randomValue = UnityEngine.Random.Range(0f, totalWeight);

        // Iterate through the items to find the one corresponding to the random value
        foreach (var item in itemList)
        {
            if (randomValue <= item.weight)
            {
                return item;
            }
            randomValue -= item.weight;
        }

        // This line should not be reached, but return null if there's an issue
        return null;
    }

void DrawLineToTargetAngle()
    {
        // Vị trí của đối tượng
        Vector3 objectPosition = transform.position;

        // Góc đích đến được chuyển đổi thành vector hướng
        Vector3 targetDirection = Quaternion.Euler(0,0, goal) * Vector3.up;

        // Điểm kết thúc của đường là vị trí của đối tượng cộng với vector hướng đích đến
        Vector3 lineEnd = objectPosition + targetDirection * 5000f; // Để dài hơn, bạn có thể điều chỉnh giá trị 5f

        // Vẽ đường từ vị trí đối tượng đến điểm kết thúc
        Gizmos.color = Color.red;
        Gizmos.DrawLine(objectPosition, lineEnd);

    }


    void SpawnUIObjects()
    {
        for (int i = 0; i < spawn.items.Count; i++)
        {
            float angle = i * Mathf.PI * 2 / spawn.items.Count;
            float x = Mathf.Cos(angle) * circleRadius;
            float y = Mathf.Sin(angle) * circleRadius;

            Vector3 spawnPosition = new Vector3(x, y, 0f)+transform.localPosition;
            spawn.items[i].transform.localPosition=spawnPosition;

        }
    }

   public void SpinObject(float targetAngle)
    {
        // Tính toán tổng số độ cần xoay dựa trên targetRotation
        float z = transform.rotation.eulerAngles.z;
        float totalRotation = z - targetAngle + (360 * numberOfSpins);

        isSpinning = true;
        // Tạo tween để xoay đối tượng
        transform.DORotate(new Vector3(0, 0, totalRotation), rotationDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.OutQuint)
            .OnComplete(() =>
            {
                Debug.Log("Xoay xong!");
            }).OnComplete(() =>
            {
                transform.eulerAngles = new Vector3(0, 0, totalRotation%360);
                isSpinning = false;
                onSpinEnd.Invoke();
            });
    }
    public void SpinObject(Transform target)
    {
        var goalDir = Quaternion.Euler(0, 0, goal) * Vector3.up;
        Vector3 to = target.position - transform.position;
        float gotoAngle = Vector3.Angle(goalDir, to);
        if (target.position.x > transform.position.x) gotoAngle =-Mathf.Abs( gotoAngle); 
        SpinObject(gotoAngle);
       Debug.Log(gotoAngle);
    }
    public void Spin()
    {
        if (isSpinning) return;
       Item item= GetRandomWeightedItem(spawn.data.itemList.ToArray());
        
        Debug.Log(item.id + " was choice");
        if (item != null)
        {
           GameObject target= spawn.items.Find(x => x.name == item.id.ToString());
            lastChoice = target.transform;
            SpinObject(target.transform);
        }
    }



}
