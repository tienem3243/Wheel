
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSpawn : MonoBehaviour
{
     public ItemData data;
    [SerializeField] GameObject prefabImage;
    [SerializeField] Transform root;
    public List<GameObject> items = new List<GameObject>();

    public void Spawn()
    {
        foreach (var item in data.itemList)
        {
            Debug.Log("----> Spawn");
            GameObject obj=Instantiate(prefabImage,root);
            var image=obj.GetComponent<SpriteRenderer>();
            image.sprite=item.sprite;
            obj.name = item.id.ToString();
            items.Add(image.gameObject);
        }
    }
}
