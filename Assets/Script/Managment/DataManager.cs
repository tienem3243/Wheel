using UnityEngine;
using System.Collections.Generic;
using MyBox;
using System;
using System.IO;
using Unity.VisualScripting;

public class DataManager : MonoBehaviour
{
    public List<Jockey> jockeyList;
    public List<Animal> animalList;




    public void Init()
    {

        // Load Jockey data from JSON
        jockeyList = new List<Jockey>();
        animalList = new List<Animal>();

        string jockeyFilePath = Path.Combine(Application.persistentDataPath, "JockeyData.json");
  
        if (File.Exists(jockeyFilePath))
        {
            string jockeyJson = File.ReadAllText(jockeyFilePath);
            SerializableList<Jockey> jockeyContainer = JsonUtility.FromJson<SerializableList<Jockey>>(jockeyJson);
            jockeyList = jockeyContainer?.list;
        }
        else
        {
            Debug.LogError("JockeyData.json not found or failed to load.");
            return;
        }

        // Load Animal data from JSON
        string animalFilePath = Path.Combine(Application.persistentDataPath, "AnimalData.json");
        Debug.Log(animalFilePath);
        if (File.Exists(animalFilePath))
        {
            string animalJson = File.ReadAllText(animalFilePath);
            SerializableList<Animal> animalContainer = JsonUtility.FromJson<SerializableList<Animal>>(animalJson);
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
    public void UpdateInfo<T>(List<T> dataList, string id, Action<T> updateAction, Predicate<T> findPredicate, Action saveAction, Func<T> createNew)
    {
        // Find the item in the list
        T itemToUpdate = dataList.Find(findPredicate);

        if (itemToUpdate != null)
        {
            // Perform the update action
            updateAction(itemToUpdate);
        }
        else
        {
            // Create a new item
            itemToUpdate = createNew();
            dataList.Add(itemToUpdate);
        }

        // Save the updated data
        saveAction();
    }

    public void UpdateAnimalInfo(Animal newAnimal)
    {
        UpdateInfo<Animal>(
            animalList,
            newAnimal.ID,
            animal =>
            {
                animal.nameAnimal = newAnimal.nameAnimal;
                animal.imagePath = newAnimal.imagePath;
            },
            animal => animal.ID == newAnimal.ID,
            SaveAnimalData,
            () => new Animal { ID = newAnimal.ID, nameAnimal = newAnimal.nameAnimal, imagePath = newAnimal.imagePath }
        );
    }

    public void UpdateJockeyInfo(Jockey newJockey)
    {
        UpdateInfo<Jockey>(
            jockeyList,
            newJockey.ID,
            jockey =>
            {
                jockey.jockeyName = newJockey.jockeyName;
                Animal newAnimal;
                // Find the Animal in the list
                if (newJockey.animal != null)
                {
                    newAnimal = animalList.Find(animal => animal.ID == newJockey.animal.ID);
                }
                else newAnimal = new Animal();
                if (newAnimal != null)
                {
                    // Update the Animal reference
                    jockey.animal = newAnimal;
                }
            },
            jockey => jockey.ID.Equals(newJockey.ID),
            SaveJockeyData,
            () => new Jockey { ID = newJockey.ID, jockeyName = newJockey.jockeyName, animal = new Animal() }
        );
    }
    public void DeleteInfo<T>(List<T> dataList, Predicate<T> deletePredicate, Action saveAction)
    {
        // Find the item in the list
        T itemToDelete = dataList.Find(deletePredicate);

        if (itemToDelete != null)
        {
            // Remove the item from the list
            dataList.Remove(itemToDelete);

            // Save the updated data
            saveAction();
        }
        else
        {
            Debug.LogWarning("Item not found for deletion.");
        }
    }

    public void DeleteAnimalInfo(string animalID)
    {
        DeleteInfo<Animal>(
            animalList,
            animal => animal.ID == animalID,
            SaveAnimalData
        );
    }

    public void DeleteJockeyInfo(string jockeyID)
    {
        DeleteInfo<Jockey>(
            jockeyList,
            jockey => jockey.ID == jockeyID,
            SaveJockeyData
        );
    }
   
    private void SaveAnimalData()
    {
        SaveData(animalList, "AnimalData.json");
    }

    private void SaveJockeyData()
    {
        SaveData(jockeyList, "JockeyData.json");
    }

    private void SaveData<T>(List<T> dataList, string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        Debug.Log(filePath);
        SaveDataToFile(dataList, filePath);
    }

    private void SaveDataToFile<T>(List<T> dataList, string filePath)
    {
        string directory = Path.GetDirectoryName(filePath);

        // Ensure the directory exists
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Save data to file
        string json = JsonUtility.ToJson(new SerializableList<T>(dataList));
        File.WriteAllText(filePath, json);
    }
    // List to store items to be deleted
    private List<Jockey> jockeysToDelete = new List<Jockey>();
    private List<Animal> animalsToDelete = new List<Animal>();


    public void AddJockeyToDelete(Jockey jockey)
    {
        if (!jockeysToDelete.Contains(jockey))
        {
            jockeysToDelete.Add(jockey);
        }
    }

    // Add Animal to the delete list
    public void AddAnimalToDelete(Animal animal)
    {
        if (!animalsToDelete.Contains(animal))
        {
            animalsToDelete.Add(animal);
        }
    }

    // Method to delete items in the delete lists
    public void DeleteChoice()
    {
        foreach (var jockey in jockeysToDelete)
        {
            DeleteJockeyInfo(jockey.ID);
        }

        foreach (var animal in animalsToDelete)
        {
            DeleteAnimalInfo(animal.ID);
        }

        // Clear the delete lists after deletion
        jockeysToDelete.Clear();
        animalsToDelete.Clear();
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
