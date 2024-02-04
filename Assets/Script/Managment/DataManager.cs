using UnityEngine;
using System.Collections.Generic;
using MyBox;
using System;

public class DataManager : MonoBehaviour
{
    public List<Jockey> jockeyList;
    public List<Animal> animalList;

   
    

    public void Init()
    {
        // Load Jockey data from JSON
        jockeyList = new List<Jockey>();
        animalList = new List<Animal>();
        TextAsset jockeyJson = Resources.Load<TextAsset>("JockeyData");
        if (jockeyJson != null)
        {
            SerializableList<Jockey> jockeyContainer = JsonUtility.FromJson<SerializableList<Jockey>>(jockeyJson.ToString());
            
            jockeyList = jockeyContainer?.list;
           
        }
        else
        {
            Debug.LogError("JockeyData.json not found or failed to load.");
            return;
        }

        // Load Animal data from JSON
        TextAsset animalJson = Resources.Load<TextAsset>("AnimalData");
        if (animalJson != null)
        {
            SerializableList<Animal> animalContainer = JsonUtility.FromJson<SerializableList<Animal>>(animalJson.ToString());
            animalList = animalContainer?.list;
        }
        else
        {
            Debug.LogError("AnimalData.json not found or failed to load.");
            return;
        }

        // Example: Print loaded Jockeys
        foreach (Jockey jockey in jockeyList)
        {
            Debug.Log("Jockey: " + jockey.ToString());
        }

        // Example: Print loaded Animals
        foreach (Animal animal in animalList)
        {
            Debug.Log("Animal: " + animal.ID);
        }

    }
    public void UpdateInfo<T>(List<T> dataList, string id, Action<T> updateAction, Predicate<T> findPredicate, Action saveAction)
    {
        // Find the item in the list
        T itemToUpdate = dataList.Find(findPredicate);

        if (itemToUpdate != null)
        {
            // Perform the update action
            updateAction(itemToUpdate);

            // Save the updated data
            saveAction();
        }
        else
        {
            Debug.LogError($"Item with ID {id} not found in the list.");
        }
    }

   
    private void SaveAnimalData()
    {
        string filePath = Application.dataPath + "/Resources/AnimalData.json";
        SaveData(animalList, filePath);
    }
    private void SaveJockeyData()
    {
        string filePath = Application.dataPath + "/Resources/JockeyData.json";
        SaveData(jockeyList, filePath);
    }

    public void UpdateAnimalInfo(Animal newAnimal)
    {
        
        UpdateInfo<Animal>(
            animalList,
            newAnimal.ID,
            animal =>
            {
               animal.nameAnimal =newAnimal.nameAnimal;
                animal.imagePath= newAnimal.imagePath;  
            },
            animal => animal.ID == newAnimal.ID,
            SaveAnimalData
        );
    }
    public void UpdateJockeyInfo(Jockey newJockey)
    {
        Debug.Log(jockeyList[0].ID);
        Debug.Log(newJockey.ID);
        UpdateInfo<Jockey>(
            jockeyList,
            newJockey.ID,
            jockey =>
            {
                jockey.jockeyName = newJockey.jockeyName;

                // Find the Animal in the list
            
                Animal newAnimal = animalList.Find(animal => animal.ID == newJockey.animal.ID);

                if (newAnimal != null)
                {
                    // Update the Animal reference
                    jockey.animal = newAnimal;
                }
            },
            jockey => jockey.ID.Equals(newJockey.ID),
            SaveJockeyData
            );
    }

    private void SaveData<T>(List<T> dataList, string filePath)
    {
        Debug.Log("cast");

        // Check if T is SerializableList<T>
        if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(SerializableList<>))
        {
            // If T is already SerializableList<T>, directly save
            string json = JsonUtility.ToJson(dataList);
            System.IO.File.WriteAllText(filePath, json);
        }
        else
        {
            // Wrap the list in SerializableList<T> and then save
            string json = JsonUtility.ToJson(new SerializableList<T>(dataList));
            System.IO.File.WriteAllText(filePath, json);
        }
    }

}

[System.Serializable]
public class SerializableList<T>
{
    public List<T> list;

    public SerializableList(List<T> originalList)
    {
        list = originalList;
    }
}