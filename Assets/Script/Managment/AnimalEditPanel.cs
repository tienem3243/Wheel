using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalEditPanel : View
{
    [SerializeField] GameObject animalEditTagPrefab;
    [SerializeField] Transform rootAnimal;
    public List<AnimalEditTag> animalEditTags;
    private AnimalEditTag currentSelected;
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
    public void SetSelect(AnimalEditTag tag)
    {
        currentSelected = tag;
    }
    public AnimalEditTag GetCurrentSelected()
    {
        return currentSelected;
    }

    [ButtonMethod]
    public override void UpdateView()
    {
        animalEditTags.ForEach(x => x.ApplyUpdate());
    }

    public override void Clear()
    {
        animalEditTags.ForEach(x=>Destroy(x.gameObject));
        animalEditTags.Clear();
    }
    public void Delete()
    {
        if (currentSelected != null)
        {
            var temp = currentSelected;
            animalEditTags.Remove(currentSelected);
            Manager.Instance.dataManager.AddAnimalToDelete(temp.GetData());
            Destroy(temp.gameObject);

        }
    }
}

