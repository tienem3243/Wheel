using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AnimalEditTag : TagView<Animal>
{
    [SerializeField] TextMeshProUGUI id;
    [SerializeField] TMP_InputField nameAnimal;
    [SerializeField] TMP_InputField filePath;
    [SerializeField] Button btn;
   
    public void SetInfo(Animal animal)
    {
        Debug.Log(animal.imagePath);
        id.text = animal.ID;
        nameAnimal.text = animal.nameAnimal;
        filePath.text = animal.imagePath;
        SetHolder(animal);
    }
   
    public override void Hide()
    {
      
        base.Hide();
    }
    public override void Show()
    {
  
        base.Show();
    }
    private void OnDestroy()
    {
        btn.onClick.RemoveAllListeners();
    }
    public override void Initialize()
    {
        btn.onClick?.AddListener(() =>
        {
            string dataPath = FileBrowserHelper.OpenFilePanel((x)=> filePath.text = x);
            Debug.Log(dataPath);
         
          
        });
      
    }
    public override void ApplyUpdate()
    {
        OnValueChange();
    }
    public void OnValueChange()
    {

        Animal animal = new Animal(id.text, nameAnimal.text, filePath.text);
        Debug.Log(filePath.text+"Casts");   
        SetHolder(animal);
       Manager.Instance.dataManager.UpdateAnimalInfo(animal);
    }
}
