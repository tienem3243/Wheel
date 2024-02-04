using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalEditPanel : View
{
    [SerializeField] GameObject animalEditTagPrefab;
    [SerializeField] Transform rootAnimal;
    public List<AnimalEditTag> animalEditTags;  
    

    public void CreateAnimalUI(Animal animal, Transform parentTransform)
    {
        GameObject animalUIPrefab = Instantiate(animalEditTagPrefab, parentTransform);
        AnimalEditTag animalEditTag = animalUIPrefab.GetComponent<AnimalEditTag>();
      
        if (animalEditTag != null)
        {
            animalEditTag.Initialize();
            animalEditTag.SetInfo(animal);
            animalEditTags.Add(animalEditTag);
            Debug.Log("Animal Created " + animal.ToString());
        }
    }

    public override void Initialize()
    {
        animalEditTags = new List<AnimalEditTag>();
        foreach (Animal animal in Manager.Instance.dataManager.animalList)
        {
            CreateAnimalUI(animal, rootAnimal);
        }
    }

    [ButtonMethod]
    public override void UpdateView()
    {
        animalEditTags.ForEach(x => x.ApplyUpdate());
    }
 
}
